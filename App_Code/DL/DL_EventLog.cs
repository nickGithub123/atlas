using System;
using System.Data;
using System.Configuration;
using System.Collections.Generic;
using System.Text;

/// <summary>
/// Summary description for DL_EventLog
/// </summary>
public class DL_EventLog
{
	public DL_EventLog()
	{
	}

    public static DataTable getEventLogDetailsBySearchOptions(string clientID, string user, string dateFrom, string dateTo, string accession, string eventCategory, string status, bool isSupervisor, string loggedInUsersID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("EVENT_RowID AS RowId,");
        sb.Append("EVENT_CLF_ParRef->CLF_CLNUM AS AccountNo,");
        sb.Append("$$CO17^XT58(EVENT_Agent->USER_UserID) AS UserID,");
        sb.Append("EVENT_DateLogged  AS DateLogged,");
        sb.Append("EVENT_TimeLogged AS TimeLogged,");
        sb.Append("%External(EVENT_Status) AS Status,");
        sb.Append("EVENT_AccessionDR->ACC_Accession As Accession,");
        sb.Append("EVENT_CategoryDR AS Category,");
        sb.Append("EVENT_CategoryDR->EVECT_IsEditable AS IsEditable,");
        sb.Append("EVENT_LogDesc AS LogDescription,");
        sb.Append("EVENT_CLF_ParRef->CLF_CLNAM AS AccountName,");
        sb.Append("EVENT_CLF_ParRef->CLF_CLMNE AS AccountMnemonic,");
        sb.Append("EVENT_CLF_ParRef->CLF_CLPHN As ClientPhone,");
        sb.Append("EVENT_CLF_ParRef->CLF_CLAD1 AS ClientAddress1,");
        sb.Append("EVENT_CLF_ParRef->CLF_CLAD2 AS ClientAddress2,");
        sb.Append("EVENT_CLF_ParRef->CLF_SalesTerritoryDR->ST_TerritoryCode AS SalesTerritory,");
        sb.Append("EVENT_CLF_ParRef->CLF_IsHot As ClientIsHot,");
        sb.Append("EVENT_CLF_ParRef->CLF_IsAlliedClient As ClientIsAllied,");
        sb.Append("EVENT_CLF_ParRef->CLF_IsNew As ClientIsNew,");
        sb.Append("EVENT_CLF_ParRef->CLF_SalesTerritoryDR->ST_SalesRepName AS SalesRepresentative,");
        sb.Append("EVENT_CLF_ParRef->CLF_CLADG AS AutodialGroup,");
        sb.Append("EVENT_CLF_ParRef->CLF_CLRTS AS RouteStop,");
        sb.Append("zoasis_num AS Zoasis,");
        sb.Append("$$GETREVHIST^XT1(EVENT_CLF_ParRef->CLF_CLMNE) AS ClientRevenue ");

        sb.Append("FROM ");
        sb.Append("CLF_ClientEventLog ");
        sb.Append("LEFT OUTER JOIN ");
        sb.Append("CLF_ClientFile Client on EVENT_CLF_ParRef = Client.CLF_CLMNE ");
        sb.Append("LEFT OUTER JOIN ");
        sb.Append("zoasis ON Client.CLF_CLNUM = zoasis.CLN_RowID ");
        sb.Append("WHERE 1=1 ");
        if (clientID.Length > 0)
        {
            sb.Append(" AND EVENT_CLF_ParRef->CLF_CLNUM ='" + clientID + "'");
        }
        if (dateFrom.Length > 0)
        {
            sb.Append(" AND EVENT_DateLogged>= TO_DATE('" + dateFrom + "','MM/DD/YYYY')");
        }
        if (dateTo.Length > 0)
        {
            sb.Append(" AND EVENT_DateLogged<= TO_DATE('" + dateTo + "','MM/DD/YYYY')");
        }
        //Supervisor can see a event of other users a non supervisor can see only his events.
        if (isSupervisor == true)
        {
            if (user.Length > 0)
            {
                sb.Append(" AND EVENT_Agent->USER_UserID  ='" + user + "'");
            }
        }
        else
        {
            sb.Append(" AND EVENT_Agent->USER_UserID  ='" + loggedInUsersID + "'");
        }
        if (accession.Length > 0)
        {
            sb.Append(" AND EVENT_AccessionDR->ACC_Accession ='" + accession + "'");
        }
        if (eventCategory.Length > 0)
        {
            sb.Append(" AND EVENT_CategoryDR ='" + eventCategory + "'");
        }
        if (status.Length > 0)
        {
            sb.Append(" AND EVENT_Status ='" + status + "'");
        }
        sb.Append(" ORDER BY EVENT_DateLogged DESC,EVENT_TimeLogged DESC");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static DataTable getEventLogDetailsReport(string clientID, string user, string dateFrom, string dateTo, string accession, string eventCategory, string status, bool isSupervisor, string loggedInUsersID)
    {
        StringBuilder sb = new StringBuilder();
        sb.Append("SELECT ");
        sb.Append("EVENT_RowID AS RowId,");
        sb.Append("EVENT_CLF_ParRef->CLF_CLNUM AS AccountNo,");
        sb.Append("EVENT_CLF_ParRef->CLF_CLNAM AS AccountName,");
        sb.Append("EVENT_CLF_ParRef->CLF_CLPHN AS AccountPhone,");
        sb.Append("$$CO17^XT58(EVENT_Agent->USER_UserID) AS UserID,");
        sb.Append("EVENT_DateLogged  AS DateLogged,");
        sb.Append("EVENT_TimeLogged AS TimeLogged,");
        sb.Append("%External(EVENT_Status) AS Status,");
        sb.Append("EVENT_AccessionDR->ACC_Accession As Accession,");
        sb.Append("EVENT_CategoryDR AS CategoryCode,");
        sb.Append("EVENT_CategoryDR->EVECT_CategoryName AS CategoryName,");
        sb.Append("EVENT_LogDesc AS LogDescription ");
       
        sb.Append("FROM ");
        sb.Append("CLF_ClientEventLog ");
        sb.Append("LEFT OUTER JOIN ");
        sb.Append("CLF_ClientFile Client on EVENT_CLF_ParRef = Client.CLF_CLMNE ");
        sb.Append("LEFT OUTER JOIN ");
        sb.Append("zoasis ON Client.CLF_CLNUM = zoasis.CLN_RowID ");
        sb.Append("WHERE 1=1 ");
        if (clientID.Length > 0)
        {
            sb.Append(" AND EVENT_CLF_ParRef->CLF_CLNUM ='" + clientID + "'");
        }
        if (dateFrom.Length > 0)
        {
            sb.Append(" AND EVENT_DateLogged>= TO_DATE('" + dateFrom + "','MM/DD/YYYY')");
        }
        if (dateTo.Length > 0)
        {
            sb.Append(" AND EVENT_DateLogged<= TO_DATE('" + dateTo + "','MM/DD/YYYY')");
        }
        //Supervisor can see a event of other users a non supervisor can see only his events.
        if (isSupervisor == true)
        {
            if (user.Length > 0)
            {
                sb.Append(" AND EVENT_Agent->USER_UserID  ='" + user + "'");
            }
        }
        else
        {
            sb.Append(" AND EVENT_Agent->USER_UserID  ='" + loggedInUsersID + "'");
        }
        if (accession.Length > 0)
        {
            sb.Append(" AND EVENT_AccessionDR->ACC_Accession ='" + accession + "'");
        }
        if (eventCategory.Length > 0)
        {
            sb.Append(" AND EVENT_CategoryDR ='" + eventCategory + "'");
        }
        if (status.Length > 0)
        {
            sb.Append(" AND EVENT_Status ='" + status + "'");
        }
        sb.Append(" ORDER BY EVENT_DateLogged DESC,EVENT_TimeLogged DESC");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sb.ToString());
    }

    public static DataTable getEventLogDetails(string eventLogRowId)
    {
        StringBuilder sbSQL = new StringBuilder();

        sbSQL.Append("SELECT ");
        sbSQL.Append(" EVENT_CLF_ParRef->CLF_CLNUM AS AccountNo,");
        sbSQL.Append(" EVENT_Agent AS UserID,");
        sbSQL.Append(" EVENT_DateLogged  AS DateLogged,");
        sbSQL.Append(" EVENT_TimeLogged AS TimeLogged,");
        sbSQL.Append(" EVENT_Lab->LABLO_LabName AS LabName,");
        sbSQL.Append(" EVENT_AccessionDR->ACC_Accession As Accession,");
        sbSQL.Append(" EVENT_CategoryDR AS CategoryRowId,");
        sbSQL.Append(" EVENT_LogDesc AS LogDescription,");
        sbSQL.Append(" EVENT_Status AS Status,");
        sbSQL.Append(" %External(EVENT_Status) AS StatusText");

        sbSQL.Append(" FROM ");
        sbSQL.Append(" CLF_ClientEventLog");
        sbSQL.Append(" WHERE EVENT_RowID ='" + eventLogRowId + "'");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    public static DataTable getEventLogDescription(string eventLogRowId)
    {
        StringBuilder sbSQL = new StringBuilder();

        sbSQL.Append("SELECT ");
        sbSQL.Append(" EVENT_LogDesc AS LogDescription,");
        sbSQL.Append(" EVENT_Status AS Status,");
        sbSQL.Append(" %External(EVENT_Status) AS StatusText");

        sbSQL.Append(" FROM ");
        sbSQL.Append(" CLF_ClientEventLog");
        sbSQL.Append(" WHERE EVENT_RowID ='" + eventLogRowId + "'");

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(sbSQL.ToString());
    }

    public static string insertEventLog(string accountNumber, string eventCategoryId, string accessionNo, string user, string lab, string logDescription, string completed)
    {
        Dictionary<String, String> EventData = new Dictionary<String, String>();
        EventData.Add("AccountNumber", accountNumber);
        EventData.Add("Lab", lab);
        EventData.Add("Agent", user);
        EventData.Add("EventCategory", eventCategoryId);
        EventData.Add("AccessionNumber", accessionNo);
        EventData.Add("EventDescription", logDescription);
        
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_AddEventLog(?,?,?,?,?,?)", EventData).Value.ToString();
    }

    public static string updateEventLog(string eventRowId, string accessionNo, string logDescription, string status, string user, string eventCategory)
    {
        Dictionary<String, String> EventData = new Dictionary<String, String>();
        EventData.Add("EventRow", eventRowId);
        EventData.Add("AccessionNumber", accessionNo);
        EventData.Add("EventDescription", logDescription);
        EventData.Add("IsCompleted", status);
        EventData.Add("User", user);
        EventData.Add("eventCategory", eventCategory);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_UpdateEventLog(?,?,?,?,?,?)", EventData).Value.ToString();
    }

    public static string reassignEvent(string eventRowId, string reassignmentAgent, string newAgent, string eventDescription)
    {
        Dictionary<String, String> EventData = new Dictionary<String, String>();
        EventData.Add("EventRow", eventRowId);
        EventData.Add("ReassignmentUser", reassignmentAgent);
        EventData.Add("NewUser", newAgent);
        EventData.Add("EventDescription", eventDescription);

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.StoredProcedure("?=call SP2_ReassignEvent(?,?,?,?)", EventData).Value.ToString();
    }
}
