using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Annotations
{
    class Program
    {
        static void Main(string[] args)
        {
            var a = new Annotations();
            a.CreateAnnotation("test1", AICategory.Info);
        }
    }
}
