using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApplication.Business
{
    public class BaseService
    {
        public string GetConnectionString()
        {
            if (ConfigurationManager.ConnectionStrings["ConnectionString"] != null)
            {
                return ConfigurationManager.ConnectionStrings["ConnectionString"].ConnectionString;
            }

            return string.Empty;
        }

    }
}
