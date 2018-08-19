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

/// <summary>
/// Summary description for DL_Message
/// </summary>
public class DL_Message
{
    public DL_Message()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    //AM Issue#32801 04/09/2008 0.0.0.9 -- AM Issue#32801 09/17/2008 AntechCSM 1.0.13.0 (Search with Message Keyword list)
    public static DataTable getMessageDetails(String SearchText, String SearchOption)
    {
        string selectStatement = "SELECT MSG_Code, MSG_FirstLine, MSG_MessageText, MSG_AutoProblemComment,MSG_AutoProblemResolution, MSG_DefaultProblemCategoryDR,MSG_KeywordList FROM DIC_Message";
        selectStatement = selectStatement + " WHERE 1=1 ";
        if (SearchOption=="Message_Code")
            selectStatement = (SearchText != "" ? selectStatement + " AND %SQLUPPER MSG_Code %STARTSWITH %SQLUPPER '" + SearchText + "'" : selectStatement);
        else
            selectStatement = (SearchText != "" ? selectStatement + " AND %SQLUPPER MSG_KeywordList LIKE %SQLUPPER '%" + SearchText + "%'" : selectStatement);
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

    public static DataTable getMessageDetailsByCode(String messageCode)
    {
        string selectStatement = "SELECT MSG_MessageText, MSG_AutoProblemComment,MSG_AutoProblemResolution, MSG_DefaultProblemCategoryDR, MSG_AutoInquiryNoteText FROM DIC_Message WHERE %SQLUPPER MSG_Code LIKE %SQLUPPER '" + messageCode + "'";
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);        
    }
}
