using System;
using System.Data;
using System.Configuration;

public class DL_Unit
{
    public DL_Unit()
    {
    }
    #region Unused code
    /*
    public static DataTable getUnitCodesForProfile(String profileRowID)
    {
        // UCPC_ComponentUnitCodeDR as fkUnitCodeRowID                  = fk Profile Code Row ID
        // UCPC_ComponentUnitCodeDR->UC_RowID1 as UnitCodeRowID         = Unit Code Row ID
        // UCPC_ComponentUnitCodeDR->UC_UnitCode As UnitCode            = Unit Code
        // UCPC_ComponentUnitCodeDR->UC_DisplayReportingTitle As UnitCodeName  = Unit Code Reporting Title
        // UCPC_UC_ParRef                                               = fk Unit Code Row ID

        String selectStatement = "SELECT UCPC_ComponentUnitCodeDR as fkUnitCodeRowID,UCPC_ComponentUnitCodeDR->UC_UnitCode as UnitCodeRowID,UCPC_ComponentUnitCodeDR->UC_UnitCode As UnitCode,UCPC_ComponentUnitCodeDR->UC_DisplayReportingTitle As UnitCodeName,UCPC_ComponentUnitCodeDR->UC_Mnemonics As UnitCodeMnemonic,UCPC_ComponentUnitCodeDR->UC_Alias As Alias FROM DIC_UnitCodeProfileComponent WHERE UCPC_UC_ParRef = '" + profileRowID + "'";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }*/
    #endregion Unused code
    public static DataTable getUnitCodesForProfile(String profileRowID, String clientCountry)
    {
    String selectStatement = "SELECT UCPC_ComponentUnitCodeDR as fkUnitCodeRowID,UCPC_ComponentUnitCodeDR->UC_UnitCode as UnitCodeRowID,UCPC_ComponentUnitCodeDR->UC_UnitCode As UnitCode,UCPC_ComponentUnitCodeDR->UC_DisplayReportingTitle As UnitCodeName,UCPC_ComponentUnitCodeDR->UC_Mnemonics As UnitCodeMnemonic,UCPC_ComponentUnitCodeDR->UC_Alias As Alias,{fn SUBSTRING(UCPC_ComponentUnitCodeDR->UC_Status,1,1)} As Status,$$GETALTVALUE^XT27(UCPC_ComponentUnitCodeDR->UC_UnitCode)As UnitAltCode FROM DIC_UnitCodeProfileComponent WHERE UCPC_UC_ParRef = '" + profileRowID + "'";
        if (clientCountry == "US")
        {
            selectStatement += " AND UCPC_ComponentUnitCodeDR->UC_IsUSCode='1'";
        }
        else if (clientCountry == "CANADA")
        {
            selectStatement += " AND UCPC_ComponentUnitCodeDR->UC_IsCanadaCode='1'";
        }
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getUnitDetails(String unitCode)
    {
        String selectStatement = "SELECT UC_Mnemonics As UnitCodeMnemonic, UC_DisplayReportingTitle As ReportingTitle, UC_Alias As Alias, UC_SpecimentType As SpecimenType, UC_Worklist As Worklist, UC_TestCodes As TestCodes, UC_ReferralToNode As ReferredToNode, UC_ReferredtoName As ReferredToName, UC_ReferredToAddress As ReferredToAddress, UC_ReferenceLabID As ReferenceLabID, UC_PathologistReview As PathologistReview, UC_SpecimenRequirements As SpecimenRequirements, UC_ContainerType As ContainerType, UC_OtherAcceptableSpecReq As AcceptableSpecimenRequirement, UC_TransportTemp As TransportTemperature, UC_MinimumSpecimenVolume As MinimumSpecimenVolume, UC_LabArea As LabArea, UC_AnalyticTime As AnalyticTime, UC_DaysTestSetUp As DaysTestSetUp, UC_TimeOfDay As TimeOfDay, UC_MaxLabTime As MaxLabTime, UC_Methodology As Methodology, UC_RefRangeAndUnitofMeas As ReferenceRangeAndUOM, UC_PanicValues As PanicValues, UC_SpecimenStability As SpecimenStability, UC_rejectDuetoHemolysis As RejectDuetoHemolysis, UC_RejectDuetoLipemia As RejectDuetoLipemia, UC_RejectDuetoThawingOranyotherRejection As RejectDueToThawing, UC_RoutineInstructionsForInquiry As RoutineInstructionsForInquiry, UC_SpecialInstructForDrawList As SpecialInstructForDrawList, UC_SpecimenRetention As SpecimenRetention, UC_ClinicalSignificance As ClinicalSignificance, UC_PrincOfAssayAndJournRef As PrincOfAssayAndJournRef, UC_CPTCode As CPTCode, UC_FEE As Fee, UC_EtiologicAgent As EtiologicAgent, UC_CapWorkUnits As CapWorkUnits, UC_NumberOfCollectLabels As NumberOfCollectionLabels, UC_AlwaysMessage As AlwaysMessageCode, UC_AlwaysMessage->MSG_MessageText As AlwaysMessage, UC_ReportSequenceForReport As ReportSequenceForReport, UC_Category As Category, UC_AlwaysMessageEffectiveDate As AlwaysMessageEffectiveDate,UC_Status As Status,UC_OrderEntryNoteOE As OrderEntryNoteOE FROM DIC_UnitCode where UC_UnitCode = '" + unitCode + "'";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }
    #region Unused code
    /*
    public static DataTable getTests_Original(String accessionNumber, String workListCode, DateTime effectiveDate, String SpeciesSex)
    {
        #region Reference Query

        //SELECT
        //WLTC_TestCodeDR->TC_ReportingName,
        //WLTC_TestCodeDR->TC_UnitOfMeasure,
        //ARPTV_ResultValue,
        //ARPTV_CriticalityFlag
        //from ORD_WorklistHistory,dic_worklisttestcode,
        //(SELECT ARPTV_ARPT_ParRef->ARPT_ACC_ParRef->ACC_Accession ARPTV_ACCESSION,ARPTV_ARPT_ParRef->ARPT_UnitCodeWorklist as ARPTV_WORKLISTID ,ARPTV_ChildSub,ARPTV_ResultValue,ARPTV_CriticalityFlag
        //FROM SQLUser.ORD_AccessionReportResultValue
        //WHERE ARPTV_ARPT_ParRef->ARPT_ACC_ParRef->ACC_Accession = 'A00010') A
        //where WLH_RowID=WLTC_WL_ParRef
        //and WLH_WorklistID='CBC'
        //and to_date(WLH_EffectiveDate,'YYYY-MM-DD')=to_date('42716','YYYY-MM-DD')
        //and WLH_WorklistID = ARPTV_WORKLISTID and WLTC_ChildSub=ARPTV_ChildSub

        #endregion Reference Query

        //String speciesR2 = SpeciesSex + "R2";

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("TAB1.TestRowID As TestRowID, ");
        sb.Append("TAB1.ReportingName As TestReportingName, ");
        sb.Append("TAB1.UnitOfMeasure As TestUnitOfMeasure, ");
        sb.Append("TAB1.Criticality As TestCriticalityFlag, ");
        sb.Append("TAB1.ResultValue As TestResultValue, ");
        sb.Append("TAB1.Message As ResultNotes, ");
        sb.Append("TAB2.ReferenceRange As NormalRange ");
        sb.Append("FROM ");
        sb.Append("(SELECT ");
        sb.Append("ARPTV_ChildSub AS ChildSub, ");
        sb.Append("WLHRL_ResultTestCodeDR->TC_RowID AS TestRowID, ");
        sb.Append("WLHRL_ResultTestCodeDR->TC_ReportingName AS ReportingName, ");
        sb.Append("WLHRL_ResultTestCodeDR->TC_UnitOfMeasure AS UnitOfMeasure, ");
        sb.Append("ARPTV_ResultValue As ResultValue, ");
        sb.Append("ARPTV_CriticalityFlag As Criticality, ");
        //sb.Append("ARPTN_MessageDR->MSG_MessageText As Message ");
        sb.Append("ARPTN_ReportNotesText As Message ");
        sb.Append("FROM ");
        sb.Append("( ");
        sb.Append("ORD_AccessionReportResultValue left outer join ORD_AccessionReportResultNote ");
        sb.Append("on ARPTV_ARPT_ParRef = ARPTN_ARPT_ParRef and ARPTV_ChildSub=ARPTN_ChildSub ");
        sb.Append("), ORD_WorklistHistoryResultList ");
        sb.Append("WHERE ");
        sb.Append("ARPTV_ARPT_ParRef->ARPT_ACC_ParRef->ACC_Accession = '" + accessionNumber + "' ");
        sb.Append("and ARPTV_ChildSub = WLHRL_ChildSub ");
        sb.Append("and to_date(WLHRL_WLH_ParRef->WLH_EffectiveDate,'YYYY-MM-DD') ='" + effectiveDate.ToString("yyyy-MM-dd") + "' ");
        sb.Append("and WLHRL_WLH_ParRef->WLH_WorklistID ='" + workListCode + "' ");
        sb.Append("and WLHRL_WLH_ParRef->WLH_WorklistID = ARPTV_ARPT_ParRef->ARPT_UnitCodeWorklist) ");
        sb.Append("As TAB1 ");
        sb.Append("left outer join ");
        sb.Append("(SELECT ");
        sb.Append("WLHRR_ChildSub AS ChildSub, ");
        sb.Append("WLHRR_ReferenceRangeToDisplay As ReferenceRange ");
        sb.Append("FROM ");
        sb.Append("ORD_WorklistHistoryReferenceRangeList ");
        sb.Append("WHERE ");
        sb.Append("WLHRR_WLH_ParRef->WLH_WorklistID='" + workListCode + "' ");
        sb.Append("and WLHRR_Species='" + SpeciesSex + "' ");
        sb.Append("and to_date(WLHRR_WLH_ParRef->WLH_EffectiveDate,'YYYY-MM-DD')='" + effectiveDate.ToString("yyyy-MM-dd") + "') ");
        sb.Append("As TAB2 ");
        sb.Append("on TAB1.ChildSub=TAB2.ChildSub ");

        String selectStatement = sb.ToString();
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }
    
    public static DataTable getTestsWithoutMumps(String accessionNumber, String workListCode, DateTime effectiveDate, String SpeciesSex)
    {
        //String speciesR2 = SpeciesSex + "R2";
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("TAB1.TestRowID As TestRowID, ");
        sb.Append("TAB1.ReportingName As TestReportingName, ");
        sb.Append("TAB1.UnitOfMeasure As TestUnitOfMeasure, ");
        sb.Append("TAB1.Criticality As TestCriticalityFlag, ");
        sb.Append("TAB1.ResultValue As TestResultValue, ");
        sb.Append("TAB1.Message As ResultNotes, ");
        sb.Append("TAB2.ReferenceRange As NormalRange ");
        sb.Append("FROM ");
        sb.Append("(SELECT ");
        sb.Append("ARPTV_ChildSub AS ChildSub, ");
        sb.Append("WLHRL_ResultTestCodeDR->TC_RowID AS TestRowID, ");
        sb.Append("WLHRL_ResultTestCodeDR->TC_ReportingName AS ReportingName, ");
        sb.Append("WLHRL_ResultTestCodeDR->TC_UnitOfMeasure AS UnitOfMeasure, ");
        sb.Append("ARPTV_ResultValue As ResultValue, ");
        sb.Append("ARPTV_CriticalityFlag As Criticality, ");
        sb.Append("ARPTV_ResultNotes As Message ");
        sb.Append("FROM ");
        sb.Append("ORD_WorklistHistoryResultList left outer join ORD_AccessionReportResultValue ");
        sb.Append("on WLHRL_ChildSub=ARPTV_ChildSub ");
        sb.Append("WHERE ");
        sb.Append("ARPTV_ARPT_ParRef->ARPT_ACC_ParRef->ACC_Accession = '" + accessionNumber + "' ");
        //sb.Append("and ARPTV_ChildSub = WLHRL_ChildSub ");
        sb.Append("and to_date(WLHRL_WLH_ParRef->WLH_EffectiveDate,'YYYY-MM-DD') ='" + effectiveDate.ToString("yyyy-MM-dd") + "' ");
        sb.Append("and WLHRL_WLH_ParRef->WLH_WorklistID ='" + workListCode + "' ");
        sb.Append("and WLHRL_WLH_ParRef->WLH_WorklistID = ARPTV_ARPT_ParRef->ARPT_UnitCodeWorklist ");
        sb.Append("and (len(ARPTV_ResultValue)>0 or len(ARPTV_ResultNotes)>0)) ");
        //sb.Append("and len(ARPTV_ResultNotes)>0) ");
        sb.Append("As TAB1 ");
        sb.Append("left outer join ");
        sb.Append("(SELECT ");
        sb.Append("WLHRR_ChildSub AS ChildSub, ");
        sb.Append("WLHRR_ReferenceRangeToDisplay As ReferenceRange ");
        sb.Append("FROM ");
        sb.Append("ORD_WorklistHistoryReferenceRangeList ");
        sb.Append("WHERE ");
        sb.Append("WLHRR_WLH_ParRef->WLH_WorklistID='" + workListCode + "' ");
        sb.Append("and WLHRR_Species='" + SpeciesSex + "' ");
        sb.Append("and to_date(WLHRR_WLH_ParRef->WLH_EffectiveDate,'YYYY-MM-DD')='" + effectiveDate.ToString("yyyy-MM-dd") + "') ");
        sb.Append("As TAB2 ");
        sb.Append("on TAB1.ChildSub=TAB2.ChildSub ");

        String selectStatement = sb.ToString();
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }*/
    #endregion Unused code
    public static DataTable getTests(String accessionNumber, String workListCode, DateTime effectiveDate, String SpeciesSex)
    {
        System.Collections.Generic.Dictionary<String, String> _tests = new System.Collections.Generic.Dictionary<String, String>();
        _tests.Add("AccessionNumber", accessionNumber);
        _tests.Add("WorkListCode", workListCode);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        DataTable returnDataTable = AtlasIndia.AntechCSM.Data.DL_functions.StringToDataTable(cache.StoredProcedure("?=call SP2_GetTests(?,?)", _tests, 999999).Value.ToString(), '^', Convert.ToChar(127));
        if (returnDataTable.Columns.Count == 8)
        {
            //// Providing Column Name to map with the GridView
            returnDataTable.Columns[0].ColumnName = "RowID";
            returnDataTable.Columns[1].ColumnName = "TestRowID";
            returnDataTable.Columns[2].ColumnName = "TestReportingName";
            returnDataTable.Columns[3].ColumnName = "TestResultValue";
            returnDataTable.Columns[4].ColumnName = "TestCriticalityFlag";
            returnDataTable.Columns[5].ColumnName = "TestUnitOfMeasure";
            returnDataTable.Columns[6].ColumnName = "NormalRange";
            returnDataTable.Columns[7].ColumnName = "ResultNotes";

            //// Display only records where TestResultNote or ResultNotes have non empty value
            DataRow[] filteredRows = returnDataTable.Select("Len(TestResultValue)>0 or Len(ResultNotes)>0","RowID ASC");
            DataTable dt = returnDataTable.Clone();
            Int32 rowCount = filteredRows.Length;
            for (Int32 i = 0; i < rowCount; i++)
            {
                dt.ImportRow(filteredRows[i]);
            }
            dt.AcceptChanges();
            return dt;
        }
        else
        {
            return new DataTable();
        }
    }

    public static Int32 getCorrectedResultsCount(String accessionNumber, String workListID)
    {
        #region Prepare Query

        System.Text.StringBuilder sb = new System.Text.StringBuilder();

        //sb.Append("SELECT COUNT(*) FROM ");
        //sb.Append("ORD_ARPTCorrectedResultValue ");
        //sb.Append("WHERE ");
        //sb.Append("ARCRV_ARPTC_ParRef %STARTSWITH '" + accessionNumber + "||" + workListID + "||'");

        sb.Append("SELECT COUNT(*) FROM ");
        sb.Append("ORD_ARPTCorrectedResult ");
        sb.Append("WHERE ");
        sb.Append("ARPTC_ARPT_ParRef='" + accessionNumber + "||" + workListID + "'");

        #endregion Prepare Query

        string selectStatement = sb.ToString();
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return Int32.Parse(cache.CacheExScalar(selectStatement).ToString());
    }

    public static String logTestDetailsViewed(String clientID, String userID, String userLab, String logType, String empty, String TestInfo, String UnitCode)
    {
        System.Collections.Generic.Dictionary<String, String> dicLogAndPrice = new System.Collections.Generic.Dictionary<String, String>();
        dicLogAndPrice.Add("ClientID", clientID);
        dicLogAndPrice.Add("UserID", userID);
        dicLogAndPrice.Add("UserLab", userLab);
        dicLogAndPrice.Add("LogType", logType);
        dicLogAndPrice.Add("Empty", empty);
        dicLogAndPrice.Add("TestInfo", TestInfo);
        dicLogAndPrice.Add("UnitCode", UnitCode);
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_logTestDetailsViewed(?,?,?,?,?,?,?)", dicLogAndPrice).Value.ToString();
    }
}
