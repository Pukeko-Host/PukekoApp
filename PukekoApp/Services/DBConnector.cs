using System;
using System.Collections.Generic;
using System.Dynamic;
using System.Net;
using System.IO;
using System.Text;
using Newtonsoft.Json;

namespace PukekoApp.Services
{
    class DBConnector
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

        public T Get<T>(Method method, string path, Object postdata)
        {
            var result = WebGet(method, String.Format(APIURL, path), postdata);

            return JsonConvert.DeserializeObject<T>(result);
        }

        public string WebGet(Method method, string path, Object postdata)
        {
            HttpWebRequest request = (HttpWebRequest)WebRequest.Create(path);
            request.Method = methodToString(method);
            request.UserAgent = "request";

            HttpWebResponse response = (HttpWebResponse)request.GetResponse();
            if (response.StatusCode == HttpStatusCode.OK)
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

                return data;
            }
            return "";
        }


    }
}
