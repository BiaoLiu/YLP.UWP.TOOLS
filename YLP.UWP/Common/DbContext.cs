using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Windows.Storage;
using SQLite.Net;
using SQLite.Net.Async;
using SQLite.Net.Platform.WinRT;
using YLP.UWP.Core.Models;

namespace YLP.UWP.Core.Data
{
    public class DbContextAsync : SingletonProvider<DbContextAsync>
    {
        public string DbFileName = "YLP.db";

        public string DbFilePath;

        async public void InitAsync()
        {
            try
            {
                DbFilePath = ApplicationData.Current.LocalFolder.Path + "\\" + DbFileName;

               // DbFilePath = "ms-appx:///"+DbFileName;

                //var folder = ApplicationData.Current.LocalFolder;
                //await folder.CreateFileAsync("YLP.db", CreationCollisionOption.FailIfExists);

                var db = GetDbConnectionAsync();

                await db.CreateTableAsync<User>();
            }
            catch (Exception)
            {
            }
        }

        public SQLiteAsyncConnection GetDbConnectionAsync()
        {
            var connectionFactory = new Func<SQLiteConnectionWithLock>(() => new SQLiteConnectionWithLock(new SQLitePlatformWinRT(), new SQLiteConnectionString(DbFilePath, storeDateTimeAsTicks: false)));

            var asyncConnection = new SQLiteAsyncConnection(connectionFactory);

            return asyncConnection;
        }
    }

    public class DbContext : SingletonProvider<DbContext>
    {
        public string DbFileName = "YLP.db";

        public string DbFilePath = ApplicationData.Current.LocalFolder.Path + "\\" + "YLP.db";

        public void Init()
        {
            DbFilePath = ApplicationData.Current.LocalFolder.Path + "\\" + DbFileName;

            using (var db = GetDbConnection())
            {
                db.CreateTable<User>();
            }
        }

        public SQLiteConnection GetDbConnection()
        {
            var connection = new SQLiteConnection(new SQLitePlatformWinRT(), DbFilePath);

            return connection;
        }
    }
}
