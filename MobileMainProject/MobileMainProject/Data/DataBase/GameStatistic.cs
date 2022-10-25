using MobileMainProject.Infrastructure.Shared;
using SQLite;
using SQLiteNetExtensions.Attributes;
using System.Collections.Generic;

namespace MobileMainProject.Data.DataBase
{
    public class GameStatistic
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        public int RightWords { get; set; }
        public int PassWords { get; set; }
        public int WrongWords { get; set; }

        [OneToMany]
        public List<CollectedData> Collected { get; set; }
    }

    public class CollectedData
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [ForeignKey(typeof(GameStatistic))]
        public int GameId { get; set; }

        [MaxLength(255)]
        public string EnglishWord { get; set; }
        public string TranslateWords { get; set; }
        [MaxLength(255)]
        public string AnsweredWord { get; set; }
        public AnswerState State { get; set; }
    }
}
