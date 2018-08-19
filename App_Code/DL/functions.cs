using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.HtmlControls;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;

namespace AtlasIndia.AntechCSM.Data
{
    public class DL_functions
    {
        public DL_functions()
        {
            //
        }

        public static DataTable getAntechLabLocations()
        {
            String selectStatement = "SELECT LABLO_RowID As ID,LABLO_LabName As Name, LABLO_DefaultEmailDR As DefaultMailID  FROM DIC_LabLocation WHERE LABLO_IsAntechLab='Y'";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);
        }

        public static DataTable getLabLocationsAddOn()
        {
            String selectStatement = "SELECT LABLO_RowID As ID,LABLO_LabName As Name, LABLO_DefaultEmailDR As DefaultMailID  FROM DIC_LabLocation WHERE LABLO_IsAntechLab='Y' AND LABLO_ProcessesAOV='Y'";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);
        }

        public static DataTable getLabLocationsCI()
        {
            String selectStatement = "SELECT LABLO_RowID As ID,LABLO_LabName As Name FROM DIC_LabLocation WHERE LABLO_IsAntechLab='Y' AND LABLO_ProcessesCI='Y'";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);
        }

        public static DataTable getLabLocations()
        {
            String selectStatement = "SELECT LABLO_RowID As ID,LABLO_LabName As Name FROM DIC_LabLocation";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);
        }
        
        public static DataTable getMessage()
        {
            String selectStatement = "SELECT Distinct MSG_Code AS MSGID, MSG_MessageText As MESSAGE FROM DIC_Message"; 
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);
        }

        public static String getChangeRequestTypes()
        {
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.StoredProcedure("? = CALL SP_getAddOnVerificationTypes()").Value.ToString();
        }

        public static DataTable getSalesTerritory()
        {
            String selectStatement = "SELECT ST_RowID, ST_TerritoryCode As Territory FROM DIC_SalesTerritory ORDER BY ST_TerritoryCode";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);
        }

        public static DataTable getProblemTypes()
        {
            String selectStatement = "SELECT PTYP_ProblemType As Type,PTYP_Description As Description  FROM DIC_ProblemType";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);
        }

        public static DataTable getProblemGroups()
        {
            String selectStatement = "SELECT PTYPG_RowID As Row,PTYPG_GroupName As GroupName  FROM DIC_ProblemTypeGroup";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);
        }

        public static DataTable getDepartments()
        {
            String selectStatement = "SELECT DISTINCT DEPT_Name As Name, DEPT_RowID As ID FROM DIC_Department WHERE DEPT_Name <> '' AND DEPT_IsCSMDepartment = 'Y' ORDER BY DEPT_Name";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);
        }

        public static DataTable getDiscountCodes()
        {
            String selectStatement = "SELECT DC_Code As Value, DC_Text As Text FROM DIC_DiscountCode ORDER By DC_Code ASC";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);
        }

        public static DataTable StringToDataTable(String input, Char rowDeliminiter, Char fieldDeliminiter)
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
                                for (Int32 j = 0; j < fieldCount + 1; j++)
                                {
                                    returnDataTable.Columns.Add();
                                }
                                returnDataTable.Columns[0].DataType = typeof(Int32);
                            }

                            DataRow dr = returnDataTable.NewRow();
                            dr[0] = i;
                            for (Int32 j = 0; j < fieldCount; j++)
                            {
                                dr[j+1] = fields[j];
                            }
                            returnDataTable.Rows.Add(dr);
                        }
                    }
                }
            }
            returnDataTable.AcceptChanges();
            return returnDataTable;
        }

        public static String getILCStatusValues()
        {
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.StoredProcedure("? = CALL SP_getILCStatus()").Value.ToString();
        }

        public static DataTable getILCMessages()
        {
            String selectStatement = "Select ILCMC_RowID As ID, ILCMC_Code As Name, ILCMC_Message As Message FROM DIC_ILCMessage";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);
        }

        public static DataTable getEmail()
        {
            DataTable returnData = new DataTable();
            String selectStatement = "SELECT EMLLO_LocationText AS Email_Location,EMLLO_DefaultLabLocationDR DefaultLabName, EMLLO_RowID AS Email_RowID FROM DIC_EmailLocation";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);            
        }

        public static DataTable getEventCategory()
        {
            String selectStatement = "SELECT EVECT_RowID As ID,EVECT_CategoryName As Name  FROM DIC_EventCategory";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);
        }

        public static DataTable getEditableEventCategory()
        {
            string strSQL = "SELECT EVECT_RowID AS CategoryRowId, EVECT_CategoryName AS CategoryName FROM DIC_EventCategory WHERE EVECT_IsEditable = 'Y'";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(strSQL);
        }

        public static DataTable getSpecimenTypes()
        {
            String selectStatement = "SELECT SPCTP_RowID As RowId, SPCTP_Code As Code,SPCTP_SpecimenType As SpecimenType  FROM DIC_SpecimenType";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);
        }

        public static DataTable getSpeciality()
        {
            String selectStatement = "SELECT SPEC_RowID As RowId, SPEC_Code As SPECCode,SPEC_Name As SPECName,SPEC_IsDefaultOnSearch As IsDefault FROM DIC_Specialty";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);
        }

        public static DataTable getTransferSpeciality()
        {
            String selectStatement = "SELECT SPEC_RowID As RowId, SPEC_Code As SPECCode,SPEC_Name As SPECName FROM DIC_Specialty WHERE SPEC_IsTransferSpecialty='Y'";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);
        }

        public static DataTable getAreaOfInterest()
        {
            String selectStatement = "SELECT INTR_RowID As RowId, INTR_Code As INTRCode,INTR_Description As INTRDescription FROM DIC_Interest";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);
        }

        public static DataTable getClientIssueReasons()
        {
            String selectStatement = "SELECT CIR_RowID As ID,CIR_ReasonText As Name FROM DIC_ClientIssueReason";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);
        }

        /// <summary>
        /// Get callback note types from lookup table
        /// </summary>
        /// <returns></returns>
        public static DataTable getIssueCallbackTypes ()
        {
            String selectStatement = "SELECT CBNT_Note As ID, CBNT_Description As Description FROM DIC_CallbackNoteType";
            CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
            return cache.FillCacheDataTable(selectStatement);
        }
    }
}
