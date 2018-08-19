using System;
using System.Data;
using System.Configuration;

/// <summary>
/// Summary description for EventLog
/// </summary>
public class EventLog
{
    #region Properties
    public string EventRowId
    {
        get;
        set;
    }
    public string AccountNumber
    {
        get;
        set;
    }
    public string AccessionNumber
    {
        get;
        set;
    }
    public string DateLogged
    {
        get;
        set;
    }

    public string TimeLogged
    {
        get;
        set;
    }
    public string LabName
    {
        get;
        set;
    }
    public string UserName
    {
        get;
        set;
    }
    public string CategoryRowId
    {
        get;
        set;
    }
    public string LogDescription
    {
        get;
        set;
    }
    public bool IsCompleted
    {
        get
        {
            if (this.Status == "C")
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public string Status
    {
        get;
        set;
    }
    public string StatusText
    {
        get;
        set;
    }
    #endregion Properties

    public EventLog()
	{
	}

    public EventLog(string eventRowId)
    {
        DataTable EventDetails = DL_EventLog.getEventLogDetails(eventRowId);

        this.EventRowId = eventRowId;
        this.AccountNumber = EventDetails.Rows[0]["AccountNo"].ToString();
        this.AccessionNumber = EventDetails.Rows[0]["Accession"].ToString();
        this.DateLogged = EventDetails.Rows[0]["DateLogged"].ToString();
        this.TimeLogged = EventDetails.Rows[0]["TimeLogged"].ToString();
        this.LabName = EventDetails.Rows[0]["LabName"].ToString();
        this.UserName = EventDetails.Rows[0]["UserID"].ToString();
        this.CategoryRowId = EventDetails.Rows[0]["CategoryRowId"].ToString();
        this.LogDescription = EventDetails.Rows[0]["LogDescription"].ToString();
        //this.IsCompleted = (EventDetails.Rows[0]["IsCompleted"].ToString() == "Y" ? true : false);
        this.Status = EventDetails.Rows[0]["Status"].ToString();
        this.StatusText = EventDetails.Rows[0]["StatusText"].ToString();
    }

    public static DataTable getEventLogDescription(string eventRowId)
    {
        return DL_EventLog.getEventLogDescription(eventRowId);
    }
    public static DataTable getEventLogDetailsBySearchOptions(string clientID, string user, string fromDate, string toDate, string accession, string eventCategory, string status)
    {
        return DL_EventLog.getEventLogDetailsBySearchOptions(clientID, user, fromDate, toDate, accession, eventCategory, status, SessionHelper.UserContext.IsSupervisor, SessionHelper.UserContext.ID);
    }

    public static DataTable getEventLogDetailsReport(string queryString)
    {
        String[] QS = queryString.Split('^');
        return DL_EventLog.getEventLogDetailsReport(QS[3], QS[0], QS[4], QS[5], QS[1], QS[2], QS[6], SessionHelper.UserContext.IsSupervisor, SessionHelper.UserContext.ID);
    }

    public static string saveEventLogDetails(string eventRowId, string accountNo, string eventCategoryId, string accessionNo, string user, string lab, string logDescription, bool isCompleted)
    {
        string strRetVal = "";
        string strCompleted = (isCompleted == true ? "Y":"N");
        if (eventRowId == null || eventRowId.Length == 0)
        {
            if (DL_EventLog.insertEventLog(accountNo, eventCategoryId, accessionNo, user, lab, logDescription, strCompleted) == "1")
            {
                strRetVal = "Event successfully added.";
            }
            else
            {
                strRetVal = "Failed to add the event.";
            }
        }
        else
        {
            if (DL_EventLog.updateEventLog(eventRowId, accessionNo, logDescription, strCompleted, user, eventCategoryId) == "1")
            {
                strRetVal = "Event successfully updated.";
            }
            else
            {
                strRetVal = "Failed to update the event.";
            }
        }
        return strRetVal;
    }

    public static bool reassignEvent(string eventRowId, string reassignmentAgent, string newAgent, string eventDescription)
    {
        string strRetVal = DL_EventLog.reassignEvent(eventRowId, reassignmentAgent, newAgent, eventDescription);

        if (strRetVal.Equals("1"))
        {
            return true;
        }
        else
        {
            return false;
        }

    }
}
