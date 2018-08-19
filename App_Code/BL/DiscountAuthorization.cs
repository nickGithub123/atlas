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
/// Summary description for DiscountAuthorization
/// </summary>
public class DiscountAuthorization
{
	public DiscountAuthorization()
	{
		//
		// TODO: Add constructor logic here
		//
	}
    
    public DiscountAuthorization(string clientID)
    {
        this._ID = clientID;
        DataTable discountData = DL_DiscountAuthorization.DiscountAuthorization(clientID);
        if (discountData == null)
        {
            this.IsValid = false;
        }
        else if (discountData.Rows.Count < 1)
        {
            this.IsValid = false;
        }
        else if (discountData.Rows.Count > 1)
        {
            this.IsValid = false;
        }
        else
        {
            this.IsValid = true;
            DataRow dr = discountData.Rows[0];
            this._accessionNumber = dr["ACCESSION"].ToString();
            this._account = dr["CLIENTNUMBER"].ToString();
            this._discountCode = dr["DISCOUNTCODE"].ToString();
            this._location = dr["LOCATION"].ToString();
            this._clientLocation = dr["CLIENTLOCATION"].ToString();
            this._authorization = dr["AUTHORIZATIONNUMBER"].ToString();
            this._animalName = dr["ANIMALNAME"].ToString();
            this._territory = dr["TERRITORY"].ToString();
            this._addtestcode = dr["ADDTESTCODE"].ToString();
            this._deletetestcode = dr["DELTESTCODE"].ToString();
            this._amount = dr["AMOUNT"].ToString();
            this._date = dr["DATEOFAUTHORIZATION"].ToString();
            this._reason = dr["REASON"].ToString();
            this._ir = dr["INHOUSERESUB"].ToString();
            this._sex = dr["SEX"].ToString();
            this._species = dr["SPECIES"].ToString();
            this._sex = dr["SEX"].ToString();
            this._ageDOB = dr["AGEDOB"].ToString();
            this._collDate = dr["COLLECTIONDATE"].ToString();
            this._collTime = dr["COLLECTIONTIME"].ToString();
            this._testOrdered =dr["TESTORDEREDSTRING"].ToString();

            string[] arrClientRevenue = dr["ClientRevenue"].ToString().Split('^');

            this._revenueFirstMonth = (arrClientRevenue[0].Length == 0 ? "0.00" : arrClientRevenue[0]);
            this._revenueSecondMonth = (arrClientRevenue[1].Length == 0 ? "0.00" : arrClientRevenue[1]);
            this._revenueThirdMonth = (arrClientRevenue[2].Length == 0 ? "0.00" : arrClientRevenue[2]);
        }
    }
    
    private Boolean _isValid;    
    public Boolean IsValid
    {
        get { return _isValid; }
        set { _isValid = value; }
    }
    
    #region Authorization Properties
    private string _ID;
    public string ID
    {
        get { return _ID; }
        //set { _ID = value; }
    }
    private string _accessionNumber;
    public string AccessionNumber
    {
        get { return _accessionNumber; }
        //set { _accessionNumber = value; }
    }
    private string _account;
    public string Account
    {
        get { return _account; }
        //set { _account = value; }
    }
    private string _discountCode;
    public string DiscountCode
    {
        get { return _discountCode; }
        //set { _discountCode = value; }
    }
    private string _location;
    public string Location
    {
        get { return _location; }
        //set { _location = value; }
    }
    private string _clientLocation;
    public string ClientLocation
    {
        get { return _clientLocation; }
        //set { _clientLocation = value; }
    }
    private string _authorization;
    public string Authorization
    {
        get { return _authorization; }
        //set { _authorization = value; }
    }
    private string _animalName;
    public string AnimalName
    {
        get { return _animalName; }
        //set { _animalName = value; }
    }
    private string _territory;
    public string Territory
    {
        get { return _territory; }
        //set { _territory = value; }
    }
    private string _addtestcode;
    public string AddTestCode
    {
        get { return _addtestcode; }
        //set { _addtestcode = value; }
    }
    private string _deletetestcode;
    public string DeleteTestCode
    {
        get { return _deletetestcode; }
        //set { _deletetestcode = value; }
    }
    private string _amount;
    public string Amount
    {
        get { return _amount; }
        //set { _amount = value; }
    }
    private string _date;
    public string Date
    {
        get { return _date; }
        //set { _date = value; }
    }
    private string _reason;
    public string Reason
    {
        get { return _reason; }
        //set { _reason = value; }
    }
    private string _ir;
    public string IR
    {
        get { return _ir; }
        //set { _ir = value; }
    }
    private string _sex;
    public string SEX
    {
        get { return _sex; }
        //set { _sex = value; }
    }
    private string _species;
    public string SPECIES
    {
        get { return _species; }
        //set { _species = value; }
    }
    private string _ageDOB;
    public string AGEDOB
    {
        get { return _ageDOB; }
        //set { _ageDOB = value; }
    }
    private string _collDate;
    public string COLLECTIONDATE
    {
        get { return _collDate; }
        //set { _collDate = value; }
    }
    private string _collTime;
    public string COLLECTIONTIME
    {
        get { return _collTime; }
        //set { _collTime = value; }
    }
    private string _testOrdered;
    public string TESTORDEREDSTRING
    {
        get { return _testOrdered; }
        //set { _testOrdered = value; }
    }
    private string _revenueFirstMonth;
    public string RevenueFirstMonth
    {
        get { return _revenueFirstMonth; }
        //set { _revenueFirstMonth = value; }
    }
    private string _revenueSecondMonth;
    public string RevenueSecondMonth
    {
        get { return _revenueSecondMonth; }
        //set { _revenueSecondMonth = value; }
    }
    private string _revenueThirdMonth;
    public string RevenueThirdMonth
    {
        get { return _revenueThirdMonth; }
        //set { _revenueThirdMonth = value; }
    }
    #endregion
    
    //public static DataTable getDiscountDetails(string ClientID, string accessionNumber)
    //{
    //    DataTable returnDataTable = new DataTable();
    //    returnDataTable = DL_DiscountAuthorization.getDiscountDetails(ClientID, accessionNumber);
    //    return returnDataTable;
    //}
    
    public static DataTable getDiscountDetails(string ClientID, string Accession, string IR, string Location, string AuthorizationNumber, string DateSentFrom, string DateSentTo, string Status, string Territory, string DiscountCode, string UID)
    {
        return DL_DiscountAuthorization.getDiscountDetails(ClientID, Accession, IR, Location, AuthorizationNumber, DateSentFrom, DateSentTo, Status, Territory, DiscountCode, UID);
    }

    public static String insertDiscountAuthorization(String ACCOUNT, String MNEMONIC, String CLIENTNAME, String ACCESSION, String TERRITORY, String ANIMALNAME, String DISCOUNTCODE, String ADDTESTCODE, String DELTESTCODE, String AMOUNT, String IHRESUB, String DATE, String LAB, String REASON, String USER)
    {
        return DL_DiscountAuthorization.insertDiscountAuthorization(ACCOUNT, MNEMONIC, CLIENTNAME, ACCESSION, TERRITORY, ANIMALNAME, DISCOUNTCODE, ADDTESTCODE, DELTESTCODE, AMOUNT, IHRESUB, DATE, LAB, REASON, USER);        
    }
}
