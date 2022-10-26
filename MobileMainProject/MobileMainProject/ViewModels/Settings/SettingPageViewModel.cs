using MobileMainProject.Models.Base;
using MobileMainProject.ViewModels.Settings;
using MobileMainProject.Views;
using MobileMainProject.Views.Settings;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileMainProject.ViewModels
{
    public class SettingPageViewModel : BaseViewModel
    {
        #region Field
        private bool _isActivityIndicator;
        #endregion

        public SettingPageViewModel()
        {
            GoToGeneralSettingsCommand = new Command(ExecuteGoToGeneralSettingsCommand);
            GoToStatisticPageCommand = new Command(ExecuteGoToStatisticPageCommand);
        }

        #region Properties
        public StatisticPage Statistic { get; private set; }
        public StatisticPageViewModel StatisticVM { get; private set; }

        public bool IsActivityIndicator
        {
            get => _isActivityIndicator;
            set => Set(ref _isActivityIndicator, value);
        }
        #endregion

        #region Commands
        public ICommand GoToGeneralSettingsCommand { get; private set; }
        public ICommand GoToStatisticPageCommand { get; private set; }

        private async void ExecuteGoToGeneralSettingsCommand(object obj)
        {
            await Shell.Current.Navigation.PushAsync(new GeneralSetting { BindingContext = new GeneralSettingViewModel() });
        }
        private async void ExecuteGoToStatisticPageCommand(object obj)
        {
            IsActivityIndicator = true;
            await BeginInvokeOnMainThreadAsync<object>(() =>
            {
                Statistic = new StatisticPage();
                StatisticVM = new StatisticPageViewModel();
                Statistic.BindingContext = StatisticVM;

                return null;
            });

            await Shell.Current.Navigation.PushAsync(Statistic);
            IsActivityIndicator = false;
        }
        #endregion
    }
}