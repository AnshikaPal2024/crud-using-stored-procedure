using Microsoft.Office.Interop.Excel;
using System.Data;

internal static class UploadfileHelpers
{
    // the method reads the excel data into a datatable
    public static System.Data.DataTable Read(Range excelRange)
    {
        DataRow row;
        System.Data.DataTable dt = new System.Data.DataTable();
        int rowCount = excelRange.Count;
        int colCount = excelRange.Count;

    }
}