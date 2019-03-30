using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProjectA
{
    public partial class ProjectReport : Form
    {
        public ProjectReport()
        {
            InitializeComponent();
        }

        private void ProjectReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet3.ProjectReport' table. You can move, or remove it, as needed.
            this.ProjectReportTableAdapter.Fill(this.DataSet3.ProjectReport);

            this.reportViewer1.RefreshReport();
        }
    }
}
