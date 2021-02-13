using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Tormor.Web.Reports;
using DevExpress.XtraReports.UserDesigner;

namespace XtraReportDesigner
{
    public static class ReportHelpers
    {
        public static void EditReport(this BaseReport rep)
        {
            using (var form = new XRDesignRibbonFormEx())
            {
                var panel = form.DesignPanel;
                panel.SetCommandVisibility(ReportCommand.NewReport, DevExpress.XtraReports.UserDesigner.CommandVisibility.None);
                panel.SetCommandVisibility(ReportCommand.OpenFile, DevExpress.XtraReports.UserDesigner.CommandVisibility.None);
                form.RibbonControl.ToolbarLocation = DevExpress.XtraBars.Ribbon.RibbonQuickAccessToolbarLocation.Hidden;
                panel.OpenReport(rep);
                form.ShowDialog();
                panel.CloseReport();
            }
        }
    }
}
