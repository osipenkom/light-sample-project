using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ant.Cargo.Services
{
    public class Configuration
    {
        /// <summary>
        /// Gets the SMS service login.
        /// </summary>
        public static String SmsServiceLogin
        {
            get
            {
                String value = ConfigurationManager.AppSettings["smsServiceLogin"];

                if (!String.IsNullOrEmpty(value))
                {
                    return value;
                }

                throw new Exception("SmsServiceLogin is not defined");
            }
        }

        /// <summary>
        /// Gets the SMS service login.
        /// </summary>
        public static String SmsServicePassword
        {
            get
            {
                String value = ConfigurationManager.AppSettings["smsServicePassword"];

                if (!String.IsNullOrEmpty(value))
                {
                    return value;
                }

                throw new Exception("SmsServicePassword is not defined");
            }
        }

        public static String SmsSender
        {
            get
            {
                String value = ConfigurationManager.AppSettings["SmsSender"];
                return value;
            }
        }
    }
}
