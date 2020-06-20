using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Threading.Tasks;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;
using Xamarin.Forms;
using System.Web;
using System.Net.NetworkInformation;

using PukekoApp.Models;

namespace PukekoApp.Services
{
    public class DBConnector
    {
        private const string APIURL = "https://pukeko.yiays.com/api/{0}";
        private string _userToken;
        public string UserToken
        {
            set {
                _userToken = value;
            }
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

        public async Task<ApiRes<T>> ApiReq<T>(Method method, string path, Object postdata = null)
        {
            ApiRes<T> apires = (ApiRes<T>)await WebReq(method, String.Format(APIURL, path), postdata);

            apires.obj = JsonConvert.DeserializeObject<T>(apires.data);

            return apires;
        }

        public async Task<WebRes> WebReq(Method method, string path, Object postdata = null)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
            request.Method = methodToString(method);
            request.UserAgent = "request";
            switch (method)
            {
                case Method.POST:
                    var postData = Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(postdata));
                    request.ContentLength = postData.Length;
                    request.ContentType = "application/json";
                    Stream postdatastream = request.GetRequestStream();
                    postdatastream.Write(postData, 0, postData.Length);
                    postdatastream.Close();
                    break;
            }

            using (HttpWebResponse response = (HttpWebResponse)await request.GetResponseAsync())
            {
                Stream receiveStream = response.GetResponseStream();
                StreamReader readStream = null;

                if (response.CharacterSet == null)
                {
                    readStream = new StreamReader(receiveStream);
                }
                else
                {
                    readStream = new StreamReader(receiveStream, Encoding.GetEncoding(response.CharacterSet));
                }

                string data = readStream.ReadToEnd();

                response.Close();
                readStream.Close();

                return new WebRes() { data=data, status=(int)response.StatusCode };
            }
        }
    }
}
