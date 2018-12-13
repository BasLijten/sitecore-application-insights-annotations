using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;
using System.Configuration;

namespace Annotations
{
    public class Annotations
    {
        private const string fwlink = "http://go.microsoft.com/fwlink/?prd=11901&pver=1.0&sbp=Application%20Insights&plcid=0x409&clcid=0x409&ar=Annotations&sar=Create%20Annotation";
        private string url = "{0}/applications/{1}/Annotations?api-version=2015-11";
        private string applicationId;
        private string apiKey;
        public Annotations()
        {            
            applicationId = ConfigurationManager.AppSettings["AI.ApplicationId"];
            apiKey = ConfigurationManager.AppSettings["AI.ApiKey"];
        }

        public bool CreateAnnotation(string v1, AICategory category)
        {
            var result = false;
            var response = GetHost(fwlink);

            string v2 = GetCategoryFromEnum(category);
            
            var body = CreateBody(v1, v2);

            var request = CreateRequest(response.OriginalString, body);
            
            try
            {
                HttpWebResponse httpWebReponse = request.GetResponse() as HttpWebResponse;
                
            }
            catch(Exception ex)
            {
                string b = "b";
            }

            return result;
        }

        private string GetCategoryFromEnum(AICategory category)
        {
            string val = String.Empty;
            switch(category)
            {
                case AICategory.Canceled:                    
                        val = "Canceled";
                        break;
                case AICategory.Critical:
                    val = "Critical";
                    break;
                case AICategory.Disabled:
                    val = "Disabled";
                    break;
                case AICategory.Error:
                    val = "Error";
                    break;
                case AICategory.Failed:
                    val = "Failed";
                    break;
                case AICategory.Info:
                    val = "Info";
                    break;
                case AICategory.None:
                    val = "None";
                    break;
                case AICategory.Pending:
                    val = "Pending";
                    break;
                case AICategory.Stopped:
                    val = "Stopped";
                    break;
                case AICategory.Success:
                    val = "Success";
                    break;
                case AICategory.Unknown:
                    val = "Unknown";
                    break;
                case AICategory.Warning:
                    val = "Warning";
                    break;
                case AICategory.Deployment:
                    val = "Deployment";
                    break;
                default:
                    val = "None";
                    break;
            }

            return val;
        }

        private HttpWebRequest CreateRequest(string originalString, byte[] body)
        {
            var requestUrl = String.Format(url, originalString, applicationId);
            HttpWebRequest request = HttpWebRequest.Create(requestUrl) as HttpWebRequest;
            request.Method = "PUT";

            using (Stream stream = request.GetRequestStream())
            {
                stream.Write(body, 0, body.Length);
            }

            request.ContentType = "application/json; charset=utf-8";

            request.Headers.Add("X-AIAPIKEY", apiKey);
            request.UserAgent = "Mozilla/5.0 (Windows NT; Windows NT 10.0; en-US) WindowsPowerShell/5.1.17134.407";
            request.ServicePoint.Expect100Continue = false;
            

            return request;
        }

        private byte[] CreateBody(string v1, string v2)
        {
            var model = new AnnotationModel(v1, v2);
            var json = new JavaScriptSerializer().Serialize(model);

            var body = System.Text.Encoding.UTF8.GetBytes(json);
            return body;            
        }

        private Uri GetHost(string url)
        {
            Uri uri = null;
            var request = HttpWebRequest.Create(url) as HttpWebRequest;
            request.AllowAutoRedirect = true;

            try
            {
                HttpWebResponse response = request.GetResponse() as HttpWebResponse;
            }
            catch (WebException wex)
            {
                uri = wex.Response.ResponseUri;                                
            }

            return uri;
        }       
    }

}
