using MobileMainProject.ViewModels;
using MobileMainProject.Views;
using Xamarin.Forms;

namespace MobileMainProject
{
    public partial class AppShell : Shell
    {
        public AppShell()
        {
            InitializeComponent();

            Routing.RegisterRoute(nameof(MainPage), typeof(MainPage));
            Routing.RegisterRoute(nameof(SettingPage), typeof(SettingPage));

            BindingContext = new AppShellViewModel();
        }
    }
}