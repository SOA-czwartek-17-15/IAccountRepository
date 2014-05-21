using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Configuration;

namespace AccountRepository
{
    class Settings
    {
        public static string ServiceRepositoryAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["ServiceRepositoryAddress"];
            }
        }

        public static string AccountServiceAddress
        {
            get
            {
                return ConfigurationManager.AppSettings["AccountServiceAddress"];
            }
        }

        public static double KeepAliveInterval
        {
            get
            {
                return Double.Parse(ConfigurationManager.AppSettings["KeepAliveInterval"]);
            }
        }
    }
}
