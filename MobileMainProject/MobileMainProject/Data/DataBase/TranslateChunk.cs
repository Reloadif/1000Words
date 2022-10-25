using SQLite;

namespace MobileMainProject.Data.DataBase
{
    public class TranslateChunk
    {
        [PrimaryKey, AutoIncrement]
        public int ID { get; set; }

        [MaxLength(255)]
        public string EnglishWord { get; set; }
        public string TranslateWords { get; set; }
    }
}
