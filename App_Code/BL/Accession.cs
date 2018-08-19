using System;
using System.Data;
using System.Configuration;

public class AccessionExtended : Accession
{
    private String dateTimeFormat;
    #region Constructors

    public AccessionExtended(String accessionNumber)
    {
        this._accessionNumber = accessionNumber;
        Int32 accessionCount = DL_Accession.AccessionCount(accessionNumber);
        this.IsValid = (accessionCount == 1) ? true : false;
    }

    #endregion Constructors

    #region Accession Methods

    public void loadDetails()
    {
        dateTimeFormat = AtlasIndia.AntechCSM.functions.getDateTimeFormat();
        if (this.IsValid)
        {
            DataTable accessionDetail = DL_Accession.getExtendedAccessionDetails(this.AccessionNumber);
            DataRow dr = accessionDetail.Rows[0];

            this.MiniLogDateTime = AtlasIndia.AntechCSM.functions.AddTimeToDateNullable(dr["MiniLogDate"].ToString(), dr["MiniLogTime"].ToString());
            this.MiniLogger = dr["MiniLoggerInitial"].ToString();
            this.MiniLoggerDispName = dr["MiniLoggerInitialDispName"].ToString();
            this.MaxiLogDateTime = AtlasIndia.AntechCSM.functions.AddTimeToDateNullable(dr["MaxiLogDate"].ToString(), dr["MaxiLogTime"].ToString());
            this.MaxiLogger = dr["MaxiLoggerInitials"].ToString();
            this.MaxiLoggerDispName = dr["MaxiLoggerInitialsDispName"].ToString();
            this.CollectedDateTime = AtlasIndia.AntechCSM.functions.AddTimeToDateNullable(dr["CollectionDateText"].ToString(), dr["CollectionTimeText"].ToString());
            this.ReceivedDateTime = AtlasIndia.AntechCSM.functions.AddTimeToDateNullable(dr["ReceivedDate"].ToString(), dr["ReceivedTime"].ToString());
            this.InternalNotes = dr["LabMessage"].ToString();
            this.ExternalNotes = dr["ReportNotes"].ToString();

            ///<Commented aagrawal 5/17/2008>
            //this.IsCritical = (dr["IsCritical"].ToString() == "N") ? false : true;
            ///</Commented aagrawal 5/17/2008>

            ///<Added aagrawal 5/17/2008>
            this.CriticalInfo = dr["CriticalInfo"].ToString();
            ///</Added aagrawal 5/17/2008>

            this.ChartNumber = dr["ChartNumber"].ToString();
            this.AutoDial = dr["AutoDial"].ToString();
            this.PetName = dr["PetName"].ToString();
            this.ClientID = dr["ClientNumber"].ToString();
            this.ClientName = dr["ClientName"].ToString();
            this.ClientMnemonic = dr["ClientMnemonic"].ToString();
            this.OwnerName = dr["OwnerName"].ToString();
            this.Sex = dr["Sex"].ToString();
            this.Species = dr["Species"].ToString();
            this.Breed = dr["Breed"].ToString();
            this.Age = dr["Age"].ToString();
            this.Doctor = dr["Doctor"].ToString();
            this.DoctorPhone = dr["DoctorPhone"].ToString();
            this.Requisition = dr["RequisitionNumber"].ToString();
            this.BillTo = dr["BillTo"].ToString();
            this.RecheckNumber = dr["RecheckNumber"].ToString();

            ///<Commented aagrawal 5/17/2008>
            //this.ZoaReq = (dr["ZoasisRequest"].ToString() == "N") ? false : true;
            ///</Commented aagrawal 5/17/2008>

            ///<Added aagrawal 5/17/2008>
            this.ZoasisRequest = dr["ZoasisRequest"].ToString();
            ///</Added aagrawal 5/17/2008>

            ///<Commented aagrawal 5/17/2008>
            //this.OrderedDateTime = AtlasIndia.AntechCSM.functions.AddTimeToDate(dr["OrderedDate"].ToString(), dr["OrderedTime"].ToString());
            ///</Commented aagrawal 5/17/2008>

            ///<Added aagrawal 5/17/2008>
            this.OrderedDateTime = this.MaxiLogDateTime;
            ///</Added aagrawal 5/17/2008>

            this.TestsOrdered = dr["TestsOrdered"].ToString();

            ///<Added aagrawal 5/17/2008>
            this.SpecimenSubmitted = dr["SpecimenSubmitted"].ToString();
            ///</Added aagrawal 5/17/2008>

            ///<Commented aagrawal 5/17/2008>
            //this.SpecimenSubmittedInformation = dr["SpecimenSubmittedInformation"].ToString();
            //this.SpecimenSubmittedVolume = dr["SpecimenSubmittedVolume"].ToString();
            ///</Commented aagrawal 5/17/2008>

            this.LabMessage = dr["LabMessage"].ToString();
            this.ReportNotes = dr["ReportNotes"].ToString();
            
            this.InquiryNotes = GetInquiryNotes(this.AccessionNumber);
            this.HasInquiryNotes = (this.InquiryNotes.Length > 0 ? true : false);
            this.ClientTimeZone = dr["ClientTimeZone"].ToString();  //SSM 20/10/2011 #114978 AntechCSM 2B - Assigning ClientTimeZone value from Database

            this.Requests = GetRequests(this.AccessionNumber);

            if (Int32.TryParse(dr["SpecimenAge"].ToString(), out _specimenAge))
            {
                // TO DO: Value is hard coded, that need to be moved to global config file and the access method also need to be placed in a class.
                Int32 SPECIMEN_VALIDITY_PERIOD = 7;
                if (this.SpecimenAge >= SPECIMEN_VALIDITY_PERIOD)
                {
                    this.NewRequestAlertMessage = "The Specimen is " + this.SpecimenAge + " Days old. New Add-on/Verification request cannot be taken.";
                }
                else
                {
                    this.NewRequestAlertMessage = "The Specimen is " + this.SpecimenAge + " Days old. New Add-on/Verification request can be taken.";
                }
            }
            else
            {
                this.NewRequestAlertMessage = "The specimen age is not specified. New Add-on/Verification request can be taken.";
            }

            this.SpecimenLocationList = DL_Accession.getSpecimenLocation(this.AccessionNumber);
            this.SpecimenLocation = AccessionExtended.getSpecimenLocation(this.SpecimenLocationList);

            #region Moved in getSpecimenLocation()
            //System.Text.StringBuilder specimenLocationInformationBuilder = new System.Text.StringBuilder();
            //string previousLabLocation = string.Empty;
            //foreach (DataRow tmpRow in this.SpecimenLocationList.Rows)
            //{
            //    string currentLabLocation = tmpRow["LabName"].ToString();
            //    if (String.Compare(currentLabLocation, previousLabLocation) != 0)
            //    {
            //        previousLabLocation = currentLabLocation;
            //        specimenLocationInformationBuilder.AppendLine();
            //        specimenLocationInformationBuilder.Append("Tube assigned to Location: ");
            //        specimenLocationInformationBuilder.AppendLine(currentLabLocation);
            //    }
            //    specimenLocationInformationBuilder.Append("Date: ");
            //    DateTime tmpLocationDate;
            //    if (DateTime.TryParse(tmpRow["LocationDate"].ToString(), out tmpLocationDate))
            //    {
            //        specimenLocationInformationBuilder.Append(tmpLocationDate.ToString("MM/dd/yyyy"));
            //    }
            //    specimenLocationInformationBuilder.Append("\t");
            //    specimenLocationInformationBuilder.Append(" Rack: ");
            //    specimenLocationInformationBuilder.Append(tmpRow["LocationRack"].ToString());
            //    specimenLocationInformationBuilder.Append("\t");
            //    specimenLocationInformationBuilder.Append(" Position: ");
            //    specimenLocationInformationBuilder.Append(tmpRow["LocationPosition"].ToString());
            //    specimenLocationInformationBuilder.Append("\t");
            //    specimenLocationInformationBuilder.Append(" Scanned By: ");
            //    specimenLocationInformationBuilder.AppendLine(tmpRow["User"].ToString());
            //}
            //if (specimenLocationInformationBuilder.Length > 1)
            //{
            //    specimenLocationInformationBuilder.Remove(0, 2);
            //}
            //this.SpecimenLocation = specimenLocationInformationBuilder.ToString(); 
            #endregion Moved in getSpecimenLocation()


            this.UnitsOrdered = DL_Accession.getUnitsOrdered(this.AccessionNumber);
            this.AccessionOrderUnitsList = new System.Collections.Generic.List<Unit_ScaledDown>();
            foreach (DataRow unitRow in this.UnitsOrdered.Rows)
            {
                this.AccessionOrderUnitsList.Add(new Unit_ScaledDown(this.AccessionNumber, this.Species, this.Sex, unitRow, false));
            }

            this.SatelliteTable = DL_Accession.getSatelliteInformation(this.AccessionNumber);

            System.Collections.Generic.Dictionary<string, string> labFilter = new System.Collections.Generic.Dictionary<string, string>();
            System.Collections.Generic.Dictionary<string, string> userFilter = new System.Collections.Generic.Dictionary<string, string>();

            System.Text.StringBuilder satelliteInformationBuilder = new System.Text.StringBuilder();
            System.Text.StringBuilder labListBuilder = new System.Text.StringBuilder();
            System.Text.StringBuilder userListBuilder = new System.Text.StringBuilder();

            satelliteInformationBuilder.Append("Unit Code");
            satelliteInformationBuilder.Append("\t");
            satelliteInformationBuilder.AppendLine("Reporting Title");
            satelliteInformationBuilder.AppendLine("-----------------------------------");

            foreach (DataRow satelliteRow in this.SatelliteTable.Rows)
            {
                satelliteInformationBuilder.Append(satelliteRow["UnitCode"].ToString());
                satelliteInformationBuilder.Append("\t");

                ///<Commented aagrawal 5/17/2008>
                //satelliteInformationBuilder.AppendLine(satelliteRow["UnitTitle"].ToString());
                ///</Commented aagrawal 5/17/2008>

                ///<Added aagrawal 5/17/2008>
                satelliteInformationBuilder.Append(satelliteRow["UnitTitle"].ToString());
                satelliteInformationBuilder.Append(" (");
                satelliteInformationBuilder.Append(satelliteRow["UnitSpecimenType"].ToString());
                satelliteInformationBuilder.AppendLine(")");
                ///</Added aagrawal 5/17/2008>

                satelliteInformationBuilder.Append("\t");

                ///<Commented aagrawal 5/20/2008>
                //DateTime receivedDateTime;
                //receivedDateTime = AtlasIndia.AntechCSM.functions.AddTimeToDate(satelliteRow["ReceivedDate"].ToString(), satelliteRow["ReceivedTime"].ToString());
                //satelliteInformationBuilder.Append(receivedDateTime.ToString("MM/dd/yyyy HH:mm"));
                ///</Commented aagrawal 5/20/2008>

                ///<Added aagrawal 5/20/2008>
                DateTime? receivedDateTime = AtlasIndia.AntechCSM.functions.AddTimeToDateNullable(satelliteRow["ReceivedDate"].ToString(), satelliteRow["ReceivedTime"].ToString());
                satelliteInformationBuilder.Append((receivedDateTime.HasValue) ? receivedDateTime.Value.ToString(dateTimeFormat) : String.Empty);
                ///</Added aagrawal 5/20/2008>

                satelliteInformationBuilder.Append("\t");
                satelliteInformationBuilder.Append(satelliteRow["ReceivedMode"].ToString());
                satelliteInformationBuilder.Append(" at ");
                satelliteInformationBuilder.Append(satelliteRow["LabID"].ToString());
                satelliteInformationBuilder.Append("\t by ");
                satelliteInformationBuilder.AppendLine(satelliteRow["UserID"].ToString());
                satelliteInformationBuilder.AppendLine();

                if (!labFilter.ContainsKey(satelliteRow["LabID"].ToString()))
                {
                    labFilter.Add(satelliteRow["LabID"].ToString(), satelliteRow["LabName"].ToString());
                    labListBuilder.AppendLine("\t" + satelliteRow["LabID"].ToString() + " = " + satelliteRow["LabName"].ToString());
                }
                if (!userFilter.ContainsKey(satelliteRow["UserID"].ToString()))
                {
                    userFilter.Add(satelliteRow["UserID"].ToString(), satelliteRow["UserFirstLastName"].ToString());
                    userListBuilder.AppendLine("\t" + satelliteRow["UserID"].ToString() + " = " + satelliteRow["UserFirstLastName"].ToString());
                }
            }
            satelliteInformationBuilder.AppendLine("Company Key:");
            satelliteInformationBuilder.AppendLine(labListBuilder.ToString());

            satelliteInformationBuilder.AppendLine("Username Key:");
            satelliteInformationBuilder.AppendLine(userListBuilder.ToString());

            this.SatelliteInformation = satelliteInformationBuilder.ToString();
        }
    }

    public Boolean updateInquiryNotes(DateTime noteEntryDateTime, String userInitials, String note)
    {
        if (DL_Accession.addInquiryNote(this.AccessionNumber, userInitials, note) > 0)
        {
            this.InquiryNotes = GetInquiryNotes(this.AccessionNumber);
            this.HasInquiryNotes = true;
            return true;
        }
        else
        {
            return false;
        }
    }

    public Boolean updateAddOnVerificationRequests(String callerName, String changeRequestType, String testsAddOnVerify, String eMail, String labLocation, String specialInstructions, String UserID, String UserLab,String ReasonForReq, String PathologistRequested, String OriginalPathalogist, String CheckTubeType)
    {
    String[] arr = DL_Accession.updateAddOnVerificationRequests(this.AccessionNumber, callerName, changeRequestType, testsAddOnVerify, eMail, labLocation, specialInstructions, this.ClientMnemonic, UserID, UserLab, ReasonForReq, PathologistRequested, OriginalPathalogist, CheckTubeType).Split('^');
        if (arr.Length == 4)
        {
            DataRow dr = this.Requests.NewRow();
            dr["ConfirmationNumber"] = arr[0];
            dr["CallerName"] = callerName;
            dr["RequestDate"] = arr[1];
            dr["RequestTime"] = arr[2];
            dr["RequestType"] = changeRequestType;
            dr["Email"] = eMail;
            dr["SpecialInstructions"] = specialInstructions;
            dr["Tests"] = testsAddOnVerify;
            dr["LabID"] = labLocation;
            dr["LabLocation"] = arr[3];
            dr["ReasonForReq"] = ReasonForReq;
            dr["PathalogistRequested"] = PathologistRequested;
            dr["OriginalPathalogist"] = OriginalPathalogist;
            dr["CheckTubeType"] = CheckTubeType;
            this.Requests.Rows.InsertAt(dr, 0);
            this.Requests.AcceptChanges();
            return true;
        }
        return false;        
    }

    public Boolean updateAddOnVerificationRequests(String confirmationNumber, String callerName, String changeRequestType, String testsAddOnVerify, String eMail, String labLocation, String specialInstructions, String userID, String ReasonForReq, String PathologistRequested, String OriginalPathalogist,String CheckTubeType, bool isModify)
    {
    if (DL_Accession.updateAddOnVerificationRequests(confirmationNumber, callerName, changeRequestType, eMail, labLocation, specialInstructions, testsAddOnVerify, userID, ReasonForReq, PathologistRequested, OriginalPathalogist, CheckTubeType, isModify) > 0)
        {
            this.Requests = GetRequests(this.AccessionNumber);
            return true;
        }
        else
        {
            return false;
        }
    }

    public Boolean sendAutoDialFax(String userName)
    {
        String message = DL_Accession.sendAutoDialFax(this.AccessionNumber, this.ClientMnemonic, this.AutoDial, userName);
        if (message.Length > 0)
        {
            return true;
        }
        return false;
    }

    public Boolean printInternalReport(String printerName, String userName)
    {
        String message = DL_Accession.printInternalReport(this.AccessionNumber, printerName, userName);
        if (message.Length > 0)
        {
            return true;
        }
        return false;
    }

    public String sendAltFax(String autoDial, String fax, String userName)
    {
        return DL_Accession.sendAltFax(this.AccessionNumber, this.ClientMnemonic, autoDial, fax, userName);
    }

    public Boolean printClientReport(String printerName, String userName)
    {
        String message = DL_Accession.printClientReport(this.AccessionNumber, printerName, userName);
        if (message.Length > 0)
        {
            return true;
        }
        return false;
    }
    
    public static String getSpecimenLocation(DataTable specimenLocationList)
    {
        System.Text.StringBuilder specimenLocationInformationBuilder = new System.Text.StringBuilder();
        String previousLabLocation = String.Empty;
        foreach (DataRow tmpRow in specimenLocationList.Rows)
        {
            String currentLabLocation = tmpRow["LabName"].ToString();
            if (String.Compare(currentLabLocation, previousLabLocation) != 0)
            {
                previousLabLocation = currentLabLocation;
                specimenLocationInformationBuilder.AppendLine();
                specimenLocationInformationBuilder.Append("Tube assigned to Location: ");
                specimenLocationInformationBuilder.AppendLine(currentLabLocation);
            }
            specimenLocationInformationBuilder.Append("Date: ");
            DateTime tmpLocationDate;
            if (DateTime.TryParse(tmpRow["LocationDate"].ToString(), out tmpLocationDate))
            {
                specimenLocationInformationBuilder.Append(tmpLocationDate.ToString("MM/dd/yyyy"));
            }
            specimenLocationInformationBuilder.Append("\t");
            specimenLocationInformationBuilder.Append(" Rack: ");
            specimenLocationInformationBuilder.Append(tmpRow["LocationRack"].ToString());
            specimenLocationInformationBuilder.Append("\t");
            specimenLocationInformationBuilder.Append(" Position: ");
            specimenLocationInformationBuilder.Append(tmpRow["LocationPosition"].ToString());
            specimenLocationInformationBuilder.Append("\t");
            specimenLocationInformationBuilder.Append(" Scanned By: ");
            specimenLocationInformationBuilder.AppendLine(tmpRow["UserID"].ToString());
        }
        if (specimenLocationInformationBuilder.Length > 1)
        {
            specimenLocationInformationBuilder.Remove(0, 2);
        }
        return specimenLocationInformationBuilder.ToString();
    }

    public static String getSpecimenLocation(String accessionNumber)
    {
        return AccessionExtended.getSpecimenLocation(DL_Accession.getSpecimenLocation(accessionNumber));        
    }

    public static void logAccessionDetailsView(String accountNumber, String userID, String lab, String accessionNumber)
    {
        DL_Accession.logAccessionDetailsView(accountNumber, userID, lab, accessionNumber);
    }

    /// <summary>
    /// Get Inquiry Notes for the accession
    /// </summary>
    /// <param name="accessionNumber"></param>
    /// <returns>Formated inquiry note</returns>
    public static string GetInquiryNotes(string accessionNumber)
    {
        return DL_Accession.getInquiries(accessionNumber).Replace("\n\r",Environment.NewLine);
    }

    public DataTable GetRequests(String accessionNumber)
    {
        DataTable requests = DL_Accession.getRequests(this.AccessionNumber);

        //Formating the data (handling newline which is ; in Antrim).
        for (int intCnt = 0; intCnt < requests.Rows.Count; intCnt++)
        {
            requests.Rows[intCnt]["SpecialInstructions"] = requests.Rows[intCnt]["SpecialInstructions"].ToString().Replace("|", "\n\r").Replace(";", "\n\r");
        }
        return requests;
    }
    #endregion

    #region Supporting Methods
    #endregion

    #region Accession Properties

    #region Mini Log Date / Time
    /// <summary>
    /// Mini Log Date Time
    /// </summary>
    private DateTime? _miniLogDateTime;
    public DateTime? MiniLogDateTime
    {
        get { return _miniLogDateTime; }
        set { _miniLogDateTime = value; }
    }
    #endregion Mini Log Date / Time

    #region Mini Logged By User
    /// <summary>
    /// Initials of the User who logged Mini
    /// </summary>
    private String _miniLogger;
    public String MiniLogger
    {
        get { return _miniLogger; }
        set { _miniLogger = value; }
    }

    private String _miniLoggerDispName;
    public String MiniLoggerDispName
    {
        get { return _miniLoggerDispName; }
        set { _miniLoggerDispName = value; }
    }
    #endregion Mini Logged By User

    #region Maxi Log Date / Time
    /// <summary>
    /// Maxi Log Date / Time
    /// </summary>
    private DateTime? _maxiLogDateTime;
    public DateTime? MaxiLogDateTime
    {
        get { return _maxiLogDateTime; }
        set { _maxiLogDateTime = value; }
    }
    #endregion Maxi Log Date / Time

    #region Maxi Logged By User
    /// <summary>
    /// Initials of the User who logged Maxi
    /// </summary>
    private String _maxiLogger;
    public String MaxiLogger
    {
        get { return _maxiLogger; }
        set { _maxiLogger = value; }
    }

    private String _maxiLoggerDispName;
    public String MaxiLoggerDispName
    {
        get { return _maxiLoggerDispName; }
        set { _maxiLoggerDispName = value; }
    }
    #endregion Maxi Logged By User

    #region Mini Collected Date Time
    /// <summary>
    /// Collection Date Time of Mini
    /// </summary>
    private DateTime? _miniCollectedDateTime;
    public DateTime? MiniCollectedDateTime
    {
        get { return _miniCollectedDateTime; }
        set { _miniCollectedDateTime = value; }
    }
    #endregion Mini Collected Time

    #region Mini Received Date / Time
    /// <summary>
    /// Mini Received Date / Time
    /// </summary>
    private DateTime? _receivedDateTime;
    public DateTime? ReceivedDateTime
    {
        get { return _receivedDateTime; }
        set { _receivedDateTime = value; }
    }
    #endregion Mini Received Date / Time

    #region Maxi Internal Notes
    /// <summary>
    /// Internal Notes (Mini-Maxi Audit)
    /// </summary>
    private String _internalNotes;
    public String InternalNotes
    {
        get { return _internalNotes; }
        set { _internalNotes = value; }
    }
    #endregion Maxi Internal Notes

    #region Mini External Notes
    /// <summary>
    /// External Notes (Mini-Maxi Audit)
    /// </summary>
    private String _externalNotes;
    public String ExternalNotes
    {
        get { return _externalNotes; }
        set { _externalNotes = value; }
    }
    #endregion Mini External Notes

    #region Inquiry Notes
    /// <summary>
    /// Existing/Updated Inquiry Notes
    /// </summary>
    private String _inquiryNotes;
    public String InquiryNotes
    {
        get { return _inquiryNotes; }
        set { _inquiryNotes = value; }
    }
    #endregion Inquiry Notes

    #region AddOn/Verify Requests
    //private System.Collections.Generic.IList<Request> _requests = new System.Collections.Generic.IList<Request>();
    /// <summary>
    /// Collection of the AddOn/Verificatin requests for the accession [Not Used Currently]
    /// </summary>
    //public System.Collections.Generic.IList<Request> Requests
    //{
    //    get { return _requests; }
    //    set { _requests = value; }
    //}


    private DataTable _requests;
    /// <summary>
    /// Collection of Requests
    /// </summary>
    public DataTable Requests
    {
        get { return _requests; }
        set { _requests = value; }
    }

    #endregion AddOn/Verify Requests

    #region Specimen Location

    private DataTable _specimenLocationList;
    private DataTable SpecimenLocationList
    {
        get { return _specimenLocationList; }
        set { _specimenLocationList = value; }
    }

    private String _specimenLocation;
    public String SpecimenLocation
    {
        get { return _specimenLocation; }
        set { _specimenLocation = value; }
    }

    #endregion Specimen Location

    #region Tests Ordered

    /// <summary>
    /// Units Odered for the Accession Number
    /// </summary>
    private DataTable _unitsOrdered;
    private DataTable UnitsOrdered
    {
        get { return _unitsOrdered; }
        set { _unitsOrdered = value; }
    }

    private System.Collections.Generic.List<Unit_ScaledDown> _accessionOrderUnitsList;
    public System.Collections.Generic.List<Unit_ScaledDown> AccessionOrderUnitsList
    {
        get { return _accessionOrderUnitsList; }
        set { _accessionOrderUnitsList = value; }
    }

    #endregion Tests Ordered

    #region Satellite Information

    private DataTable _satelliteTable;
    private DataTable SatelliteTable
    {
        get { return _satelliteTable; }
        set { _satelliteTable = value; }
    }

    private String _satelliteInformation;
    public String SatelliteInformation
    {
        get { return _satelliteInformation; }
        set { _satelliteInformation = value; }
    }

    #endregion Satellite Information

    #endregion

    #region Supporting Properties

    #region AddOn/Verification Message

    private Int32 _specimenAge;
    public Int32 SpecimenAge
    {
        get { return _specimenAge; }
        set { _specimenAge = value; }
    }

    private String _newRequestAlertMessage;
    public String NewRequestAlertMessage
    {
        get { return _newRequestAlertMessage; }
        set { _newRequestAlertMessage = value; }
    }

    #endregion AddOn/Verification Message

    #endregion Supporting Properties
}

public class Accession
{
    #region Constructors

    public Accession()
    {
        //// Kept Empty Intentionally
    }

    public Accession(string accessionNumber)
    {
        this._accessionNumber = accessionNumber;
        DataTable accessionDetail = DL_Accession.getAccessionDetails(accessionNumber);
        if (accessionDetail == null)
        {
            this.IsValid = false;
        }
        else if (accessionDetail.Rows.Count < 1)
        {
            this.IsValid = false;
        }
        else if (accessionDetail.Rows.Count > 1)
        {
            this.IsValid = false;
        }
        else
        {
            this.IsValid = true;

            DataRow dr = accessionDetail.Rows[0];
            this.CollectedDateTime = AtlasIndia.AntechCSM.functions.AddTimeToDateNullable(dr["CollectionDateText"].ToString(), dr["CollectionTimeText"].ToString());
            ///<Commented aagrawal 5/17/2008>
            //this.IsCritical = (dr["IsCritical"].ToString() == "N") ? false : true;
            ///</Commented aagrawal 5/17/2008>
            ///<Added aagrawal 5/17/2008>
            this.CriticalInfo = dr["CriticalInfo"].ToString();
            ///</Added aagrawal 5/17/2008>
            this.ChartNumber = dr["ChartNumber"].ToString();
            this.AutoDial = dr["AutoDial"].ToString();
            this.PetName = dr["PetName"].ToString();
            this.ClientID = dr["ClientNumber"].ToString();
            this.ClientName = dr["ClientName"].ToString();
            this.ClientMnemonic = dr["ClientMnemonic"].ToString();
            this.OwnerName = dr["OwnerName"].ToString();
            this.OwnerLastName = dr["OwnerLastName"].ToString();
            this.Sex = dr["Sex"].ToString();
            this.Species = dr["Species"].ToString();
            this.Breed = dr["Breed"].ToString();
            this.Age = dr["Age"].ToString();
            this.Doctor = dr["Doctor"].ToString();
            this.DoctorPhone = dr["DoctorPhone"].ToString();
            this.Requisition = dr["RequisitionNumber"].ToString();
            this.BillTo = dr["BillTo"].ToString();
            this.RecheckNumber = dr["RecheckNumber"].ToString();

            ///<Commented aagrawal 5/17/2008>
            //this.ZoaReq = (dr["ZoasisRequest"].ToString() == "N") ? false : true;
            ///</Commented aagrawal 5/17/2008>

            ///<Added aagrawal 5/17/2008>
            this.ZoasisRequest = dr["ZoasisRequest"].ToString();
            ///</Added aagrawal 5/17/2008>

            this.OrderedDateTime = AtlasIndia.AntechCSM.functions.AddTimeToDateNullable(dr["MiniLogDate"].ToString(), dr["MiniLogTime"].ToString());
            this.TestsOrdered = dr["TestsOrdered"].ToString();

            ///<Added aagrawal 5/17/2008>
            this.SpecimenSubmitted = dr["SpecimenSubmitted"].ToString();
            ///</Added aagrawal 5/17/2008>

            ///<Commented aagrawal 5/17/2008>
            //this.SpecimenSubmittedInformation = dr["SpecimenSubmittedInformation"].ToString();
            //this.SpecimenSubmittedVolume = dr["SpecimenSubmittedVolume"].ToString();
            ///</Commented aagrawal 5/17/2008>

            this.LabMessage = dr["LabMessage"].ToString();
            this.ReportNotes = dr["ReportNotes"].ToString();
            this.HasInquiryNotes = (dr["HasInquiryNotes"].ToString().ToUpper() == "NO" || dr["HasInquiryNotes"].ToString().ToUpper() == "N") ? false : true;
        }
    }

    #endregion Constructors

    #region Accession Properties

    #region Accession Number
    /// <summary>
    /// Accession Number
    /// </summary>
    protected String _accessionNumber;
    public String AccessionNumber
    {
        get { return _accessionNumber; }
        //set { _accessionNumber = value; }
    }
    #endregion Accession Number

    #region Collected Date Time
    /// <summary>
    /// Collection Date Time
    /// </summary>
    private DateTime? _collectedDateTime;
    public DateTime? CollectedDateTime
    {
        get { return _collectedDateTime; }
        set { _collectedDateTime = value; }
    }
    #endregion Collected Date Time

    #region Criticality Information

    /// <summary>
    /// Is Critical
    /// Note: following property is obsolete now. It should be replaced everywhere CriticalInfo Property.
    /// </summary>
    #region Obsolete Code

    ///<Commented aagrawal 5/17/2008>
    //private Boolean _isCritical;
    //public Boolean IsCritical
    //{
    //    get { return _isCritical; }
    //    set { _isCritical = value; }
    //}
    ///</Commented aagrawal 5/17/2008>

    #endregion Obsolete Code

    /// <summary>
    /// Criticality Information
    /// </summary>
    private String _criticalInfo;
    public String CriticalInfo
    {
        get { return _criticalInfo; }
        set { _criticalInfo = value; }
    }
    #endregion Criticality Information

    #region Chart Number
    /// <summary>
    /// Chart Number
    /// </summary>
    private String _chartNumber;
    public String ChartNumber
    {
        get { return _chartNumber; }
        set { _chartNumber = value; }
    }
    #endregion Chart Number

    #region Auto Dial
    /// <summary>
    /// Auto Dial
    /// </summary>
    private String _autoDial;
    public String AutoDial
    {
        get { return _autoDial; }
        set { _autoDial = value; }
    }
    #endregion Auto Dial

    #region Pet Name
    /// <summary>
    /// Pet Name
    /// </summary>
    private String _petName;
    public String PetName
    {
        get { return _petName; }
        set { _petName = value; }
    }
    #endregion Pet Name

    #region Client ID
    /// <summary>
    /// Client ID
    /// </summary>
    private String _clientID;
    public String ClientID
    {
        get { return _clientID; }
        set { _clientID = value; }
    }
    #endregion Client ID

    #region Client Name
    /// <summary>
    /// Client Name
    /// </summary>
    private String _clientName;
    public String ClientName
    {
        get { return _clientName; }
        set { _clientName = value; }
    }
    #endregion Client Name

    #region Client Mnemonic
    /// <summary>
    /// Client Mnemonic
    /// </summary>
    private String _clientMnemonic;
    public String ClientMnemonic
    {
        get { return _clientMnemonic; }
        set { _clientMnemonic = value; }
    }
    #endregion Client Mnemonic

    #region Patient Name
    /// <summary>
    /// Patient Name (Pet Owner)
    /// </summary>
    private String _ownerName;
    public String OwnerName
    {
        get { return _ownerName; }
        set { _ownerName = value; }
    }
    #endregion Patient Name

    private string _ownerLastName;

    public string OwnerLastName
    {
        get { return _ownerLastName; }
        set { _ownerLastName = value; }
    }

    #region Sex
    /// <summary>
    /// Sex of Pet
    /// </summary>
    private String _sex;
    public String Sex
    {
        get { return _sex; }
        set { _sex = value; }
    }
    #endregion Sex

    #region Species
    /// <summary>
    /// Species of Pet
    /// </summary>
    private String _species;
    public String Species
    {
        get { return _species; }
        set { _species = value; }
    }
    #endregion Species

    #region Breed
    /// <summary>
    /// Breed of Pet
    /// </summary>
    private String _breed;
    public String Breed
    {
        get { return _breed; }
        set { _breed = value; }
    }
    #endregion Breed

    #region Age
    /// <summary>
    /// Age of Pet
    /// </summary>
    private String _age;
    public String Age
    {
        get { return _age; }
        set { _age = value; }
    }
    #endregion Age

    #region Doctor
    /// <summary>
    /// Doctor
    /// </summary>
    private String _doctor;
    public String Doctor
    {
        get { return _doctor; }
        set { _doctor = value; }
    }
    #endregion Doctor

    #region Phone of Doctor
    /// <summary>
    /// Phone Number of Doctor
    /// </summary>
    private String _doctorPhone;
    public String DoctorPhone
    {
        get { return _doctorPhone; }
        set { _doctorPhone = value; }
    }
    #endregion Phone of Doctor

    #region Requisition
    /// <summary>
    /// Requisition
    /// </summary>
    private String _requisition;
    public String Requisition
    {
        get { return _requisition; }
        set { _requisition = value; }
    }
    #endregion Requisition

    #region Bill To
    /// <summary>
    /// Bill To
    /// </summary>
    private String _billTo;
    public String BillTo
    {
        get { return _billTo; }
        set { _billTo = value; }
    }
    #endregion Bill To

    #region Recheck #
    /// <summary>
    /// Recheck #
    /// </summary>
    private String _recheckNumber;
    public String RecheckNumber
    {
        get { return _recheckNumber; }
        set { _recheckNumber = value; }
    }
    #endregion Recheck #

    #region Zoasis Request
    /// <summary>
    /// Zoasis Request
    /// </summary>
    //
    #region Obsolete Code

    ///<Commented aagrawal 5/17/2008>
    //private Boolean _zoaReq;
    //public Boolean ZoaReq
    //{
    //    get { return _zoaReq; }
    //    set { _zoaReq = value; }
    //}
    ///</Commented aagrawal 5/17/2008>

    #endregion Obsolete Code

    ///<Added aagrawal 5/17/2008>
    private String _zoasisRequest;
    public String ZoasisRequest
    {
        get { return _zoasisRequest; }
        set { _zoasisRequest = value; }
    }
    ///</Added aagrawal 5/17/2008>

    #endregion Zoasis Request

    #region Ordered Date/Time
    /// <summary>
    /// Ordered Date/Time
    /// </summary>
    private DateTime? _orderedDateTime;
    public DateTime? OrderedDateTime
    {
        get { return _orderedDateTime; }
        set { _orderedDateTime = value; }
    }
    #endregion Ordered Date/Time

    #region Tests Ordered
    /// <summary>
    /// Tests Ordered
    /// </summary>
    private String _testsOrdered;
    public String TestsOrdered
    {
        get { return _testsOrdered; }
        set { _testsOrdered = value; }
    }
    #endregion Tests Ordered

    ///<Added aagrawal 5/17/2008>
    #region Specimens Submitted
    /// <summary>
    /// Specimens Submitted
    /// </summary>
    private String _specimenSubmitted;
    public String SpecimenSubmitted
    {
        get { return _specimenSubmitted; }
        set { _specimenSubmitted = value; }
    }
    #endregion Specimens Submitted
    ///</Added aagrawal 5/17/2008>

    ///<Commented aagrawal 5/17/2008>
    //#region Specimens Submitted Information
    ///// <summary>
    ///// Specimens Submitted Information
    ///// </summary>
    //private String _specimenSubmittedInformation;
    //public String SpecimenSubmittedInformation
    //{
    //    get { return _specimenSubmittedInformation; }
    //    set { _specimenSubmittedInformation = value; }
    //}
    //#endregion Specimens Submitted Information

    //#region Specimens Submitted Volume
    ///// <summary>
    ///// Specimens Submitted Volume
    ///// </summary>
    //private String _specimenSubmittedVolume;
    //public String SpecimenSubmittedVolume
    //{
    //    get { return _specimenSubmittedVolume; }
    //    set { _specimenSubmittedVolume = value; }
    //}
    //#endregion Specimens Submitted Volume
    ///</Commented aagrawal 5/17/2008>

    #region Lab Message
    /// <summary>
    /// Lab Message
    /// </summary>
    private String _labMessage;
    public String LabMessage
    {
        get { return _labMessage; }
        set { _labMessage = value; }
    }
    #endregion Lab Message

    #region Report Notes
    /// <summary>
    /// Report Notes
    /// </summary>
    private String _reportNotes;
    public String ReportNotes
    {
        get { return _reportNotes; }
        set { _reportNotes = value; }
    }
    #endregion Report Notes

    #region Has Inquiry Notes
    /// <summary>
    /// Value is 'true' if the Inquiry Notes available.
    /// </summary>
    private Boolean _hasInquiryNotes;
    public Boolean HasInquiryNotes
    {
        get { return _hasInquiryNotes; }
        set { _hasInquiryNotes = value; }
    }
    #endregion Has Inquiry Notes

    #endregion

    #region Supporting Properties

    #region Is Accession Valid
    private Boolean _isValid = false;
    public Boolean IsValid
    {
        get { return _isValid; }
        set { _isValid = value; }
    }
    #endregion Is Accession Valid

    #endregion Supporting Properties

    #region ClientTimeZone
    //+SSM 20/10/2011 #114978 AntechCSM 2B - Added ClientTimeZone property
    private string _ClientTimeZone = string.Empty;

    public string ClientTimeZone
    {
        get { return _ClientTimeZone; }
        set { _ClientTimeZone = value; }
    }
    //-SSM
    #endregion
}

/// <summary>
/// Represents the AddOn/Verify request for an Accession
/// </summary>
public class Request
{
    #region Constructors

    public Request(string confirmationNumber, string callerName, DateTime requestDateTime, string requestType, string email, string labLocation, string specialInstructions, string requestedTests)
    {
        this._confirmationNumber = confirmationNumber;
        this.CallerName = callerName;
        this.AddOnDateTime = requestDateTime;
        this.RequestType = requestType;
        this.Email = email;
        this.LabLocation = labLocation;
        this.SpecialInstructions = specialInstructions;
        this.TestsRequested = requestedTests;
    }

    public Request()
    {
        //
    }

    public Request(string confirmationNumber)
    {
        this._confirmationNumber = confirmationNumber;
    }

    #endregion

    #region Request Properties

    #region Confirmation Number
    private String _confirmationNumber;
    public String ConfirmationNumber
    {
        get { return _confirmationNumber; }
        //set { _confirmationNumber = value; }
    }
    #endregion Confirmation Number

    #region Caller Name
    private String _callerName;
    public String CallerName
    {
        get { return _callerName; }
        set { _callerName = value; }
    }
    #endregion Caller Name

    #region Request Date/Time
    private DateTime _addOnDateTime;
    public DateTime AddOnDateTime
    {
        get { return _addOnDateTime; }
        set { _addOnDateTime = value; }
    }
    #endregion Request Date/Time

    #region Request Type
    private String _requestType;
    public String RequestType
    {
        get { return _requestType; }
        set { _requestType = value; }
    }
    #endregion Request Type

    #region Email
    private String _email;
    public String Email
    {
        get { return _email; }
        set { _email = value; }
    }
    #endregion Email

    #region Lab Location
    private String _labLocation;
    public String LabLocation
    {
        get { return _labLocation; }
        set { _labLocation = value; }
    }
    #endregion Lab Location

    #region Special Instructions
    private String _specialInstructions;
    public String SpecialInstructions
    {
        get { return _specialInstructions; }
        set { _specialInstructions = value; }
    }
    #endregion Special Instructions

    #region Requested Tests for AddOn/Verify
    private String _testsRequested;
    public String TestsRequested
    {
        get { return _testsRequested; }
        set { _testsRequested = value; }
    }
    #endregion Requested Tests for AddOn/Verify

    #endregion Request Properties
}