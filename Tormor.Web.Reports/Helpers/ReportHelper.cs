using System;
using System.IO;
using System.Web.Mvc;
using DevExpress.XtraPrinting;
using System.Collections.Generic;
using DevExpress.XtraReports.Parameters;

namespace Tormor.Web.Reports.Helpers
{
    public static class ReportHelper
    {
        public static byte[] DevReportToFile(this Controller controller,
            string exportType, string reportName, string reportLayout, out string mimeType,
            Dictionary<string, string> paramDict,
            object Model, object Model2 = null, object Model3 = null, object Model4 = null)
        {
            mimeType = "";
            var xRpt = GetReportByClassName(reportName);
            if (xRpt != null)
            {
                if (!string.IsNullOrEmpty(reportLayout))
                {
                    var fileName = controller.Server.MapPath(String.Format("~/Content/Reports/{0}.repx", reportLayout));
                    if (File.Exists(fileName))
                        xRpt.LoadLayout(fileName);
                }

                xRpt.BindData(Model, Model2, Model3, Model4);

                //localReport.DataSources.Add(reportDataSource2);
                if (paramDict != null)
                {
                    // Add the parameter to the report, and force the report 
                    // to request the parameter's value in preview.
                    //xRpt.Parameters.Clear();
                    foreach (var dict in paramDict)
                    {
                        var param1 = new Parameter
                        {
                            Name = dict.Key,
                            Value = dict.Value,
                            Type = typeof(System.String),
                            Visible = true
                        };
                        xRpt.Parameters.Add(param1);
                    }
                    xRpt.RequestParameters = true;
                }

                xRpt.CreateDocument();
                using (var stream = new MemoryStream())
                {
                    switch (exportType)
                    {
                        case "PDF":
                            mimeType = "application/pdf";
                            //Response.AddHeader("Content-Disposition", "attachment;filename=aaa.pdf");
                            xRpt.ExportToPdf(stream, new PdfExportOptions());
                            break;
                        case "XLS":
                            mimeType = "application/vnd.ms-excel";
                            xRpt.ExportToXls(stream, new XlsExportOptions
                                                        {
                                                            ShowGridLines = true,
                                                            Suppress256ColumnsWarning = true,
                                                            Suppress65536RowsWarning = true
                                                        });
                            break;
                        case "XLSX":
                            mimeType = "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet";
                            xRpt.ExportToXlsx(stream, new XlsxExportOptions());
                            break;
                        case "DOC":
                            mimeType = "application/msword";
                            xRpt.ExportToRtf(stream, new RtfExportOptions { ExportMode = RtfExportMode.SingleFile });
                            break;
                        default:
                            break;
                    }
                    if (stream.Length > 0)
                    {
                        byte[] result = stream.ToArray();
                        // return the actual pdf
                        return result;
                    }
                }
            }
            return null;
        }

        public static byte[] DevReportToPDF(this Controller controller,
            string reportName, string reportLayout, out string mimeType,
            Dictionary<string, string> paramDict,
            object Model, object Model2 = null, object Model3 = null, object Model4 = null)
        {
            return controller.DevReportToFile("PDF", reportName, reportLayout, out mimeType, paramDict, Model, Model2, Model3, Model4);
        }

        public static byte[] DevReportToExcel(this Controller controller,
            string reportName, string reportLayout, out string mimeType,
            Dictionary<string, string> paramDict,
            object Model, object Model2 = null, object Model3 = null, object Model4 = null)
        {
            return controller.DevReportToFile("XLS", reportName, reportLayout, out mimeType, paramDict, Model, Model2, Model3, Model4);
        }

        public static byte[] DevReportToExcel2007(this Controller controller,
            string reportName, string reportLayout, out string mimeType,
            Dictionary<string, string> paramDict,
            object Model, object Model2 = null, object Model3 = null, object Model4 = null)
        {
            return controller.DevReportToFile("XLSX", reportName, reportLayout, out mimeType, paramDict, Model, Model2, Model3, Model4);
        }

        public static BaseReport GetReportByClassName(string reportName)
        {
            var xRpt = (BaseReport)null;
            foreach (var assem in AppDomain.CurrentDomain.GetAssemblies())
            {
                if (assem.GetName().FullName.Contains("Web.Reports"))
                {
                    foreach (var t in assem.GetTypes())
                    {
                        if (t.Name.ToLower() == reportName.ToLower())
                        {
                            Object o = Activator.CreateInstance(t);
                            xRpt = (BaseReport)o;
                            break;
                        }
                    }
                }
                if (xRpt != null)
                    break;
            }
            return xRpt;
        }

        //function เก่าที่เป็นการ export ไป Crystal report และ Local Report
        //public static Stream CRReportToPDF(this Controller controller, string ReportName, object Model, out string mimeType)
        //{
        //    ReportClass rptH = new ReportClass();
        //    rptH.FileName = controller.Server.MapPath("~/Content/Reports/" + ReportName + ".rpt");
        //    rptH.Load();
        //    rptH.SetDataSource(Model);
        //    Stream stream = rptH.ExportToStream(CrystalDecisions.Shared.ExportFormatType.PortableDocFormat);
        //    mimeType = "application/pdf";
        //    return stream;
        //}

        //public static byte[] RenderReportToPDF(this Controller controller, string ReportName, object Model, 
        //    Dictionary<string,string> paramDict, out string mimeType)
        //{
        //    using (var localReport = new LocalReport())
        //    {
        //        localReport.ReportPath = controller.Server.MapPath(String.Format("~/Content/Reports/{0}.rdlc", ReportName));
        //        var reportDataSource = new ReportDataSource("DataSet1", Model);
        //        //ReportDataSource reportDataSource2 = new ReportDataSource("DataSet2", Model2);
        //        localReport.DataSources.Add(reportDataSource);
        //        //localReport.DataSources.Add(reportDataSource2);
        //        if (paramDict != null)
        //        {
        //            var xparams = new List<ReportParameter>();
        //            foreach (var dict in paramDict)
        //                xparams.Add(new ReportParameter(dict.Key, dict.Value, true));
        //            localReport.SetParameters(xparams);
        //        }
        //        const string reportType = "PDF";
        //        string encoding;
        //        string fileNameExtension;
        //        //The DeviceInfo settings should be changed based on the reportType
        //        //http://msdn2.microsoft.com/en-us/library/ms155397.aspx
        //        string deviceInfo = "<DeviceInfo>" + "  <OutputFormat>PDF</OutputFormat>" + //"  <PageWidth>8.5in</PageWidth>" +
        //            //"  <PageHeight>11in</PageHeight>" +
        //            //"  <MarginTop>0.5in</MarginTop>" +
        //            //"  <MarginLeft>1in</MarginLeft>" +
        //            //"  <MarginRight>1in</MarginRight>" +
        //            //"  <MarginBottom>0.5in</MarginBottom>" +
        //        "</DeviceInfo>";
        //        Warning[] warnings;
        //        string[] streams;
        //        byte[] renderedBytes;
        //        //Render the report
        //        renderedBytes = localReport.Render(reportType, deviceInfo, out mimeType, out encoding, out fileNameExtension, out streams, out warnings);
        //        //Response.AddHeader("content-disposition", "attachment; filename=NorthWindCustomers." + fileNameExtension);
        //        return renderedBytes;
        //    }
        //}
    }
}
