using System;
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
        static readonly Lazy<SQLiteAsyncConnection> lazyInitializer = new Lazy<SQLiteAsyncConnection>(() => new SQLiteAsyncConnection(DatabasePath, openFlags));
        static SQLiteAsyncConnection SQLiteDatabase => lazyInitializer.Value;
        async Task InitializeAsync()
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

                initialized = true;
            }
        }
        static bool initialized = false;

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
