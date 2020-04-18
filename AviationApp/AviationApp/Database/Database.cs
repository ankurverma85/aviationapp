using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

using AviationApp.FAADataParser;
using AviationApp.FAADataParser.Fixes;

using SQLite;

namespace AviationApp.Database
{
    public class Database
    {
        public Database() => InitializeAsync().SafeFireAndForget(false);
        private static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() => new SQLiteAsyncConnection(DatabasePath, openFlags));
        private static SQLiteAsyncConnection SQLiteDatabase => lazyInitializer.Value;
        private async Task InitializeAsync()
        {
            if (!initialized)
            {
                if (!SQLiteDatabase.TableMappings.Any(m => m.MappedType.Name == typeof(Cycle).Name))
                {
                    _ = await SQLiteDatabase.CreateTableAsync(typeof(Cycle)).ConfigureAwait(false);
                }
                if (!SQLiteDatabase.TableMappings.Any(m => m.MappedType.Name == typeof(Fix1).Name))
                {
                    _ = await SQLiteDatabase.CreateTableAsync(typeof(Fix1)).ConfigureAwait(false);
                }
                await CleanUpOldCycles();

                initialized = true;
            }
        }

        private async Task CleanUpOldCycles()
        {
            (bool found, DateTime currentCycle, bool fiftySixDayCycle) = Cycle.GetCurrentCycle();
            if (found)
            {
                AsyncTableQuery<Cycle> cycleQuery = SQLiteDatabase.Table<Cycle>().Where(i => i.StartDate < currentCycle);
                List<Cycle> oldCycles = await cycleQuery.ToListAsync();
                foreach (Cycle cycle in oldCycles)
                {
                    await new Task(() =>
                    {
                        _ = SQLiteDatabase.ExecuteAsync("delete * from " + nameof(Fix1) + " where " + nameof(Fix1.Cycle) + " = ?", cycle.CycleID);
                        _ = SQLiteDatabase.DeleteAsync<Cycle>(cycle.CycleID);
                    });
                }
            }
        }

        private static bool initialized = false;

        private static string DatabasePath
        {
            get
            {
                string localAppDataPath = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData);
                return Path.Combine(localAppDataPath, databaseFilename);
            }
        }
        private const string databaseFilename = "AviationApp.Sqlite.db3";
        private const SQLiteOpenFlags openFlags = SQLiteOpenFlags.ReadWrite | SQLiteOpenFlags.Create | SQLiteOpenFlags.SharedCache;
    }
}
