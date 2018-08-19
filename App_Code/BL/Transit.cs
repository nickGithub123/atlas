using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for Transit
/// </summary>
public class Transit
{
	public Transit(string rowID)
	{
        if (!string.IsNullOrEmpty(rowID))
        {
            getCITDetails(rowID);
        }
    }

    #region Properties
    
    public string RowId
    {
        get;
        set;
    }

    public string Account
    {
        get;
        set;
    }

    public string OnwerName
    {
        get;
        set;
    }

    public string PetName
    {
        get;
        set;
    }

    public string Caller
    {
        get;
        set;
    }

    public string RequestedDate
    {
        get;
        set;
    }

    public string RequestedTime
    {
        get;
        set;
    }

    public string SpecialInstruction
    {
        get;
        set;
    }

    public string EnteredByUser
    {
        get;
        set;
    }

    public string LabID
    {
        get;
        set;
    }

    public string Lab
    {
        get;
        set;
    }

    public string Email
    {
        get;
        set;
    }

    public string RequestType
    {
        get;
        set;
    }

    public string Tests
    {
        get;
        set;
    }

    private bool isInvalidTransitNo = false;
    public bool IsInvalidTransitNo
    {
        get
        {
            return this.isInvalidTransitNo;
        }
    }
    #endregion Properties

    public void getCITDetails(string rowId)
    {
        DataTable dtTransitDetails = DL_Transit.getCITDetails(rowId);
        if (dtTransitDetails != null && dtTransitDetails.Rows.Count > 0)
        {
            this.RowId = dtTransitDetails.Rows[0]["ConfirmationNumber"].ToString();
            //this.
            this.Caller = dtTransitDetails.Rows[0]["CallerName"].ToString();
            this.Account = dtTransitDetails.Rows[0]["Account"].ToString();
            this.Lab = dtTransitDetails.Rows[0]["LabLocation"].ToString();
            this.LabID = dtTransitDetails.Rows[0]["LabID"].ToString();
            this.RequestedDate = dtTransitDetails.Rows[0]["RequestDate"].ToString();
            this.RequestedTime = dtTransitDetails.Rows[0]["RequestTime"].ToString();
            this.SpecialInstruction = dtTransitDetails.Rows[0]["SpecialInstructions"].ToString().Replace("|", "\n").Replace(";", "\n");
            this.EnteredByUser = dtTransitDetails.Rows[0]["UserValue"].ToString();
            this.Email = dtTransitDetails.Rows[0]["Email"].ToString();
            this.RequestType = dtTransitDetails.Rows[0]["RequestType"].ToString();
            this.OnwerName = "";
            this.PetName = "";
            this.Tests = dtTransitDetails.Rows[0]["Tests"].ToString().Replace("|", "\n\r").Replace(";", "\n\r");
        }
        else
        {
            this.isInvalidTransitNo = true;
        }
    }

    public static DataTable getTransitRecords(string strAccountNo,string strConfNo,string strFromDate,string strToDate)
    {
    return DL_Transit.getTransitRecords(strAccountNo, strConfNo, strFromDate, strToDate);
    }

    public static string saveTransitRequest(String ClientMnemonic, String callerName, String changeRequestType, String testsAddOnVerify, String eMail, String labLocation, String specialInstructions, String UserID, String UserLab)
    {
        String[] arr = DL_Accession.updateAddOnVerificationRequests("", callerName, changeRequestType, testsAddOnVerify, eMail, labLocation, specialInstructions, ClientMnemonic, UserID, UserLab, "", "", "", "").Split('^');
        return arr[0];
    }

    public static Boolean updateTransitRequest(String confirmationNumber, String callerName, String changeRequestType, String testsAddOnVerify, String eMail, String labLocation, String specialInstructions, String userID)
    {
        if (DL_Accession.updateAddOnVerificationRequests(confirmationNumber, callerName, changeRequestType, eMail, labLocation, specialInstructions, testsAddOnVerify, userID, "", "", "", "", true) > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }
}
