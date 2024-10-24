using System;
using System.IO;

namespace MyPractice
{
    internal class ExcelPackage
    {
        public object Workbook { get; internal set; }

        internal object SaveAs(MemoryStream stream)
        {
            throw new NotImplementedException();
        }
    }
}