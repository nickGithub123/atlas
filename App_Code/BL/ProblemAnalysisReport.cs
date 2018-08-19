using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data;

/// <summary>
/// Summary description for ProblemAnalysisReport
/// </summary>
public class ProblemAnalysisReport
{
	public ProblemAnalysisReport()
	{
		
	}

    #region Problem Report Section

    public static DataTable getProblemSuperCatReport()
    {
        return DL_ProblemAnalysisReport.getProblemSuperCatReport();
    }

    public static DataTable getProblemAnalysisByLabLocationSummary(string strQS, out string dateFrom, out string dateTo)
    {
        String[] QS = strQS.Split('^');
        dateFrom = QS[0];
        dateTo = QS[1];
        DataTable dtRawData = DL_ProblemAnalysisReport.getProblemAnalysisByLabLocationSummary(dateFrom, dateTo, FormatInputString(QS[2]), FormatInputString(QS[3]), FormatInputString(QS[4]), FormatInputString(QS[5]), FormatInputString(QS[6]));
        DataTable dtReport = new DataTable();
        dtReport.Columns.Add("LabLocation", typeof(string));
        dtReport.Columns.Add("TotalProbs", typeof(int));
        dtReport.Columns.Add("LabProbs", typeof(int));
        dtReport.Columns.Add("TransProbs", typeof(int));
        dtReport.Columns.Add("MissingProbs", typeof(int));
        dtReport.Columns.Add("SubmissionProbs", typeof(int));
        dtReport.Columns.Add("OtherProbs", typeof(int));

        string strLabLocation = "";
        string strOldLabLocation = "";
        string strProbType = "";
        int intTotalProbs = 0, intLabProbs = 0, intTransProbs = 0, intMissingProbs = 0, intSubmissionProbs = 0, intOtherProbs = 0;

        if (dtRawData != null && dtRawData.Rows.Count > 0)
        {
            for (int intCnt = 0; intCnt < dtRawData.Rows.Count; intCnt++)
            {
                strLabLocation = dtRawData.Rows[intCnt]["Location"].ToString();
                if (strLabLocation.Length == 0)
                {
                    continue;
                }
                if (strLabLocation.Equals(strOldLabLocation) || strOldLabLocation.Length == 0)
                {
                    strProbType = dtRawData.Rows[intCnt]["Type"].ToString();
                    switch (strProbType)
                    {
                        case "LAB":
                            intLabProbs++;
                            break;
                        case "TRANSPORTATION":
                            intTransProbs++;
                            break;
                        case "MISSING":
                            intMissingProbs++;
                            break;
                        case "SUBMISSION":
                            intSubmissionProbs++;
                            break;
                        default:
                            intOtherProbs++;
                            break;
                    }

                }
                else
                {
                    intTotalProbs = intLabProbs + intTransProbs + intMissingProbs + intSubmissionProbs + intOtherProbs;
                    if (intTotalProbs > 0)
                    {
                        DataRow drNewRow = dtReport.NewRow();
                        drNewRow["LabLocation"] = strOldLabLocation;
                        drNewRow["TotalProbs"] = intTotalProbs;
                        drNewRow["LabProbs"] = intLabProbs;
                        drNewRow["TransProbs"] = intTransProbs;
                        drNewRow["MissingProbs"] = intMissingProbs;
                        drNewRow["SubmissionProbs"] = intSubmissionProbs;
                        drNewRow["OtherProbs"] = intOtherProbs;
                        dtReport.Rows.Add(drNewRow);
                    }
                    intLabProbs = 0; intTransProbs = 0; intMissingProbs = 0; intSubmissionProbs = 0; intOtherProbs = 0; intTotalProbs = 0;
                    intCnt--;
                }
                strOldLabLocation = strLabLocation;
            }

            intTotalProbs = intLabProbs + intTransProbs + intMissingProbs + intSubmissionProbs + intOtherProbs;
            if (intTotalProbs > 0)
            {
                DataRow drNewRow = dtReport.NewRow();
                drNewRow["LabLocation"] = strOldLabLocation;
                drNewRow["TotalProbs"] = intTotalProbs;
                drNewRow["LabProbs"] = intLabProbs;
                drNewRow["TransProbs"] = intTransProbs;
                drNewRow["MissingProbs"] = intMissingProbs;
                drNewRow["SubmissionProbs"] = intSubmissionProbs;
                drNewRow["OtherProbs"] = intOtherProbs;
                dtReport.Rows.Add(drNewRow);
            }
        }
        return dtReport;
    }

    public static DataTable getProblemAnalysisByTerritorySummary(string strQS, out string dateFrom, out string dateTo,out int iTotalProbAllLabs)
    {
        String[] QS = strQS.Split('^');
        dateFrom = QS[0];
        dateTo = QS[1];
        DataTable dtProbCount = DL_ProblemAnalysisReport.getTotalProbCount(dateFrom, dateTo);
        DataTable dtRawData = DL_ProblemAnalysisReport.getProblemAnalysisByTerritorySummary(dateFrom, dateTo, FormatInputString(QS[2]), FormatInputString(QS[3]), FormatInputString(QS[4]), FormatInputString(QS[5]), FormatInputString(QS[6]));
        DataTable dtReport = new DataTable();
        dtReport.Columns.Add("Territory", typeof(string));
        dtReport.Columns.Add("SalesRepName", typeof(string));
        dtReport.Columns.Add("TotalProbs", typeof(int));
        dtReport.Columns.Add("PercProbs", typeof(double));
        dtReport.Columns.Add("LabProbs", typeof(string));
        dtReport.Columns.Add("TransProbs", typeof(string));
        dtReport.Columns.Add("MissingProbs", typeof(string));
        dtReport.Columns.Add("SubmissionProbs", typeof(string));
        dtReport.Columns.Add("OtherProbs", typeof(string));

        string strTerritory = "";
        string strOldTerritory = "";
        string strProbType = "";
        double dPercProbs = 0.000;
        string strSalesRep = "";
        iTotalProbAllLabs = 0;
        if (dtProbCount != null && dtProbCount.Rows.Count > 0)
        {
            iTotalProbAllLabs = Convert.ToInt32(dtProbCount.Rows[0]["TotalProbsAll"].ToString());
        }
        int intTotalProbs = 0, intLabProbs = 0, intTransProbs = 0, intMissingProbs = 0, intSubmissionProbs = 0, intOtherProbs = 0;

        string strLabProb = string.Empty, strTransProbs = string.Empty, strMissingProbs = string.Empty, strSubmissionProbs = string.Empty, strOtherProbs = string.Empty;
        string strProlemTypeGroup = FormatInputString(QS[4]).Trim();
        if (strProlemTypeGroup.Length > 0)
        {
            if (strProlemTypeGroup.IndexOf("LAB") < 0)
            {
                strLabProb = "N/A";
            }
            if (strProlemTypeGroup.IndexOf("TRANSPORTATION") < 0)
            {
                strTransProbs = "N/A";
            }
            if (strProlemTypeGroup.IndexOf("MISSING") < 0)
            {
                strMissingProbs = "N/A";
            }
            if (strProlemTypeGroup.IndexOf("SUBMISSION") < 0)
            {
                strSubmissionProbs = "N/A";
            }
            if (strProlemTypeGroup.IndexOf("OTHER") < 0)
            {
                strOtherProbs = "N/A";
            }
        }

        if (dtRawData != null && dtRawData.Rows.Count > 0)
        {
            for (int intCnt = 0; intCnt < dtRawData.Rows.Count; intCnt++)
            {
                strTerritory = dtRawData.Rows[intCnt]["Territory"].ToString();
                if (strTerritory.Length == 0)
                {
                    continue;
                }
                if (strTerritory.Equals(strOldTerritory) || strOldTerritory.Length == 0)
                {
                    strProbType = dtRawData.Rows[intCnt]["Type"].ToString();
                    strSalesRep = dtRawData.Rows[intCnt]["SalesRepName"].ToString();
                    switch (strProbType)
                    {
                        case "LAB":
                            intLabProbs++;
                            break;
                        case "TRANSPORTATION":
                            intTransProbs++;
                            break;
                        case "MISSING":
                            intMissingProbs++;
                            break;
                        case "SUBMISSION":
                            intSubmissionProbs++;
                            break;
                        default:
                            intOtherProbs++;
                            break;
                    }

                }
                else
                {
                    intTotalProbs = intLabProbs + intTransProbs + intMissingProbs + intSubmissionProbs + intOtherProbs;
                    if (iTotalProbAllLabs > 0)
                    {
                        dPercProbs = Math.Round((Convert.ToDouble(intTotalProbs) / Convert.ToDouble(iTotalProbAllLabs)) * 100, 3);
                    }
                    if (intTotalProbs > 0)
                    {
                        DataRow drNewRow = dtReport.NewRow();
                        drNewRow["Territory"] = strOldTerritory;
                        drNewRow["SalesRepName"] = strSalesRep;
                        drNewRow["TotalProbs"] = intTotalProbs;
                        drNewRow["PercProbs"] = dPercProbs;
                        drNewRow["LabProbs"] = (strLabProb.Length > 0) ? strLabProb : intLabProbs.ToString();
                        drNewRow["TransProbs"] = (strTransProbs.Length > 0) ? strTransProbs : intTransProbs.ToString();
                        drNewRow["MissingProbs"] = (strMissingProbs.Length > 0) ? strMissingProbs : intMissingProbs.ToString();
                        drNewRow["SubmissionProbs"] = (strSubmissionProbs.Length > 0) ? strSubmissionProbs : intSubmissionProbs.ToString();
                        drNewRow["OtherProbs"] = (strOtherProbs.Length > 0) ? strOtherProbs : intOtherProbs.ToString();
                        dtReport.Rows.Add(drNewRow);
                    }
                    strSalesRep = "";
                    dPercProbs = 0.000;
                    intLabProbs = 0; intTransProbs = 0; intMissingProbs = 0; intSubmissionProbs = 0; intOtherProbs = 0; intTotalProbs = 0;
                    intCnt--;
                }
                strOldTerritory = strTerritory;
            }

            intTotalProbs = intLabProbs + intTransProbs + intMissingProbs + intSubmissionProbs + intOtherProbs;
            dPercProbs = Math.Round((Convert.ToDouble(intTotalProbs) / Convert.ToDouble(iTotalProbAllLabs)) * 100, 3);
            if (intTotalProbs > 0)
            {
                DataRow drNewRow = dtReport.NewRow();
                drNewRow["Territory"] = strOldTerritory;
                drNewRow["SalesRepName"] = strSalesRep;
                drNewRow["TotalProbs"] = intTotalProbs;
                drNewRow["PercProbs"] = dPercProbs;
                drNewRow["LabProbs"] = (strLabProb.Length > 0) ? strLabProb : intLabProbs.ToString();
                drNewRow["TransProbs"] = (strTransProbs.Length > 0) ? strTransProbs : intTransProbs.ToString();
                drNewRow["MissingProbs"] = (strMissingProbs.Length > 0) ? strMissingProbs : intMissingProbs.ToString();
                drNewRow["SubmissionProbs"] = (strSubmissionProbs.Length > 0) ? strSubmissionProbs : intSubmissionProbs.ToString();
                drNewRow["OtherProbs"] = (strOtherProbs.Length > 0) ? strOtherProbs : intOtherProbs.ToString();
                dtReport.Rows.Add(drNewRow);
            }
        }
        dtReport.DefaultView.Sort = "TotalProbs DESC";
        dtReport = dtReport.DefaultView.ToTable();
        return dtReport;
    }

    public static DataTable getProblemAnalysisByLabLocationDetails(string strQS, out string dateFrom, out string dateTo)
    {
        String[] QS = strQS.Split('^');
        dateFrom = QS[0];
        dateTo = QS[1];
        DataTable dtProbCount = DL_ProblemAnalysisReport.getTotalProbAccCountForLab(dateFrom, dateTo, QS[2]);
        DataTable dtProbByLab = DL_ProblemAnalysisReport.getProbAnalysisByLab(dateFrom, dateTo, FormatInputString(QS[2]), FormatInputString(QS[3]), FormatInputString(QS[4]), FormatInputString(QS[5]), FormatInputString(QS[6]));
        DataTable dtReport = new DataTable();
        dtReport.Columns.Add("LabLocation", typeof(string));
        dtReport.Columns.Add("TotalProbs", typeof(double));
        dtReport.Columns.Add("TotalProbsAll", typeof(int));
        dtReport.Columns.Add("PercentageProbs", typeof(double));
        dtReport.Columns.Add("DetailTable", typeof(DataTable));
        dtReport.Columns.Add("TotalAccAll", typeof(int));
        dtReport.Columns.Add("TotalAccLab", typeof(int));

        int iTotalProbAll = 0;
        int iTotalAccAll = 0;
        int iTotalAccLab = 0;

        if(dtProbCount !=null && dtProbByLab.Rows.Count >0)
        {
            iTotalProbAll = Convert.ToInt32(dtProbCount.Rows[0]["TotalProbsAll"]);
            iTotalAccAll = Convert.ToInt32(dtProbCount.Rows[0]["TotalAccAll"]);
        }
        
        if (dtProbByLab != null && dtProbByLab.Rows.Count > 0)
        {
            DataTable dtReportSchema = new DataTable();
            dtReportSchema.Columns.Add("Type", typeof(string));
            dtReportSchema.Columns.Add("DESC", typeof(string));
            dtReportSchema.Columns.Add("Count", typeof(int));
            dtReportSchema.Columns.Add("PercentageProb", typeof(double));

            DataTable dtReportChild = DL_ProblemAnalysisReport.getProbAnalysisByLabChildDetails(dateFrom, dateTo, FormatInputString(QS[2]), FormatInputString(QS[3]), FormatInputString(QS[4]), FormatInputString(QS[5]), FormatInputString(QS[6]));
            string strLabLocations = "";

            for (int intRowCnt = 0; intRowCnt < dtProbByLab.Rows.Count; intRowCnt++)
            {
                if (strLabLocations.Length > 0)
                {
                    strLabLocations = strLabLocations + ",";
                }
                strLabLocations = strLabLocations + dtProbByLab.Rows[intRowCnt]["Location"].ToString();
            }

            DataTable dtLabAccCount = DL_ProblemAnalysisReport.getTotalAccCountForLab(dateFrom, dateTo, strLabLocations);
            for (int intRowCnt = 0; intRowCnt < dtProbByLab.Rows.Count; intRowCnt++)
            {
                string strLabLocation = dtProbByLab.Rows[intRowCnt]["Location"].ToString();
                double dProbCount = Convert.ToDouble(dtProbByLab.Rows[intRowCnt]["CountValue"]);
                double dProbPercent = 0.000;
                if (iTotalProbAll > 0)
                {
                    dProbPercent = Math.Round((dProbCount / Convert.ToDouble(iTotalProbAll)) * 100, 3);
                }
                if (strLabLocation.Length > 0)
                {
                    DataTable dtChildDetails = getProblemAnalysisByLabLocChild(FormatInputString(strLabLocation), dProbCount, dtReportChild, dtLabAccCount, out iTotalAccLab, dtReportSchema.Clone());
                    dtReport.Rows.Add(strLabLocation, dProbCount, iTotalProbAll, dProbPercent, dtChildDetails, iTotalAccAll, iTotalAccLab);
                }
            }
        }
        return dtReport;
    }

    public static DataTable getProblemAnalysisByLabLocChild(string strLabLocation, double dProbCount, DataTable dtReportChild, DataTable dtLabAccCount, out int iTotalAccLab,DataTable reportTable)
    {
        iTotalAccLab = 0;
        
        DataRow[] drReport = dtReportChild.Select("Lab=" + strLabLocation + "", "Type ASC");
        DataRow[] drReportLabCount = dtLabAccCount.Select("Lab=" + strLabLocation + "");
        Int32 iTotalProbs = 0;
        double dPercTotal = 0.000;
        string strGroupName = "";
        string strOldGroupName = "";

        if (drReportLabCount != null && drReportLabCount.Length > 0)
        {
            iTotalAccLab = Convert.ToInt32(drReportLabCount[0]["Count"]);
        }
        if (drReport != null && drReport.Length > 0)
        {
            for (int iCnt = 0; iCnt < drReport.Length; iCnt++)
            {
                strGroupName = drReport[iCnt]["Type"].ToString();
                if (strGroupName.Equals(strOldGroupName) || iCnt == 0)
                {
                    string strCount = drReport[iCnt]["CountValue"].ToString();
                    string strDesc = drReport[iCnt]["DESCVALUE"].ToString();
                    if (strCount.Length > 0)
                    {
                        iTotalProbs += Convert.ToInt32(strCount);
                    }
                    double dPercent = Math.Round((Convert.ToDouble(strCount) / dProbCount) * 100, 3);
                    dPercTotal += dPercent;
                    reportTable.Rows.Add(strGroupName, strDesc, strCount, dPercent);
                }
                else
                {
                    reportTable.Rows.Add("TOTAL", "Total " + strOldGroupName, iTotalProbs, dPercTotal);
                    iTotalProbs = 0;
                    dPercTotal = 0.000;
                    iCnt--;
                }
                strOldGroupName = strGroupName;
            }
            reportTable.Rows.Add("TOTAL", "Total " + strOldGroupName, iTotalProbs, dPercTotal);
        }

        return reportTable;
    }

    public static DataTable getProblemAnalysisByTerritoryDetails(string strQS, out string dateFrom, out string dateTo)
    {
        String[] QS = strQS.Split('^');
        dateFrom = QS[0];
        dateTo = QS[1];
        DataTable dtProbByTerritory = DL_ProblemAnalysisReport.getProbAnalysisByTerritory(dateFrom, dateTo, FormatInputString(QS[2]), FormatInputString(QS[3]), FormatInputString(QS[4]), FormatInputString(QS[5]), FormatInputString(QS[6]));
        DataTable dtReport = new DataTable();
        dtReport.Columns.Add("Territory", typeof(string));
        dtReport.Columns.Add("SalesRep", typeof(string));
        dtReport.Columns.Add("TotalProbs", typeof(int));
        dtReport.Columns.Add("TotalProbsALL", typeof(int));
        dtReport.Columns.Add("PercentageProbs", typeof(double));
        dtReport.Columns.Add("DetailTable", typeof(DataTable));


        if (dtProbByTerritory != null && dtProbByTerritory.Rows.Count > 0)
        {
            DataTable dtReportSchema = new DataTable();
            dtReportSchema.Columns.Add("Type", typeof(string));
            dtReportSchema.Columns.Add("DESC", typeof(string));
            dtReportSchema.Columns.Add("Count", typeof(int));
            dtReportSchema.Columns.Add("PercentageProb", typeof(double));

            double dProbPercent = 0.000;
            DataTable dtReportChild = DL_ProblemAnalysisReport.getProbAnalysisByTerritoryChildDetails(dateFrom, dateTo, FormatInputString(QS[2]), FormatInputString(QS[3]), FormatInputString(QS[4]), FormatInputString(QS[5]), FormatInputString(QS[6]));
            for (int intRowCnt = 0; intRowCnt < dtProbByTerritory.Rows.Count; intRowCnt++)
            {
                string strTerritory = dtProbByTerritory.Rows[intRowCnt]["Territory"].ToString();
                string strSalesRep = dtProbByTerritory.Rows[intRowCnt]["SalesRepName"].ToString();
                int iTotalProbsAll = Convert.ToInt32(dtProbByTerritory.Rows[intRowCnt]["TotalProbsALL"]);
                double dProbCount = Convert.ToInt32(dtProbByTerritory.Rows[intRowCnt]["CountValue"].ToString());
                dProbPercent = Math.Round((dProbCount / Convert.ToDouble(iTotalProbsAll)) * 100, 3);
                if (strTerritory.Length > 0 && dProbCount > 0)
                {
                    DataTable dtChildDetails = getProblemAnalysisByTerritoryChild(dProbCount, FormatInputString(strTerritory), dtReportChild,dtReportSchema.Clone());
                    dtReport.Rows.Add(strTerritory, strSalesRep, dProbCount, iTotalProbsAll, dProbPercent, dtChildDetails);
                }
            }
        }
        return dtReport;
    }

    public static DataTable getProblemAnalysisByTerritoryChild(double dProbCount, string strSalesTerritory, DataTable dtReportChild, DataTable dtReport)
    {
        DataRow[] drReport = dtReportChild.Select("Territory=" + strSalesTerritory + "","Type ASC");
        Int32 iTotalProbs = 0;
        double dPercTotal = 0.000;
        string strGroupName = "";
        string strOldGroupName = "";
        
        if (drReport != null && drReport.Length > 0)
        {
            for (int iCnt = 0; iCnt < drReport.Length; iCnt++)
            {
                strGroupName = drReport[iCnt]["Type"].ToString();
                if (strGroupName.Equals(strOldGroupName) || iCnt == 0)
                {
                    string strCount = drReport[iCnt]["CountValue"].ToString();
                    string strDesc = drReport[iCnt]["DESCVALUE"].ToString();
                    if (strCount.Length > 0)
                    {
                        iTotalProbs += Convert.ToInt32(strCount);
                    }
                    double dPercent = Math.Round((Convert.ToDouble(strCount) / dProbCount) * 100, 3);
                    dPercTotal += dPercent;
                    dtReport.Rows.Add(strGroupName, strDesc, strCount, dPercent);
                }
                else
                {
                    dtReport.Rows.Add("TOTAL", "Total " + strOldGroupName, iTotalProbs, dPercTotal);
                    iTotalProbs = 0;
                    dPercTotal = 0.000;
                    iCnt--;
                }
                strOldGroupName = strGroupName;
            }
            dtReport.Rows.Add("TOTAL", "Total " + strOldGroupName, iTotalProbs, dPercTotal);
        }

        return dtReport;
    }

    public static DataTable getProblemByAccountSummary(string strQS, out string dateFrom, out string dateTo)
    {
        String[] QS = strQS.Split('^');
        dateFrom = QS[0];
        dateTo = QS[1];
        DataTable dtRawData = DL_ProblemAnalysisReport.getProblemByAcctSummary(dateFrom, dateTo, FormatInputString(QS[2]), FormatInputString(QS[3]), FormatInputString(QS[4]), FormatInputString(QS[5]), FormatInputString(QS[6]));
        DataTable dtReport = new DataTable();
        dtReport.Columns.Add("Acct", typeof(string));
        dtReport.Columns.Add("AcctName", typeof(string));
        dtReport.Columns.Add("Territory", typeof(string));
        dtReport.Columns.Add("SalesRep", typeof(string));
        dtReport.Columns.Add("Rev1", typeof(double));
        dtReport.Columns.Add("Rev2", typeof(double));
        dtReport.Columns.Add("Rev3", typeof(double));
        dtReport.Columns.Add("TotalProbs", typeof(int));
        dtReport.Columns.Add("LabProbs", typeof(int));
        dtReport.Columns.Add("TransProbs", typeof(int));
        dtReport.Columns.Add("MissingProbs", typeof(int));
        dtReport.Columns.Add("SubmissionProbs", typeof(int));
        dtReport.Columns.Add("OtherProbs", typeof(int));

        string strAcct = "";
        string strOldAcct = "";
        string strProbType = "";
        string strSalesRep = "";
        string strTerritory = "";
        string strAcctName = "";
        string[] strRevenue;
        double dRev1 = 0.000; double dRev2 = 0.000; double dRev3 = 0.000;

        int intTotalProbs = 0, intLabProbs = 0, intTransProbs = 0, intMissingProbs = 0, intSubmissionProbs = 0, intOtherProbs = 0;

        if (dtRawData != null && dtRawData.Rows.Count > 0)
        {
            for (int intCnt = 0; intCnt < dtRawData.Rows.Count; intCnt++)
            {
                strAcct = dtRawData.Rows[intCnt]["Acct"].ToString();
                if (strAcct.Length == 0)
                {
                    continue;
                }
                if (strAcct.Equals(strOldAcct) || strOldAcct.Length == 0)
                {
                    strProbType = dtRawData.Rows[intCnt]["Type"].ToString();
                    strSalesRep = dtRawData.Rows[intCnt]["SalesRepName"].ToString();
                    strTerritory = dtRawData.Rows[intCnt]["Territory"].ToString();
                    strAcctName = dtRawData.Rows[intCnt]["AcctName"].ToString();
                    strRevenue = dtRawData.Rows[intCnt]["ClientRevenue"].ToString().Split('^');
                    dRev1 = (strRevenue[0].Length == 0 ? 0.000 : Convert.ToDouble(strRevenue[0]));
                    dRev2 = (strRevenue[1].Length == 0 ? 0.000 : Convert.ToDouble(strRevenue[1]));
                    dRev3 = (strRevenue[2].Length == 0 ? 0.000 : Convert.ToDouble(strRevenue[2]));

                    switch (strProbType)
                    {
                        case "LAB":
                            intLabProbs++;
                            break;
                        case "TRANSPORTATION":
                            intTransProbs++;
                            break;
                        case "MISSING":
                            intMissingProbs++;
                            break;
                        case "SUBMISSION":
                            intSubmissionProbs++;
                            break;
                        default:
                            intOtherProbs++;
                            break;
                    }

                }
                else
                {
                    intTotalProbs = intLabProbs + intTransProbs + intMissingProbs + intSubmissionProbs + intOtherProbs;
                    if (intTotalProbs > 0)
                    {
                        DataRow drNewRow = dtReport.NewRow();
                        drNewRow["Acct"] = strOldAcct;
                        drNewRow["AcctName"] = strAcctName;
                        drNewRow["Territory"] = strTerritory;
                        drNewRow["SalesRep"] = strSalesRep;
                        drNewRow["Rev1"] = dRev1;
                        drNewRow["Rev2"] = dRev2;
                        drNewRow["Rev3"] = dRev3;
                        drNewRow["TotalProbs"] = intTotalProbs;
                        drNewRow["LabProbs"] = intLabProbs;
                        drNewRow["TransProbs"] = intTransProbs;
                        drNewRow["MissingProbs"] = intMissingProbs;
                        drNewRow["SubmissionProbs"] = intSubmissionProbs;
                        drNewRow["OtherProbs"] = intOtherProbs;
                        dtReport.Rows.Add(drNewRow);
                    }
                    strSalesRep = ""; strTerritory = ""; strAcctName = ""; strRevenue = null; dRev1 = 0.000; dRev2 = 0.000; dRev3 = 0.000;
                    intLabProbs = 0; intTransProbs = 0; intMissingProbs = 0; intSubmissionProbs = 0; intOtherProbs = 0; intTotalProbs = 0;
                    intCnt--;
                }
                strOldAcct = strAcct;
            }

            intTotalProbs = intLabProbs + intTransProbs + intMissingProbs + intSubmissionProbs + intOtherProbs;
            if (intTotalProbs > 0)
            {
                DataRow drNewRow = dtReport.NewRow();
                drNewRow["Acct"] = strOldAcct;
                drNewRow["AcctName"] = strAcctName;
                drNewRow["Territory"] = strTerritory;
                drNewRow["SalesRep"] = strSalesRep;
                drNewRow["Rev1"] = dRev1;
                drNewRow["Rev2"] = dRev2;
                drNewRow["Rev3"] = dRev3;
                drNewRow["TotalProbs"] = intTotalProbs;
                drNewRow["LabProbs"] = intLabProbs;
                drNewRow["TransProbs"] = intTransProbs;
                drNewRow["MissingProbs"] = intMissingProbs;
                drNewRow["SubmissionProbs"] = intSubmissionProbs;
                drNewRow["OtherProbs"] = intOtherProbs;
                dtReport.Rows.Add(drNewRow);
            }
        }
        dtReport.DefaultView.Sort = "TotalProbs DESC";
        dtReport = dtReport.DefaultView.ToTable();
        return dtReport;
    }

    public static DataTable getProblemAnalysisLabComp(string strQS, out string dateFrom, out string dateTo)
    {
        String[] QS = strQS.Split('^');
        dateFrom = QS[0];
        dateTo = QS[1];
        string[] strLabLocations=QS[2].Split(new char[] {';'});
        DataTable dtReport = new DataTable();
        DataTable dtRawData = new DataTable();
        dtReport = DL_ProblemAnalysisReport.getTotalProbAccCountForComp(dateFrom, dateTo, strLabLocations);
        dtRawData = DL_ProblemAnalysisReport.getProblemAnalysisLabComparison(dateFrom, dateTo, QS[2], FormatInputString(QS[3]), FormatInputString(QS[4]), FormatInputString(QS[5]), FormatInputString(QS[6]));

        for (int labCount = 0; labCount < strLabLocations.Length; labCount++)
        {
            dtRawData.Columns.Add("Lab" + (labCount + 1) + "_Perc", typeof(string));
        }

        if (dtRawData != null && dtRawData.Rows.Count > 0 && dtReport != null && dtReport.Rows.Count >0)
        {
            for (int rowCount = 0; rowCount < dtRawData.Rows.Count; rowCount++)
            {
                for (int labCount = 0; labCount < strLabLocations.Length; labCount++)
                {
                    string  strLabCount = (dtRawData.Rows[rowCount]["Lab" + (labCount + 1) + "_Count"].ToString()== "" ? "0" : dtRawData.Rows[rowCount]["Lab" + (labCount + 1) + "_Count"].ToString());
                    dtRawData.Rows[rowCount]["Lab" + (labCount + 1) + "_Count"] = strLabCount;
                    dtRawData.Rows[rowCount]["Lab" + (labCount + 1) + "_Perc"] = Math.Round((Convert.ToDouble(strLabCount) / Convert.ToDouble(dtReport.Rows[0]["TotalProbCount_Lab" + (labCount + 1)].ToString())) * 100, 3);
                }
            }
            addHeaderForLabComp(strLabLocations, dtRawData, dtReport);
        }
        return dtRawData;
    }

    public static DataTable addHeaderForLabComp(string[] strLabLocations,DataTable dtTable,DataTable dtCount)
    {
        DataRow dr = dtTable.NewRow();
        dr["PTYP_Description"] = "";
        dr["Lab1_Count"] = "<B>Number Reported</B>";
        dr["Lab1_Perc"] = "<B>% of Total Prob</B>";
        dr["Lab2_Count"] = "<B>Number Reported</B>";
        dr["Lab2_Perc"] = "<B>% of Total Prob</B>";
        if (strLabLocations.Length > 2)
        {
            dr["Lab3_Count"] = "<B>Number Reported</B>";
            dr["Lab3_Perc"] = "<B>% of Total Prob</B>";
        }
        if (strLabLocations.Length == 4)
        {
            dr["Lab4_Count"] = "<B>Number Reported</B>";
            dr["Lab4_Perc"] = "<B>% of Total Prob</B>";
        }
        dtTable.Rows.InsertAt(dr, 0);

        dr = dtTable.NewRow();

        dr["PTYP_Description"] = "LINE";
        dtTable.Rows.InsertAt(dr, 0);

        dr = dtTable.NewRow();
        dr["PTYP_Description"] = "<B>Total Accessions</B>";
        dr["Lab1_Count"] = "<B>" + dtCount.Rows[0]["TotalAccCount_Lab1"].ToString()+ "</B>";
        dr["Lab1_Perc"] = "";
        dr["Lab2_Count"] = "<B>" + dtCount.Rows[0]["TotalAccCount_Lab2"].ToString() + "</B>";
        dr["Lab2_Perc"] = "";
        if (strLabLocations.Length > 2)
        {
            dr["Lab3_Count"] = "<B>" + dtCount.Rows[0]["TotalAccCount_Lab3"].ToString() + "</B>";
            dr["Lab3_Perc"] = "";
        }
        if (strLabLocations.Length == 4)
        {
            dr["Lab4_Count"] = "<B>" + dtCount.Rows[0]["TotalAccCount_Lab4"].ToString() + "</B>";
            dr["Lab4_Perc"] = "";
        }
        dtTable.Rows.InsertAt(dr, 0);

        dr = dtTable.NewRow();
        dr["PTYP_Description"] = "<B>Total Problems<B>";
        dr["Lab1_Count"] = dtCount.Rows[0]["TotalProbCount_Lab1"].ToString();
        dr["Lab1_Perc"] = "";
        dr["Lab2_Count"] = dtCount.Rows[0]["TotalProbCount_Lab2"].ToString();
        dr["Lab2_Perc"] = "";
        if (strLabLocations.Length > 2)
        {
            dr["Lab3_Count"] = dtCount.Rows[0]["TotalProbCount_Lab3"].ToString();
            dr["Lab3_Perc"] = "";
        }
        if (strLabLocations.Length == 4)
        {
            dr["Lab4_Count"] = dtCount.Rows[0]["TotalProbCount_Lab4"].ToString();
            dr["Lab4_Perc"] = "";
        }
        dtTable.Rows.InsertAt(dr, 0);

        dr = dtTable.NewRow();
        dr["PTYP_Description"] = "";
        dr["Lab1_Count"] = "<B>" + strLabLocations[0] + "</B>";
        dr["Lab1_Perc"] = "";
        dr["Lab2_Count"] = "<B>" + strLabLocations[1] + "</B>";
        dr["Lab2_Perc"] = "";
        if (strLabLocations.Length > 2)
        {
            dr["Lab3_Count"] = "<B>" + strLabLocations[2] + "</B>";
            dr["Lab3_Perc"] = "";
        }
        if (strLabLocations.Length == 4)
        {
            dr["Lab4_Count"] = "<B>" + strLabLocations[3] + "</B>";
            dr["Lab4_Perc"] = "";
        }
        dtTable.Rows.InsertAt(dr, 0);
        return dtTable;
    }

    public static string FormatInputString(string strInput)
    {
        string strOutput = strInput;
        if (strOutput.Length > 0)
        {
            strOutput = "'" + strInput.Replace(";", "','") + "'";
        }
        return strOutput;
    }

    public static DataTable getClientAtRiskSummary(string strQS, out string strTotalClientCount)
    {
        String[] QS = strQS.Split('^');
        string dateFrom = QS[0];
        string dateTo = QS[1];
        string strSessionID = "";
        strTotalClientCount = "";

        DataTable dtReport = new DataTable();
        dtReport.Columns.Add("Acct", typeof(string));
        dtReport.Columns.Add("AcctName", typeof(string));
        dtReport.Columns.Add("Territory", typeof(string));
        dtReport.Columns.Add("SalesRep", typeof(string));
        dtReport.Columns.Add("Rev1", typeof(double));
        dtReport.Columns.Add("Rev2", typeof(double));
        dtReport.Columns.Add("Rev3", typeof(double));
        dtReport.Columns.Add("TotalProbs", typeof(string));
        dtReport.Columns.Add("LabProbs", typeof(string));
        dtReport.Columns.Add("TransProbs", typeof(string));
        dtReport.Columns.Add("MissingProbs", typeof(string));
        dtReport.Columns.Add("SubmissionProbs", typeof(string));
        dtReport.Columns.Add("OtherProbs", typeof(string));

        string strAcct = "", strAcctName = "", strTerritory = "", strSalesRep = "", strProbType = "", strClientDR = "";
        string[] strRevenue;
        double dRev1 = 0.000; double dRev2 = 0.000; double dRev3 = 0.000;

        int intTotalProbs = 0;
        string strProblemCount="", strLabProbs = "", strTransProbs = "", strMissingProbs = "", strSubmissionProbs = "", strOtherProbs = "";
        
        DataTable dtRawData = DL_ProblemAnalysisReport.getClientAtRisk(dateFrom, dateTo, QS[2],out strSessionID);
        
        if (dtRawData != null && dtRawData.Rows.Count > 0)
        {
            DataTable dtDetailTable = DL_ProblemAnalysisReport.getClientAtRiskDetails();
            DataTable dtDetailTableChild = DL_ProblemAnalysisReport.getClientAtRiskDetailsChild();
            strTotalClientCount = DL_ProblemAnalysisReport.getTotalClientCount(strSessionID);


            for (int iCount = 0; iCount < dtRawData.Rows.Count; iCount++)
            {
                intTotalProbs = 0;
                strLabProbs = "0"; strTransProbs = "0"; strMissingProbs = "0"; strSubmissionProbs = "0"; strOtherProbs = "0";
                
                strAcct = dtRawData.Rows[iCount]["AccountNumber"].ToString();
                strAcctName = dtRawData.Rows[iCount]["AccountName"].ToString();
                strTerritory = dtRawData.Rows[iCount]["Territory"].ToString();
                strSalesRep = dtRawData.Rows[iCount]["SalesRepName"].ToString();
                
                strClientDR = dtRawData.Rows[iCount]["BICAR_RowID"].ToString();
                
                strRevenue = dtRawData.Rows[iCount]["ClientRevenue"].ToString().Split('^');
                dRev1 = (strRevenue[0].Length == 0 ? 0.000 : Convert.ToDouble(strRevenue[0]));
                dRev2 = (strRevenue[1].Length == 0 ? 0.000 : Convert.ToDouble(strRevenue[1]));
                dRev3 = (strRevenue[2].Length == 0 ? 0.000 : Convert.ToDouble(strRevenue[2]));

                DataRow[] drarray = dtDetailTable.Select("BICD_BICAR_PR='" + strClientDR + "'");
                if (drarray != null && drarray.Length > 0)
                {
                    for (int i = 0; i < drarray.Length; i++)
                    {
                        DataRow[] drReportChild = dtDetailTableChild.Select("CPTPL_BICD_PR='" + drarray[i]["BICD_RowID"] + "'", "CPTPL_ProblemTypeDR ASC");
                        int count = 0;
                        for (int iCnt = 0; iCnt < drReportChild.Length; iCnt++)
                        {
                            count += Convert.ToInt32(drReportChild[iCnt]["CPTPL_Count"]);
                        }
                        strProbType = drarray[i]["BICD_ProblemTypeGroupDR"].ToString();
                        strProblemCount = count.ToString();

                        if (strProblemCount.Trim().Length > 0)
                        {
                            intTotalProbs += Convert.ToInt32(strProblemCount.Trim());
                        }

                        switch (strProbType)
                        {
                            case "LAB":
                                strLabProbs = strProblemCount;
                                break;
                            case "TRANSPORTATION":
                                strTransProbs = strProblemCount;
                                break;
                            case "MISSING":
                                strMissingProbs = strProblemCount;
                                break;
                            case "SUBMISSION":
                                strSubmissionProbs = strProblemCount;
                                break;
                            case "OTHER":
                                strOtherProbs = strProblemCount;
                                break;

                        }
                    }
                }

                DataRow drNewRow = dtReport.NewRow();
                drNewRow["Acct"] = strAcct;
                drNewRow["AcctName"] = strAcctName;
                drNewRow["Territory"] = strTerritory;
                drNewRow["SalesRep"] = strSalesRep;
                drNewRow["Rev1"] = dRev1;
                drNewRow["Rev2"] = dRev2;
                drNewRow["Rev3"] = dRev3;
                drNewRow["TotalProbs"] = intTotalProbs.ToString();
                drNewRow["LabProbs"] = strLabProbs;
                drNewRow["TransProbs"] = strTransProbs;
                drNewRow["MissingProbs"] = strMissingProbs;
                drNewRow["SubmissionProbs"] = strSubmissionProbs;
                drNewRow["OtherProbs"] = strOtherProbs;
                dtReport.Rows.Add(drNewRow);

            }
        }

        return dtReport;
    }

    public static DataTable getClientAtRisk(string strQS, out string strTotalClientCount)
    {
        String[] QS = strQS.Split('^');
        string dateFrom = QS[0];
        string dateTo = QS[1];
        string strSessionID = "";
        strTotalClientCount = "";

        DataTable dtRawData = DL_ProblemAnalysisReport.getClientAtRisk(dateFrom, dateTo, QS[2], out strSessionID);
        if (dtRawData != null)
        {
            dtRawData.Columns.Add("Rev1", typeof(double));
            dtRawData.Columns.Add("Rev2", typeof(double));
            dtRawData.Columns.Add("Rev3", typeof(double));
            dtRawData.Columns.Add("DetailTable", typeof(DataTable));

            DataTable dtDetailTable = DL_ProblemAnalysisReport.getClientAtRiskDetails();
            DataTable dtDetailTableChild = DL_ProblemAnalysisReport.getClientAtRiskDetailsChild();
            strTotalClientCount = DL_ProblemAnalysisReport.getTotalClientCount(strSessionID);

            DataTable dtReportSchema = new DataTable();
            dtReportSchema.Columns.Add("ProblemGroup", typeof(string));
            dtReportSchema.Columns.Add("ProblemDesc", typeof(string));
            dtReportSchema.Columns.Add("Count", typeof(int));

            string[] strRevenue;
            string strClientDR = "";

            for (int iCount = 0; iCount < dtRawData.Rows.Count; iCount++)
            {
                strClientDR = dtRawData.Rows[iCount]["BICAR_RowID"].ToString();
                strRevenue = dtRawData.Rows[iCount]["ClientRevenue"].ToString().Split('^');
                dtRawData.Rows[iCount]["Rev1"] = (strRevenue[0].Length == 0 ? 0.000 : Convert.ToDouble(strRevenue[0]));
                dtRawData.Rows[iCount]["Rev2"] = (strRevenue[1].Length == 0 ? 0.000 : Convert.ToDouble(strRevenue[1]));
                dtRawData.Rows[iCount]["Rev3"] = (strRevenue[2].Length == 0 ? 0.000 : Convert.ToDouble(strRevenue[2]));
                DataTable dtChild = getClientAtRiskChild(dtDetailTable, dtReportSchema.Clone(), strClientDR, dtDetailTableChild);
                dtRawData.Rows[iCount]["DetailTable"] = dtChild;
            }
        }
        return dtRawData;
    }

    public static DataTable getClientAtRiskChild(DataTable dtChild, DataTable dtReport, string client, DataTable dtDetailTableChild)
    {
        DataRow[] drReport = dtChild.Select("BICD_BICAR_PR='" + client + "'", "BICD_ProblemTypeGroupDR ASC");
        if (drReport != null)
        {
            string strGroupName = "";
            for (int iCnt = 0; iCnt < drReport.Length; iCnt++)
            {
                DataRow[] drReportChild = dtDetailTableChild.Select("CPTPL_BICD_PR='" + drReport[iCnt]["BICD_RowID"] + "'", "CPTPL_ProblemTypeDR ASC");
                strGroupName = drReport[iCnt]["BICD_ProblemTypeGroupDR"].ToString();
                if (drReportChild != null)
                {
                    Int32 iTotalProbs = 0;
                    string strProbType = "";
                    for (int i = 0; i < drReportChild.Length; i++)
                    {
                        strProbType = drReportChild[i]["CPTPL_ProblemTypeDR"].ToString();
                        string strCount = drReportChild[i]["CPTPL_Count"].ToString();
                        if (strCount.Length > 0)
                        {
                            iTotalProbs += Convert.ToInt32(strCount);
                        }
                        if (i == 0) //Showing group name only in the first row
                        {
                            dtReport.Rows.Add(strGroupName, strProbType, strCount);
                        }
                        else
                        {
                            dtReport.Rows.Add("", strProbType, strCount);
                        }
                    }
                    dtReport.Rows.Add("TOTAL", "Total " + strGroupName, iTotalProbs);
                }
            }
        }
        return dtReport;
    }

    #endregion Problem Report Section
}
