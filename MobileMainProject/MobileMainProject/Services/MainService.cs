using System.Collections.Generic;

namespace MobileMainProject.Services
{
    public static class MainService
    {
        public static List<string> TranslateWordsToList(string str)
        {
            return new List<string>(str.Split(';'));
        }
    }
}
