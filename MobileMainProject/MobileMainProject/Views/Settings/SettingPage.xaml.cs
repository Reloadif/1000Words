using MobileMainProject.ViewModels;
using Xamarin.Forms;

namespace MobileMainProject.Views
{
    public partial class SettingPage : ContentPage
    {
        public SettingPage()
        {
            InitializeComponent();
            BindingContext = new SettingPageViewModel();
        }
    }
}