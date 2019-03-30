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
    public partial class EvaluationReport : Form
    {
        public EvaluationReport()
        {
            InitializeComponent();
        }

        private void EvaluationReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet4.EvaluationReport' table. You can move, or remove it, as needed.
            this.EvaluationReportTableAdapter.Fill(this.DataSet4.EvaluationReport);

            this.reportViewer1.RefreshReport();
        }
    }
}
