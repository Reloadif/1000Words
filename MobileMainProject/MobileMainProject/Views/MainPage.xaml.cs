using MobileMainProject.ViewModels;
using Xamarin.Forms;

namespace MobileMainProject.Views
{
    public partial class MainPage : ContentPage
    {
        public MainPage()
        {
            InitializeComponent();
            BindingContext = new MainPageViewModel();
        }
    }
}