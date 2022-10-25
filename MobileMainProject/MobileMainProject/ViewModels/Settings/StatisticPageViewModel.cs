using MobileMainProject.Data.DataBase;
using MobileMainProject.Models.Base;
using MobileMainProject.Services;
using MobileMainProject.Views.Settings;
using System.Collections.ObjectModel;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileMainProject.ViewModels.Settings
{
    public class StatisticPageViewModel : BaseViewModel
    {
        #region Fields
        private ObservableCollection<GameStatistic> _statisticsCollection;
        #endregion

        public StatisticPageViewModel()
        {
            StatisticsCollection = new ObservableCollection<GameStatistic>(App.TranslateDB.GetGameStatisticsAsync().GetAwaiter().GetResult());

            CollectionItemSelectionCommand = new Command(ExecuteCollectionItemSelectionCommand);
            Mediator.Subscribe("UpdateGameStatistic", UpdateStatistics);
        }

        #region Properties
        public ObservableCollection<GameStatistic> StatisticsCollection
        {
            get => _statisticsCollection;
            set => Set(ref _statisticsCollection, value);
        }
        #endregion

        #region Commands
        public ICommand CollectionItemSelectionCommand { get; private set; }

        private async void ExecuteCollectionItemSelectionCommand(object obj)
        {
            await Shell.Current.Navigation.PushAsync(new GameStatisticPage { BindingContext = new GameStatisticPageViewModel(obj as GameStatistic) });
        }
        #endregion

        private void UpdateStatistics(object obj)
        {
            StatisticsCollection = new ObservableCollection<GameStatistic>(App.TranslateDB.GetGameStatisticsAsync().GetAwaiter().GetResult());
        }
    }
}
