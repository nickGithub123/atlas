using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Text;
using System.Collections.Generic;

public class DL_Catalog
{
    public DL_Catalog()
    {
        //
        // TODO: Add constructor logic here
        //
    }

    public static DataTable getUnitsBySearchOption(string clientAccount, String searchText, String showReplacedCode)
    {
        DataTable dtblUnit = new DataTable();
        dtblUnit.Columns.Add("UnitCodeRowID");
        dtblUnit.Columns.Add("UnitCode");
        dtblUnit.Columns.Add("UnitCodeName");
        dtblUnit.Columns.Add("UnitCodeMnemonic");
        dtblUnit.Columns.Add("UnitCodeIsProfile");
        dtblUnit.Columns.Add("Status");
        dtblUnit.Columns.Add("Alias");
        dtblUnit.Columns.Add("UnitAltCode");

        // Get the data 
        Dictionary<String, String> UCData = new Dictionary<String, String>();
        UCData.Add("AccountNo", clientAccount);
        UCData.Add("SearchText", searchText);
        UCData.Add("ShowReplacedCode", showReplacedCode);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        string strDetails = cache.StoredProcedure("?=call SP2_GetUnitCodes(?,?,?)", UCData, 32000).Value.ToString();
        string[] arrLines = strDetails.Split(new char[] { '^' });
        for (int intCount = 0; intCount < arrLines.Length; intCount++)
        {
            string strCols = arrLines[intCount];
            if (strCols.Length == 0)
                continue;
            string[] arrColVals = strCols.Split(new char[] { '|' });

            DataRow drowNewRow = dtblUnit.NewRow();
            drowNewRow["UnitCodeRowID"] = arrColVals[0];
            drowNewRow["UnitCode"] = arrColVals[2];
            drowNewRow["UnitCodeName"] = arrColVals[1];
            drowNewRow["UnitCodeIsProfile"] = arrColVals[4];
            if (arrColVals[5].Length > 1)
            {
                drowNewRow["Status"] = arrColVals[5].Substring(0, 1);
            }
            else
            {
                drowNewRow["Status"] = arrColVals[5];
            }
            drowNewRow["Alias"] = arrColVals[6];
            drowNewRow["UnitCodeMnemonic"] = arrColVals[3];
            drowNewRow["UnitAltCode"] = arrColVals[12];
            dtblUnit.Rows.Add(drowNewRow);
        }
        return dtblUnit;
    }

    public static DataTable getUnitsdetailsBySearchOption(string clientAccount, String searchText, String showReplacedCode)
    {
        DataTable dtblUnit = new DataTable();
        dtblUnit.Columns.Add("UnitCodeRowID");
        dtblUnit.Columns.Add("UnitCode");
        dtblUnit.Columns.Add("UnitCodeName");
        dtblUnit.Columns.Add("UnitCodeMnemonic");
        dtblUnit.Columns.Add("UnitCodeIsProfile");
        dtblUnit.Columns.Add("Status");
        dtblUnit.Columns.Add("Alias");

        dtblUnit.Columns.Add("SpecimenRequirement");
        dtblUnit.Columns.Add("TestSchedule");
        dtblUnit.Columns.Add("Price");
        dtblUnit.Columns.Add("UnitAltCode");
        dtblUnit.Columns.Add("UnitIsNew");

        // Get the data 
        Dictionary<String, String> UCData = new Dictionary<String, String>();
        UCData.Add("AccountNo", clientAccount);
        UCData.Add("SearchText", searchText);
        UCData.Add("ShowReplacedCode", showReplacedCode);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        string strDetails = cache.StoredProcedure("?=call SP2_GetUnitCodes(?,?,?)", UCData, 32000).Value.ToString();
        string[] arrLines = strDetails.Split(new char[] { '^' });
        for (int intCount = 0; intCount < arrLines.Length; intCount++)
        {
            string strCols = arrLines[intCount];

            if (strCols.Length == 0)
                continue;
            string[] arrColVals = strCols.Split(new char[] { '|' });

            DataRow drowNewRow = dtblUnit.NewRow();
            drowNewRow["UnitCodeRowID"] = arrColVals[0];
            drowNewRow["UnitCode"] = arrColVals[2];
            drowNewRow["UnitCodeName"] = arrColVals[1];
            drowNewRow["UnitCodeIsProfile"] = arrColVals[4];
            if (arrColVals[5].Length > 1)
            {
                drowNewRow["Status"] = arrColVals[5].Substring(0,1);
            }
            else
            {
                drowNewRow["Status"] = arrColVals[5];
            }
            
            drowNewRow["Alias"] = arrColVals[6];
            drowNewRow["UnitCodeMnemonic"] = arrColVals[3];

            drowNewRow["SpecimenRequirement"] = arrColVals[7];
            drowNewRow["TestSchedule"] = "Day(s) Test Set-up:  " + arrColVals[8] + "<br>Time of Day:" + arrColVals[9] + " <br>Maximum Lab Time:" + arrColVals[10];
            drowNewRow["Price"] = arrColVals[11];
            drowNewRow["UnitAltCode"] = arrColVals[12];
            drowNewRow["UnitIsNew"] = arrColVals[13];

            dtblUnit.Rows.Add(drowNewRow);
        }
        return dtblUnit;
    }

    public static DataTable getUnitsByCode(String unitCode)
    {
    string selectStatement = "SELECT UC_RowID As UnitCodeRowID,UC_UnitCode As UnitCode,UC_DisplayReportingTitle As UnitCodeName,UC_IsProfile As UnitCodeIsProfile,$$GETALTVALUE^XT27(UC_UnitCode)As UnitAltCode FROM DIC_UnitCode WHERE UC_UnitCode ='" + unitCode + "'";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getUnitsByName(String unitName)
    {
    String selectStatement = "SELECT UC_RowID As UnitCodeRowID,UC_UnitCode As UnitCode,UC_DisplayReportingTitle As UnitCodeName,UC_IsProfile As UnitCodeIsProfile,$$GETALTVALUE^XT27(UC_UnitCode)As UnitAltCode FROM DIC_UnitCode WHERE %SQLUPPER UC_DisplayReportingTitle %STARTSWITH %SQLUPPER '" + unitName + "'";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getUnitsByCombinedIndex(String searchValue)
    {
    String selectStatement = "SELECT UC_RowID As UnitCodeRowID,UC_UnitCode As UnitCode,UC_DisplayReportingTitle As UnitCodeName,UC_IsProfile As UnitCodeIsProfile,$$GETALTVALUE^XT27(UC_UnitCode)As UnitAltCode FROM DIC_UnitCode WHERE UC_SearchField %STARTSWITH '" + searchValue + "'";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getUnitsByCode(String unitCode, String clientCountry)
    {
    String selectStatement = "SELECT UC_RowID As UnitCodeRowID,UC_UnitCode As UnitCode,UC_DisplayReportingTitle As UnitCodeName,UC_IsProfile As UnitCodeIsProfile,UC_Mnemonics As UnitCodeMnemonic,UC_Alias As Alias,{fn SUBSTRING(UC_Status,1,1)} As Status,$$GETALTVALUE^XT27(UC_UnitCode)As UnitAltCode FROM DIC_UnitCode WHERE UC_UnitCode ='" + unitCode + "'";
        if (clientCountry == "US")
        {
            selectStatement += " AND UC_IsUSCode='1'";
        }
        else if (clientCountry == "CANADA")
        {
            selectStatement += " AND UC_IsCanadaCode='1'";
        }
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }
    public static DataTable getUnitsByCode(String unitCode, String clientCountry, int startIndex, int noOfRecords)
    {
    String selectStatement = "SELECT UC_RowID As UnitCodeRowID,UC_UnitCode As UnitCode,UC_DisplayReportingTitle As UnitCodeName,UC_IsProfile As UnitCodeIsProfile,UC_Mnemonics As UnitCodeMnemonic,UC_Alias As Alias,{fn SUBSTRING(UC_Status,1,1)} As Status,$$GETALTVALUE^XT27(UC_UnitCode)As UnitAltCode FROM DIC_UnitCode WHERE UC_UnitCode ='" + unitCode + "'";
        if (clientCountry == "US")
        {
            selectStatement += " AND UC_IsUSCode='1'";
        }
        else if (clientCountry == "CANADA")
        {
            selectStatement += " AND UC_IsCanadaCode='1'";
        }
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement, startIndex, noOfRecords);
    }

    public static DataTable getUnitsByName(String unitName, String clientCountry)
    {
    String selectStatement = "SELECT UC_RowID As UnitCodeRowID,UC_UnitCode As UnitCode,UC_DisplayReportingTitle As UnitCodeName,UC_IsProfile As UnitCodeIsProfile,UC_Mnemonics As UnitCodeMnemonic,UC_Alias As Alias,{fn SUBSTRING(UC_Status,1,1)} As Status,$$GETALTVALUE^XT27(UC_UnitCode)As UnitAltCode FROM SQLUser.DIC_UnitCode WHERE %SQLUPPER UC_DisplayReportingTitle %STARTSWITH %SQLUPPER '" + unitName + "'";
        if (clientCountry == "US")
        {
            selectStatement += " AND UC_IsUSCode='1'";
        }
        else if (clientCountry == "CANADA")
        {
            selectStatement += " AND UC_IsCanadaCode='1'";
        } CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getUnitsByName(String unitName, String clientCountry, int startIndex, int noOfRecords)
    {
    String selectStatement = "SELECT UC_RowID As UnitCodeRowID,UC_UnitCode As UnitCode,UC_DisplayReportingTitle As UnitCodeName,UC_IsProfile As UnitCodeIsProfile,UC_Mnemonics As UnitCodeMnemonic,UC_Alias As Alias,{fn SUBSTRING(UC_Status,1,1)} As Status,$$GETALTVALUE^XT27(UC_UnitCode)As UnitAltCode FROM SQLUser.DIC_UnitCode WHERE %SQLUPPER UC_DisplayReportingTitle %STARTSWITH %SQLUPPER '" + unitName + "'";
        if (clientCountry == "US")
        {
            selectStatement += " AND UC_IsUSCode='1'";
        }
        else if (clientCountry == "CANADA")
        {
            selectStatement += " AND UC_IsCanadaCode='1'";
        } CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement, startIndex, noOfRecords);
    }

    public static DataTable getUnitsByCombinedIndex(String searchValue, String clientCountry)
    {
    String selectStatement = "SELECT UC_RowID As UnitCodeRowID,UC_UnitCode As UnitCode,UC_DisplayReportingTitle As UnitCodeName,UC_IsProfile As UnitCodeIsProfile,UC_Mnemonics As UnitCodeMnemonic,UC_Alias As Alias,{fn SUBSTRING(UC_Status,1,1)} As Status,$$GETALTVALUE^XT27(UC_UnitCode)As UnitAltCode FROM DIC_UnitCode WHERE UC_SearchField %STARTSWITH '" + searchValue + "'";
        if (clientCountry == "US")
        {
            selectStatement += " AND UC_IsUSCode='1'";
        }
        else if (clientCountry == "CANADA")
        {
            selectStatement += " AND UC_IsCanadaCode='1'";
        }
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getUnitsByCombinedIndex(String searchValue, int startIndex, int noOfRecords, String clientCountry)
    {
    String selectStatement = "SELECT UC_RowID As UnitCodeRowID,UC_UnitCode As UnitCode,UC_DisplayReportingTitle As UnitCodeName,UC_IsProfile As UnitCodeIsProfile,UC_Mnemonics As UnitCodeMnemonic,UC_Alias As Alias,{fn SUBSTRING(UC_Status,1,1)} As Status,$$GETALTVALUE^XT27(UC_UnitCode)As UnitAltCode FROM DIC_UnitCode WHERE UC_SearchField %STARTSWITH '" + searchValue + "'";
        if (clientCountry == "US")
        {
            selectStatement += " AND UC_IsUSCode='1'";
        }
        else if (clientCountry == "CANADA")
        {
            selectStatement += " AND UC_IsCanadaCode='1'";
        }
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement, startIndex, noOfRecords);
    }

    private static DataTable getProfilesByCode(String[] searchValues, String AndOrSwitch)
    {
        #region Reference Query
        //SELECT
        //    UC_RowID,
        //    SP_getProfileUnits(UC_RowID) AS ProfileDetails
        //FROM
        //    DIC_UnitCode
        //WHERE
        //    UC_IsProfile='Y'
        //AND
        //    (SP_getProfileUnits(UC_RowID) ='100' OR SP_getProfileUnits(UC_RowID) LIKE '100,%' OR SP_getProfileUnits(UC_RowID) LIKE '%,100' OR SP_getProfileUnits(UC_RowID) LIKE '%,100,%')
        //AND/OR
        //    (SP_getProfileUnits(UC_RowID) ='652' OR SP_getProfileUnits(UC_RowID) LIKE '652,%' OR SP_getProfileUnits(UC_RowID) LIKE '%,652' OR SP_getProfileUnits(UC_RowID) LIKE '%,652,%')
        #endregion Reference Query

        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("UC_RowID As ProfileCode, ");
        sb.Append("UC_DisplayReportingTitle ProfileName ");
        sb.Append("FROM DIC_UnitCode ");
        sb.Append("WHERE ");
        sb.Append("UC_IsProfile='Y' ");
        for (int i = 0; i < searchValues.Length; i++)
        {
            string searchvalue = searchValues[i].Trim();
            if (searchvalue.Length > 0)
            {
                if (i == 0)
                {
                    sb.Append("AND ");
                }
                else
                {
                    sb.Append(AndOrSwitch + " ");
                }
                sb.Append("(SP_getProfileUnits(UC_RowID) ='" + searchvalue + "' ");
                sb.Append("OR ");
                sb.Append("SP_getProfileUnits(UC_RowID) LIKE '" + searchvalue + ",%' ");
                sb.Append("OR ");
                sb.Append("SP_getProfileUnits(UC_RowID) LIKE '%," + searchvalue + "' ");
                sb.Append("OR ");
                sb.Append("SP_getProfileUnits(UC_RowID) LIKE '%," + searchvalue + ",%') ");
            }
        }
        string selectStatement = sb.ToString();
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        DataSet returnDS = cache.FillCacheDataSet(selectStatement);
        if (returnDS.Tables.Count > 0)
        {
            return returnDS.Tables[0];
        }
        else
        {
            return null;
        }
    }

    private static DataTable getProfilesByCode(String[] searchValues, String AndOrSwitch, String clientCountry)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("UC_RowID As ProfileCode, ");
        sb.Append("UC_DisplayReportingTitle ProfileName, ");
        sb.Append("UC_Mnemonics As UnitCodeMnemonic, ");
        sb.Append("UC_Alias As Alias, ");
        sb.Append("{fn SUBSTRING(UC_Status,1,1)} As Status, ");
        sb.Append("$$GETALTVALUE^XT27(UC_UnitCode)As UnitAltCode ");
        sb.Append("FROM DIC_UnitCode ");
        sb.Append("WHERE ");
        sb.Append("UC_IsProfile='Y' ");
        for (int i = 0; i < searchValues.Length; i++)
        {
            string searchvalue = searchValues[i].Trim();
            if (searchvalue.Length > 0)
            {
                if (i == 0)
                {
                    sb.Append("AND ");
                }
                else
                {
                    sb.Append(AndOrSwitch + " ");
                }
                sb.Append("(SP_getProfileUnits(UC_RowID) ='" + searchvalue + "' ");
                sb.Append("OR ");
                sb.Append("SP_getProfileUnits(UC_RowID) LIKE '" + searchvalue + ",%' ");
                sb.Append("OR ");
                sb.Append("SP_getProfileUnits(UC_RowID) LIKE '%," + searchvalue + "' ");
                sb.Append("OR ");
                sb.Append("SP_getProfileUnits(UC_RowID) LIKE '%," + searchvalue + ",%') ");
            }
        }
        String selectStatement = sb.ToString();
        if (clientCountry == "US")
        {
            selectStatement += " AND UC_IsUSCode='1'";
        }
        else if (clientCountry == "CANADA")
        {
            selectStatement += " AND UC_IsCanadaCode='1'";
        }
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    //public static DataTable getProfilesByCodeUsingOR(String[] searchValues)
    //{
    //    return getProfilesByCode(searchValues, "OR");
    //}

    //public static DataTable getProfilesByCodeUsingAND(String[] searchValues)
    //{
    //    return getProfilesByCode(searchValues, "AND");
    //}

    public static DataTable getProfilesByCodeUsingOR(String[] searchValues, String clientCountry)
    {
        return getProfilesByCode(searchValues, "OR", clientCountry);
    }

    public static DataTable getProfilesByCodeUsingAND(String[] searchValues, String clientCountry)
    {
        return getProfilesByCode(searchValues, "AND", clientCountry);
    }

    public static DataTable getDetailedUnitsByCode(String clientID, String unitCode)
    {
        String selectStatement = "SELECT UC_RowID As UnitCodeRowID,UC_UnitCode As UnitCode,UC_DisplayReportingTitle As UnitCodeName,UC_SpecimenRequirements As SpecimenRequirement,'Day(s) Test Set-up: '||UC_DaysTestSetUp||'<br>Time of Day: '||UC_TimeOfDay||'<br>Maximum Lab Time: '||UC_MaxLabTime As TestSchedule,SP_returnPrice('" + clientID + "','" + unitCode + "') As Price FROM DIC_UnitCode WHERE UC_UnitCode ='" + unitCode + "'";
        //String selectStatement = "SELECT UC_SpecimenRequirements, UC_DaysTestSetUp||'\r\n'||UC_TimeOfDay||'\r\n'||UC_MaxLabTime AS TestSchedule, SP_returnPrice('99',UC_UnitCode,'08/04/2007') FROM DIC_UnitCode
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getDetailedUnitsByName(String clientID, String unitName, Int32 startIndex, Int32 noOfRecords)
    {
        String selectStatement = "SELECT UC_RowID As UnitCodeRowID,UC_UnitCode As UnitCode,UC_DisplayReportingTitle As UnitCodeName,UC_SpecimenRequirements As SpecimenRequirement,'Day(s) Test Set-up: '||UC_DaysTestSetUp||'<br>Time of Day: '||UC_TimeOfDay||'<br>Maximum Lab Time: '||UC_MaxLabTime As TestSchedule,SP_returnPrice('" + clientID + "',UC_UnitCode) As Price FROM DIC_UnitCode WHERE %SQLUPPER UC_DisplayReportingTitle %STARTSWITH %SQLUPPER '" + unitName + "'";
        //String selectStatement = "SELECT UC_RowID As UnitCodeRowID,UC_UnitCode As UnitCode,UC_DisplayReportingTitle As UnitCodeName,UC_SpecimenRequirements As SpecimenRequirement,'Test Data - Test Schedule' As TestSchedule,'Test Data - Test Price' As Price FROM DIC_UnitCode WHERE %STRING(UC_DisplayReportingTitle) %STARTSWITH %STRING('" + unitName + "')";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement, startIndex, noOfRecords);
    }

    public static DataTable getUnitsByCombinedIndex(String clientID, String unitName, Int32 startIndex, Int32 noOfRecords)
    {
        String selectStatement = "SELECT UC_RowID As UnitCodeRowID,UC_UnitCode As UnitCode,UC_DisplayReportingTitle As UnitCodeName,UC_SpecimenRequirements As SpecimenRequirement,'Day(s) Test Set-up: '||UC_DaysTestSetUp||'<br>Time of Day: '||UC_TimeOfDay||'<br>Maximum Lab Time: '||UC_MaxLabTime As TestSchedule,SP_returnPrice('" + clientID + "',UC_UnitCode) As Price FROM DIC_UnitCode WHERE UC_SearchField %STARTSWITH '" + unitName + "'";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement, startIndex, noOfRecords);
    }
    
    public static DataTable getDetailedUnitsByCode(String clientID, String unitCode, String clientCountry)
    {
        String selectStatement = "SELECT UC_RowID As UnitCodeRowID,UC_UnitCode As UnitCode,UC_DisplayReportingTitle As UnitCodeName,UC_Mnemonics As UnitCodeMnemonic, UC_SpecimenRequirements As SpecimenRequirement,'Day(s) Test Set-up: '||UC_DaysTestSetUp||'<br>Time of Day: '||UC_TimeOfDay||'<br>Maximum Lab Time: '||UC_MaxLabTime As TestSchedule,SP_returnPrice('" + clientID + "','" + unitCode + "') As Price FROM DIC_UnitCode WHERE UC_UnitCode ='" + unitCode + "'"; //PD Antech 05/12/2011
        if (clientCountry == "US")
        {
            selectStatement += " AND UC_IsUSCode='1'";
        }
        else if (clientCountry == "CANADA")
        {
            selectStatement += " AND UC_IsCanadaCode='1'";
        }
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }
    
    public static DataTable getDetailedUnitsByName(String clientID, String unitName, Int32 startIndex, Int32 noOfRecords, String clientCountry)
    {
        String selectStatement = "SELECT UC_RowID As UnitCodeRowID,UC_UnitCode As UnitCode,UC_DisplayReportingTitle As UnitCodeName,UC_SpecimenRequirements As SpecimenRequirement,'Day(s) Test Set-up: '||UC_DaysTestSetUp||'<br>Time of Day: '||UC_TimeOfDay||'<br>Maximum Lab Time: '||UC_MaxLabTime As TestSchedule,SP_returnPrice('" + clientID + "',UC_UnitCode) As Price,{fn SUBSTRING(UC_Status,1,1)} As Status FROM DIC_UnitCode WHERE %SQLUPPER UC_DisplayReportingTitle %STARTSWITH %SQLUPPER '" + unitName + "'";
        if (clientCountry == "US")
        {
            selectStatement += " AND UC_IsUSCode='1'";
        }
        else if (clientCountry == "CANADA")
        {
            selectStatement += " AND UC_IsCanadaCode='1'";
        }
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement, startIndex, noOfRecords);
    }
    
    public static DataTable getUnitsByCombinedIndex(String clientID, String unitName, Int32 startIndex, Int32 noOfRecords, String clientCountry)
    {
        String selectStatement = "SELECT UC_RowID As UnitCodeRowID,UC_UnitCode As UnitCode,UC_DisplayReportingTitle As UnitCodeName,UC_Mnemonics As UnitCodeMnemonic, UC_SpecimenRequirements As SpecimenRequirement,'Day(s) Test Set-up: '||UC_DaysTestSetUp||'<br>Time of Day: '||UC_TimeOfDay||'<br>Maximum Lab Time: '||UC_MaxLabTime As TestSchedule,SP_returnPrice('" + clientID + "',UC_UnitCode) As Price,{fn SUBSTRING(UC_Status,1,1)} As Status FROM DIC_UnitCode WHERE UC_SearchField %STARTSWITH '" + unitName + "'"; //PD Antech 05/12/2011
        if (clientCountry == "US")
        {
            selectStatement += " AND UC_IsUSCode='1'";
        }
        else if (clientCountry == "CANADA")
        {
            selectStatement += " AND UC_IsCanadaCode='1'";
        }
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement, startIndex, noOfRecords);
    }

}
