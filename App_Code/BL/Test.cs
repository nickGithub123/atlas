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
/// Lab Test
/// </summary>
public class LabTest
{
    #region Lab Test Constructors

    public LabTest()
    {
        //
    }

    public LabTest(DataRow testRow, Boolean loadDetails)
    {
        this._testCode = testRow["TestRowID"].ToString();
        if (loadDetails)
        {
            this.TestName = testRow["TestReportingName"].ToString();
            this.UnitOfMeasure = testRow["TestUnitOfMeasure"].ToString();
            this.Results = testRow["TestResultValue"].ToString();
            this.IsResultCritical = (testRow["TestCriticalityFlag"].ToString().Length > 0) ? true : false;
            this.CriticalityInformation = testRow["TestCriticalityFlag"].ToString();
            this.NormalRange = testRow["NormalRange"].ToString();
            this.ResultNotes = testRow["ResultNotes"].ToString();
        }
    }

    public LabTest(String accessionNumber, String workListID, DataRow testRow, Boolean loadDetails)
    {
        this._testCode = testRow["TestRowID"].ToString();
        if (loadDetails)
        {
            this.TestName = testRow["TestReportingName"].ToString();
            this.UnitOfMeasure = testRow["TestUnitOfMeasure"].ToString();
            this.Results = testRow["TestResultValue"].ToString();
            this.IsResultCritical = (testRow["TestCriticalityFlag"].ToString().Length > 0) ? true : false;
            this.CriticalityInformation = testRow["TestCriticalityFlag"].ToString();
            this.NormalRange = testRow["NormalRange"].ToString();
            this.ResultNotes = testRow["ResultNotes"].ToString();
            if (DL_Test.getCorrectedResultsCount(accessionNumber, workListID, _testCode) > 0)
            {
                this.HasCorrectedResults = true;
            }
            this.WorkListID = workListID;
        }
    }

    #endregion Lab Test Constructors

    #region Supporting Methods

    public DataTable getCorrectedResults(String accessionNumber, String workListID, String testCode)
    {
        return DL_Test.getCorrectedResults(accessionNumber, workListID, testCode);
    }

    #endregion Supporting Methods

    #region Lab Test Properties

    #region Test Code
    private String _testCode;
    public String TestCode
    {
        get { return _testCode; }
        // set { _testCode = value; }
    }
    #endregion Test Code

    #region Test Name [Reporting Title]
    private String _testName;
    public String TestName
    {
        get { return _testName; }
        set { _testName = value; }
    }
    #endregion Test Name [Reporting Title]

    #region Test Results
    private String _results;
    public String Results
    {
        get { return _results; }
        set { _results = value; }
    }
    #endregion Test Results

    #region Is Result Critical
    private Boolean _isResultCritical;
    public Boolean IsResultCritical
    {
        get { return _isResultCritical; }
        set { _isResultCritical = value; }
    }
    #endregion Is Result Critical

    #region Criticality Information
    private String _criticalityInformation;
    public String CriticalityInformation
    {
        get { return _criticalityInformation; }
        set { _criticalityInformation = value; }
    }
    #endregion Criticality Information

    #region Unit of Measure
    private String _unitOfMeasure;
    public String UnitOfMeasure
    {
        get { return _unitOfMeasure; }
        set { _unitOfMeasure = value; }
    }
    #endregion Unit of Measure

    #region Normal Range for Test Results
    private String _normalRange;
    public String NormalRange
    {
        get { return _normalRange; }
        set { _normalRange = value; }
    }
    #endregion Normal Range for Test Results

    #region Result Notes
    private String _resultNotes;
    public String ResultNotes
    {
        get { return _resultNotes; }
        set { _resultNotes = value; }
    }
    #endregion Result Notes

    #region Has Corrected Results
    private Boolean _hasCorrectedResults;
    public Boolean HasCorrectedResults
    {
        get { return _hasCorrectedResults; }
        set { _hasCorrectedResults = value; }
    }
    #endregion Has Corrected Results

    #region WorkList ID
    private String _workListID;
    public String WorkListID
    {
        get { return _workListID; }
        set { _workListID = value; }
    }
    #endregion WorkList ID

    #endregion Test Properties
}