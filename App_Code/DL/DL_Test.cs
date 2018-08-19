using System;
using System.Data;
using System.Configuration;

public class DL_Test
{
    public DL_Test()
    {
        //
    }

    public static DataTable getCorrectedResults(string accessionNumber, string workListID, string testCode)
    {
        #region Reference Query
        //SELECT 	
        //    ARCRV_ARPTC_ParRef->ARPTC_CorrectedTestCodeDR->TC_ReportingName,
        //    ARCRV_ARPTC_ParRef->ARPTC_CorrectedTestCodeDR->TC_UnitOfMeasure,
        //    ARCRV_UserEnteredBy,
        //    ARCRV_UserReleasedBy,
        //    ARCRV_DateReleased,
        //    ARCRV_ResultValue,	
        //    ARCRV_MessageDR->MSG_MessageText
        //FROM
        //    ORD_ARPTCorrectedResultValue
        //WHERE 
        //        ARCRV_ARPTC_ParRef='AA0000010||RIA||847'
        // worklist reference reange talbe se use worklist + worklist effective date
        #endregion Reference Query

        #region Prepare Query

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ARCRV_ARPTC_ParRef->ARPTC_CorrectedTestCodeDR->TC_ReportingName AS TestName, ");
        sb.Append("ARCRV_ARPTC_ParRef->ARPTC_CorrectedTestCodeDR->TC_UnitOfMeasure As UnitOfMeasure, ");
        sb.Append("ARCRV_UserEnteredBy As EnteredBy, ");
        sb.Append("ARCRV_UserReleasedBy As ReleasedBy, ");
        sb.Append("ARCRV_DateReleased As DateReleased, ");
        sb.Append("ARCRV_ResultValue As Results, ");
        sb.Append("ARCRV_MessageDR->MSG_MessageText As ResultNotes ");
        sb.Append("FROM ");
        sb.Append("ORD_ARPTCorrectedResultValue ");
        sb.Append("WHERE ");
        sb.Append("ARCRV_ARPTC_ParRef='" + accessionNumber + "||" + workListID + "||" + testCode + "'");
        
        #endregion Prepare Query

        string selectStatement = sb.ToString();
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static Int32 getCorrectedResultsCount(String accessionNumber, String workListID, String testCode)
    {
        #region Prepare Query

        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT COUNT(*) FROM ");
        sb.Append("ORD_ARPTCorrectedResultValue ");
        sb.Append("WHERE ");
        sb.Append("ARCRV_ARPTC_ParRef='" + accessionNumber + "||" + workListID + "||" + testCode + "'");

        #endregion Prepare Query

        string selectStatement = sb.ToString();
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return Int32.Parse(cache.CacheExScalar(selectStatement).ToString());
    }
}