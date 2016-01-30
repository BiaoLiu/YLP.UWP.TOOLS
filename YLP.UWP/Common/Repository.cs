using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using SQLite.Net;
using SQLite.Net.Async;
using YLP.UWP.Core.Data;
using YLP.UWP.Core.Models;

namespace YLP.UWP.Common
{
    public class RepositoryAsync
    {
        public AsyncTableQuery<User> User => Conn.Table<User>();

        public SQLiteAsyncConnection Conn => DbContextAsync.Instance.GetDbConnectionAsync();

        public async Task<int> InsertUserAsync(User user)
        {
            var result = await Conn.InsertAsync(user);

            return result;
        }

        public async Task<int> UpdateUserAsync(User user)
        {
            var result = await Conn.UpdateAsync(user);

            return result;
        }

        public async Task<List<User>> GetAllUserAsync()
        {
            var result = await Conn.Table<User>().ToListAsync();

            return result;
        }

        public async Task<List<User>> GetRandomUsers(int count)
        {
            return await Conn.QueryAsync<User>($"select * from user order by random() limit {count}");
        }

        public async Task<List<User>> GetUsersAsync(int pageIndex, int pageSize)
        {
            // var result = await Conn.Table<User>().OrderBy(u => u.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();

            return null;
            // return result;
        }

        public async Task<int> GetUserCount(string sql)
        {
            return await Conn.ExecuteScalarAsync<int>(sql);
        }
    }

    public class Repository
    {
        public SQLiteConnection Conn => DbContext.Instance.GetDbConnection();

        public TableQuery<User> User => Conn.Table<User>();

        public int InsertUser(User user)
        {
            using (var db = DbContext.Instance.GetDbConnection())
            {
                var result = db.Insert(user);

                return result;
            }
        }

        public int UpdateUser(User user)
        {
            using (var db = DbContext.Instance.GetDbConnection())
            {
                var result = db.Update(user);

                return result;
            }
        }

        public List<User> GetAllUser()
        {
            var result = Conn.Table<User>().ToList();
            Conn.Dispose();

            return result;
        }

        public List<User> GetUsers(int pageIndex, int pageSize)
        {
            //var result = Conn.Table<User>().OrderBy(u => u.Id).Skip((pageIndex - 1) * pageSize).Take(pageSize).ToList();
            //Conn.Dispose();

            //return result;

            return null;
        }

        public int GetUserCount(string sql)
        {
            var result = Conn.ExecuteScalar<int>(sql);
            Conn.Dispose();

            return result;
        }
    }
}
