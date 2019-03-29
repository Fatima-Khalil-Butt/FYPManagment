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
    public partial class StudentEvaluationReport : Form
    {
        public StudentEvaluationReport()
        {
            InitializeComponent();
        }

        private void StudentEvaluationReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet2.Studentevaluation' table. You can move, or remove it, as needed.
            this.StudentevaluationTableAdapter.Fill(this.DataSet2.Studentevaluation);

            this.reportViewer1.RefreshReport();
        }
    }
}
