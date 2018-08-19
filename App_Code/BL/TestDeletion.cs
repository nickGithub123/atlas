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
/// Summary description for TestDeletion
/// </summary>
public class TestDeletion
{
    public TestDeletion()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public TestDeletion(string testDelID)
    {
        this._ID = testDelID;
        DL_TestDeletion currentTestDelID = new DL_TestDeletion();
        DataTable testDelData = currentTestDelID.getTestDeletionDetails(testDelID);
        if (testDelData == null)
        {
            this.IsValid = false;
        }
        else if (testDelData.Rows.Count < 1)
        {
            this.IsValid = false;
        }
        else if (testDelData.Rows.Count > 1)
        {
            this.IsValid = false;
        }
        else
        {
            this.IsValid = true;

            DataRow dr = testDelData.Rows[0];
            this._department = dr["DEPARTMENT"].ToString();
            this._testDeleted = dr["TESTDELETED"].ToString();
            this._reason = dr["REASON"].ToString();
            this._comments = dr["COMMENTS"].ToString();
            this._staus = dr["STATUS"].ToString();
            this._processedby = dr["PROCESSEDBY"].ToString();
            this._processedDate = dr["DATEPROCESSED"].ToString();
            this._processedTime = dr["TIMEPROCESSED"].ToString();
            this._printedby = dr["PRINTEDBY"].ToString();
            this._checkedDate = dr["DATECHECKED"].ToString();
            this._checkedTime = dr["TIMECHECKED"].ToString();
            this._enteredBy = dr["ENTEREDBY"].ToString();
            this._enteredDate = dr["DATEENTERED"].ToString();
            this._enteredTime = dr["TIMEENTERED"].ToString();
        }
    }
    private Boolean _isValid;
    public Boolean IsValid
    {
        get { return _isValid; }
        set { _isValid = value; }
    }
    
    #region Test Deletion Properties
    private string _ID;
    public string ID
    {
        get { return _ID; }
        //set { _ID = value; }
    }
    private string _department;
    public string Department
    {
        get { return _department; }
        //set { _department = value; }
    }
    private string _reason;
    public string Reason
    {
        get { return _reason; }
        //set { _reason = value; }
    }
    private string _testDeleted;
    public string TestDeleted
    {
        get { return _testDeleted; }
        //set { _testDeleted = value; }
    }
    private string _comments;
    public string Comments
    {
        get { return _comments; }
        //set { _comments = value; }
    }
    private string _staus;
    public string Status
    {
        get { return _staus; }
        //set { _staus = value; }
    }
    private string _processedby;
    public string Processedby
    {
        get { return _processedby; }
        //set { _processedby = value; }
    }
    private string _processedDate;
    public string ProcessedDate
    {
        get { return _processedDate; }
        //set { _processedDate = value; }
    }
    private string _processedTime;
    public string ProcessedTime
    {
        get { return _processedTime; }
        //set { _processedTime = value; }
    }
    private string _printedby;
    public string Printedby
    {
        get { return _printedby; }
        //set { _printedby = value; }
    }
    private string _checkedDate;
    public string CheckedDate
    {
        get { return _checkedDate; }
        //set { _checkedDate = value; }
    }
    private string _checkedTime;
    public string CheckedTime
    {
        get { return _checkedTime; }
        //set { _checkedTime = value; }
    }
    private string _enteredBy;
    public string EnteredBy
    {
        get { return _enteredBy; }
        //set { _enteredBy = value; }
    }
    private string _enteredDate;
    public string EnteredDate
    {
        get { return _enteredDate; }
        //set { _enteredDate = value; }
    }
    private string _enteredTime;
    public string EnteredTime //AM Issue#42364 AntechCSM 1.0.20.0
    {
        get { return _enteredTime; }
        //set { _enteredTime = value; }
    }
    #endregion
    
    //AM Issue#38926 05/26/2008 Build Number 1.0.0.9
    public static DataTable getReason(string DepartmentID)
    {
        DataTable returnData = new DataTable();
        returnData = DL_TestDeletion.getReason(DepartmentID);
        return returnData;
    }
    
    public static DataTable getReason()
    {
        DataTable returnData = new DataTable();
        returnData = DL_TestDeletion.getReason();
        return returnData;
    }
    //AM Issue#46793 AntechCSM 1.0.21.0 10/27/2008
    public DataTable getSearchResult(String strQS)
    {
        return DL_TestDeletion.getSearchResult(strQS);
    }
    //AM Issue#32875 03/26/2008 Build Number 1.0.0.9
    public static DataTable getAccessionDetails(string AccessionNumber)
    {
        DataTable returnDataTable = new DataTable();
        DL_TestDeletion testDeletion = new DL_TestDeletion();
        //returnDataTable = testDeletion.getAccessionDetails(AccessionNumber);
        returnDataTable = DL_Accession.getUnitsOrderedByAccession(AccessionNumber); //AM Issue#32876 09/22/2008 Build Number 1.0.14.0 
        return returnDataTable;
    }
    
    //AM Issue#32875 03/26/2008 Build Number 1.0.0.9
    public DataTable getTestDeletionDetails(string accountNumber, string accessionNumber, string dateDeletedFrom, string dateDeletedTo, string department, string location, string status, string reason, string enteredBy, String checkedBy)
    {
        DataTable returnDataTable = new DataTable();
        DL_TestDeletion testDeletion = new DL_TestDeletion();
        returnDataTable = testDeletion.getTestDeletionDetails(accountNumber, accessionNumber, dateDeletedFrom, dateDeletedTo, department, location, status, reason, enteredBy, checkedBy);//AM Issue#46178 1.0.0.18 10/13/2008 - adding new search criteria 'Entered By'
        return returnDataTable;
    }
    
    public static String insertTestDeletion(string Accession, string DateEntered, string Department, string LabLocation, string ReasonForDeletion, string TestsToDeleteString, string Comments, string ProcessingStatus, string UserEnteredBy)
    {
        return DL_TestDeletion.insertTestDeletion(Accession, DateEntered, Department, LabLocation, ReasonForDeletion, TestsToDeleteString, Comments, ProcessingStatus, UserEnteredBy);
    }
    
    public static void insertTestDeletion(string Accession, string DateEntered, string Department, string LabLocation, string ReasonForDeletion, string Comments, string TestsToDeleteString, string userID)
    {
        DL_TestDeletion.insertTestDeletion(Accession, DateEntered, Department, LabLocation, ReasonForDeletion, Comments, TestsToDeleteString, userID);
    }

    public static void updateTestDelStatus(String testID, String processedBy, String CheckedOut, String accession)
    {
        DL_TestDeletion.updateTestDelStatus(testID, processedBy, CheckedOut, accession);
    }
    
    public static void updateTestDelStatus(string testID, string CheckedOut, string CheckedBy)
    {
        DL_TestDeletion.updateTestDelStatus(testID, CheckedOut, CheckedBy);
    }
    
    //AM Issue#32877 AntechCSM 1.0.15.0 09/24/2008
    public static void deleteTestDeletion(String TESTROW,String accession, String UserID)
    {
        DL_TestDeletion.deleteTestDeletion(TESTROW,accession,UserID);
    }
    
    //AM Issue#32877 AntechCSM 1.0.16.0 09/25/2008
    public static void SendClientGramTestDeletion(String ACCOUNT, String ACCESSION, String USERID, String TESTS, String LAB,String REASON,String strAutodial)
    {
        DL_TestDeletion.sendClientgram(ACCOUNT, ACCESSION, USERID, TESTS, LAB, REASON, strAutodial);
    }
    public String verifyTestDeletion(String Reason)
    {
        return DL_TestDeletion.verifyTestDeletion(Reason);
    }
}
