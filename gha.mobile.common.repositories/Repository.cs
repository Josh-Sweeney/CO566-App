using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading.Tasks;
using SQLite;
using Xamarin.Forms;

namespace gha.mobile.common.repositories
{
    public abstract class Repository<T> : IRepository<T>
    {
        public event EventHandler<T> OnAdded;
        public event EventHandler<T> OnUpdated;
        public event EventHandler<T> OnDeleted;

        protected SQLiteAsyncConnection connection;
        protected string databasePath;

        protected void CreateConnection(string Database, string Cipher)
        {
            if (connection != null)
            {
                return;
            }

            switch (Device.RuntimePlatform)
            {
                case Device.UWP:
                    databasePath = Database;
                    break;
                case Device.iOS:
                case Device.Android:
                default:
                    var documentPath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
                    databasePath = Path.Combine(documentPath, Database);
                    break;
            }

            var options = new SQLiteConnectionString(databasePath, true, key: Cipher);

            connection = new SQLiteAsyncConnection(options);
        }

        public async Task Add(T item)
        {
            await connection.InsertAsync(item);
            OnAdded?.Invoke(this, item);
        }

        public async Task AddOrUpdate(T item, long ID)
        {
            if (ID == 0)
            {
                await Add(item);
            }
            else
            {
                await Update(item);
            }
        }

        public async Task Delete(T item)
        {
            await connection.DeleteAsync(item);
            OnDeleted?.Invoke(this, item);
        }

        public async Task Update(T item)
        {
            await connection.UpdateAsync(item);
            OnUpdated?.Invoke(this, item);
        }
    }
}
