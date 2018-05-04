using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace Library.WEB.Config
{
    public class ConnectionCfg
    {
        public string connection = ConfigurationManager.ConnectionStrings["LibraryContext"].ConnectionString;
    }
}