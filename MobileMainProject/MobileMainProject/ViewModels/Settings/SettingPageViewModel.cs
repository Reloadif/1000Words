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
        public SettingPageViewModel()
        {
            GoToGeneralSettingsCommand = new Command(ExecuteGoToGeneralSettingsCommand);
            GoToStatisticPageCommand = new Command(ExecuteGoToStatisticPageCommand);
        }

        #region Commands
        public ICommand GoToGeneralSettingsCommand { get; private set; }
        public ICommand GoToStatisticPageCommand { get; private set; }

        private async void ExecuteGoToGeneralSettingsCommand(object obj)
        {
            await Shell.Current.Navigation.PushAsync(new GeneralSetting { BindingContext = new GeneralSettingViewModel() });
        }
        private async void ExecuteGoToStatisticPageCommand(object obj)
        {
            await Shell.Current.Navigation.PushAsync(new StatisticPage { BindingContext = new StatisticPageViewModel() });
        }
        #endregion
    }
}