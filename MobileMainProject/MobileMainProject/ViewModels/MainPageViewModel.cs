using MobileMainProject.Data.DataBase;
using MobileMainProject.Models.Base;
using MobileMainProject.Services;
using MobileMainProject.Views;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileMainProject.ViewModels
{
    public class MainPageViewModel : BaseViewModel
    {
        #region Field
        private bool _isActivityIndicator;
        #endregion

        public MainPageViewModel()
        {
            ManualInputCommand = new Command(ExecuteManualInputCommand);
        }

        #region Properties
        public ManualInput ManualInputPage { get; private set; }
        public ManualInputViewModel ManualInputVM { get; private set; }

        public SingleStatisticPage SingleStatistic { get; private set; }
        public SingleStatisticPageViewModel SingleStatisticVM { get; private set; }

        public bool IsActivityIndicator
        {
            get => _isActivityIndicator;
            set => Set(ref _isActivityIndicator, value);
        }
        #endregion

        #region Commands
        public ICommand ManualInputCommand { get; private set; }

        private async void ExecuteManualInputCommand(object obj)
        {
            (Application.Current.MainPage.BindingContext as AppShellViewModel).SettingTabIsEnabled = false;

            IsActivityIndicator = true;
            await BeginInvokeOnMainThreadAsync<object>(() =>
            {
                ManualInputPage = new ManualInput();
                ManualInputVM = new ManualInputViewModel();
                ManualInputVM.OnClosing += OnManualClosing;
                ManualInputPage.BindingContext = ManualInputVM;

                return null;
            });

            await Shell.Current.Navigation.PushAsync(ManualInputPage);
            IsActivityIndicator = false;
        }
        #endregion

        private async void OnManualClosing()
        {
            SingleStatistic = new SingleStatisticPage();
            SingleStatisticVM = new SingleStatisticPageViewModel(ManualInputVM);
            SingleStatisticVM.OnClosing += OnSingleSatisticClosing;
            SingleStatistic.BindingContext = SingleStatisticVM;

            Shell.Current.Navigation.InsertPageBefore(SingleStatistic, ManualInputPage);
            await Shell.Current.Navigation.PopAsync();
        }

        private async void OnSingleSatisticClosing(bool isAccepted)
        {
            if (isAccepted)
            {
                await SaveGameStatistic();
                Mediator.Notify("UpdateGameStatistic");
            }

            await Shell.Current.Navigation.PopAsync();
            (Application.Current.MainPage.BindingContext as AppShellViewModel).SettingTabIsEnabled = true;
        }

        private async Task SaveGameStatistic()
        {
            GameStatistic gameStatistic = new GameStatistic
            {
                RightWords = SingleStatisticVM.RightWords,
                PassWords = SingleStatisticVM.PassWords,
                WrongWords = SingleStatisticVM.WrongWords
            };
            await App.TranslateDB.InsertGameStatisticAsync(gameStatistic);

            List<CollectedData> colectedDatas = new List<CollectedData>();
            foreach (var element in SingleStatisticVM.StatisticElements)
            {
                colectedDatas.Add(new CollectedData
                {
                    GameId = gameStatistic.ID,
                    EnglishWord = element.Chunk.EnglishWord,
                    TranslateWords = element.Chunk.TranslateWords,
                    AnsweredWord = element.AnsweredWord,
                    State = element.Answer
                });

                await App.TranslateDB.SaveCollectedDataAsync(colectedDatas[colectedDatas.Count() - 1]);
            }

            gameStatistic.Collected = colectedDatas;
            await App.TranslateDB.UpdateGameStatisticAsync(gameStatistic);
        }
    }
}
