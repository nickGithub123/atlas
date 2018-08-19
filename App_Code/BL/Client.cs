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

public class Client
{
    #region Client Constructors

    public Client()
    {
        //
    }

    public Client(String clientID)
    {
        this._ID = clientID;
        DataTable clientData = null;
        if (clientID.Length > 0)
        {
            clientData = DL_Client.getClientDetails(clientID);
        }
        if (clientData == null)
        {
            this.IsValid = false;
        }
        else if (clientData.Rows.Count < 1)
        {
            this.IsValid = false;
        }
        else if (clientData.Rows.Count > 1)
        {
            this.IsValid = false;
        }
        else
        {
            this.IsValid = true;
            this.NoOfRecentIssues = 5;

            DataRow dr = clientData.Rows[0];
            this._name = dr["CLF_CLNAM"].ToString();
            this._mnemonic = dr["CLF_CLMNE"].ToString();
            this._phoneNumber = dr["CLF_CLPHN"].ToString();
            this._address.Address1 = dr["CLF_CLAD1"].ToString();
            this._address.Address2 = dr["CLF_CLAD2"].ToString();
            this._address.Address3 = dr["CLF_CLAD3"].ToString();
            this._address.Address4 = dr["CLF_CLAD4"].ToString();
            this._salesTerritory = dr["CLF_CLTER"].ToString();
            this._salesRepresentative = dr["SALES_REP"].ToString();
            this._autodialGroups = dr["AutoDial_Group"].ToString();
            this._zoasisNumber = dr["ZOASIS_NUMBER"].ToString();
            this._routeStop = dr["CLF_CLRTS"].ToString();
            this._location = dr["LOCATION"].ToString();
            this._alphaName = dr["CLF_CLANAM"].ToString();
            this._sortKey = dr["CLF_CLSRT"].ToString();
            this._afterHours = dr["CLF_CLAPH"].ToString();
            this._fax = dr["CLF_CLFAX"].ToString();
            this._ofcContact = dr["CLF_CLCON"].ToString();
            this._description = dr["CLF_CLTYP"].ToString();
            this._reportingPartials = dr["CLF_PART"].ToString();
            this._reportMicroPartials = dr["CLF_PMB"].ToString();
            this._comprehensiveFinals = dr["CLF_PCOMP"].ToString();
            this._customUnitCodes = dr["CustomUnitCode"].ToString();
            this._orderEntryFlashMessage = dr["CLF_FLASH"].ToString();
            this._orderEntryInstruction = dr["OEMOEMSG"].ToString();
            //this._region = dr["Region"].ToString();
            this._salesRepExtension = dr["SalesRepExt"].ToString();
            this._salesRepEmail = dr["SalesRepEmail"].ToString();
            //+SSM 02/11/2011 #Issue115785 Migrating SalesRep and RelatedChanges
            this._salesRepCellPh = dr["SalesRepCellPh"].ToString();
            this._salesLastModified = dr["LastUpdated"].ToString();
            this._salesTerritoryMgr = dr["SalesRepMgrName"].ToString();
            this._salesTerritoryMgrCellPh = dr["SalesRepMgrCellPh"].ToString();
            this._salesTerritoryMgrEmail = dr["SalesRepMgrEmail"].ToString();
            this._salesTerritoryMgrExtension = dr["SalesRepMgrExt"].ToString();
            //-SSM
            this._accountNotes = dr["AccountNotes"].ToString();
            this._priceInqPasswd = dr["PriceInqPasswd"].ToString();
            if (dr["ClientIsHot"].ToString().ToUpper() == "Y")
            {
                this._isHot = true;
            }
            else
            {
                this._isHot = false;
            }
            if (dr["ClientIsAllied"].ToString().ToUpper() == "Y")
            {
                this._isAllied = true;
            }
            else
            {
                this._isAllied = false;
            }
            this._isNew = (dr["ClientIsNew"].ToString().ToUpper() == "Y");
            string[] arrClientRevenue = dr["ClientRevenue"].ToString().Split('^');
            this._revenueFirstMonth = (arrClientRevenue[0].Length ==0 ? "0" : arrClientRevenue[0]);
            this._revenueSecondMonth = (arrClientRevenue[1].Length == 0 ? "0" : arrClientRevenue[1]);
            this._revenueThirdMonth = (arrClientRevenue[2].Length == 0 ? "0" : arrClientRevenue[2]);
            this._inquiryMessages = dr["InquiryMessages"].ToString();

            this._clientTimeZone = dr["ClientTimeZone"].ToString();
            //this._country = dr["Country"].ToString();
            String country;
            getClientCountry(this.ID,out country);
            this._country = country;
        }
    }

    #endregion Client Constructors

    /// <summary>
    /// Client Mnemonic is the rowID, so it's fesiable to fetch client
    /// details based on mnemonic where mnemonic is available.
    /// </summary>
    /// <param name="clientMnemonic">Client Mnemonic</param>
    public void GetClientDetailsByMnemonic(String clientMnemonic)
    {
        DataTable clientData = null;
        if (clientMnemonic.Length > 0)
        {
            clientData = DL_Client.getClientDetailsByMnemonic(clientMnemonic);
        }
        if (clientData == null)
        {
            this.IsValid = false;
        }
        else if (clientData.Rows.Count < 1)
        {
            this.IsValid = false;
        }
        else if (clientData.Rows.Count > 1)
        {
            this.IsValid = false;
        }
        else
        {
            this.IsValid = true;
            this.NoOfRecentIssues = 5;

            DataRow dr = clientData.Rows[0];
            this._ID = dr["CLF_CLNUM"].ToString();
            this._name = dr["CLF_CLNAM"].ToString();
            this._mnemonic = dr["CLF_CLMNE"].ToString();
            this._phoneNumber = dr["CLF_CLPHN"].ToString();
            this._address.Address1 = dr["CLF_CLAD1"].ToString();
            this._address.Address2 = dr["CLF_CLAD2"].ToString();
            this._address.Address3 = dr["CLF_CLAD3"].ToString();
            this._address.Address4 = dr["CLF_CLAD4"].ToString();
            this._salesTerritory = dr["CLF_CLTER"].ToString();
            this._salesRepresentative = dr["SALES_REP"].ToString();
            this._autodialGroups = dr["AutoDial_Group"].ToString();
            this._zoasisNumber = dr["ZOASIS_NUMBER"].ToString();
            this._routeStop = dr["CLF_CLRTS"].ToString();
            this._location = dr["LOCATION"].ToString();
            this._alphaName = dr["CLF_CLANAM"].ToString();
            this._sortKey = dr["CLF_CLSRT"].ToString();
            this._afterHours = dr["CLF_CLAPH"].ToString();
            this._fax = dr["CLF_CLFAX"].ToString();
            this._ofcContact = dr["CLF_CLCON"].ToString();
            this._description = dr["CLF_CLTYP"].ToString();
            this._reportingPartials = dr["CLF_PART"].ToString();
            this._reportMicroPartials = dr["CLF_PMB"].ToString();
            this._comprehensiveFinals = dr["CLF_PCOMP"].ToString();
            this._customUnitCodes = dr["CustomUnitCode"].ToString();
            this._orderEntryFlashMessage = dr["CLF_FLASH"].ToString();
            this._orderEntryInstruction = dr["OEMOEMSG"].ToString();
            //this._region = dr["Region"].ToString();
            this._salesRepExtension = dr["SalesRepExt"].ToString();
            this._salesRepEmail = dr["SalesRepEmail"].ToString();
            //+SSM 02/11/2011 #Issue115785 Migrating SalesRep and RelatedChanges
            this._salesRepCellPh = dr["SalesRepCellPh"].ToString();
            this._salesLastModified = dr["LastUpdated"].ToString();
            this._salesTerritoryMgr = dr["SalesRepMgrName"].ToString();
            this._salesTerritoryMgrCellPh = dr["SalesRepMgrCellPh"].ToString();
            this._salesTerritoryMgrEmail = dr["SalesRepMgrEmail"].ToString();
            this._salesTerritoryMgrExtension = dr["SalesRepMgrExt"].ToString();
            //-SSM
            this._accountNotes = dr["AccountNotes"].ToString();
            if (dr["ClientIsHot"].ToString().ToUpper() == "Y")
            {
                this._isHot = true;
            }
            else
            {
                this._isHot = false;
            }
            if (dr["ClientIsAllied"].ToString().ToUpper() == "Y")
            {
                this._isAllied = true;
            }
            else
            {
                this._isAllied = false;
            }
            this._isNew = (dr["ClientIsNew"].ToString().ToUpper() == "Y");
            string[] arrClientRevenue = dr["ClientRevenue"].ToString().Split('^');
            this._revenueFirstMonth = (arrClientRevenue[0].Length == 0 ? "0" : arrClientRevenue[0]);
            this._revenueSecondMonth = (arrClientRevenue[1].Length == 0 ? "0" : arrClientRevenue[1]);
            this._revenueThirdMonth = (arrClientRevenue[2].Length == 0 ? "0" : arrClientRevenue[2]);
            this._inquiryMessages = dr["InquiryMessages"].ToString();
            //this._country = dr["Country"].ToString();
            String country;
            getClientCountry(this.ID, out country);
            this._country = country;
        }
    }

    public void GetClientRevenueDetails(String clientMnemonic)
    {
        DataTable clientData = null;
        if (clientMnemonic.Length > 0)
        {
            clientData = DL_Client.getClientDetailsByMnemonic(clientMnemonic);
        }
        if (clientData == null)
        {
            this.IsValid = false;
        }
        else if (clientData.Rows.Count < 1)
        {
            this.IsValid = false;
        }
        else if (clientData.Rows.Count > 1)
        {
            this.IsValid = false;
        }
        else
        {
            this.IsValid = true;

            if (clientData.Rows[0]["ClientIsHot"].ToString().ToUpper() == "Y")
            {
                this._isHot = true;
            }
            else
            {
                this._isHot = false;
            }
            if (clientData.Rows[0]["ClientIsAllied"].ToString().ToUpper() == "Y")
            {
                this._isAllied= true;
            }
            else
            {
                this._isAllied = false;
            }
            this._isNew = (clientData.Rows[0]["ClientIsNew"].ToString().ToUpper() == "Y");
            string[] arrClientRevenue = clientData.Rows[0]["ClientRevenue"].ToString().Split('^');
            this._revenueFirstMonth = (arrClientRevenue[0].Length == 0 ? "0" : arrClientRevenue[0]);
            this._revenueSecondMonth = (arrClientRevenue[1].Length == 0 ? "0" : arrClientRevenue[1]);
            this._revenueThirdMonth = (arrClientRevenue[2].Length == 0 ? "0" : arrClientRevenue[2]);
        }
    }

    public static Boolean getClientCountry(String clientID, out String clientCountry)
    {
        clientCountry = DL_Client.getClientCountry(clientID);
        if (clientCountry.Length > 0)
        {
            return true;
        }
        return false;
    }

    #region Supporting Properties

    private string _clientTimeZone;

    public string ClientTimeZone
    {
        get { return _clientTimeZone; }
    }

    private Boolean _isValid;

    public Boolean IsValid
    {
        get { return _isValid; }
        set { _isValid = value; }
    }

    private Int32 _noOfRecentIssues;

    private Int32 NoOfRecentIssues
    {
        get { return _noOfRecentIssues; }
        set { _noOfRecentIssues = value; }
    }

    #endregion Supporting Properties

    #region Client Properties

    #region Client ID

    private string _ID;
    public string ID
    {
        get { return _ID; }
        //set { _ID = value; }
    }

    #endregion Client ID

    #region Client Name

    private string _name;
    public string Name
    {
        get { return _name; }
        //set { _name = value; }
    }

    #endregion Client Name

    #region Client Mnemonic

    private string _mnemonic;
    public string Mnemonic
    {
        get { return _mnemonic; }
        //set { _mnemonic = value; }
    }

    #endregion Client Mnemonic

    #region Phone Number

    private string _phoneNumber;
    public string PhoneNumber
    {
        get { return _phoneNumber; }
        //set { _phoneNumber = value; }
    }

    #endregion Phone Number

    #region Address

    private Address _address;
    public Address Address
    {
        get { return _address; }
        //set { _address = value; }
    }

    #endregion Address

    #region Sales Territory

    private string _salesTerritory;
    public string SalesTerritory
    {
        get { return _salesTerritory; }
        //set { _salesTerritory = value; }
    }

    #endregion Sales Territory
    //+SSM 02/11/2011 #Issue115785 Migrating SalesRep and RelatedChanges
    #region Sales Info Last Modified

    private string _salesLastModified;
    public string SalesInfoLastModified
    {
        get { return _salesLastModified; }
    }
    #endregion Sales Info Last Modified

    #region Sales Rep Cell Phone
    private string _salesRepCellPh;
    public string SalesRepCellPh
    {
        get { return _salesRepCellPh; }
    }
    #endregion Sales Rep Cell Phone

    #region Territory Manager
    private string _salesTerritoryMgr;
    public string SalesTerritoryMgr
    {
        get { return _salesTerritoryMgr; }
    }
    #endregion Territory Manager

    #region Territory Manager  Email
    private string _salesTerritoryMgrEmail;
    public string SalesTerritoryMgrEmail
    {
        get { return _salesTerritoryMgrEmail; }
    }
    #endregion Territory Manager Email

    #region Territory Manager Cell Phone
    private string _salesTerritoryMgrCellPh;
    public string SalesTerritoryMgrCellPh
    {
        get { return _salesTerritoryMgrCellPh; }
    }
    #endregion Territory Manager Cell Phone

    #region Territory Manager Extension
    private string _salesTerritoryMgrExtension;
    public string SalesTerritoryMgrExtension
    {
        get { return _salesTerritoryMgrExtension; }
    }
    #endregion Territory Manager Extension
    //-SSM
    #region Sales Representative

    private string _salesRepresentative;
    public string SalesRepresentative
    {
        get { return _salesRepresentative; }
        //set { _salesRepresentative = value; }
    }

    #endregion Sales Representative

    #region AutoDial Groups

    private string _autodialGroups;
    public string AutodialGroups
    {
        get { return _autodialGroups; }
        //set { _autodialGroups = value; }
    }

    #endregion AutoDial Groups

    #region Zoasis Number

    private string _zoasisNumber;
    public string ZoasisNumber
    {
        get { return _zoasisNumber; }
        //set { _zoasisNumber = value; }
    }

    #endregion Zoasis Number

    #region Route Stop

    private string _routeStop;
    public string RouteStop
    {
        get { return _routeStop; }
        //set { _routeStop = value; }
    }

    #endregion Route Stop

    #region Lab Location

    private string _location;
    public string Location
    {
        get { return _location; }
        //set { _location = value; }
    }

    #endregion Lab Location

    #region Alpha Name

    private string _alphaName;
    public string AlphaName
    {
        get { return _alphaName; }
        //set { _alphaName = value; }
    }

    #endregion Alpha Name

    #region Sort Key

    private string _sortKey;
    public string SortKey
    {
        get { return _sortKey; }
        //set { _sortKey = value; }
    }

    #endregion Sort Key

    #region After Hours

    private string _afterHours;
    public string AfterHours
    {
        get { return _afterHours; }
        //set { _afterHours = value; }
    }

    #endregion After Hours

    #region Fax

    private string _fax;
    public string Fax
    {
        get { return _fax; }
        //set { _fax = value; }
    }

    #endregion Fax

    #region OFC Contact

    private string _ofcContact;
    public string OfcContact
    {
        get { return _ofcContact; }
        //set { _ofcContact = value; }
    }

    #endregion OFC Contact

    #region Description

    private string _description;
    public string Description
    {
        get { return _description; }
        //set { _description = value; }
    }

    #endregion Description

    #region Reporting Partials

    private string _reportingPartials;
    public string ReportingPartials
    {
        get { return _reportingPartials; }
        //set { _reportingPartials = value; }
    }

    #endregion Reporting Partials

    #region Report Micro Partials

    private string _reportMicroPartials;
    public string ReportMicroPartials
    {
        get { return _reportMicroPartials; }
        //set { _reportMicroPartials = value; }
    }

    #endregion Report Micro Partials

    #region Comprehensive Finals

    private String _comprehensiveFinals;
    public String ComprehensiveFinals
    {
        get { return _comprehensiveFinals; }
        //set { _comprehensiveFinals = value; }
    }

    #endregion Comprehensive Finals

    #region Courier Contacts

    private String _courierContacts;
    public String CourierContacts
    {
        get { return _courierContacts; }
        //set { _courierContacts = value; }
    }

    #endregion Courier Contacts

    #region Custom Unit Codes

    private string _customUnitCodes;
    public string CustomUnitCodes
    {
        get { return _customUnitCodes; }
        //set { _customUnitCodes = value; }
    }

    #endregion Custom Unit Codes

    #region Order Entry Flash Message

    private string _orderEntryFlashMessage;
    public string OrderEntryFlashMessage
    {
        get { return _orderEntryFlashMessage; }
        //set { _orderEntryFlashMessage = value; }
    }

    #endregion Order Entry Flash Message

    #region Order Entry Instruction

    private string _orderEntryInstruction;
    public string OrderEntryInstruction
    {
        get { return _orderEntryInstruction; }
        //set { _orderEntryInstruction = value; }
    }

    #endregion Order Entry Instruction

    #region Region

    private string _region;
    public string Region
    {
        get { return _region; }
        //set { _region = value; }
    }

    #endregion Region

    #region Sales Representative Extension

    private string _salesRepExtension;
    public string SalesRepExtension
    {
        get { return _salesRepExtension; }
        //set { _salesRepExtension = value; }
    }

    #endregion Sales Representative Extension

    #region Sales Representative Email

    private string _salesRepEmail;
    public string SalesRepEmail
    {
        get { return _salesRepEmail; }
        //set { _salesRepEmail = value; }
    }

    #endregion Sales Representative Email

    #region Account Notes

    private string _accountNotes;
    public string AccountNotes
    {
        get { return _accountNotes; }
        //set { _accountNotes = value; }
    }

    #endregion Account Notes

    #region Price Inq Password

    private string _priceInqPasswd;
    public string priceInqPasswd
    {
        get { return _priceInqPasswd; }
    }

    #endregion Price Inq Password

    #region Price Inq Password Set

    public Boolean  isPriceInqPasswdSet
    {
        get {
                if (this.priceInqPasswd.Length > 0)
                {
                    return true;
                }
                else
                {
                    return false;
                }
            }
    }

    #endregion Price Inq Password Set

    #region Is Hot

    private Boolean _isHot;
    public Boolean IsHot
    {
        get { return _isHot; }
        //set { _isHot = value; }
    }

    #endregion Is Hot

    #region Is Allied

    private Boolean _isAllied;
    public Boolean IsAllied
    {
        get { return _isAllied; }
    }

    #endregion Is Allied

    #region Is New

    private Boolean _isNew;
    public Boolean IsNew
    {
        get { return _isNew; }
    }

    #endregion Is New

    #region First Month Revenue

    private string _revenueFirstMonth;
    public string RevenueFirstMonth
    {
        get { return _revenueFirstMonth; }
        //set { _revenueFirstMonth = value; }
    }

    #endregion First Month Revenue

    #region Second Month Revenue

    private string _revenueSecondMonth;
    public string RevenueSecondMonth
    {
        get { return _revenueSecondMonth; }
        //set { _revenueSecondMonth = value; }
    }

    #endregion Second Month Revenue

    #region Third Month Revenue

    private string _revenueThirdMonth;
    public string RevenueThirdMonth
    {
        get { return _revenueThirdMonth; }
        //set { _revenueThirdMonth = value; }
    }

    #endregion Third Month Revenue

    #region Inquiry Messages

    private string _inquiryMessages;
    public string InquiryMessages
    {
        get { return _inquiryMessages; }
        //set { _inquiryMessages = value; }
    }

    #endregion Inquiry Messages

    #region Client Country

    private String _country;
    public String Country
    {
        get { return _country; }
        //set { _inquiryMessages = value; }
    }

    #endregion Client Country

    #endregion

    public DataTable getRecentIssues()
    {
        return getRecentIssues(this.NoOfRecentIssues);
    }

    private DataTable getRecentIssues(Int32 noOfTopRecords)
    {
        if (this.Mnemonic.Length > 0)
        {
            return DL_Client.getRecentIssuesByClientMnemonic(noOfTopRecords, this.Mnemonic);
        }
        else
        {
            return DL_Client.getRecentIssuesByClientID(noOfTopRecords, this.ID);
        }
    }

    public DataTable getEvents()
    {
        if (this.Mnemonic.Length > 0)
        {
            return DL_Client.getEventsByClientMnemonic(this.Mnemonic);
        }
        else
        {
            return DL_Client.getEventsByClientID(this.ID);
        }
    }

    //AM Issue#32815 05/06/2008 Build Number 0.0.0.9
    public Boolean insertReportOption(string strFROMDATE, string strTODATE,String userId,String userLab)
    {
        String message = DL_Client.insertReportOption(this._mnemonic, this._autodialGroups, strFROMDATE, strTODATE,userId,userLab);
        if (message.Length > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    public DataTable getTestsInquiry(String unitCode, String dateOfPrice, out String UnitCodeTitle, out String SpecialPrice, out String SpecialOptionPrice, String userId, String userLab, String testName, bool logEvent)
    {
        UnitCodeTitle = String.Empty;
        SpecialPrice = String.Empty;
        SpecialOptionPrice = String.Empty;
        string strLogEvent = (logEvent == true ? "Y" : "N");
        return DL_Client.getTestsInquiry(this.ID, unitCode, dateOfPrice, out UnitCodeTitle, out SpecialPrice, out SpecialOptionPrice,this.Mnemonic,userId,userLab,testName, strLogEvent);
    }

   
    public Boolean update(String newAccountNote,String priceInqPasswd)
    {
        return (DL_Client.updateAccountNote(this.Mnemonic, newAccountNote, priceInqPasswd,SessionHelper.UserContext.canChangeClientPassword) > 0) ? true : false;
    }

    public String validateAutodial(String CLID)
    {
        return DL_Client.validateAutodial(CLID);
    }
}
