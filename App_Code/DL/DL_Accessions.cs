using System;
using System.Data;
using System.Configuration;
using System.Web.Security;

/// <summary>
/// Containes methods to fetch a set of Accessions
/// </summary>
public class DL_Accessions
{
    public DL_Accessions()
    {
        //
    }

    #region Soundex Overloads
    public static DataTable getAccessionDetailsExtended(String AccountNumber, String ChartNumber, String PetName, String owner, String TestCode, String DateFrom, String DateTo, String DoctorName, Boolean useSoundex)
    {
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();

        #region Preparing Query
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ACC_Accession,");
        sb.Append("ACC_PatientName AS PATIENT,");
        sb.Append("ACC_TestsOrderedDisplayString AS TESTSORDERED,");
        sb.Append("ACC_ClientDR->CLF_CLNUM AS ACCOUNTNUM,");
        sb.Append("ACC_MiniLogDate As ORDERDATE,");
        sb.Append("ACC_MiniLogTime As ORDERTIME,");
        sb.Append("ACC_ReportStatus As ReportStatus,");
        sb.Append("ACC_IsStat As StatDesignation ");
        sb.Append("FROM ");
        sb.Append("ORD_Accession ");
        sb.Append("WHERE ");
        sb.Append("1=1");
        if (!String.IsNullOrEmpty(DoctorName))
        {
            sb.Append(" AND ACC_RequestingPhysician %STARTSWITH '" + DoctorName + "'");
        }
        if (!String.IsNullOrEmpty(AccountNumber))
        {
            sb.Append(" AND ACC_ClientDR->CLF_CLNUM ='" + AccountNumber + "'");
        }
        if (!String.IsNullOrEmpty(ChartNumber))
        {
            sb.Append(" AND ACC_MedicalRecordsNumber ='" + ChartNumber + "'");
        }

        if (!useSoundex)
        {
            if (owner.Length > 0)
            {
                sb.Append(" AND ACC_OwnerLastName %STARTSWITH '" + owner + "'");
            }
            if (PetName.Length > 0)
            {
                sb.Append(" AND ACC_PetFirstName %STARTSWITH '" + PetName + "'");
            }
        }
        else
        {
            if (owner.Length > 0)
            {
                System.Collections.Generic.Dictionary<string, string> _problemData = new System.Collections.Generic.Dictionary<string, string>();
                _problemData.Add("OwnerName", owner);
                _problemData.Add("PetName", String.Empty);
                String SoundexString = cache.StoredProcedure("?=call SP2_getSoundex(?,?)", _problemData).Value.ToString();
                if (SoundexString.Length > 0)
                {
                    String[] OwnerPet = SoundexString.Split('#');
                    sb.Append(" AND (");
                    for (Int32 i = 0; i < OwnerPet.Length; i++)
                    {
                        String[] tmp = OwnerPet[i].Split('^');
                        sb.Append(" (ACC_PetFirstName %STARTSWITH '' AND ACC_OwnerLastName %STARTSWITH '" + tmp[0] + "') OR");
                        //// Note:  'ACC_PetFirstName' is for petName with tmp[1]
                        ////        'ACC_OwnerLastName' is for owner with tmp[0]
                    }
                    sb.Remove(sb.Length - 2, 2);
                    sb.Append(" )");
                }
            }
            else if (PetName.Length > 0)
            {
                System.Collections.Generic.Dictionary<string, string> _problemData = new System.Collections.Generic.Dictionary<string, string>();
                _problemData.Add("OwnerName", owner);
                _problemData.Add("PetName", PetName);
                String SoundexString = cache.StoredProcedure("?=call SP2_getSoundex(?,?)", _problemData).Value.ToString();
                if (SoundexString.Length > 0)
                {
                    String[] OwnerPet = SoundexString.Split('#');
                    sb.Append(" AND (");
                    for (Int32 i = 0; i < OwnerPet.Length; i++)
                    {
                        String[] tmp = OwnerPet[i].Split('^');
                        sb.Append(" (ACC_PetFirstName %STARTSWITH '" + tmp[1] + "' AND ACC_OwnerLastName %STARTSWITH '') OR");
                        //// Note:  'ACC_PetFirstName' is for petName with tmp[1]
                        ////        'ACC_OwnerLastName' is for owner with tmp[0]
                    }
                    sb.Remove(sb.Length - 2, 2);
                    sb.Append(" )");
                }
            }
        }

        if (!String.IsNullOrEmpty(TestCode))
        {
            sb.Append(" AND (ACC_TestsOrderedDisplayString ='" + TestCode + "' OR ACC_TestsOrderedDisplayString %STARTSWITH '" + TestCode + ",' OR ACC_TestsOrderedDisplayString LIKE '%, " + TestCode + "' OR ACC_TestsOrderedDisplayString LIKE '%, " + TestCode + ", %')");
        }
        if (!String.IsNullOrEmpty(DateFrom) && !String.IsNullOrEmpty(DateTo))
        {
            sb.Append(" AND ACC_MiniLogDate>= {d '" + DateTime.Parse(DateFrom).ToString("yyyy-MM-dd") + "'} AND ACC_MiniLogDate<={d '" + DateTime.Parse(DateTo).ToString("yyyy-MM-dd") + "'}");
        }

        if ((!String.IsNullOrEmpty(DateFrom) && !String.IsNullOrEmpty(DateTo)) || (!String.IsNullOrEmpty(AccountNumber)))
        {
            sb.Append(" ORDER BY ACC_MiniLogDate DESC, ACC_MiniLogTime DESC, ACC_OwnerLastName");
        }
        else
        {
            sb.Append(" ORDER BY ACC_OwnerLastName");
        }

        string selectStatement = sb.ToString();
        #endregion Preparing Query

        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getAccessionDetailsExtended(String AccountNumber, String ChartNumber, String PetName, String owner, String TestCode, String DateFrom, String DateTo, String DoctorName, Int32 startIndex, Int32 noOfRecords, out String query, Boolean useSoundex)
    {
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        #region Preparing Query
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ACC_Accession,");
        //sb.Append("ACC_PatientName AS PATIENT,");
        sb.Append("ACC_OwnerLastName||', '||ACC_PetFirstName AS PATIENT,");     // Last Name + First Name

        sb.Append("ACC_TestsOrderedDisplayString AS TESTSORDERED,");
        sb.Append("ACC_ClientDR->CLF_CLNUM AS ACCOUNTNUM,");
        sb.Append("ACC_MiniLogDate As ORDERDATE,");
        sb.Append("ACC_MiniLogTime As ORDERTIME,");
        sb.Append("ACC_ReportStatus As ReportStatus,");
        sb.Append("ACC_IsStat As StatDesignation,");
        sb.Append("ACC_HasInquiryNotes As HasInquiryNotes ");
        sb.Append("FROM ");
        sb.Append("ORD_Accession ");
        sb.Append("WHERE ");
        sb.Append("1=1");
        if (DoctorName.Length > 0)
        {
            sb.Append(" AND ACC_RequestingPhysician %STARTSWITH '" + DoctorName + "'");
        }
        if (AccountNumber.Length > 0)
        {
            sb.Append(" AND ACC_ClientDR->CLF_CLNUM ='" + AccountNumber + "'");
        }
        if (ChartNumber.Length > 0)
        {
            sb.Append(" AND ACC_MedicalRecordsNumber ='" + ChartNumber + "'");
        }
        if (!useSoundex)
        {
            if (owner.Length > 0)
            {
                sb.Append(" AND ACC_OwnerLastName %STARTSWITH '" + owner + "'");
            }
            if (PetName.Length > 0)
            {
                sb.Append(" AND ACC_PetFirstName %STARTSWITH '" + PetName + "'");
            }
        }
        else
        {
            if (owner.Length > 0)
            {
                System.Collections.Generic.Dictionary<string, string> tmpCol = new System.Collections.Generic.Dictionary<string, string>();
                tmpCol.Add("OwnerName", owner);
                tmpCol.Add("PetName", String.Empty);
                String SoundexString = cache.StoredProcedure("?=call SP2_getSoundex(?,?)", tmpCol).Value.ToString();
                if (SoundexString.Length > 0)
                {
                    SoundexString = owner + "^#" + SoundexString;
                }
                else
                {
                    SoundexString = owner + "^";
                }
                if (SoundexString.Length > 0)
                {
                    String[] OwnerPet = SoundexString.Split('#');
                    tmpCol.Clear();
                    sb.Append(" AND (");
                    for (Int32 i = 0; i < OwnerPet.Length; i++)
                    {
                        String[] tmp = OwnerPet[i].Split('^');
                        if (!tmpCol.ContainsKey(tmp[0]) && tmp[0].Length > 0)
                        {
                            tmpCol.Add(tmp[0], String.Empty);
                            sb.Append(" (ACC_PetFirstName %STARTSWITH '" + PetName + "' AND ACC_OwnerLastName %STARTSWITH '" + tmp[0] + "') OR");
                            //// Note:  'ACC_PetFirstName' is for petName with tmp[1]
                            ////        'ACC_OwnerLastName' is for owner with tmp[0]
                        }
                    }
                    sb.Remove(sb.Length - 2, 2);
                    sb.Append(" )");
                }
            }
            else if (PetName.Length > 0)
            {
                System.Collections.Generic.Dictionary<string, string> tmpCol = new System.Collections.Generic.Dictionary<string, string>();
                tmpCol.Add("OwnerName", owner);
                tmpCol.Add("PetName", PetName);
                String SoundexString = cache.StoredProcedure("?=call SP2_getSoundex(?,?)", tmpCol).Value.ToString();
                if (SoundexString.Length > 0)
                {
                    SoundexString = "^" + PetName + "#" + SoundexString;
                }
                else
                {
                    SoundexString = "^" + PetName;
                }
                if (SoundexString.Length > 0)
                {
                    String[] OwnerPet = SoundexString.Split('#');
                    tmpCol.Clear();
                    sb.Append(" AND (");
                    for (Int32 i = 0; i < OwnerPet.Length; i++)
                    {
                        String[] tmp = OwnerPet[i].Split('^');
                        if (!tmpCol.ContainsKey(tmp[1]) && tmp[1].Length > 0)
                        {
                            tmpCol.Add(tmp[1], String.Empty);
                            sb.Append(" (ACC_PetFirstName %STARTSWITH '" + tmp[1] + "' AND ACC_OwnerLastName %STARTSWITH '') OR");
                            //// Note:  'ACC_PetFirstName' is for petName with tmp[1]
                            ////        'ACC_OwnerLastName' is for owner with tmp[0]
                        }
                    }
                    sb.Remove(sb.Length - 2, 2);
                    sb.Append(" )");
                }
            }
        }
        if (!String.IsNullOrEmpty(TestCode))
        {
            sb.Append(" AND (ACC_TestsOrderedDisplayString ='" + TestCode + "' OR ACC_TestsOrderedDisplayString %STARTSWITH '" + TestCode + ",' OR ACC_TestsOrderedDisplayString LIKE '%, " + TestCode + "' OR ACC_TestsOrderedDisplayString LIKE '%, " + TestCode + ", %')");
        }
        if (!String.IsNullOrEmpty(DateFrom) && !String.IsNullOrEmpty(DateTo))
        {
            sb.Append(" AND ACC_MiniLogDate>= {d '" + DateTime.Parse(DateFrom).ToString("yyyy-MM-dd") + "'} AND ACC_MiniLogDate<={d '" + DateTime.Parse(DateTo).ToString("yyyy-MM-dd") + "'}");
        }
        if ((!String.IsNullOrEmpty(DateFrom) && !String.IsNullOrEmpty(DateTo)) || (!String.IsNullOrEmpty(AccountNumber)))
        {
            //sb.Append(" ORDER BY ACC_MiniLogDate DESC, ACC_OwnerLastName");
            sb.Append(" ORDER BY ACC_MiniLogDate DESC, ACC_MiniLogTime DESC");
        }
        else
        {
            //sb.Append(" ORDER BY ACC_OwnerLastName");
        }

        string selectStatement = sb.ToString();
        #endregion Preparing Query
        query = selectStatement;
        return cache.FillCacheDataTable(selectStatement, startIndex, noOfRecords);
    }

    #endregion Soundex Overloads

    #region Reset Search Overloads
    public static DataTable getAccessionDialogsDetails(String AccessionNumber)
    {
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ACC_Accession,");
        sb.Append("ACC_PatientName AS PATIENT,");
        sb.Append("ACC_TestsOrderedDisplayString AS TESTSORDERED,");
        sb.Append("ACC_MiniLogDate As ORDERDATE,");
        sb.Append("ACC_MiniLogTime AS OrderTime, ");
        sb.Append("ACC_ClientDR->CLF_CLNUM AS ACCOUNTNUM ");
        sb.Append("FROM ");
        sb.Append("ORD_Accession ");
        sb.Append("WHERE ");
        sb.Append("1=1");
        if (!String.IsNullOrEmpty(AccessionNumber))
        {
            sb.Append(" AND ACC_Accession ='" + AccessionNumber + "'");
        }
        string selectStatement = sb.ToString();

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement);
    }

    public static DataTable getAccessionDetailsExtended_OnlyforTestName(String TestCode, Int32 noOfRecords, String pageName, Int32 timeout, out Boolean isError)
    {
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        #region Preparing Query
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT TOP ");
        sb.Append(noOfRecords.ToString());
        sb.Append(" ACC_Accession,");
        sb.Append("ACC_OwnerLastName||', '||ACC_PetFirstName AS PATIENT,");     // Last Name + First Name
        sb.Append("ACC_TestsOrderedDisplayString AS TESTSORDERED,");
        sb.Append("ACC_ClientDR->CLF_CLNUM AS ACCOUNTNUM,");
        sb.Append("ACC_MiniLogDate As ORDERDATE,");
        sb.Append("ACC_MiniLogTime As ORDERTIME,");
        sb.Append("ACC_ReportStatus As ReportStatus,");
        sb.Append("ACC_IsStat As StatDesignation,");
        sb.Append("$$GETINQNOTE^XT11(ACC_Accession) As InqNotes ");
        sb.Append("FROM ");
        sb.Append("ORD_Accession ");
        sb.Append("WHERE ");
        sb.Append("1=1");
        sb.Append(" AND (ACC_TestsOrderedDisplayString ='" + TestCode + "' OR ACC_TestsOrderedDisplayString %STARTSWITH '" + TestCode + ",' OR ACC_TestsOrderedDisplayString LIKE '%, " + TestCode + "' OR ACC_TestsOrderedDisplayString LIKE '%, " + TestCode + ", %')");
        String selectStatement = sb.ToString();
        #endregion Preparing Query
        return cache.FillCacheDataTable(selectStatement, pageName, timeout, out isError);
    }

    
    public static DataTable getAccessionDetailsByAccessionNumber(String AccessionNumber, String pageName, Int32 timeout, out Boolean isError)
    {
        #region Preparing Query
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ACC_Accession,");
        sb.Append("ACC_PatientName AS PATIENT,"); //SSM 13/10/2011 Issue#114139 Commented for passing the formatted patient name
        sb.Append("ACC_OwnerLastName||', '||ACC_PetFirstName AS PATIENTFULLNAME,"); //SSM 18/10/2011 #Issue114138 Modified fieldName for not to interrupt other feature
        sb.Append("ACC_TestsOrderedDisplayString AS TESTSORDERED,");
        sb.Append("ACC_ClientDR->CLF_CLNUM AS ACCOUNTNUM,");
        sb.Append("ACC_MiniLogDate As ORDERDATE,");
        sb.Append("ACC_MiniLogTime As ORDERTIME,");
        sb.Append("ACC_ReportStatus As ReportStatus,");
        sb.Append("ACC_IsStat As StatDesignation,");
        sb.Append("$$GETINQNOTE^XT11(ACC_Accession) As InqNotes ");
        sb.Append("FROM ");
        sb.Append("ORD_Accession ");
        sb.Append("WHERE ");
        sb.Append("ACC_Accession  ='" + AccessionNumber + "'");
        string selectStatement = sb.ToString();
        #endregion Preparing Query

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement, pageName, timeout, out isError);
    }
    
    public static DataTable getAccessionDetailsByZoasisReq(String ZoasisReq, String pageName, Int32 timeout, out Boolean isError)
    {
        #region Preparing Query
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ACC_Accession,");
        sb.Append("ACC_PatientName AS PATIENT,");
        sb.Append("ACC_TestsOrderedDisplayString AS TESTSORDERED,");
        sb.Append("ACC_ClientDR->CLF_CLNUM AS ACCOUNTNUM,");
        sb.Append("ACC_MiniLogDate As ORDERDATE,");
        sb.Append("ACC_MiniLogTime As ORDERTIME,");
        sb.Append("ACC_ReportStatus As ReportStatus,");
        sb.Append("ACC_IsStat As StatDesignation,");
        sb.Append("$$GETINQNOTE^XT11(ACC_Accession) As InqNotes ");
        sb.Append("FROM ");
        sb.Append("ORD_Accession ");
        sb.Append("WHERE ");
        sb.Append("ACC_RequesitionNumber  ='" + ZoasisReq + "'");
        string selectStatement = sb.ToString();
        #endregion Preparing Query

        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement, pageName, timeout, out isError);
    }
    

    public static DataTable getAccessionDetailsExtended(String AccountNumber, String ChartNumber, String PetName, String owner, String TestCode, String DateFrom, String DateTo, String DoctorName, Int32 startIndex, Int32 noOfRecords, out String query, Boolean useSoundex, String pageName, Int32 timeout, out Boolean isError)
    {
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        #region Preparing Query
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ACC_Accession,");
        sb.Append("ACC_OwnerLastName||', '||ACC_PetFirstName AS PATIENT,");
        sb.Append("ACC_TestsOrderedDisplayString AS TESTSORDERED,");
        sb.Append("ACC_ClientDR->CLF_CLNUM AS ACCOUNTNUM,");
        sb.Append("ACC_MiniLogDate As ORDERDATE,");
        sb.Append("ACC_MiniLogTime As ORDERTIME,");
        sb.Append("ACC_ReportStatus As ReportStatus,");
        sb.Append("ACC_IsStat As StatDesignation,");
        sb.Append("$$GETINQNOTE^XT11(ACC_Accession) As InqNotes ");
        sb.Append("FROM ");
        sb.Append("ORD_Accession ");
        sb.Append("WHERE ");
        sb.Append("1=1");
        //There is an index with Doctor & Account.
        //If Doctor is present as search criteria, then client has to be present in the search criteria
        if (DoctorName.Length > 0)
        {
            sb.Append(" AND ACC_RequestingPhysician %STARTSWITH '" + DoctorName + "'");

            if (AccountNumber.Length > 0)
            {
                sb.Append(" AND ACC_ClientDR->CLF_CLNUM ='" + AccountNumber + "'");
            }
            else if (DateFrom.Length == 0 && DateTo.Length == 0)    //SSM 03/11/2011 #Issue115786 Migrating Accession search Changes
            {
                sb.Append(" AND ACC_ClientDR->CLF_CLNUM <>''");
            }
        }
        else if (AccountNumber.Length > 0)
        {
            sb.Append(" AND ACC_ClientDR->CLF_CLNUM ='" + AccountNumber + "'");
        }
        if (ChartNumber.Length > 0)
        {
            sb.Append(" AND ACC_MedicalRecordsNumber ='" + ChartNumber + "'");
        }
        if (!useSoundex)
        {
            if (owner.Length > 0)
            {
                sb.Append(" AND ACC_OwnerLastName %STARTSWITH '" + owner + "'");
            }
            if (PetName.Length > 0)
            {
                sb.Append(" AND ACC_PetFirstName %STARTSWITH '" + PetName + "'");
            }
        }
        else
        {
            if (owner.Length > 0)
            {
                System.Collections.Generic.Dictionary<string, string> tmpCol = new System.Collections.Generic.Dictionary<string, string>();
                tmpCol.Add("OwnerName", owner);
                tmpCol.Add("PetName", String.Empty);
                //String SoundexString = cache.StoredProcedure("?=call SP2_getSoundex(?,?)", tmpCol).Value.ToString();
                String SoundexString = cache.StoredProcedure("?=call SP2_getSoundex(?,?)", tmpCol, pageName, timeout, out isError).Value.ToString();
                if (isError) // for Reset Search
                {
                    query = "";
                    return null;
                }
                if (SoundexString.Length > 0)
                {
                    SoundexString = owner + "^#" + SoundexString;
                }
                else
                {
                    SoundexString = owner + "^";
                }
                if (SoundexString.Length > 0)
                {
                    String[] OwnerPet = SoundexString.Split('#');
                    tmpCol.Clear();
                    sb.Append(" AND (");
                    for (Int32 i = 0; i < OwnerPet.Length; i++)
                    {
                        String[] tmp = OwnerPet[i].Split('^');
                        if (!tmpCol.ContainsKey(tmp[0]) && tmp[0].Length > 0)
                        {
                            tmpCol.Add(tmp[0], String.Empty);
                            sb.Append(" (ACC_PetFirstName %STARTSWITH '" + PetName + "' AND ACC_OwnerLastName %STARTSWITH '" + tmp[0] + "') OR");
                            //// Note:  'ACC_PetFirstName' is for petName with tmp[1]
                            ////        'ACC_OwnerLastName' is for owner with tmp[0]
                        }
                    }
                    sb.Remove(sb.Length - 2, 2);
                    sb.Append(" )");
                }
            }
            else if (PetName.Length > 0)
            {
                System.Collections.Generic.Dictionary<string, string> tmpCol = new System.Collections.Generic.Dictionary<string, string>();
                tmpCol.Add("OwnerName", owner);
                tmpCol.Add("PetName", PetName);
                // String SoundexString = cache.StoredProcedure("?=call SP2_getSoundex(?,?)", tmpCol).Value.ToString();
                String SoundexString = cache.StoredProcedure("?=call SP2_getSoundex(?,?)", tmpCol, pageName, timeout, out isError).Value.ToString();
                if (isError) // for Reset Search
                {
                    query = "";
                    return null;
                }

                if (SoundexString.Length > 0)
                {
                    SoundexString = "^" + PetName + "#" + SoundexString;
                }
                else
                {
                    SoundexString = "^" + PetName;
                }
                if (SoundexString.Length > 0)
                {
                    String[] OwnerPet = SoundexString.Split('#');
                    tmpCol.Clear();
                    sb.Append(" AND (");
                    for (Int32 i = 0; i < OwnerPet.Length; i++)
                    {
                        String[] tmp = OwnerPet[i].Split('^');
                        if (!tmpCol.ContainsKey(tmp[1]) && tmp[1].Length > 0)
                        {
                            tmpCol.Add(tmp[1], String.Empty);
                            sb.Append(" (ACC_PetFirstName %STARTSWITH '" + tmp[1] + "' AND ACC_OwnerLastName %STARTSWITH '') OR");
                            //// Note:  'ACC_PetFirstName' is for petName with tmp[1]
                            ////        'ACC_OwnerLastName' is for owner with tmp[0]
                        }
                    }
                    sb.Remove(sb.Length - 2, 2);
                    sb.Append(" )");
                }
            }
        }
        if (!String.IsNullOrEmpty(TestCode))
        {
            TestCode = TestCode.Replace("'", "''");
            sb.Append(" AND (ACC_TestsOrderedDisplayString ='" + TestCode + "' OR ACC_TestsOrderedDisplayString %STARTSWITH '" + TestCode + ",' OR ACC_TestsOrderedDisplayString LIKE '%, " + TestCode + "' OR ACC_TestsOrderedDisplayString LIKE '%, " + TestCode + ", %')");
        }
        if (!String.IsNullOrEmpty(DateFrom) && !String.IsNullOrEmpty(DateTo))
        {
            sb.Append(" AND ACC_MiniLogDate>= {d '" + DateTime.Parse(DateFrom).ToString("yyyy-MM-dd") + "'} AND ACC_MiniLogDate<={d '" + DateTime.Parse(DateTo).ToString("yyyy-MM-dd") + "'}");
        }
        if ((!String.IsNullOrEmpty(DateFrom) && !String.IsNullOrEmpty(DateTo)) || (!String.IsNullOrEmpty(AccountNumber)))
        {
            //sb.Append(" ORDER BY ACC_MiniLogDate DESC, ACC_OwnerLastName");
            sb.Append(" ORDER BY ACC_MiniLogDate DESC, ACC_MiniLogTime DESC");
        }
        else
        {
            //sb.Append(" ORDER BY ACC_OwnerLastName");
        }

        string selectStatement = sb.ToString();
        #endregion Preparing Query
        query = selectStatement;
        return cache.FillCacheDataTable(selectStatement, startIndex, noOfRecords, pageName, timeout, out isError);
    }

    public static DataTable getRelatedAccessions(String PetName, String owner, String DateFrom, String DateTo, String AccountNo)
    {
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        #region Preparing Query
        System.Text.StringBuilder sb = new System.Text.StringBuilder();
        sb.Append("SELECT ");
        sb.Append("ACC_Accession,");
        sb.Append("ACC_OwnerLastName||', '||ACC_PetFirstName AS PATIENT,");
        sb.Append("ACC_TestsOrderedDisplayString AS TESTSORDERED,");
        sb.Append("ACC_ClientDR->CLF_CLNUM AS ACCOUNTNUM,");
        sb.Append("ACC_MiniLogDate As ORDERDATE,");
        sb.Append("ACC_MiniLogTime As ORDERTIME ");
        sb.Append("FROM ");
        sb.Append("ORD_Accession ");
        sb.Append("WHERE ");
        sb.Append("ACC_ClientDR->CLF_CLNUM = '");
        sb.Append(AccountNo);
        sb.Append("' AND ACC_OwnerLastName ='");
        sb.Append(owner);
        sb.Append("' AND ACC_PetFirstName = '");
        sb.Append(PetName);
        sb.Append("' AND ACC_MiniLogDate>= {d '");
        sb.Append(DateTime.Parse(DateFrom).ToString("yyyy-MM-dd"));
        sb.Append("'} AND ACC_MiniLogDate<={d '");
        sb.Append(DateTime.Parse(DateTo).ToString("yyyy-MM-dd"));
        sb.Append("'}");
        sb.Append(" AND $$ISCONSULT^XT11(ACC_Accession)='0' ");
        sb.Append(" ORDER BY ACC_MiniLogDate DESC, ACC_MiniLogTime DESC");

        #endregion Preparing Query

        return cache.FillCacheDataTable(sb.ToString());
    }

    public static DataTable getAccessionDetailsExtended(String selectStatement, Int32 startIndex, Int32 noOfRecords, String pageName, Int32 timeout, out Boolean isError)
    {
        CACHEDAL.ConnectionClass cache = new CACHEDAL.ConnectionClass();
        return cache.FillCacheDataTable(selectStatement, startIndex, noOfRecords, pageName, timeout, out isError);
    }

    #endregion Reset Search Overloads
}