using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;

namespace AtlasIndia.AntechCSM.UI
{
    /// <summary>
    /// Generalized Methods for UI
    /// </summary>
    public class UIfunctions
    {
        #region Constructors

        public UIfunctions()
        {
            //
        }

        #endregion Constructors

        #region Methods

        public static String EncryptString(string inputString)
        {
            string outputString;
            //// To Do: Encryption Logic Here
            outputString = inputString;
            return outputString;
        }

        public static String getDateTimeFormat()
        {
            return System.Configuration.ConfigurationManager.AppSettings["DateTimeFormat"];
        }

        public static String getDateFormat()
        {
            return System.Configuration.ConfigurationManager.AppSettings["DateFormat"];
        }

        public static String getTimeFormat()
        {
            return System.Configuration.ConfigurationManager.AppSettings["TimeFormat"];
        }

        public static string combineDateTime(string date, string time)
        {
            String dateTimeFormat = AtlasIndia.AntechCSM.UI.UIfunctions.getDateTimeFormat();
            DateTime? returnDateTime = new DateTime();
            returnDateTime = AtlasIndia.AntechCSM.functions.AddTimeToDateNullable(date, time);
            return (returnDateTime.HasValue) ? returnDateTime.Value.ToString(dateTimeFormat) : String.Empty;
        }

        public static string cutomizeDate(string date)
        {
            String dateFormat = AtlasIndia.AntechCSM.UI.UIfunctions.getDateFormat();
            DateTime returnDate;
            String returnString = date;
            if (DateTime.TryParse(date, out returnDate))
            {
                returnString = returnDate.ToString(dateFormat);
            }
            return returnString;
        }

        public static string cutomizeTime(string time)
        {
            String timeFormat = AtlasIndia.AntechCSM.UI.UIfunctions.getTimeFormat();
            DateTime returnTime;
            String returnString = time;
            if (DateTime.TryParse(time, out returnTime))
            {
                returnString = returnTime.ToString(timeFormat);
            }
            return returnString;
        }

        public static string concatStringUsingSymbol(string str1, string str2,string symbol)
        {
            if (str2.Length > 0)
            {
                return str1 + symbol + str2;
            }
            return str1;
        }

        public static String trimmer(String originalString, Int32 length)
        {
            if (originalString.Length > length)
            {
                return originalString.Substring(0, length - 1) + "...";
            }
            return originalString;
        }

        /// <summary>
        /// To pass the encoded value in Java Script
        /// </summary>
        /// <param name="input"></param>
        /// <returns></returns>
        public static String HTMLEncodeExtended(String input)
        {
            //// Encode Slashes
            input = input.Replace("\\", @"\x5C");
            //// Encode Apostrophes
            input = input.Replace("\'", @"\x27");
            //// Encode Double Quotes
            input = input.Replace("\"", @"\x22");
            //// Normal HTML Encode
            input = HttpUtility.HtmlEncode(input);
            return input;
        }

        public static DataTable getDetailedUnits(String searchValue, String searchKey, Int32 startIndex, Int32 noOfRecords)
        {
            Catalog.SearchOption key;
            switch (searchKey)
            {
                case "UNIT_CODE":
                    key = Catalog.SearchOption.UNIT_CODE;
                    break;
                case "UNIT_NAME":
                    key = Catalog.SearchOption.UNIT_NAME;
                    break;
                case "COMBINED":
                    key = Catalog.SearchOption.COMBINED;
                    break;
                default:
                    key = Catalog.SearchOption.COMBINED;
                    break;
            }
            return Catalog.getDetailedUnits(SessionHelper.ClientContext, searchValue, key, startIndex, noOfRecords);
        }

        public static DataTable getDetailedUnits(String searchValue, String searchKey, Int32 startIndex, Int32 noOfRecords, String clientCountry)
        {
            Catalog.SearchOption key;
            switch (searchKey)
            {
                case "UNIT_CODE":
                    key = Catalog.SearchOption.UNIT_CODE;
                    break;
                case "UNIT_NAME":
                    key = Catalog.SearchOption.UNIT_NAME;
                    break;
                case "COMBINED":
                    key = Catalog.SearchOption.COMBINED;
                    break;
                default:
                    key = Catalog.SearchOption.COMBINED;
                    break;
            }
            return Catalog.getDetailedUnits(SessionHelper.ClientContext, searchValue, key, startIndex, noOfRecords, clientCountry);
        }

        public static DataTable getDetailedUnits(String searchValue, String showReplacedCode)
        {
            return Catalog.getDetailedUnits(SessionHelper.ClientContext, searchValue, showReplacedCode);
        }

        public static Int32 getDefaultTimeout(String PageName)
        {
            String key = "TIMEOUT_" + PageName; //e.g. - "TIMEOUT_" + AccessionSearch = TIMEOUT_AccessionSearch
            Int32 returnVal = 999999; // default value should be very high to avoid the reset search in case the timeout is not available
            if (System.Configuration.ConfigurationManager.AppSettings[key] != null)
            {
                Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings[key], out returnVal);
            }
            return returnVal;
        }

        /// <summary>
        /// This function returns the HTML to mark the client as hot.
        /// </summary>
        /// <returns>HTML to mark the client as hot</returns>
        public static string GetHotClientIndicatorHTML()
        {
            string strText = System.Configuration.ConfigurationManager.AppSettings["HotClientIndicatorText"];
            return "<div class=\"RCBorder80\"><b class=\"RCBlock\"><b class=\"RCL1 REDRCL1\"></b><b class=\"RCL2 REDRCL2\"></b><b class=\"RCL3 REDRCL3\"></b><b class=\"RCL4 REDRCL4\"></b></b><div class=\"RCContent REDRCContent\"><b class=\"RCL5 REDRCL5 Font10\">" + strText + "</b></div><b class=\"RCBlock\"><b class=\"RCL4 REDRCL4\"></b><b class=\"RCL3 REDRCL3\"></b><b class=\"RCL2 REDRCL2\"></b><b class=\"RCL1 REDRCL1\"></b></b></div>";
        }

        /// <summary>
        /// This function returns the HTML to mark the client as Ali.
        /// </summary>
        /// <returns>HTML to mark the client as hot</returns>
        public static string GetAliClientIndicatorHTML()
        {
            string strText = System.Configuration.ConfigurationManager.AppSettings["AliClientIndicatorText"];
            return "<div class=\"RCBorder80\"><b class=\"RCBlock\"><b class=\"RCL1 REDRCL1\"></b><b class=\"RCL2 REDRCL2\"></b><b class=\"RCL3 REDRCL3\"></b><b class=\"RCL4 REDRCL4\"></b></b><div class=\"RCContent REDRCContent\"><b class=\"RCL5 REDRCL5 Font10\">" + strText + "</b></div><b class=\"RCBlock\"><b class=\"RCL4 REDRCL4\"></b><b class=\"RCL3 REDRCL3\"></b><b class=\"RCL2 REDRCL2\"></b><b class=\"RCL1 REDRCL1\"></b></b></div>";
        }

        /// <summary>
        /// This function returns the HTML to mark the client as New.
        /// </summary>
        /// <returns>HTML to mark the client as New</returns>
        public static string GetNewClientIndicatorHTML()
        {
            string strText = System.Configuration.ConfigurationManager.AppSettings["NewClientIndicatorText"];
            return "<div class=\"RCBorder80\"><b class=\"RCBlock\"><b class=\"RCL1 REDRCL1\"></b><b class=\"RCL2 REDRCL2\"></b><b class=\"RCL3 REDRCL3\"></b><b class=\"RCL4 REDRCL4\"></b></b><div class=\"RCContent REDRCContent\"><b class=\"RCL5 REDRCL5 Font10\">" + strText + "</b></div><b class=\"RCBlock\"><b class=\"RCL4 REDRCL4\"></b><b class=\"RCL3 REDRCL3\"></b><b class=\"RCL2 REDRCL2\"></b><b class=\"RCL1 REDRCL1\"></b></b></div>";
        }

        /// <summary>
        /// This function returns the copyright text
        /// </summary>
        /// <returns>copyright text</returns>
        public static string GetCopyrightText()
        {
            return "&copy; " + DateTime.Now.Year + " Atlas Development Corporation";
        }
        public static int getNoofEmailstoFetch()
        {
            Int32 returnValue = 500;
            if (System.Configuration.ConfigurationManager.AppSettings["EMAIL_BATCH_COUNT"] != null)
            {
                Int32.TryParse(System.Configuration.ConfigurationManager.AppSettings["EMAIL_BATCH_COUNT"], out returnValue);
            }
            return returnValue;
        }

        public static string GetServerLocation()
        {
            if (System.Configuration.ConfigurationManager.AppSettings["SERVER_LOCATION"] != null)
            {
                return System.Configuration.ConfigurationManager.AppSettings["SERVER_LOCATION"];
            }
            return "";
        }

        public static string GetServerLocationText()
        {
            String strServerLocation = GetServerLocation();
            String strLocation = "";
            if (strServerLocation.ToUpper() == "E")
            {
                strLocation = "EAST";
            }
            else if (strServerLocation.ToUpper() == "W")
            {
                strLocation = "WEST";
            }
            return strLocation;
        }

        /// <summary>
        /// Gets left pad count for revenue blob
        /// </summary>
        /// <param name="originalValueLength">Length of original value</param>
        /// <returns>Integer left pad value</returns>
        public static int GetRevenueTextLeftPadValue(int originalValueLength)
        {
            if(originalValueLength>14 && originalValueLength<0)
            {
                return 0;
            }
            return (14 - originalValueLength);
        }

        /// <summary>
        /// Returns revenue values of three months in array of string
        /// </summary>
        /// <param name="firstMonthRevenue">First month revenue</param>
        /// <param name="secondMonthRevenue">Second month revenue</param>
        /// <param name="thirdMonthRevenue">Third month revenue</param>
        /// <returns>Array of revenues</returns>
        public static string[] GetRevenueDetails(string firstMonthRevenue, string secondMonthRevenue, string thirdMonthRevenue)
        {
            string[] arrDetails = new string[3];
            arrDetails[0] = string.Empty;
            arrDetails[1] = string.Empty;
            arrDetails[2] = string.Empty;
            string revenueDetails = string.Empty;

            revenueDetails = Convert.ToDecimal(firstMonthRevenue).ToString("0,0.00");
            revenueDetails = "$" + revenueDetails.PadLeft(GetRevenueTextLeftPadValue(revenueDetails.Length), ' ');
            arrDetails[0] = revenueDetails;

            revenueDetails = Convert.ToDecimal(secondMonthRevenue).ToString("0,0.00");
            revenueDetails = "$" + revenueDetails.PadLeft(GetRevenueTextLeftPadValue(revenueDetails.Length), ' ');
            arrDetails[1] = revenueDetails;

            revenueDetails = Convert.ToDecimal(thirdMonthRevenue).ToString("0,0.00");
            revenueDetails = "$" + revenueDetails.PadLeft(GetRevenueTextLeftPadValue(revenueDetails.Length), ' ');
            arrDetails[2] = revenueDetails;

            return arrDetails;
        }

        #endregion Methods
    }
}