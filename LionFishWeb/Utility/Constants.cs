using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;

namespace LionFishWeb.Utility
{
    public static class Constants
    {
        public static string conn => ConfigurationManager.ConnectionStrings["conn"].ConnectionString;
    }
}