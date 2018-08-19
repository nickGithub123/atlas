using System;
using System.Data;
using System.Configuration;
using AtlasIndia.AntechCSM.Data;

namespace AtlasIndia.AntechCSM
{
    /// <summary>
    /// Generalized Methods for Business Layer
    /// </summary>
    public static class functions
    {
        #region Constructors

        static functions()
        {
            //
        }

        #endregion Constructors

        #region Methods

        public static DataTable SearchForProfiles(String searchValue, String searchKey, String clientCountry)
        {
            Catalog.AndOrSearch key;
            switch (searchKey)
            {
                case "AND":
                    key = Catalog.AndOrSearch.AND;
                    break;
                case "OR":
                    key = Catalog.AndOrSearch.OR;
                    break;
                default:
                    key = Catalog.AndOrSearch.AND;
                    break;
            }
            String[] searchValues = searchValue.Split(new char[1] { ',' });
            return Catalog.getProfiles(searchValues, key, clientCountry);
        }

        public static DateTime AddTimeToDate(String date, String time)
        {
            DateTime tmpDate;
            DateTime tmpTime;
            if (DateTime.TryParse(date, out tmpDate) == false)
            {
                tmpDate = DateTime.MinValue;
            }
            if (DateTime.TryParse(time, out tmpTime) == false)
            {
                tmpTime = DateTime.MinValue;
            }
            tmpDate = tmpDate.AddHours(tmpTime.Hour);
            tmpDate = tmpDate.AddMinutes(tmpTime.Minute);
            tmpDate = tmpDate.AddSeconds(tmpTime.Second);
            tmpDate = tmpDate.AddMilliseconds(tmpTime.Millisecond);
            return tmpDate;
        }

        public static DateTime? AddTimeToDateNullable(String date, String time)
        {
            DateTime tmpDate;
            DateTime tmpTime;
            if (DateTime.TryParse(date, out tmpDate) == false)
            {
                return null;
            }
            if (DateTime.TryParse(time, out tmpTime) == false)
            {
                tmpTime = DateTime.MinValue;
            }
            tmpDate = tmpDate.AddHours(tmpTime.Hour);
            tmpDate = tmpDate.AddMinutes(tmpTime.Minute);
            tmpDate = tmpDate.AddSeconds(tmpTime.Second);
            tmpDate = tmpDate.AddMilliseconds(tmpTime.Millisecond);
            return tmpDate;
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

        public static DataTable getLabLocations()
        {
            return DL_functions.getAntechLabLocations();
        }

        public static DataTable getClientIssueReasons()
        {
            return DL_functions.getClientIssueReasons();
        }

        public static DataTable getLabLocationsAddOn()
        {
            return DL_functions.getLabLocationsAddOn();
        }

        public static DataTable getLabLocationsCI()
        {
            return DL_functions.getLabLocationsCI();
        }

        public static DataTable getMessage()
        {
            return DL_functions.getMessage();
        }

        public static DataTable getAddOnVerificationRequestType()
        {
            DataTable returnDataTable = new DataTable();
            String addOnVerificationTypes = DL_functions.getChangeRequestTypes();
            if (addOnVerificationTypes.Length > 0)
            {
                String[] lists = addOnVerificationTypes.Split('^');
                if (lists.Length == 2)
                {
                    String[] keys = lists[0].Split(',');
                    String[] values = lists[1].Split(',');
                    if (keys.Length > 0 && keys.Length == values.Length)
                    {
                        returnDataTable.Columns.Add("DisplayList");
                        returnDataTable.Columns.Add("ValueList");
                        for (int i = 0; i < keys.Length; i++)
                        {
                            DataRow dr = returnDataTable.NewRow();
                            dr[0] = keys[i];
                            dr[1] = values[i];
                            returnDataTable.Rows.Add(dr);
                        }
                    }
                }
            }
            returnDataTable.AcceptChanges();
            return returnDataTable;
        }

        public static DataTable getSalesTerritory()
        {
            return DL_functions.getSalesTerritory();
        }

        public static DataTable getProblemTypes()
        {
            return DL_functions.getProblemTypes();
        }

        public static DataTable getProblemGroups()
        {
            return DL_functions.getProblemGroups();
        }

        public static DataTable getDepartments()
        {
            return DL_functions.getDepartments();
        }

        public static DataTable getDiscountCodes()
        {
            return DL_functions.getDiscountCodes();
        }

        public static DataTable StringToDataTable(String input, Char rowDeliminiter, Char fieldDeliminiter, Char fieldIndex)
        {
            DataTable returnDataTable = new DataTable();
            if (input.Length > 0)
            {
                String[] rows = input.Split(rowDeliminiter);
                Int32 rowsCount = rows.Length;
                Int32 fieldCount;
                if (rowsCount > 0)
                {
                    for (Int32 i = 0; i < rowsCount; i++)
                    {
                        String[] fields = rows[i].Split(fieldDeliminiter);
                        fieldCount = fields.Length;
                        if (fieldCount > 0)
                        {
                            if (i == 0)
                            {
                                for (Int32 j = 0; j < fieldCount; j++)
                                {
                                    returnDataTable.Columns.Add();
                                }
                            }

                            DataRow dr = returnDataTable.NewRow();
                            for (Int32 j = 0; j < fieldCount; j++)
                            {
                                String[] values = fields[j].Split(fieldIndex);
                                if (values.Length > 0)
                                {
                                    dr[j] = values[values.Length - 1];
                                }
                            }
                            returnDataTable.Rows.Add(dr);
                        }
                    }
                }
            }
            returnDataTable.AcceptChanges();
            return returnDataTable;
        }

        public static String AddTimeToDateString(String date, String time)
        {
            String returnValue;
            DateTime tmpDateTime;
            if (DateTime.TryParse(date, out tmpDateTime) == false)
            {
                returnValue = date.ToString();
            }
            else
            {
                returnValue = tmpDateTime.ToString(AtlasIndia.AntechCSM.functions.getDateFormat());
            }

            if (DateTime.TryParse(time, out tmpDateTime) == false)
            {
                returnValue = returnValue + " " + time.ToString();
            }
            else
            {
                returnValue = returnValue + " " + tmpDateTime.ToString(AtlasIndia.AntechCSM.functions.getTimeFormat());
            }
            return returnValue.Trim();
        }

        public static DataTable getILCStatusValues()
        {
            DataTable returnDataTable = new DataTable();
            String statusValues = DL_functions.getILCStatusValues();
            if (statusValues.Length > 0)
            {
                String[] lists = statusValues.Split('^');
                if (lists.Length == 2)
                {
                    String[] keys = lists[0].Split(',');
                    String[] values = lists[1].Split(',');
                    if (keys.Length > 0 && keys.Length == values.Length)
                    {
                        returnDataTable.Columns.Add("DisplayList");
                        returnDataTable.Columns.Add("ValueList");
                        for (int i = 0; i < keys.Length; i++)
                        {
                            DataRow dr = returnDataTable.NewRow();
                            dr[0] = keys[i];
                            dr[1] = values[i];
                            returnDataTable.Rows.Add(dr);
                        }
                    }
                }
            }
            returnDataTable.AcceptChanges();
            return returnDataTable;
        }

        public static DataTable getILCMessages()
        {
            return DL_functions.getILCMessages();
        }

        public static DataTable getEmail()
        {
            return DL_functions.getEmail();
        }
        public static DataTable getEventCategory()
        {
            return DL_functions.getEventCategory();
        }
        public static DataTable getEditableEventCategory()
        {
            return DL_functions.getEditableEventCategory();
        }

        public static DataTable getSpecimenTypes()
        {
            return DL_functions.getSpecimenTypes();
        }
        
        public static DataTable getSpeciality()
        {
            return DL_functions.getSpeciality();
        }

        public static DataTable getTransferSpeciality()
        {
            return DL_functions.getTransferSpeciality();
        }
        
        public static DataTable getAreaOfInterest()
        {
            return DL_functions.getAreaOfInterest();
        }

        public static String getSupplyOrderPrefix()
        {
            string strSupplyPrefix = "";
            if (System.Configuration.ConfigurationManager.AppSettings["ANTECH_SUPPLY_PREFIX"] != null)
            {
                strSupplyPrefix = System.Configuration.ConfigurationManager.AppSettings["ANTECH_SUPPLY_PREFIX"].ToString();
            }

            return strSupplyPrefix;
        }

        /// <summary>
        /// Get Callback types from lookup table
        /// </summary>
        /// <returns></returns>
        public static DataTable getIssueCallbackTypes ()
        {
            return DL_functions.getIssueCallbackTypes();
        }
        #endregion Methods
    }
}