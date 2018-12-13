using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Script.Serialization;

namespace Annotations
{
    public class AnnotationModel
    {
        public string Properties { get; set; }
        public string EventTime { get; set; }
        public Guid Id = Guid.NewGuid();
        public string AnnotationName { get; set; }
        public string Category { get; set; }
        

        
        

        public AnnotationModel(string name, string category) : this(name, category, DateTime.Now)
        { }

        public AnnotationModel(string name, string category, DateTime date)
        {
            this.AnnotationName = name;
            this.Category = category;
            var d = date.ToUniversalTime().GetDateTimeFormats('s')[0];
            EventTime = d;
            
            

            var properties = new Dictionary<string, string>();

            properties.Add("ReleaseName", name);
            //properties.Add("Label", "Info");
            Properties = new JavaScriptSerializer().Serialize(properties);
        }

        private string CreateEventDate(DateTime date)
        {            
            return date.ToUniversalTime().ToString("MMddyyyy_HHmmss");            
        }

    }
}
