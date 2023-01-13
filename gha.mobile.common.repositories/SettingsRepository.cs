using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using gha.mobile.common.entities;
using gha.mobile.common.helpers;
using Plugin.DeviceInfo;
using Xamarin.Forms;

namespace gha.mobile.common.repositories
{
    public class SettingsRepository : Repository<Settings>
    {
        public string DeviceID
        {
            get => (Task.Run(async () => await Get("DeviceID")).Result.Value);
            set => Task.Run(async () => await AddOrUpdate("DeviceID", value));
        }
        
        public string Plant
        {
            get => (Task.Run(async () => await Get("Plant")).Result.Value);
            set => Task.Run(async () => await AddOrUpdate("Plant", value));
        }

        public string AppTitle
        {
            get => (Task.Run(async () => await Get("AppTitle")).Result.Value);
            set => Task.Run(async () => await AddOrUpdate("AppTitle", value));
        }

        public string AppVersion
        {
            get => (Task.Run(async () => await Get("AppVersion")).Result.Value);
            set => Task.Run(async () => await AddOrUpdate("AppVersion", value));
        }

        public string BackColour
        {
            get => (Task.Run(async () => await Get("Background")).Result.Value);
            set => Task.Run(async () => await AddOrUpdate("Background", value));
        }

        public string ForeColour
        {
            get => (Task.Run(async () => await Get("Foreground")).Result.Value);
            set => Task.Run(async () => await AddOrUpdate("Foreground", value));
        }

        public byte[] Logo
        {
            get => Encoding.UTF8.GetBytes("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");
            set => Task.Run(async () => await AddOrUpdate("Logo", value));
        }

        public string ERP
        {
            get
            {
                var result = Task.Run(async () => await Get("ERP")).Result.Value;

                return string.IsNullOrEmpty(result) ? "EPICOR" : result;
            }
            set => Task.Run(async () => await AddOrUpdate("ERP", value));
        }

        public string ERPServer
        {
            get => (Task.Run(async () => await Get("ERPServer")).Result.Value);
            set => Task.Run(async () => await AddOrUpdate("ERPServer", value));
        }

        public string ERPPDatabase
        {
            get => (Task.Run(async () => await Get("ERPPDatabase")).Result.Value);
            set => Task.Run(async () => await AddOrUpdate("ERPPDatabase", value));
        }

        public string ERPUserName
        {
            get => (Task.Run(async () => await Get("ERPUserName")).Result.Value);
            set => Task.Run(async () => await AddOrUpdate("ERPUserName", value));
        }

        public string ERPPassword
        {
            get => (Task.Run(async () => await Get("ERPPassword")).Result.Value);
            set => Task.Run(async () => await AddOrUpdate("ERPPassword", value));
        }

        public string ERPCompany
        {
            get => (Task.Run(async () => await Get("ERPCompany")).Result.Value);
            set => Task.Run(async () => await AddOrUpdate("ERPCompany", value));
        }

        public string ERPLicense
        {
            get => "Web";
            set => Task.Run(async () => await AddOrUpdate("ERPLicense", value));
        }

        #region Connection

        public async Task CreateConnection()
        {
            if (base.connection == null)
            {
                base.CreateConnection($"settings.db", ApplicationConfig.CipherSettings);
                await base.connection.CreateTableAsync<Settings>();
            }
        }

        #endregion

        #region CRUD operations

        public async Task<List<Settings>> Get()
        {
            await CreateConnection();
            return await base.connection.Table<Settings>().ToListAsync();
        }

        public async Task<Settings> Get(string key)
        {
            await CreateConnection();
            var list = await connection.Table<Settings>().ToListAsync();
            return list.FirstOrDefault(r => r.Key == key) ?? new Settings { Value = string.Empty };
        }

        public async Task<OS> GetOS(string key)
        {
            await CreateConnection();
            var setting = (await base.connection.Table<Settings>().ToListAsync()).Where(r => r.Key == key)
                .FirstOrDefault();
            var value = setting.Value;
            return (value == "droid") ? OS.droid : OS.ios;
        }

        

        public new async Task Add(Settings item)
        {
            await CreateConnection();
            await base.Add(item);
        }

        public new async Task Update(Settings item)
        {
            await CreateConnection();
            await base.Update(item);
        }

        public async Task AddOrUpdate(string key, string value)
        {
            await CreateConnection();
            Settings item = (await base.connection.Table<Settings>().ToListAsync()).Where(r => r.Key == key)
                .FirstOrDefault();

            if (item == null)
            {
                item = new Settings { Key = key, Value = value };
                await base.Add(item);
            }
            else
            {
                item.Value = value;
                await base.Update(item);
            }
        }

        public async Task AddOrUpdate(string key, byte[] value)
        {
            await AddOrUpdate(key, Encoding.UTF8.GetString(value));
        }

        public async Task AddOrUpdate(string key, bool value)
        {
            await AddOrUpdate(key, value.ToString());
        }

        public async Task AddOrUpdate(string key, OS value)
        {
            await AddOrUpdate(key, (value == OS.droid) ? "droid" : "ios");
        }

        public new async Task Delete(Settings item)
        {
            await CreateConnection();
            await base.Delete(item);
        }

        #endregion

        #region Settings database population

        public async Task CheckSettings(string appVersion, string appLicenceKey, string appTitle)
        {
            await CreateConnection();
            var xcount = await base.connection.Table<Settings>().CountAsync();

            if (xcount == 0)
            {
                await DeleteAllKeys();
                await AddAllKeys(appVersion, appLicenceKey, appTitle);
                return;
            }

            if (await connection.Table<Settings>().Where(r => r.Key == "AppVersion").CountAsync() == 0)
            {
                await DeleteAllKeys();
                await AddAllKeys(appVersion, appLicenceKey, appTitle);
                return;
            }

            var AppVersion = (await Get("AppVersion")).Value;

            if (AppVersion != appVersion)
            {
                await AddAllKeys(appVersion, appLicenceKey, appTitle);
                await AddOrUpdate("AppVersion", appVersion);
                await AddOrUpdate("AppLicenceKey", appLicenceKey);
                await AddOrUpdate("AppTitle", appTitle);
            }
        }

        private async Task DeleteAllKeys()
        {
            //Connection has been opened in CheckSettings()
            var keys = await base.connection.Table<Settings>().ToListAsync();
            foreach (var key in keys)
            {
                await base.Delete(key);
            }
        }

        private OS GetOs()
        {
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    return OS.ios;
                case Device.Android:
                    return OS.droid;
                default:
                    return OS.unknown;
            }
        }

        private async Task AddAllKeys(string appVersion, string appLicenceKey, string appTitle)
        {
            //Connection has been opened in CheckSettings()
            if (await connection.Table<Settings>().Where(r => r.Key == "Logo").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "Logo", Value = ApplicationConfig.GHALogo });
            if (await connection.Table<Settings>().Where(r => r.Key == "Foreground").CountAsync() == 0)
                await connection.InsertAsync(new Settings()
                    { Key = "Foreground", Value = ApplicationConfig.DefaultForeground });
            if (await connection.Table<Settings>().Where(r => r.Key == "Background").CountAsync() == 0)
                await connection.InsertAsync(new Settings()
                    { Key = "Background", Value = ApplicationConfig.DefaultBackground });
            if (await connection.Table<Settings>().Where(r => r.Key == "CustomisationStamp").CountAsync() == 0)
                await connection.InsertAsync(new Settings()
                    { Key = "CustomisationStamp", Value = ApplicationConfig.CustomisationStamp });
            if (await connection.Table<Settings>().Where(r => r.Key == "AppLicenceKey").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "AppLicenceKey", Value = appLicenceKey });
            if (await connection.Table<Settings>().Where(r => r.Key == "AppTitle").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "AppTitle", Value = appTitle });
            if (await connection.Table<Settings>().Where(r => r.Key == "LicenceCode").CountAsync() == 0)
                await connection.InsertAsync(new Settings()
                    { Key = "LicenceCode", Value = ApplicationConfig.LicenceCode });
            if (await connection.Table<Settings>().Where(r => r.Key == "CompanyCode").CountAsync() == 0)
                await connection.InsertAsync(new Settings()
                    { Key = "CompanyCode", Value = ApplicationConfig.CompanyCode });
            if (await connection.Table<Settings>().Where(r => r.Key == "CompanyName").CountAsync() == 0)
                await connection.InsertAsync(new Settings()
                    { Key = "CompanyName", Value = ApplicationConfig.CompanyName });
            if (await connection.Table<Settings>().Where(r => r.Key == "AppVersion").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "AppVersion", Value = appVersion });
            if (await connection.Table<Settings>().Where(r => r.Key == "DeviceName").CountAsync() == 0)
                await connection.InsertAsync(new Settings()
                    { Key = "DeviceName", Value = CrossDeviceInfo.Current.DeviceName });
            if (await connection.Table<Settings>().Where(r => r.Key == "DeviceID").CountAsync() == 0)
                await connection.InsertAsync(new Settings()
                    { Key = "DeviceID", Value = CrossDeviceInfo.Current.GenerateAppId() });
            if (await connection.Table<Settings>().Where(r => r.Key == "OS").CountAsync() == 0)
                await AddOrUpdate("OS", GetOs());
            if (await connection.Table<Settings>().Where(r => r.Key == "Suspended").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "Suspended", Value = "false" });
            if (await connection.Table<Settings>().Where(r => r.Key == "TestDevice").CountAsync() == 0)
                await connection.InsertAsync(new Settings { Key = "TestDevice", Value = "false" });
            if (await connection.Table<Settings>().Where(r => r.Key == "AppServerURL").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "AppServerURL", Value = string.Empty });
            if (await connection.Table<Settings>().Where(r => r.Key == "UserName").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "UserName", Value = string.Empty });
            if (await connection.Table<Settings>().Where(r => r.Key == "Password").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "Password", Value = string.Empty });
            if (await connection.Table<Settings>().Where(r => r.Key == "LocalUserName").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "LocalUserName", Value = string.Empty });
            if (await connection.Table<Settings>().Where(r => r.Key == "LocalPassword").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "LocalPassword", Value = string.Empty });
            if (await connection.Table<Settings>().Where(r => r.Key == "Company").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "Company", Value = string.Empty });
            if (await connection.Table<Settings>().Where(r => r.Key == "Plant").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "Plant", Value = string.Empty });
            if (await connection.Table<Settings>().Where(r => r.Key == "FirebaseToken").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "FirebaseToken", Value = string.Empty });

            if (await connection.Table<Settings>().Where(r => r.Key == "ERP").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "ERP", Value = string.Empty });
            if (await connection.Table<Settings>().Where(r => r.Key == "ERPServer").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "ERPServer", Value = string.Empty });
            if (await connection.Table<Settings>().Where(r => r.Key == "ERPPDatabase").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "ERPPDatabase", Value = string.Empty });
            if (await connection.Table<Settings>().Where(r => r.Key == "ERPUserName").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "ERPUserName", Value = string.Empty });
            if (await connection.Table<Settings>().Where(r => r.Key == "ERPPassword").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "ERPPassword", Value = string.Empty });
            if (await connection.Table<Settings>().Where(r => r.Key == "ERPCompany").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "ERPCompany", Value = string.Empty });
            if (await connection.Table<Settings>().Where(r => r.Key == "ERPLicense").CountAsync() == 0)
                await connection.InsertAsync(new Settings() { Key = "ERPLicense", Value = string.Empty });
        }

        #endregion
    }
}