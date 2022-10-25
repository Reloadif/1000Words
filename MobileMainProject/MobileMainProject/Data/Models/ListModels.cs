using MobileMainProject.Data.DataBase;
using MobileMainProject.Infrastructure.Shared;

namespace MobileMainProject.Data.Models
{
    public class StatisticElementModel
    {
        public TranslateChunk Chunk { get; set; }
        public string AnsweredWord { get; set; }
        public AnswerState Answer { get; set; }
    }
}
