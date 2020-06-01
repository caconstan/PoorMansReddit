using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.Text.Json;
using System.Threading.Tasks;

namespace PoorMansReddit.Models
{
    public class AWWSubredditDAL
    {
        private readonly IConfiguration Configuration;
        public AWWSubredditDAL(IConfiguration configuration)
        {
            Configuration = configuration;
        }
        public string GetAPIString()
        {
            try
            {
                string url = $"https://www.reddit.com/r/aww/.json";
                HttpWebRequest request = WebRequest.CreateHttp(url);
                HttpWebResponse response = (HttpWebResponse)request.GetResponse();

                StreamReader rd = new StreamReader(response.GetResponseStream());
                string output = rd.ReadToEnd();
                return output;
            }
            catch (Exception)
            {
                return null;
            }
        }
        public Subreddit GetEntries()
        {
            string listOfEntries = GetAPIString();
            if (listOfEntries == null) return null;
            JObject json = JObject.Parse(listOfEntries);
            Subreddit m = JsonConvert.DeserializeObject<Subreddit>(json.ToString());
            //var des = (MyClass)Newtonsoft.Json.JsonConvert.DeserializeObject(response, typeof(MyClass)
            return m;
        }
    }
}
