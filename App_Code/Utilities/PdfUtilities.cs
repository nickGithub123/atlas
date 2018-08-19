using System;
using WebSupergoo.ABCpdf9;

/// <summary>
/// Summary description for PDFService
/// </summary>
public class PdfUtilities
{
    public Byte[] ReadRTFToPdfDoc(Byte[] rtfByteArray)
    {
        using (Doc document = new Doc())
        {
            XReadOptions options = new XReadOptions();
            options.ReadModule = ReadModuleType.Pdf;//.MSOffice;
            options.FileExtension = "pdf";//"rtf";
            document.Read(rtfByteArray, options);
            return document.GetData();
        }
    }


}