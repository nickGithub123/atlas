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

public class Unit : Unit_ScaledDown
{
    #region Unit Constructors

    public Unit()
    {
    }

    public Unit(string unitCode, Boolean loadDetails)
        : base(unitCode, false)
    {
        if (loadDetails)
        {
            DataTable dt = DL_Unit.getUnitDetails(unitCode);
            DataRow dr;
            if (dt != null && dt.Rows.Count > 0)
            {
                dr = dt.Rows[0];
                this.OrderingMnemonics = dr["UnitCodeMnemonic"].ToString();
                this.ReportingTitle = dr["ReportingTitle"].ToString();
                this.Alias = dr["Alias"].ToString();
                this.SpecimenType = dr["SpecimenType"].ToString();
                this.WorkList = dr["Worklist"].ToString();
                this.TestCodes = dr["TestCodes"].ToString();
                this.ReferredToNode = dr["ReferredToNode"].ToString();
                this.ReferredToName = dr["ReferredToName"].ToString();
                this.ReferredToAddress = dr["ReferredToAddress"].ToString();
                this.ReferenceLabID = dr["ReferenceLabID"].ToString();
                this.PathologistReview = dr["PathologistReview"].ToString();
                this.SpecimenRequirements = dr["SpecimenRequirements"].ToString();
                this.ContainerType = dr["ContainerType"].ToString();
                this.AcceptableSpecimenRequirements = dr["AcceptableSpecimenRequirement"].ToString();
                this.TransportTemperature = dr["TransportTemperature"].ToString();
                this.MinimumSpecimenVolume = dr["MinimumSpecimenVolume"].ToString();
                this.LaboratoryArea = dr["LabArea"].ToString();
                this.AnalyticTime = dr["AnalyticTime"].ToString();
                this.DaysTestSetup = dr["DaysTestSetUp"].ToString();
                this.TimeOfDay = dr["TimeOfDay"].ToString();
                this.MaximumLabTime = dr["MaxLabTime"].ToString();
                this.Methodology = dr["Methodology"].ToString();
                this.ReferenceRangeAndUnitsOfMeasure = dr["ReferenceRangeAndUOM"].ToString();
                this.PanicValues = dr["PanicValues"].ToString();
                this.SpecimenStability = dr["SpecimenStability"].ToString();
                this.RejectDueToHemolysis = dr["RejectDuetoHemolysis"].ToString();
                this.RejectDueToLipemia = dr["RejectDuetoLipemia"].ToString();
                this.RejectDueToThawingOrOther = dr["RejectDueToThawing"].ToString();
                this.RoutineInstructionsForInquiry = dr["RoutineInstructionsForInquiry"].ToString();
                this.SpecialInstructForDrawList = dr["SpecialInstructForDrawList"].ToString();
                this.SpecimenRetention = dr["SpecimenRetention"].ToString();
                this.ClinicalSignificance = dr["ClinicalSignificance"].ToString();
                this.PrincipleOfAssayAndJournalReferences = dr["PrincOfAssayAndJournRef"].ToString();
                this.CPTCode = dr["CPTCode"].ToString();
                this.Fee = dr["Fee"].ToString();
                this.EtiologicAgent = dr["EtiologicAgent"].ToString();
                this.WorkloadProcedureCodes = dr["CapWorkUnits"].ToString();
                this.NumberOfCollectionLabels = dr["NumberOfCollectionLabels"].ToString();
                this.AlwaysMessage = dr["AlwaysMessage"].ToString();
                this.AlwaysMessageCode = dr["AlwaysMessageCode"].ToString();
                this.ReportSequence = dr["ReportSequenceForReport"].ToString();
                this.Category = dr["Category"].ToString();
                DateTime? tmpDate = AtlasIndia.AntechCSM.functions.AddTimeToDateNullable(dr["AlwaysMessageEffectiveDate"].ToString(),"");
                if (tmpDate.HasValue)
                {
                    this.AlwaysMessageEffectiveDate =  tmpDate.Value.ToString(AtlasIndia.AntechCSM.functions.getDateFormat());
                }
                else
                {
                    this.AlwaysMessageEffectiveDate =  dr["AlwaysMessageEffectiveDate"].ToString();
                }
                this.Status = dr["Status"].ToString();
                this.OrderEntryNote = dr["OrderEntryNoteOE"].ToString();
            }
        }
    }

    #endregion

    #region Unit Properties

    //#region Unit Code
    //private string _unitCode;
    //public string UnitCode
    //{
    //    get { return _unitCode; }
    //    //set { _unitCode = value; }
    //}
    //#endregion Unit Code

    //#region Reporting Title
    //private string _reportingTitle;
    //public string ReportingTitle
    //{
    //    get { return _reportingTitle; }
    //    //set { _reportingTitle= value; }
    //}
    //#endregion  Reporting Title

    #region Ordering Mnemonics
    private string _mnemonics;
    public String OrderingMnemonics
    {
        get { return _mnemonics; }
        set { _mnemonics = value; }
    }
    #endregion Ordering Mnemonics

    #region Routine Instructions for Inquiry
    private string _routineInstructionsForInquiry;
    public string RoutineInstructionsForInquiry
    {
        get { return _routineInstructionsForInquiry; }
        set { _routineInstructionsForInquiry = value; }
    }
    #endregion  Routine Instructions for Inquiry

    #region Clinical Significance
    private string _clinicalSignificance;
    public string ClinicalSignificance
    {
        get { return _clinicalSignificance; }
        set { _clinicalSignificance = value; }
    }
    #endregion Clinical Significance

    #region Principle of Assay and Journal References
    private string _principleOfAssayAndJournalReferences;
    public string PrincipleOfAssayAndJournalReferences
    {
        get { return _principleOfAssayAndJournalReferences; }
        set { _principleOfAssayAndJournalReferences = value; }
    }
    #endregion Principle of Assay and Journal References

    #region CPT Code
    private string _cptCode;
    public string CPTCode
    {
        get { return _cptCode; }
        set { _cptCode = value; }
    }
    #endregion CPT Code

    #region Alias
    private string _alias;
    public string Alias
    {
        get { return _alias; }
        set { _alias = value; }
    }
    #endregion Alias

    #region Specimen Type
    private string _specimenType;
    public string SpecimenType
    {
        get { return _specimenType; }
        set { _specimenType = value; }
    }
    #endregion Specimen Type

    #region WorkList
    // moved to parent class
    //private string _workList;
    //public string WorkList
    //{
    //    get { return _workList; }
    //    //set { _workList= value; }
    //}
    #endregion WorkList

    #region Test Codes
    private string _testCodes;
    public string TestCodes
    {
        get { return _testCodes; }
        set { _testCodes = value; }
    }
    #endregion Test Codes

    #region Report Sequence
    private string _reportSequence;
    public string ReportSequence
    {
        get { return _reportSequence; }
        set { _reportSequence = value; }
    }
    #endregion Report Sequence

    #region Laboratory Area
    private string _laboratoryArea;
    public string LaboratoryArea
    {
        get { return _laboratoryArea; }
        set { _laboratoryArea = value; }
    }
    #endregion Laboratory Area

    #region Reject Due to Lipemia
    private string _rejectDueToLipemia;
    public string RejectDueToLipemia
    {
        get { return _rejectDueToLipemia; }
        set { _rejectDueToLipemia = value; }
    }
    #endregion Reject Due to Lipemia

    #region Category
    private string _category;
    public string Category
    {
        get { return _category; }
        set { _category = value; }
    }
    #endregion Category

    #region Methodology
    private string _methodology;
    public string Methodology
    {
        get { return _methodology; }
        set { _methodology = value; }
    }
    #endregion Methodology

    #region Reject due to Hemolysis
    private string _rejectDueToHemolysis;
    public string RejectDueToHemolysis
    {
        get { return _rejectDueToHemolysis; }
        set { _rejectDueToHemolysis = value; }
    }
    #endregion Reject due to Hemolysis

    #region Days Test Setup
    private string _daysTestSetup;
    public string DaysTestSetup
    {
        get { return _daysTestSetup; }
        set { _daysTestSetup = value; }
    }
    #endregion Days Test Setup

    #region Time of Day
    private string _timeOfDay;
    public string TimeOfDay
    {
        get { return _timeOfDay; }
        set { _timeOfDay = value; }
    }
    #endregion Time of Day

    #region Maximum Lab Time
    private string _maximumLabTime;
    public string MaximumLabTime
    {
        get { return _maximumLabTime; }
        set { _maximumLabTime = value; }
    }
    #endregion Maximum Lab Time

    #region Specimen Requirements
    private string _specimenRequirements;
    public string SpecimenRequirements
    {
        get { return _specimenRequirements; }
        set { _specimenRequirements = value; }
    }
    #endregion Specimen Requirements

    #region Minimum Specimen Volume
    private string _minimumSpecimenVolume;
    public string MinimumSpecimenVolume
    {
        get { return _minimumSpecimenVolume; }
        set { _minimumSpecimenVolume = value; }
    }
    #endregion Minimum Specimen Volume

    #region Special Instructions For DrawList
    private string _specialInstructForDrawList;
    public string SpecialInstructForDrawList
    {
        get { return _specialInstructForDrawList; }
        set { _specialInstructForDrawList = value; }
    }
    #endregion Special Instructions For DrawList

    #region Specimen Retention
    private String _specimenRetention;
    public String SpecimenRetention
    {
        get { return _specimenRetention; }
        set { _specimenRetention = value; }
    }
    #endregion Specimen Retention

    #region Referred To Node
    private string _referredToNode;
    public string ReferredToNode
    {
        get { return _referredToNode; }
        set { _referredToNode = value; }
    }
    #endregion Referred To Node

    #region Referred To Name
    private string _referredToName;
    public string ReferredToName
    {
        get { return _referredToName; }
        set { _referredToName = value; }
    }
    #endregion Referred To Name

    #region Referred To Address
    private string _referredToAddress;
    public string ReferredToAddress
    {
        get { return _referredToAddress; }
        set { _referredToAddress = value; }
    }
    #endregion Referred To Address

    #region Reference Lab ID
    private String _referenceLabID;
    public string ReferenceLabID
    {
        get { return _referenceLabID; }
        set { _referenceLabID = value; }
    }
    #endregion Reference Lab ID

    #region Pathologist Review
    private String _pathologistReview;
    public string PathologistReview
    {
        get { return _pathologistReview; }
        set { _pathologistReview = value; }
    }
    #endregion Pathologist Review

    #region Container Type
    private string _containerType;
    public string ContainerType
    {
        get { return _containerType; }
        set { _containerType = value; }
    }
    #endregion Container Type

    #region Acceptable Specimen Requirements
    private string _acceptableSpecimenRequirements;
    public string AcceptableSpecimenRequirements
    {
        get { return _acceptableSpecimenRequirements; }
        set { _acceptableSpecimenRequirements = value; }
    }
    #endregion Acceptable Specimen Requirements

    #region Transport Temperature
    private string _transportTemperature;
    public string TransportTemperature
    {
        get { return _transportTemperature; }
        set { _transportTemperature = value; }
    }
    #endregion Transport Temperature

    #region Reference Range and Units of Measure
    private string _refRangeAndUOM;
    public string ReferenceRangeAndUnitsOfMeasure
    {
        get { return _refRangeAndUOM; }
        set { _refRangeAndUOM = value; }
    }
    #endregion Reference Range and Units of Measure

    #region Panic Values
    private string _panicValues;
    public string PanicValues
    {
        get { return _panicValues; }
        set { _panicValues = value; }
    }
    #endregion Panic Values

    #region Specimen Stability
    private string _specimenStability;
    public string SpecimenStability
    {
        get { return _specimenStability; }
        set { _specimenStability = value; }
    }
    #endregion Specimen Stability

    #region Analytic Time
    private string _analyticTime;
    public string AnalyticTime
    {
        get { return _analyticTime; }
        set { _analyticTime = value; }
    }
    #endregion Analytic Time

    #region Reject due to Thawing or Other
    private string _rejDueToThawing;
    public string RejectDueToThawingOrOther
    {
        get { return _rejDueToThawing; }
        set { _rejDueToThawing = value; }
    }
    #endregion Reject due to Thawing or Other

    #region Fee
    private string _fee;
    public string Fee
    {
        get { return _fee; }
        set { _fee = value; }
    }
    #endregion Fee

    #region Etiologic Agent
    private string _etiologicAgent;
    public string EtiologicAgent
    {
        get { return _etiologicAgent; }
        set { _etiologicAgent = value; }
    }
    #endregion Etiologic Agent

    #region Workload Procedure Code(s)
    private string _workloadProcedureCodes;
    public string WorkloadProcedureCodes
    {
        get { return _workloadProcedureCodes; }
        set { _workloadProcedureCodes = value; }
    }
    #endregion Workload Procedure Code(s)

    #region Number Of Collection Labels
    private string _noOfColLabels;
    public string NumberOfCollectionLabels
    {
        get { return _noOfColLabels; }
        set { _noOfColLabels = value; }
    }
    #endregion Number Of Collection Labels

    #region Always Message Code
    private String _alwaysMessageCode;
    public String AlwaysMessageCode
    {
        get { return _alwaysMessageCode; }
        set { _alwaysMessageCode = value; }
    }
    #endregion Always Message Code

    #region Always Message
    private String _alwaysMessage;
    public String AlwaysMessage
    {
        get { return _alwaysMessage; }
        set { _alwaysMessage = value; }
    }
    #endregion Always Message

    #region Always Message Effective Date
    private String _alwaysMessageEffectiveDate;
    public String AlwaysMessageEffectiveDate
    {
        get { return _alwaysMessageEffectiveDate; }
        set { _alwaysMessageEffectiveDate = value; }
    }
    #endregion Always Message Effective Date

    #region Status
    private String _status;
    public String Status
    {
        get { return _status; }
        set { _status = value; }
    }
    #endregion Status

    #region Order Entry Note
    private String _orderEntryNote;
    public String OrderEntryNote
    {
        get { return _orderEntryNote; }
        set { _orderEntryNote = value; }
    }
    #endregion Order Entry Note
    
    #endregion Unit Properties

    public static void logTestDetailsViewed(String clientID, String userID, String userLab, String UnitCode, String TestName)
    {
        DL_Unit.logTestDetailsViewed(clientID, userID, userLab, "TEST", "", UnitCode + " - " + TestName, UnitCode);
    }

    #region Supporting Properties

    #region Is Profile
    private string _isProfile;
    public string IsProfile
    {
        get { return _isProfile; }
        //set { _isProfile= value; }
    }
    #endregion Is Profile

    #endregion Supporting Properties

    #region Supporting Methods
    public DataTable getUnderlyingUnits(String clientCountry)
    {
        return DL_Unit.getUnitCodesForProfile(this.UnitCode, clientCountry);
    }

    public static DataTable getUnitDetailsForReport(string unitCode)
    {
        return DL_Unit.getUnitDetails(unitCode);
    }
    
    #endregion
}

/// <summary>
/// Scaled down version of Unit [Only Unit Code and Reporting Title]
/// </summary>
public class Unit_ScaledDown
{
    #region Scaled Down Unit Constructors

    public Unit_ScaledDown()
    {
        //
    }

    public Unit_ScaledDown(String unitCode, Boolean loadDetails)
    {
        this._unitCode = unitCode;
        if (loadDetails)
        {
            // TO DO: Write logic to get reporting title for unit code and assign to the Reporting Title Properry
        }
    }

    public Unit_ScaledDown(String accessionNumber, String species, String sex, DataRow unitRow, Boolean loadTest)
    {
        this._unitCode = unitRow["UnitCode"].ToString();
        this._componentUnitCode = unitRow["ComponentUnitCode"].ToString();
        this._accessionNumber = accessionNumber;
        this.ReportingTitle = unitRow["ReportingTitle"].ToString();
        this.WorkList = unitRow["WorkList"].ToString();
        this.WorkListMessage = generateWorkListNote(unitRow["WorkList"].ToString(), unitRow["WorklistOwnedByLabName"].ToString(), unitRow["WorklistLoad"].ToString(), unitRow["WorklistSequence"].ToString(), unitRow["WorklistBuiltBy"].ToString(), unitRow["WorklistDateBuilt"].ToString(), unitRow["WorklistTimeBuilt"].ToString(), unitRow["TestPerformedAtLabName"].ToString(), unitRow["WorkListEnteredBy"].ToString(), unitRow["WorkListReleasedBy"].ToString(), unitRow["WorklistReleasedDate"].ToString(), unitRow["WorklistReleasedTime"].ToString(), unitRow["WorkListReportDate"].ToString(), unitRow["WorkListReportTime"].ToString(), unitRow["WorkListBatchToDisplay"].ToString());
        this.IsResultCritical = (string.Compare(unitRow["IsAbnormal"].ToString(), "Y", true) == 0) ? true : false;
        this.TestsOrdered = new System.Collections.Generic.List<LabTest>();
        this.ReportNotes = unitRow["ReportNotes"].ToString();
        this.TurnaroundTimeNote = generateTurnaroundTimeNote(unitRow["UnitCodeDaysTestSetUp"].ToString(), unitRow["UnitCodeTimeOfDay"].ToString(), unitRow["UnitCodeMaxLabTime"].ToString());
        DateTime.TryParse(unitRow["WorklistEffectiveDate"].ToString(), out _workListEffectiveDate);         //// to handle the Invalid DateTime data (Empty Strings) from the Database
        this._cannedMessage = unitRow["CannedMessage"].ToString();
        if (sex == "M" || sex == "m")
        {
            this.AssociatedAccessionSpeciesSex = species + "R3";
        }
        else if (sex == "F" || sex == "f")
        {
            this.AssociatedAccessionSpeciesSex = species + "R4";
        }
        else
        {
            this.AssociatedAccessionSpeciesSex = species + "R2";
        }

        if (loadTest)
        {
            DataTable testsOrdered = DL_Unit.getTests(this.AccessionNumber, this.WorkList, this.WorkListEffectiveDate, this.AssociatedAccessionSpeciesSex);
            this.TestsOrdered = new System.Collections.Generic.List<LabTest>();
            if (testsOrdered != null && testsOrdered.Rows.Count > 0)
            {
                if (DL_Unit.getCorrectedResultsCount(this.AccessionNumber, this.WorkList) > 0)
                {
                    this.HasCorrectedResults = true;
                    for (Int32 i = 0; i < testsOrdered.Rows.Count; i++)
                    {
                        this.TestsOrdered.Add(new LabTest(this.AccessionNumber, this.WorkList, testsOrdered.Rows[i], true));
                    }
                }
                else
                {
                    for (Int32 i = 0; i < testsOrdered.Rows.Count; i++)
                    {
                        this.TestsOrdered.Add(new LabTest(testsOrdered.Rows[i], true));
                    }
                }
            }
            this._testsLoaded = true;
        }

    }

    public void loadTests()
    {
        DataTable testsOrdered = DL_Unit.getTests(this.AccessionNumber, this.WorkList, this.WorkListEffectiveDate, this.AssociatedAccessionSpeciesSex);
        this.TestsOrdered = new System.Collections.Generic.List<LabTest>();
        if (testsOrdered != null && testsOrdered.Rows.Count > 0)
        {
            if (DL_Unit.getCorrectedResultsCount(this.AccessionNumber, this.WorkList) > 0)
            {
                this.HasCorrectedResults = true;
                for (Int32 i = 0; i < testsOrdered.Rows.Count; i++)
                {
                    this.TestsOrdered.Add(new LabTest(this.AccessionNumber, this.WorkList, testsOrdered.Rows[i], true));
                }
            }
            else
            {
                for (Int32 i = 0; i < testsOrdered.Rows.Count; i++)
                {
                    this.TestsOrdered.Add(new LabTest(testsOrdered.Rows[i], true));
                }
            }
        }
        this._testsLoaded = true;
    }

    #endregion Scaled Down Unit Constructors

    #region Supporting Methods

    private string generateWorkListNote(string unitCodeWorkList, string workListOwnedByLabName, string workListLoad, string WorkListSequence, string workListBuiltBy, string workListBuiltDate, string workListBuiltTime, string testsPerformedAtLabName, string workListEnteredBy, string workListReleasedBy, string workListReleasedDate, string workListReleasedTime, string workListReportingDate, string workListReportingTime, string batchToDisplay)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        String dateTimeFormat = AtlasIndia.AntechCSM.functions.getDateTimeFormat();
        sb.Append(unitCodeWorkList);
        if (workListOwnedByLabName.Length > 0)
        {
            sb.Append("\t owned by " + workListOwnedByLabName);
        }
        sb.AppendLine();
        if (workListLoad.Length > 0)
        {
            sb.Append("Load ");
            sb.Append(workListLoad);

        }
        if (WorkListSequence.Length > 0)
        {
            sb.Append("\t Seq ");
            sb.Append(WorkListSequence);
        }
        if (workListBuiltBy.Length > 0)
        {
            sb.Append("\t built by ");
            sb.Append(workListBuiltBy + " ");

            DateTime? workListBuiltDateTime = AtlasIndia.AntechCSM.functions.AddTimeToDateNullable(workListBuiltDate, workListBuiltTime);
            if (workListBuiltDateTime.HasValue)
            {
                sb.Append(workListBuiltDateTime.Value.ToString(dateTimeFormat));
            }
        }
        sb.AppendLine();
        if (testsPerformedAtLabName.Length > 0)
        {
            sb.Append("Test performed at: ");
            sb.Append(testsPerformedAtLabName);
        }
        sb.AppendLine();
        if (workListEnteredBy.Length > 0)
        {
            sb.Append("Entered by : ");
            sb.Append(workListEnteredBy);
        }
        if (workListReleasedBy.Length > 0)
        {
            sb.Append("\t Released by : ");
            sb.Append(workListReleasedBy + " ");

            DateTime? workListReleasedDateTime = AtlasIndia.AntechCSM.functions.AddTimeToDateNullable(workListReleasedDate, workListReleasedTime);
            if (workListReleasedDateTime.HasValue)
            {
                sb.Append(workListReleasedDateTime.Value.ToString(dateTimeFormat));
            }
        }
        sb.AppendLine();

        // +AA Issue #59586 AntechCSM 1.0.82.0
        /// The database contains the value in the reporting time field which can not be converted to time.
        /// for example the values are somewhere 07:38 and somewhere 07:38 EDT.
        /// To avoid this issue we are adding the new function 'AddTimeToDateString' and using it to manipulate DateTime value.
        String reportingDateTime;
        reportingDateTime = AtlasIndia.AntechCSM.functions.AddTimeToDateString(workListReportingDate, workListReportingTime);
        if (reportingDateTime.Length > 0)
        {
            sb.Append("Reported on: ");
            sb.Append(reportingDateTime);
        }
        // -AA Issue #59586 AntechCSM 1.0.82.0

        // +Commented Issue #59586 AntechCSM 1.0.82.0
        //if (workListReportingDate.Length > 0)
        //{
        //    sb.Append("Reported on: ");

        //    DateTime? workListReportingDateTime = AtlasIndia.AntechCSM.functions.AddTimeToDateNullable(workListReportingDate, workListReportingTime);
        //    if (workListReportingDateTime.HasValue)
        //    {
        //        sb.Append(workListReportingDateTime.Value.ToString(dateTimeFormat));
        //    }
        //}
        // -Commented Issue #59586 AntechCSM 1.0.82.0
        
        if (batchToDisplay.Length > 0)
        {
            sb.Append("\t Batch: ");
            sb.Append(batchToDisplay);
        }
        return sb.ToString();

        #region Example Note with References of Data

        //ARPT_UnitCodeWorklist  owned by  ARPT_WorklistOwnedByLabDR-> LABLO_LabName
        //Load ARPT_WorklistLoad ARPT_WorklistSequence built by ARPT_WorklistBuiltBy ARPT_WorklistDateBuilt ARPT_WorklistTimeBuilt
        //Test Performed at : ARPT_TestPerformedAtLabDR-> LABLO_LabName
        //Entered by : ARPT_WorklistEnteredBy Released by : ARPT_WorklistReleasedBy
        //ARPT_WorklistReleasedDate
        //ARPT_WorklistReleasedTime
        //Reported on: ARPT_ReportedBatchDR-> ACRB_ReportDate ARPT_ReportedBatchDR-> ACRB_ReportTime
        //Batch : ARPT_ReportedBatchDR-> ACRB_BatchToDisplay


        //TDM owned by MAIN
        //Load 083101 Seq 160 built by MR 09/01/2007 5:32 AM EDT
        //Test performed at: MAIN
        //Entered by: NYFR2 Released by: NYFR2 09/01/2007 10:51 AM EDT
        //Reported on: 09/01/2007 10:56 AM EDT  Batch: 13

        #endregion
    }

    private string generateTurnaroundTimeNote(string dayTestSetup, string timeOfDay, string maximumLabTime)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("Day(s) Test Set-up: ");
        sb.AppendLine(dayTestSetup);
        sb.Append("Time of Day: ");
        sb.AppendLine(timeOfDay);
        sb.Append("Maximum Lab Time: ");
        sb.Append(maximumLabTime);
        return sb.ToString();

        #region Example Turnaround Time Note

        //Day(s) Test Set-up                  NY MON-SAT,  ATL/MEM TUES-SAT
        //Time of Day
        //    NEW YORK BY 3PM DAY OF RUN   ATL/MEM  6PM DAY OF RUN
        //Maximum Lab Time                    1-2 DAYS

        #endregion Example Turnaround Time Note
    }

    #endregion Supporting Methods

    #region Scaled Down Unit Properties

    #region Unit Code
    private string _unitCode;
    public string UnitCode
    {
        get { return _unitCode; }
        //set { _unitCode = value; }
    }
    #endregion Unit Code

    #region Component Unit Code
    private string _componentUnitCode;
    public string ComponentUnitCode
    {
        get { return _componentUnitCode; }
        //set { _componentUnitCode = value; }
    }
    #endregion Component Unit Code

    #region Associated Accession Number
    private string _accessionNumber;
    public string AccessionNumber
    {
        get { return _accessionNumber; }
        //set { _accessionNumber = value; }
    }
    #endregion Associated Accession Number

    #region Reporting Title
    private string _reportingTitle;
    public string ReportingTitle
    {
        get { return _reportingTitle; }
        set { _reportingTitle = value; }
    }
    #endregion  Reporting Title

    #region Tests Collection
    private System.Collections.Generic.List<LabTest> _testsOrdered;
    public System.Collections.Generic.List<LabTest> TestsOrdered
    {
        get { return _testsOrdered; }
        set { _testsOrdered = value; }
    }
    #endregion  Tests Collection

    #region Is Result Critical
    private Boolean _isResultCritical;
    public Boolean IsResultCritical
    {
        get { return _isResultCritical; }
        set { _isResultCritical = value; }
    }
    #endregion  Is Result Critical

    #region WorkList
    private String _workList;
    public String WorkList
    {
        get { return _workList; }
        set { _workList = value; }
    }

    private String _workListMessage;
    public String WorkListMessage
    {
        get { return _workListMessage; }
        set { _workListMessage = value; }
    }

    #endregion WorkList

    #region Report Notes
    private String _reportNotes;
    public String ReportNotes
    {
        get { return _reportNotes; }
        set { _reportNotes = value; }
    }

    #endregion Report Notes

    #region Turnaround Time Note
    private String _turnaroundTimeNote;
    public String TurnaroundTimeNote
    {
        get { return _turnaroundTimeNote; }
        set { _turnaroundTimeNote = value; }
    }

    #endregion Turnaround Time Note

    #region WorkList Effective Date
    private DateTime _workListEffectiveDate;
    public DateTime WorkListEffectiveDate
    {
        get { return _workListEffectiveDate; }
        set { _workListEffectiveDate = value; }
    }

    #endregion WorkList Effective Date

    #region Associated Accession Species
    private String _associatedAccessionSpeciesSex;
    public String AssociatedAccessionSpeciesSex
    {
        get { return _associatedAccessionSpeciesSex; }
        set { _associatedAccessionSpeciesSex = value; }
    }

    #endregion Associated Accession Species

    #region Canned Message
    private String _cannedMessage;
    public String CannedMessage
    {
        get { return _cannedMessage; }
        set { _cannedMessage = value; }
    }

    #endregion Canned Message

    #region Has Corrected Results
    private Boolean _hasCorrectedResults;
    public Boolean HasCorrectedResults
    {
        get { return _hasCorrectedResults; }
        set { _hasCorrectedResults = value; }
    }
    #endregion Has Corrected Results

    #region Tests Loaded
    private Boolean _testsLoaded;
    public Boolean TestsLoaded
    {
        get { return _testsLoaded; }
        set { _testsLoaded = value; }
    }
    #endregion Tests Loaded

    #endregion Scaled Down Unit Properties
}