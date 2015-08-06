using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace Utility.Extension
{
   public class GeneralUtility
    {
        public static int DayNumberCheck(string day)
        {
            DateTime objTodayDate = System.DateTime.Now;
            for (int i = 0; i < 7; i++)
            {
                if (objTodayDate.DayOfWeek.ToString().ToLower() == day.ToLower())
                {
                    return i;
                }
            }
            return 0;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="readField"></param>
        /// <returns></returns>
        public static string ToStr(object readField)
        {
            if ((readField != null))
            {
                if (readField.GetType() != typeof(DBNull))
                {
                    return Convert.ToString(readField);
                }
            }
            return "";
        }

        /// <summary>
        /// if object is not able to be converted to int, it returns 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int GetInt(object obj)
        {
            string strObj = Convert.ToString(obj);
            int result;
            int.TryParse(strObj, out result);
            return result;
        }

        /// <summary>
        /// if object is not able to be converted to int, it returns 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static double GetDouble(object obj)
        {
            string strObj = Convert.ToString(obj);
            double result;
            double.TryParse(strObj, out result);

            return result;
        }

        /// <summary>
        /// if object is not able to be converted to int, it returns 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static Int64 GetInt64(object obj)
        {
            string strObj = Convert.ToString(obj);
            Int64 result;
            Int64.TryParse(strObj, out result);
            return result;
        }

        /// <summary>
        /// if object is not able to be converted to int, it returns 0
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static int? GetNullInt(object obj)
        {
            string strObj = Convert.ToString(obj);
            int result;
            bool bResult = int.TryParse(strObj, out result);
            int? result1 = new int?();
            return bResult ? result : result1;
        }

        /// <summary>
        /// checks for the string value and converts it into decimal , if empty value then 0
        /// </summary>
        /// <param name="strBool"></param>
        /// <returns></returns>
        public static bool GetBoolean(string strBool)
        {
            if (strBool != string.Empty)
            {
                return Convert.ToBoolean(strBool);
            }
            return false;
        }

        /// <summary>
        /// checks for the string value and converts it into decimal , if empty value then 0
        /// </summary>
        /// <param name="nullBool"></param>
        /// <returns></returns>
        public static bool GetBoolean(bool? nullBool)
        {
            if (nullBool != null)
            {
                return Convert.ToBoolean(nullBool);
            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static decimal GetDecimal(object obj)
        {
            string strObj = Convert.ToString(obj);
            decimal result;
            decimal.TryParse(strObj, out result);
            return result;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static DateTime GetDateTime(object obj)
        {
            string strObj = Convert.ToString(obj);
            DateTime result;
            DateTime.TryParse(strObj, out result);
            return result;
        }


        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public static string GetLocalIPAddress()
        {
            string localIP = "?";
            IPHostEntry host = Dns.GetHostEntry(Dns.GetHostName());
            foreach (IPAddress ip in host.AddressList)
            {
                if (ip.AddressFamily == AddressFamily.InterNetwork)
                {
                    localIP = ip.ToString();
                }
            }
            return localIP;
        }


        public static int GetRound(decimal value)
        {
            decimal roundValue = Math.Round(value, MidpointRounding.AwayFromZero);
            if (roundValue < value)
               return GeneralUtility.GetInt(roundValue + 1);
            return GeneralUtility.GetInt(roundValue);
        }
    }
}
