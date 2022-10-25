using MobileMainProject.Models.Base;
using Xamarin.Essentials;

namespace MobileMainProject.ViewModels
{
    public class GeneralSettingViewModel : BaseViewModel
    {
        private int _wordsInRound;

        public GeneralSettingViewModel()
        {
            WordsInRound = Preferences.Get("WordsInRound", 20);
        }

        #region Properties
        public int WordsInRound
        {
            get => _wordsInRound;
            set
            {
                if (Set(ref _wordsInRound, value))
                {
                    Preferences.Set("WordsInRound", _wordsInRound);
                }
            }
        }
        #endregion
    }
}
