using System;
using System.Data;
using System.Configuration;
using System.Web;
using System.Web.Security;
using System.Web.UI;
using System.Web.UI.WebControls;
using System.Web.UI.WebControls.WebParts;
using System.Web.UI.HtmlControls;
using System.IO;
using System.Text;


/// <summary>
/// Summary description for PrintWebControl_Test
/// </summary>
public class PrintControl
{
    public PrintControl()
    {
        //
        // TODO: Add constructor logic here
        //
    }
    public static void PrintWebControl(ControlCollection CC)
    {

        PrintWebControl(CC, string.Empty);
    }
    //---------------------------------------------------------------------------------
    // AM Issue#46793 10/21/2008 - Prints any Control i.e. User Control, DataGrid,Repeater,
    //                             Page , Panel etc.
    //---------------------------------------------------------------------------------
    public static void PrintWebControl(ControlCollection CC, string Script)
    {
        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        Page pg = new Page();
        HtmlForm frm = new HtmlForm();
        pg.EnableEventValidation = false;
        if (Script != string.Empty)
        {

            pg.RegisterStartupScript("PrintJavaScript", Script);
        }
        for (int icount = 0; icount < CC.Count; icount++) // form created with control collection(s) for printing
        {
            frm.Controls.Add(CC[icount]);
        }
        pg.Controls.Add(frm);
        string scr = "<script>window.onafterprint= function (){history.back(1);}</script>";
        htmlWrite.Write(scr);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        //HttpContext.Current.Response.Write("<head><title>ANTECH</title><link rel=\"Stylesheet\" type=\"text/css\" href=\"App_Themes/Default/Default.css\" /><link rel=\"stylesheet\" type=\"text/css\" href=\"App_Themes/Print.css\" media=\"print\">");
        //HttpContext.Current.Response.Write("<div class=\"post-img\"><img src=\"App_Themes/Default/images/antech_logo.png\" /></div></head>");
        //HttpContext.Current.Response.Write("<!DOCTYPE html PUBLIC \"-//W3C//DTD HTML 4.01//EN\" \"http://www.w3.org/TR/html4/strict.dtd\"><html lang=\"en-US\"><head profile=\"http://www.w3.org/2005/10/profile\"><title><link rel=\"icon\" type=\"image/png\" href=\"App_Themes/Default/images/antech_logo.png\"></title><link rel=\"Stylesheet\" type=\"text/css\" href=\"App_Themes/Default/Default.css\" /></head>");        
        HttpContext.Current.Response.Write("<head><link rel=\"icon\" type=\"image/x-icon\" href=\"~/App_Themes/Default/images/antech_logo.png\"><title>ANTECH</title><link rel=\"Stylesheet\" type=\"text/css\" href=\"App_Themes/Default/Default.css\" /></head>");        
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.Write("<script>window.print();</script>");
        HttpContext.Current.Response.End();
    }

    public static void PreviewWebControl(ControlCollection CC)
    {

        PreviewWebControl(CC, string.Empty);
    }
    /// <summary>
    /// Prints any Control i.e. User Control, DataGrid,Repeater, Page , Panel etc.
    /// </summary>
    /// <param name="ctrl">Control to be printed</param>
    public static void PreviewWebControl(ControlCollection CC, string Script)
    {
        StringWriter stringWrite = new StringWriter();
        HtmlTextWriter htmlWrite = new HtmlTextWriter(stringWrite);
        Page pg = new Page();
        HtmlForm frm = new HtmlForm();
        pg.EnableEventValidation = false;
        if (Script != string.Empty)
        {

            pg.RegisterStartupScript("PrintJavaScript", Script);
        }
        for (int icount = 0; icount < CC.Count; icount++)
        {
            frm.Controls.Add(CC[icount]);
        }
        pg.Controls.Add(frm);
        //string scr = "<script>function window.onafterprint(){history.back(1);}</script>";
        //htmlWrite.Write(scr);
        pg.DesignerInitialize();
        pg.RenderControl(htmlWrite);
        string strHTML = stringWrite.ToString();
        HttpContext.Current.Response.Clear();
        HttpContext.Current.Response.Write("<head><link rel=\"icon\" type=\"image/x-icon\" href=\"~/App_Themes/Default/images/antech_logo.png\"><title>ANTECH</title><link rel=\"Stylesheet\" type=\"text/css\" href=\"App_Themes/Default/Default.css\" /></head>");        
        HttpContext.Current.Response.Write(strHTML);
        HttpContext.Current.Response.End();
    }
    public static Panel ControlPanel(string panelStyle,int height,int width)
    {
        Panel pnlctl = new Panel();
        pnlctl.Height = height;
        pnlctl.Style.Value = panelStyle;
        return pnlctl;
    }
}