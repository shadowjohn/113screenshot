using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace utility
{
    class myinclude
    {
        public string pwd()
        {
            return System.IO.Path.GetDirectoryName(System.Reflection.Assembly.GetEntryAssembly().Location);
            //return dirname(System.Web.HttpContext.Current.Server.MapPath("~/"));
        }
        public bool is_file(string filepath)
        {
            return File.Exists(filepath);
        }
    }
}
