using System;
using System.IO;
using System.Windows.Forms;
using Tormor.DomainModel;
using Tormor.Web.Reports;
using Tormor.Web.Reports.Models;
using Tormor.Web.Reports.Helpers;
using System.Reflection;

namespace XtraReportDesigner
{
    public partial class fmMain : Form
    {
        public fmMain()
        {
            InitializeComponent();
        }

        private void btnDesigner_Click(object sender, EventArgs e)
        {
            using (var report = ReportHelper.GetReportByClassName(listBox1.SelectedItem.ToString()))
            {
                if (report != null)
                    report.EditReport();
            }
        }

        private void btnOpenFromFile_Click(object sender, EventArgs e)
        {
            using (var openFileDialog = new OpenFileDialog
            {
                InitialDirectory = Environment.CurrentDirectory,
                Filter = "repx files (*.repx)|*.repx",
                FilterIndex = 1
            })
            {
                if (openFileDialog.ShowDialog() == DialogResult.OK)
                {
                    var report = (BaseReport)BaseReport.FromFile(openFileDialog.FileName,false);
                    report.LoadLayout(openFileDialog.FileName);
                    report.EditReport();
                }
            }
        }

        private void LoadReportList()
        {
            listBox1.Items.Clear();
            var filePath = Path.Combine(Application.StartupPath, "Tormor.Web.Reports.dll");
            var assem = Assembly.LoadFile(filePath);
            if (assem != null)
            {
                foreach (var t in assem.GetTypes())
                {
                    if (t.BaseType.Name == "BaseReport")
                        listBox1.Items.Add(t.Name);
                }
            }
        }

        private void fmMain_Load(object sender, EventArgs e)
        {
        }

        private void simpleButton1_Click(object sender, EventArgs e)
        {
            LoadReportList();
        }
    }

}
