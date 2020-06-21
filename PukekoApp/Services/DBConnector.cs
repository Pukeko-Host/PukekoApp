using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text;
using System.Linq;
using Newtonsoft.Json;
using Xamarin.Forms;

using PukekoApp.Models;
using System.Web;

namespace PukekoApp.Services
{
    public class DBConnector
    {
        private const string APIURL = "https://pukeko.yiays.com/api/{0}";
        private CookieContainer cookieContainer;

        public DBConnector()
        {
            cookieContainer = new CookieContainer();
            /*if(App.Current.Properties.ContainsKey("cookieContainer"))
            cookieContainer = (CookieContainer)App.Current.Properties["cookieContainer"];*/
        }

        public enum Method
        {
            GET,
            HEAD,
            POST,
            PUT,
            DELETE
        }

        private string methodToString(Method method)
        {
            switch (method)
            {
                case Method.GET:
                    return "GET";
                case Method.HEAD:
                    return "HEAD";
                case Method.POST:
                    return "POST";
                case Method.PUT:
                    return "PUT";
                case Method.DELETE:
                    return "DELETE";
                default:
                    return null;
            }
        }

        public class WebRes
        {
            public string data;
            public int status;
        }
        public class ApiRes<T> : WebRes
        {
            public T obj;
        }

        public async Task<ApiRes<T>> ApiReq<T>(Method method, string path, Dictionary<string,object> postdata = null)
        {
            WebRes webres = await WebReq(method, String.Format(APIURL, path), postdata);
            T obj;
            try {
                obj = JsonConvert.DeserializeObject<T>(webres.data);
            } catch (InvalidCastException)
            {
                obj = default;
            }
            return new ApiRes<T>()
            {
                data = webres.data,
                status = webres.status,
                obj = obj
            };
        }

        public async Task<WebRes> WebReq(Method method, string path, Dictionary<string,object> postdata = null)
        {
            try
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
                request.Method = methodToString(method);
                request.UserAgent = "request";
                request.CookieContainer = cookieContainer;
                switch (method)
                {
                    case Method.POST:
                        byte[] postData = Encoding.UTF8.GetBytes(QueryString(postdata));
                        request.ContentLength = postData.Length;
                        request.ContentType = "application/x-www-form-urlencoded";
                        Stream postdatastream = request.GetRequestStream();
                        postdatastream.Write(postData, 0, postData.Length);
                        postdatastream.Close();
                        break;
                }
                using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = new StreamReader(receiveStream);

                    int statuscode = (int)response.StatusCode;
                    string data = readStream.ReadToEnd();

                    cookieContainer.Add(response.Cookies);
                    //App.Current.Properties["cookieContainer"] = cookieContainer;
                    //await App.Current.SavePropertiesAsync();

                    response.Close();
                    readStream.Close();

                    return new WebRes() { data = data, status = statuscode };
                }
            }
            catch (WebException ex)
            {
                using (HttpWebResponse response = (HttpWebResponse)ex.Response)
                {
                    Stream receiveStream = response.GetResponseStream();
                    StreamReader readStream = new StreamReader(receiveStream);

                    int statuscode = (int)response.StatusCode;
                    string data = readStream.ReadToEnd();

                    response.Close();
                    readStream.Close();

                    return new WebRes() { data = data, status = statuscode };
                }
            }
            catch (Exception)
            {
                await Application.Current.MainPage.DisplayAlert(title: "Network error!", message: "There seems to be an issue connecting to the internet.", cancel: "Okay");
                return new WebRes() { data = null, status = -1 };
            }
        }
        public static string QueryString(IDictionary<string, object> dict)
        {
            var list = new List<string>();
            foreach (var item in dict)
            {
                list.Add(HttpUtility.UrlEncode(item.Key) + "=" + HttpUtility.UrlEncode(item.Value.ToString()));
            }
            return string.Join("&", list);
        }
    }
}
