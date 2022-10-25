using MobileMainProject.Data.DataBase;
using MobileMainProject.Infrastructure.Shared;
using MobileMainProject.Models.Base;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Windows.Input;
using Xamarin.Forms;

namespace MobileMainProject.ViewModels.Settings
{
    public class GameStatisticPageViewModel : BaseViewModel
    {
        #region Fields
        private int _selectedIndex = 0;
        #endregion

        public GameStatisticPageViewModel(GameStatistic gameStatistic)
        {
            Game = gameStatistic;

            StatisticElements = new ObservableCollection<CollectedData>(Game.Collected);
            FilterElements = new ObservableCollection<string> { "Все", "Правильные", "Пропущенные", "Неправильные" };

            PickerSelectedIndexCommand = new Command(ExecutePickerSelectedIndexCommand);
        }

        #region Properties
        public GameStatistic Game { get; set; }

        public ObservableCollection<CollectedData> StatisticElements { get; private set; }
        public ObservableCollection<string> FilterElements { get; private set; }

        public int SelectedIndex
        {
            get => _selectedIndex;
            set => Set(ref _selectedIndex, value);
        }
        #endregion

        #region Commands
        public ICommand PickerSelectedIndexCommand
        {
            get;
            private set;
        }

        private void ExecutePickerSelectedIndexCommand(object obj)
        {
            AnswerState currentFilterAnswer = ConvertStringToAnswerState(obj.ToString());

            List<CollectedData> filteredElements = currentFilterAnswer != AnswerState.None
                ? Game.Collected.Where(element => element.State == currentFilterAnswer).ToList()
                : new List<CollectedData>(Game.Collected);

            foreach (CollectedData element in Game.Collected)
            {
                if (!filteredElements.Contains(element))
                {
                    _ = StatisticElements.Remove(element);
                }
                else if (!StatisticElements.Contains(element))
                {
                    StatisticElements.Add(element);
                }
            }
        }
        #endregion

        private AnswerState ConvertStringToAnswerState(string str)
        {
            if (str == "Правильные")
            {
                return AnswerState.Right;
            }
            if (str == "Пропущенные")
            {
                return AnswerState.Pass;
            }
            if (str == "Неправильные")
            {
                return AnswerState.Wrong;
            }

            return AnswerState.None;
        }
    }
}
