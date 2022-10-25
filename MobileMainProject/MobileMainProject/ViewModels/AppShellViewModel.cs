
using MobileMainProject.Models.Base;

namespace MobileMainProject.ViewModels
{
    public class AppShellViewModel : BaseViewModel
    {
        private bool _settingTabIsEnabled = true;

        public bool SettingTabIsEnabled
        {
            get => _settingTabIsEnabled;
            set => Set(ref _settingTabIsEnabled, value);
        }
    }
}
