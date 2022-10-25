using SQLite;
using SQLiteNetExtensionsAsync.Extensions;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace MobileMainProject.Data.DataBase
{
    public class TranslateDataBase
    {
        private readonly SQLiteAsyncConnection db;

        public int TranslateChunkCount => db.Table<TranslateChunk>().CountAsync().GetAwaiter().GetResult();
        public int GameStatisticCount => db.Table<GameStatistic>().CountAsync().GetAwaiter().GetResult();
        public int CollectedDataCount => db.Table<CollectedData>().CountAsync().GetAwaiter().GetResult();

        public TranslateDataBase(string connectionString)
        {
            db = new SQLiteAsyncConnection(connectionString);
            db.CreateTableAsync<TranslateChunk>().Wait();
            db.CreateTableAsync<GameStatistic>().Wait();
            db.CreateTableAsync<CollectedData>().Wait();
        }

        #region TranslateChunk
        public Task<List<TranslateChunk>> GetTranslateChunksAsync()
        {
            return db.Table<TranslateChunk>().ToListAsync();
        }

        public Task<TranslateChunk> GetTranslateChunkAsync(int id)
        {
            return db.Table<TranslateChunk>().Where(el => el.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveTranslateChunkAsync(TranslateChunk translateChunk)
        {
            return translateChunk.ID != 0 ? db.UpdateAsync(translateChunk) : db.InsertAsync(translateChunk);
        }

        public Task<int> DeleteTranslateChunkAsync(TranslateChunk translateChunk)
        {
            return db.DeleteAsync(translateChunk);
        }
        #endregion

        #region GameStatistic
        public Task<List<GameStatistic>> GetGameStatisticsAsync()
        {
            return db.GetAllWithChildrenAsync<GameStatistic>();
        }

        public Task<GameStatistic> GetGameStatisticAsync(int id)
        {
            return db.Table<GameStatistic>().Where(el => el.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> InsertGameStatisticAsync(GameStatistic gameStatistic)
        {
            return db.InsertAsync(gameStatistic);
        }

        public Task UpdateGameStatisticAsync(GameStatistic gameStatistic)
        {
            return db.UpdateWithChildrenAsync(gameStatistic);
        }

        public Task<int> DeleteGameStatisticAsync(GameStatistic gameStatistic)
        {
            return db.DeleteAsync(gameStatistic);
        }
        #endregion

        #region CollectedData
        public Task<List<CollectedData>> GetCollectedDatasAsync()
        {
            return db.Table<CollectedData>().ToListAsync();
        }

        public Task<CollectedData> GetCollectedDataAsync(int id)
        {
            return db.Table<CollectedData>().Where(el => el.ID == id).FirstOrDefaultAsync();
        }

        public Task<int> SaveCollectedDataAsync(CollectedData collectedData)
        {
            return collectedData.ID != 0 ? db.UpdateAsync(collectedData) : db.InsertAsync(collectedData);
        }

        public Task<int> DeleteCollectedDataAsync(CollectedData collectedData)
        {
            return db.DeleteAsync(collectedData);
        }
        #endregion
    }
}
