using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Net;
using System.IO;

namespace Translator
{
    public class TranslatorApi
    {
        private AdmAccessToken admToken;
        private AdmAuthentication admAuth;

        public TranslatorApi()
        {
            //Get Client Id and Client Secret from https://datamarket.azure.com/developer/applications/
            //Refer obtaining AccessToken (http://msdn.microsoft.com/en-us/library/hh454950.aspx) 
            //admAuth = new AdmAuthentication("clientID", "client secret");
            admAuth = new AdmAuthentication("client20120605test", "CjCgzuvMr9Ajv8OkEw+1EMe31xawr6a90lbeN5I3taI=");
        }

        public string Translate(string inText)
        {
            string outText = string.Empty;
            string headerValue;
            try
            {
                // アクセストークン取得
                // アクセストークンは10分間有効であるが、当アプリケーションでは簡略化のため考慮せず、
                // 毎回アクセストークンを取得する。
                admToken = admAuth.GetAccessToken();
                // Create a header with the access_token property of the returned token
                headerValue = "Bearer " + admToken.access_token;

                // 翻訳実施
                outText = TranslateMethod(headerValue, inText);
            }
            catch (WebException e)
            {
                throw new ApplicationException(GetErrorMessage(e), e);
            }

            return outText;
        }

        private string TranslateMethod(string authToken, string text)
        {
            string translation = string.Empty;
            string from = "en";
            string to = "ja";

            string uri = "http://api.microsofttranslator.com/v2/Http.svc/Translate?text="
                + System.Web.HttpUtility.UrlEncode(text) + "&from=" + from + "&to=" + to;

            HttpWebRequest httpWebRequest = (HttpWebRequest)WebRequest.Create(uri);
            httpWebRequest.Headers.Add("Authorization", authToken);
            WebResponse response = null;
            try
            {
                response = httpWebRequest.GetResponse();
                using (Stream stream = response.GetResponseStream())
                {
                    System.Runtime.Serialization.DataContractSerializer dcs =
                        new System.Runtime.Serialization.DataContractSerializer(Type.GetType("System.String"));
                    translation = (string)dcs.ReadObject(stream);
                }
            }
            finally
            {
                if (response != null)
                {
                    response.Close();
                    response = null;
                }
            }

            return translation;
        }

        private string GetErrorMessage(WebException e)
        {
            StringBuilder sb = new StringBuilder();
            sb.AppendLine(e.ToString());

            // Obtain detailed error information
            string strResponse = string.Empty;
            using (HttpWebResponse response = (HttpWebResponse)e.Response)
            {
                using (Stream responseStream = response.GetResponseStream())
                {
                    using (StreamReader sr = new StreamReader(responseStream, System.Text.Encoding.ASCII))
                    {
                        strResponse = sr.ReadToEnd();
                    }
                }
            }
            sb.AppendLine("Http status code=" + e.Status + ", error message=" + strResponse);

            return sb.ToString();
        }
    }
}
