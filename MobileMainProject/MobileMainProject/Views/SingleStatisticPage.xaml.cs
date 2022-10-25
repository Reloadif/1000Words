using MobileMainProject.ViewModels;
using Xamarin.Forms;

namespace MobileMainProject.Views
{
    public partial class SingleStatisticPage : ContentPage
    {
        public SingleStatisticPage()
        {
            InitializeComponent();
        }

        protected override bool OnBackButtonPressed()
        {
            (Application.Current.MainPage.BindingContext as AppShellViewModel).SettingTabIsEnabled = true;
            return base.OnBackButtonPressed();
        }
    }
}