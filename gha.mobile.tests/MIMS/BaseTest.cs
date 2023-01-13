using gha.mobile.common.entities;
using Newtonsoft.Json.Linq;
using System.IO;

namespace gha.mobile.tests.MIMS
{
    public class BaseTest
    {
        /// <summary>
        /// Reads the epicor connection from a JSON in the same path
        /// The environment will determine the name of the file that this will read from
        /// 
        /// env102600 - EpicorConnection-102600.json
        /// env102600clean - EpicorConnection-120600clean.json
        /// env28597test - EpicorConnection-28957test.json
        /// </summary>
        public virtual EpicorConnection GetEpicorConnection(EpicorEnvironment environment)
        {
            JObject json = environment switch
            {
                EpicorEnvironment.env102600 => ReadJSONFromFile("./EpicorConnection-102600.json"),
                EpicorEnvironment.env102600clean => ReadJSONFromFile("./EpicorConnection-102600clean.json"),
                EpicorEnvironment.env28957test => ReadJSONFromFile("./EpicorConnection-28957test.json"),
                _ => null
            };

            if (json == null) return null;

            return new EpicorConnection()
            {
                AppPoolHost = json["AppPoolHost"].ToString(),
                AppPoolInstance = json["AppPoolInstance"].ToString(),
                UserName = json["UserName"].ToString(),
                Password = json["Password"].ToString(),
                Company = json["Company"].ToString(),
                Plant = json["Plant"].ToString(),
            };
        }

        private JObject ReadJSONFromFile(string path)
        {
            return JObject.Parse(File.ReadAllText(path));
        }
    }
}