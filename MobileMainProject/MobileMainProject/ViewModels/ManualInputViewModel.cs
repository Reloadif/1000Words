using MobileMainProject.Data.DataBase;
using MobileMainProject.Infrastructure.Shared;
using MobileMainProject.Models.Base;
using MobileMainProject.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileMainProject.ViewModels
{
    public class ManualInputViewModel : BaseViewModel
    {
        #region Fields
        private readonly int _wordsInRound;

        private int _currentWord;

        private int _rightWords;
        private int _passWords;
        private int _wrongWords;

        private string _inputWord = "";
        private AnimationInfo _animationState;

        private bool _isFirst = true;
        #endregion

        public ManualInputViewModel()
        {
            Random random = new Random((int)DateTime.Now.Ticks);
            _wordsInRound = Preferences.Get("WordsInRound", 20);

            TranslateChunks = new List<TranslateChunk>();
            CollectedData = new List<Tuple<string, AnswerState>>();

            var tmpList = new List<int>();
            for (int i = 0; i < _wordsInRound; ++i)
            {
                int currentNumber = random.Next(1, App.TranslateDB.TranslateChunkCount);
                while (tmpList.Contains(currentNumber))
                {
                    currentNumber = random.Next(1, App.TranslateDB.TranslateChunkCount);
                }
                tmpList.Add(currentNumber);

                TranslateChunks.Add(App.TranslateDB.GetTranslateChunkAsync(currentNumber).GetAwaiter().GetResult());
            }

            SkipWordCommand = new Command(ExecuteSkipWordCommand, CanExecuteSkipWordCommand);
            AcceptWordCommand = new Command(ExecuteAcceptWordCommand, CanExecuteAcceptWordCommand);
        }

        #region Properties
        public List<TranslateChunk> TranslateChunks { get; private set; }
        public List<Tuple<string, AnswerState>> CollectedData { get; private set; }

        public string ProgressInformation => CurrentWord + " / " + _wordsInRound;

        public int CurrentWord
        {
            get => _currentWord;
            set
            {
                if (Set(ref _currentWord, value))
                {
                    OnPropertyChanged("ProgressInformation");
                    (SkipWordCommand as Command).ChangeCanExecute();
                    (AcceptWordCommand as Command).ChangeCanExecute();
                }
            }
        }

        public int RightWords
        {
            get => _rightWords;
            set => Set(ref _rightWords, value);
        }
        public int PassWords
        {
            get => _passWords;
            set => Set(ref _passWords, value);
        }
        public int WrongWords
        {
            get => _wrongWords;
            set => Set(ref _wrongWords, value);
        }

        public string EnglishWord => TranslateChunks[CurrentWord].EnglishWord;
        public AnimationInfo AnimationState
        {
            get => _animationState;
            set => Set(ref _animationState, value);
        }

        public string InputWord
        {
            get => _inputWord;
            set => Set(ref _inputWord, value.ToLower().Trim());
        }

        public Task<bool> AnimationTask { get; set; }
        #endregion

        #region Events
        public event Action OnClosing;
        #endregion

        #region Commands
        public ICommand SkipWordCommand { get; private set; }
        public ICommand AcceptWordCommand { get; private set; }

        private async void ExecuteSkipWordCommand(object obj)
        {
            PassWords += 1;
            CollectedData.Add(new Tuple<string, AnswerState>(InputWord, AnswerState.Pass));
            CurrentWord += 1;
            InputWord = "";

            AnimationState = AnimationInfo.AnimationOut;
            _ = await AnimationTask;
            OnPropertyChanged(nameof(EnglishWord));
            AnimationState = AnimationInfo.AnimationIn;
            _ = await AnimationTask;

            if (CurrentWord >= _wordsInRound)
            {
                if (_isFirst)
                {
                    _isFirst = false;
                    OnClosing.Invoke();
                }
            }
        }
        private async void ExecuteAcceptWordCommand(object obj)
        {
            if (!string.IsNullOrEmpty(InputWord) && MainService.TranslateWordsToList(TranslateChunks[CurrentWord].TranslateWords).Contains(InputWord))
            {
                RightWords += 1;
                CollectedData.Add(new Tuple<string, AnswerState>(InputWord, AnswerState.Right));
            }
            else
            {
                WrongWords += 1;
                CollectedData.Add(new Tuple<string, AnswerState>(InputWord, AnswerState.Wrong));
            }
            CurrentWord += 1;
            InputWord = "";

            AnimationState = AnimationInfo.AnimationOut;
            _ = await AnimationTask;
            OnPropertyChanged(nameof(EnglishWord));
            AnimationState = AnimationInfo.AnimationIn;
            _ = await AnimationTask;


            if (CurrentWord >= _wordsInRound)
            {
                if (_isFirst)
                {
                    _isFirst = false;
                    OnClosing.Invoke();
                }
            }
        }

        private bool CanExecuteSkipWordCommand(object obj)
        {
            return _currentWord < _wordsInRound;
        }
        private bool CanExecuteAcceptWordCommand(object obj)
        {
            return _currentWord < _wordsInRound;
        }
        #endregion
    }
}
