using System;
using System.Collections.Generic;
using System.Linq;
using Excel = Microsoft.Office.Interop.Excel;
using System.Drawing;
using System.Reflection;
using System.ComponentModel;

namespace Extensions
{
    public static class ExcelExtensions
    {
        /// <summary>
        /// Converts Excel datetime value to a valid DateTime object.
        /// </summary>
        /// <param name="excelDate">The Excel date value.</param>
        /// <returns>A valid DateTime object corresponding with the Excel value.</returns>
        public static DateTime ConvertToDateTime(double excelDate)
        {
            if (excelDate < 1) { throw new ArgumentException("Excel date cannot be less than 0!"); }
            DateTime dateOfReference = new DateTime(1900, 1, 1);
            if (excelDate > 60d) { excelDate = excelDate - 2; }
            else { excelDate = excelDate - 1; }
            return dateOfReference.AddDays(excelDate);
        }


        /// <summary>
        /// Extension method to write list data to excel.
        /// </summary>
        /// <typeparam name="T">Generic list</typeparam>
        /// <param name="list"></param>
        /// <param name="PathToSave">Path to save file.</param>
        public static void ToExcel<T>(this List<T> list, string PathToSave)
        {
            #region Declarations

            if (string.IsNullOrEmpty(PathToSave))
            {
                throw new Exception("Invalid file path.");
            }
            else if (PathToSave.ToLower().Contains("") == false)
            {
                throw new Exception("Invalid file path.");
            }

            if (list == null)
            {
                throw new Exception("No data to export.");
            }

            Excel.Application excelApp = null;
            Excel.Workbooks books = null;
            Excel._Workbook book = null;
            Excel.Sheets sheets = null;
            Excel._Worksheet sheet = null;
            Excel.Range range = null;
            Excel.Font font = null;
            // Optional argument variable
            object optionalValue = Missing.Value;

            string strHeaderStart = "A2";
            string strDataStart = "A3";
            #endregion

            #region Processing


            try
            {
                #region Init Excel app.


                excelApp = new Excel.Application();
                books = (Excel.Workbooks)excelApp.Workbooks;
                book = (Excel._Workbook)(books.Add(optionalValue));
                sheets = (Excel.Sheets)book.Worksheets;
                sheet = (Excel._Worksheet)(sheets.get_Item(1));

                #endregion

                #region Creating Header


                Dictionary<string, string> objHeaders = new Dictionary<string, string>();

                PropertyInfo[] headerInfo = typeof(T).GetProperties();


                foreach (var property in headerInfo)
                {
                    var attribute = property.GetCustomAttributes(typeof(DisplayNameAttribute), false)
                                            .Cast<DisplayNameAttribute>().FirstOrDefault();
                    objHeaders.Add(property.Name, attribute == null ?
                                        property.Name : attribute.DisplayName);
                }


                range = sheet.get_Range(strHeaderStart, optionalValue);
                range = range.get_Resize(1, objHeaders.Count);

                range.set_Value(optionalValue, objHeaders.Values.ToArray());
                range.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing);

                font = range.Font;
                font.Bold = true;
                range.Interior.Color = Color.LightGray.ToArgb();

                #endregion

                #region Writing data to cell


                int count = list.Count;
                object[,] objData = new object[count, objHeaders.Count];

                for (int j = 0; j < count; j++)
                {
                    var item = list[j];
                    int i = 0;
                    foreach (KeyValuePair<string, string> entry in objHeaders)
                    {
                        var y = typeof(T).InvokeMember(entry.Key.ToString(), BindingFlags.GetProperty, null, item, null);
                        objData[j, i++] = (y == null) ? "" : y.ToString();
                    }
                }


                range = sheet.get_Range(strDataStart, optionalValue);
                range = range.get_Resize(count, objHeaders.Count);

                range.set_Value(optionalValue, objData);
                range.BorderAround(Type.Missing, Excel.XlBorderWeight.xlThin, Excel.XlColorIndex.xlColorIndexAutomatic, Type.Missing);

                range = sheet.get_Range(strHeaderStart, optionalValue);
                range = range.get_Resize(count + 1, objHeaders.Count);
                range.Columns.AutoFit();

                #endregion

                #region Saving data and Opening Excel file.


                if (string.IsNullOrEmpty(PathToSave) == false)
                    book.SaveAs(PathToSave);

                excelApp.Visible = true;

                #endregion

                #region Release objects

                try
                {
                    if (sheet != null)
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(sheet);
                    sheet = null;

                    if (sheets != null)
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(sheets);
                    sheets = null;

                    if (book != null)
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(book);
                    book = null;

                    if (books != null)
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(books);
                    books = null;

                    if (excelApp != null)
                        System.Runtime.InteropServices.Marshal.ReleaseComObject(excelApp);
                    excelApp = null;
                }
                catch (Exception)
                {
                    sheet = null;
                    sheets = null;
                    book = null;
                    books = null;
                    excelApp = null;
                }
                finally
                {
                    GC.Collect();
                }

                #endregion
            }
            catch (Exception ex)
            {
                throw ex;
            }

            #endregion
        }
    }
}
