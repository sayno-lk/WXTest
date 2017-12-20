using OfficeOpenXml;
using OfficeOpenXml.Style;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Reflection;

namespace AutoModule.Models
{
    public class ImportExportTool_T
    {
        static int _cellCount = 8;
        //private static ICollection<Dyson_Order> _objModel;
        //public static ICollection<Dyson_Order> UserModel
        //{
        //    get
        //    {
        //        return _objModel;
        //    }
        //    set
        //    {
        //        _objModel = value;
        //    }
        //}

        //public static ICollection<Dyson_Order> Import(Stream stream)
        //{
        //    ICollection<Dyson_Order> certificate = new List<Dyson_Order>();

        //    #region read excel
        //    using (stream)
        //    {
        //        ExcelPackage package = new ExcelPackage(stream);

        //        ExcelWorksheet sheet = package.Workbook.Worksheets[1];

        //        #region check excel format
        //        if (sheet == null)
        //        {
        //            return certificate;

        //            //sheet.Cells[1, 6].Value = "CertificateStatus";
        //            //sheet.Cells[1, 7].Value = "IssueDate";
        //            //sheet.Cells[1, 8].Value = "DueDate";
        //            //sheet.Cells[1, 9].Value = "ApplicationStandard";
        //            //sheet.Cells[1, 10].Value = "ManufacturersCH";
        //            //sheet.Cells[1, 11].Value = "ManufacturersEN";
        //            //sheet.Cells[1, 12].Value = "AddressCH";
        //            //sheet.Cells[1, 13].Value = "AddressEN";
        //            //sheet.Cells[1, 14].Value = "RemarkDetail";
        //        }
        //        if (!sheet.Cells[1, 1].Value.Equals("order_number") ||
        //            !sheet.Cells[1, 2].Value.Equals("order_name") ||
        //            !sheet.Cells[1, 3].Value.Equals("order_phone") ||
        //            !sheet.Cells[1, 4].Value.Equals("order_period") ||
        //            !sheet.Cells[1, 5].Value.Equals("order_city") ||
        //            !sheet.Cells[1, 6].Value.Equals("order_content") ||
        //            !sheet.Cells[1, 7].Value.Equals("order_hairstyle") ||
        //            !sheet.Cells[1, 8].Value.Equals("order_date")
        //             )
        //        {
        //            return certificate;
        //        }
        //        #endregion

        //        #region get last row index
        //        int lastRow = sheet.Dimension.End.Row;
        //        while (sheet.Cells[lastRow, 1].Value == null)
        //        {
        //            lastRow--;
        //        }
        //        #endregion

        //        #region read datas
        //        for (int i = 2; i <= lastRow; i++)
        //        {

        //            if (sheet.Cells[i, 1].Value == null || sheet.Cells[i, 1].Value.ToString() == "")
        //            {
        //                continue;
        //            }
        //            //GetConvertDateTime gdt1 = GetObjectToTime(sheet.Cells[i, 7].Value);

        //            //int status = (int)SysData.CertificateState.未审批;
        //            //if (gdt1.Result == false)
        //            //{
        //            //    status = (int)SysData.CertificateState.错误;
        //            //}


        //            if ((sheet.Cells[i, 2].Value == null || sheet.Cells[i, 2].Value.ToString() == "") ||
        //                (sheet.Cells[i, 3].Value == null || sheet.Cells[i, 3].Value.ToString() == "") ||
        //                (sheet.Cells[i, 4].Value == null || sheet.Cells[i, 4].Value.ToString() == "") ||
        //                (sheet.Cells[i, 5].Value == null || sheet.Cells[i, 5].Value.ToString() == "") ||
        //                (sheet.Cells[i, 6].Value == null || sheet.Cells[i, 6].Value.ToString() == "")
        //                )
        //            {
        //                //status = (int)SysData.CertificateState.错误;
        //            }

        //            certificate.Add(new Dyson_Order
        //            {
        //                order_number = GetObjectToString(sheet.Cells[i, 1].Value),
        //                order_name = GetObjectToString(sheet.Cells[i, 2].Value),
        //                order_phone = GetObjectToString(sheet.Cells[i, 3].Value),
        //                order_period = GetObjectToString(sheet.Cells[i, 4].Value),
        //                order_city = GetObjectToString(sheet.Cells[i, 5].Value),
        //                order_content = GetObjectToString(sheet.Cells[i, 6].Value),
        //                order_hairstyle = GetObjectToString(sheet.Cells[i, 7].Value),
        //                order_date = GetObjectToString(sheet.Cells[i, 8].Value, true),

        //            });
        //        }
        //        #endregion

        //    }
        //    #endregion

        //    return certificate;
        //}

        public static string ExportToFile<T>(List<T> objList, string filePath) where T : new()
        {
            FileInfo newFile = new FileInfo(filePath);
            if (newFile.Exists)
            {
                newFile.Delete();
                newFile = new FileInfo(filePath);
            }
            ExcelPackage package = new ExcelPackage(newFile);

            package.Workbook.Worksheets.Add("list2");
            ExcelWorksheet sheet = package.Workbook.Worksheets[1];

            sheet.Row(1).Height = 20;//设置行高
            sheet.Column(1).Width = 10;//设置列宽
            sheet.Column(2).Width = 15;//设置列宽
            sheet.Column(3).Width = 20;//设置列宽
            sheet.Column(4).Width = 20;//设置列宽
            sheet.Column(5).Width = 20;//设置列宽
            sheet.Column(5).Style.Numberformat.Format = "YYYY-MM-DD hh:mm:ss";
            //sheet.Column(6).Width = 20;//设置列宽
            //sheet.Column(7).Width = 20;//设置列宽
            //sheet.Column(8).Width = 20;//设置列宽
            //sheet.Column(9).Width = 20;//设置列宽
            //sheet.Column(10).Width = 20;//设置列宽
            //sheet.Column(11).Width = 20;//设置列宽
            //sheet.Column(12).Width = 30;//设置列宽
            //sheet.Column(12).Style.Numberformat.Format = "YYYY-MM-DD hh:mm:ss";
            #region write header
            T t = new T();
            PropertyInfo[] propertys = ConvertObjectEntity<T>.GetProperties(t);
            int _cellIndex = 1;
            foreach (var item in propertys)
            {
                sheet.Cells[1, _cellIndex].Value = item.Name;
                _cellIndex++;
            }

            //sheet.Cells[1, 1].Value = "Id";
            //sheet.Cells[1, 2].Value = "机器码";
            //sheet.Cells[1, 3].Value = "姓名";
            //sheet.Cells[1, 4].Value = "手机";
            //sheet.Cells[1, 5].Value = "时间段";
            //sheet.Cells[1, 6].Value = "内容";
            //sheet.Cells[1, 7].Value = "发型";
            //sheet.Cells[1, 8].Value = "日期";
            //sheet.Cells[1, 9].Value = "类型";
            //sheet.Cells[1, 10].Value = "打印状态";
            //sheet.Cells[1, 11].Value = "打印序号";
            //sheet.Cells[1, 12].Value = "打印时间";

            //sheet.Cells[1, 13].Value = "AddressEN";
            //sheet.Cells[1, 14].Value = "RemarkDetail";

            using (ExcelRange range = sheet.Cells[1, 1, 1, _cellIndex - 1])
            {
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.Gray);
            }
            #endregion

            #region write content
            int pos = 2;
            PropertyInfo[] pars = null;
            foreach (object obj in objList)
            {
                _cellIndex = 1;
                pars = ConvertObjectEntity<T>.GetProperties((T)obj);
                foreach (var key in pars)
                {
                    sheet.Cells[pos, _cellIndex].Value = key.GetValue(obj, null);
                    _cellIndex++;
                }

                using (ExcelRange range = sheet.Cells[pos, 1, pos, _cellIndex - 1])
                {
                    //range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    //range.Style.Border.Bottom.Color.SetColor(Color.Black);

                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));//设置单元格所有边框
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                }
                pos++;
            }
            #endregion

            package.Save();
            return filePath;
        }

        public static MemoryStream Export<T>(List<T> objList) where T : new()
        {
            MemoryStream stream = new MemoryStream();
            ExcelPackage package = new ExcelPackage(stream);

            package.Workbook.Worksheets.Add("list");
            ExcelWorksheet sheet = package.Workbook.Worksheets[1];

            // sheet.Cells.Style.ShrinkToFit = true;//单元格自动适应大小
            sheet.Row(1).Height = 20;//设置行高
            //sheet.Row(1).CustomHeight = true;//自动调整行高
            sheet.Column(2).Width = 20;//设置列宽
            sheet.Column(3).Width = 30;//设置列宽
            sheet.Column(4).Width = 20;//设置列宽
            sheet.Column(5).Width = 20;//设置列宽
            //sheet.Column(5).Style.Numberformat.Format = "YYYY-MM-dd HH:mm:ss";
            //sheet.Column(10).Width = 15;//设置列宽
            //sheet.Column(11).Width = 20;//设置列宽
            //sheet.Column(11).Style.Numberformat.Format = "YYYY-MM-dd HH:mm:ss";
            //sheet.View.ShowGridLines = false;//去掉sheet的网格线
            //sheet.Cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
            //sheet.Cells.Style.Fill.BackgroundColor.SetColor(Color.White);//设置背景色
            //sheet.Cells[2, 1, 2, 6].Style.Fill.BackgroundColor.SetColor(Color.Red);

            #region write header
            T t = new T();
            PropertyInfo[] propertys = ConvertObjectEntity<T>.GetProperties(t);
            int _cellIndex = 1;
            foreach (var item in propertys)
            {
                sheet.Cells[1, _cellIndex].Value = item.Name;
                _cellIndex++;
            }
            //sheet.Cells[1, 1].Value = "ID";
            //sheet.Cells[1, 2].Value = "名称";
            //sheet.Cells[1, 3].Value = "商户订单编号";
            //sheet.Cells[1, 4].Value = "微信订单编号";
            //sheet.Cells[1, 5].Value = "openID";
            //sheet.Cells[1, 6].Value = "总金额";

            //sheet.Cells[1, 7].Value = "机器编号";
            //sheet.Cells[1, 8].Value = "机器详情";
            //sheet.Cells[1, 9].Value = "下单时间";
            //sheet.Cells[1, 10].Value = "状态";
            //sheet.Cells[1, 11].Value = "出货时间";

            using (ExcelRange range = sheet.Cells[1, 1, 1, _cellIndex - 1])
            {
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.Gray);

                //range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                //range.Style.Border.Bottom.Color.SetColor(Color.Black);

                //range.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));//设置单元格所有边框
                //range.AutoFitColumns(30, 100);
            }
            #endregion   

            #region write content
            int pos = 2;
            PropertyInfo[] pars = null;
            foreach (object obj in objList)
            {
                _cellIndex = 1;
                pars = ConvertObjectEntity<T>.GetProperties((T)obj);
                foreach (var key in pars)
                {
                    sheet.Cells[pos, _cellIndex].Value = key.GetValue(obj, null);
                    _cellIndex++;
                }
                using (ExcelRange range = sheet.Cells[pos, 1, pos, _cellIndex - 1])
                {
                    //range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    //range.Style.Border.Bottom.Color.SetColor(Color.Black);

                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));//设置单元格所有边框
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                }
                pos++;
            }
            #endregion

            package.Save();

            return stream;
        }
        public static MemoryStream Export<T>(List<T> objList, int timeCell1) where T : new()
        {
            MemoryStream stream = new MemoryStream();
            ExcelPackage package = new ExcelPackage(stream);

            package.Workbook.Worksheets.Add("list");
            ExcelWorksheet sheet = package.Workbook.Worksheets[1];
            T t = new T();
            PropertyInfo[] propertys = ConvertObjectEntity<T>.GetProperties(t);

            //sheet.Cells.Style.ShrinkToFit = true;//单元格自动适应大小
            sheet.Row(1).Height = 20;//设置行高
                                     //sheet.Row(1).CustomHeight = true;//自动调整行高
            sheet.Column(1).Width = 20;//设置列宽
            sheet.Column(2).Width = 20;//设置列宽
            for (int i = 2; i < propertys.Length; i++)
            {
                sheet.Column(i + 1).Width = 30;//设置列宽
            }
            sheet.Column(timeCell1).Width = 20;//设置列宽        
            sheet.Column(timeCell1).Style.Numberformat.Format = "YYYY-MM-dd HH:mm:ss";

            #region write header

            int _cellIndex = 1;
            foreach (var item in propertys)
            {
                sheet.Cells[1, _cellIndex].Value = item.Name;
                _cellIndex++;
            }


            using (ExcelRange range = sheet.Cells[1, 1, 1, _cellIndex - 1])
            {
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.Gray);

            }
            #endregion   

            #region write content
            int pos = 2;
            PropertyInfo[] pars = null;
            foreach (object obj in objList)
            {
                _cellIndex = 1;
                pars = ConvertObjectEntity<T>.GetProperties((T)obj);
                foreach (var key in pars)
                {
                    sheet.Cells[pos, _cellIndex].Value = key.GetValue(obj, null);
                    _cellIndex++;
                }
                using (ExcelRange range = sheet.Cells[pos, 1, pos, _cellIndex - 1])
                {
                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));//设置单元格所有边框
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                }
                pos++;
            }
            #endregion

            package.Save();

            return stream;
        }
        public static MemoryStream Export<T>(List<T> objList, int timeCell1, int timeCell2) where T : new()
        {
            MemoryStream stream = new MemoryStream();
            ExcelPackage package = new ExcelPackage(stream);

            package.Workbook.Worksheets.Add("list");
            ExcelWorksheet sheet = package.Workbook.Worksheets[1];
            T t = new T();
            PropertyInfo[] propertys = ConvertObjectEntity<T>.GetProperties(t);

            // sheet.Cells.Style.ShrinkToFit = true;//单元格自动适应大小
            sheet.Row(1).Height = 20;//设置行高
                                     //sheet.Row(1).CustomHeight = true;//自动调整行高
            sheet.Column(1).Width = 20;//设置列宽
            sheet.Column(2).Width = 20;//设置列宽
            for (int i = 2; i < propertys.Length; i++)
            {
                sheet.Column(i + 1).Width = 30;//设置列宽
            }
            sheet.Column(timeCell1).Width = 20;//设置列宽        
            sheet.Column(timeCell1).Style.Numberformat.Format = "YYYY-MM-dd HH:mm:ss";
            sheet.Column(timeCell2).Width = 20;//设置列宽
            sheet.Column(timeCell2).Style.Numberformat.Format = "YYYY-MM-dd HH:mm:ss";
            //sheet.Column(10).Width = 15;//设置列宽
            //sheet.Column(11).Width = 20;//设置列宽
            //sheet.Column(11).Style.Numberformat.Format = "YYYY-MM-dd HH:mm:ss";
            //sheet.View.ShowGridLines = false;//去掉sheet的网格线
            //sheet.Cells.Style.Fill.PatternType = ExcelFillStyle.Solid;
            //sheet.Cells.Style.Fill.BackgroundColor.SetColor(Color.White);//设置背景色
            //sheet.Cells[2, 1, 2, 6].Style.Fill.BackgroundColor.SetColor(Color.Red);

            #region write header

            int _cellIndex = 1;
            foreach (var item in propertys)
            {
                sheet.Cells[1, _cellIndex].Value = item.Name;
                _cellIndex++;
            }
            //sheet.Cells[1, 1].Value = "ID";
            //sheet.Cells[1, 2].Value = "名称";
            //sheet.Cells[1, 3].Value = "商户订单编号";
            //sheet.Cells[1, 4].Value = "微信订单编号";
            //sheet.Cells[1, 5].Value = "openID";
            //sheet.Cells[1, 6].Value = "总金额";

            //sheet.Cells[1, 7].Value = "机器编号";
            //sheet.Cells[1, 8].Value = "机器详情";
            //sheet.Cells[1, 9].Value = "下单时间";
            //sheet.Cells[1, 10].Value = "状态";
            //sheet.Cells[1, 11].Value = "出货时间";

            using (ExcelRange range = sheet.Cells[1, 1, 1, _cellIndex - 1])
            {
                range.Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;
                range.Style.Fill.BackgroundColor.SetColor(Color.Gray);

                //range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thick;
                //range.Style.Border.Bottom.Color.SetColor(Color.Black);

                //range.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));//设置单元格所有边框
                //range.AutoFitColumns(30, 100);
            }
            #endregion   

            #region write content
            int pos = 2;
            PropertyInfo[] pars = null;
            foreach (object obj in objList)
            {
                _cellIndex = 1;
                pars = ConvertObjectEntity<T>.GetProperties((T)obj);
                foreach (var key in pars)
                {
                    sheet.Cells[pos, _cellIndex].Value = key.GetValue(obj, null);
                    _cellIndex++;
                }
                using (ExcelRange range = sheet.Cells[pos, 1, pos, _cellIndex - 1])
                {
                    //range.Style.Border.Bottom.Style = OfficeOpenXml.Style.ExcelBorderStyle.Thin;
                    //range.Style.Border.Bottom.Color.SetColor(Color.Black);

                    range.Style.Border.BorderAround(ExcelBorderStyle.Thin, Color.FromArgb(191, 191, 191));//设置单元格所有边框
                    range.Style.HorizontalAlignment = OfficeOpenXml.Style.ExcelHorizontalAlignment.Left;
                }
                pos++;
            }
            #endregion

            package.Save();

            return stream;
        }
    
        /// <summary>
        /// 扩展方法
        /// </summary>
        /// <param name="obj"></param>
        /// <returns></returns>
        public static string GetObjectToString(Object obj)
        {
            if (obj == null)
            {
                return null;
            }
            else
            {
                return obj.ToString();
            }

        }

        public static string GetObjectToString(Object obj, bool isDate)
        {
            if (obj == null)
            {
                return null;
            }
            else
            {
                if (obj is DateTime && isDate)
                {
                    DateTime dt = (DateTime)obj;
                    return dt.ToShortDateString();
                }
                return obj.ToString();
            }

        }



    }
}
