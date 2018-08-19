using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services;
using System.Web.Script.Services;

/// <summary>
/// To call BL from AJAX, regarding Consult logic
/// </summary>
[WebService(Namespace = "http://tempuri.org/")]
[WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
[System.Web.Script.Services.ScriptService]
public class Consult : System.Web.Services.WebService
{


    [WebMethod]
    [ScriptMethod(ResponseFormat = ResponseFormat.Xml)]
    public string DoClaim(string status, string currentConsultant, string consultantNo, string specialty,
        string reqConsultant, string selectedConsultant, string selectedSpecialty, string userId)
    {        

        if (status == "PEND")
        {
            ConsultRecorder.updateCheckOutStatus("PROG", consultantNo, userId, "", "");

            return "PROG";
        }
        else if (status == "PROG")
        {
            if (currentConsultant == userId)
            {
                ConsultRecorder.updateCheckOutStatus("PEND", consultantNo, userId, selectedSpecialty, selectedConsultant);

                return "PEND";
            }
            else
            {
                // should be replaced by new method which will do both the action in DB call.
                ConsultRecorder.updateCheckOutStatus("PEND", consultantNo, userId, "", "");
                ConsultRecorder.updateCheckOutStatus("PROG", consultantNo, userId, selectedSpecialty, selectedConsultant);

                return "PROG";
            }
        }

        return "";
    }

}
