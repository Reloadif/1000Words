using MobileMainProject.Data.Models;
using MobileMainProject.Models.Base;
using System;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace MobileMainProject.ViewModels
{
    public class SingleStatisticPageViewModel : BaseViewModel
    {
        public SingleStatisticPageViewModel(ManualInputViewModel manualInputVM)
        {
            WordsInRound = Preferences.Get("WordsInRound", 20);

            RightWords = manualInputVM.RightWords;
            PassWords = manualInputVM.PassWords;
            WrongWords = manualInputVM.WrongWords;

            StatisticElements = new ObservableCollection<StatisticElementModel>();

            for (int i = 0; i < WordsInRound; ++i)
            {
                StatisticElements.Add(new StatisticElementModel
                {
                    Chunk = manualInputVM.TranslateChunks[i],
                    AnsweredWord = manualInputVM.CollectedData[i].Item1,
                    Answer = manualInputVM.CollectedData[i].Item2
                });
            }

            DeleteCommand = new Command(ExecuteDeleteCommand);
            SaveCommand = new Command(ExecuteSaveCommand);
        }

        #region Properties
        public ObservableCollection<StatisticElementModel> StatisticElements { get; private set; }

        public int WordsInRound { get; set; }
        public int RightWords { get; set; }
        public int PassWords { get; set; }
        public int WrongWords { get; set; }
        #endregion

        #region Events
        public event Action<bool> OnClosing;
        #endregion

        #region Commands
        public ICommand DeleteCommand
        {
            get;
            private set;
        }
        public ICommand SaveCommand
        {
            get;
            private set;
        }

        private void ExecuteDeleteCommand(object obj)
        {
            OnClosing.Invoke(false);
        }
        private void ExecuteSaveCommand(object obj)
        {
            OnClosing.Invoke(true);
        }
        #endregion
    }
}
