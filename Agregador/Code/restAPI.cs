﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.IO;
using System.Net;
using System.Text;
using System.Configuration;

public enum HttpVerb
{
    GET,
    POST,
    PUT,
    DELETE
}

namespace Agregador
{
    public class RestAPI
    {
        public string EndPoint { get; set; }
        public HttpVerb Method { get; set; }
        public string ContentType { get; set; }
        public string PostData { get; set; }
        
        private static string credentials = ConfigurationManager.AppSettings["CamilyoAPIUser"] + ":" + ConfigurationManager.AppSettings["CamilyoAPIPassword"];

        public RestAPI()
        {
            EndPoint = "";
            Method = HttpVerb.GET;
            ContentType = "text/json";
            PostData = "";
        }
        public RestAPI(string endpoint)
        {
            EndPoint = endpoint;
            Method = HttpVerb.GET;
            ContentType = "text/json";
            PostData = "";
        }
        public RestAPI(string endpoint, HttpVerb method)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/json";
            PostData = "";
        }

        public RestAPI(string endpoint, HttpVerb method, string postData)
        {
            EndPoint = endpoint;
            Method = method;
            ContentType = "text/json";
            PostData = postData;
        }


        public string MakeRequest()
        {
            return MakeRequest("");
        }

        public string MakeRequest(string parameters)
        {
            try
            {
                var request = (HttpWebRequest)WebRequest.Create(EndPoint + parameters);

                request.Method = Method.ToString();
                request.ContentLength = 0;
                request.ContentType = ContentType;
                request.Headers.Add("Authorization", "Basic " + Convert.ToBase64String(Encoding.UTF8.GetBytes(credentials)));

                if (!string.IsNullOrEmpty(PostData) && (Method == HttpVerb.POST || Method == HttpVerb.PUT))
                {
                    var encoding = new UTF8Encoding();
                    //var bytes = Encoding.GetEncoding("iso-8859-1").GetBytes(PostData);
                    var bytes = Encoding.GetEncoding("utf-8").GetBytes(PostData);
                    request.ContentLength = bytes.Length;

                    using (var writeStream = request.GetRequestStream())
                    {
                        writeStream.Write(bytes, 0, bytes.Length);
                    }
                }

                using (var response = (HttpWebResponse)request.GetResponse())
                {
                    var responseValue = string.Empty;

                    if (response.StatusCode != HttpStatusCode.OK)
                    {
                        if (response.StatusCode.ToString() == "Created")
                            responseValue = response.StatusCode.ToString();
                        /*else
                            var message = String.Format("Request failed. Received HTTP {0}", response.StatusCode);*/
                        //throw new ApplicationException(message);
                    }

                    using (var responseStream = response.GetResponseStream())
                    {
                        if (responseStream != null)
                            using (var reader = new StreamReader(responseStream))
                            {
                                responseValue = reader.ReadToEnd();
                            }
                    }

                    return responseValue;
                }
            }
            catch (Exception ex)
            {
                new BOAgregador().guardarError("rest_Agregador", ex.Message);
                return ex.Message;
            }
        }
    }
}