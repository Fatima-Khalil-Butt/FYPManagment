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
    public partial class StudentReport : Form
    {
        public StudentReport()
        {
            InitializeComponent();
        }

        private void StudentReport_Load(object sender, EventArgs e)
        {
            // TODO: This line of code loads data into the 'DataSet1.View1' table. You can move, or remove it, as needed.
            this.View1TableAdapter.Fill(this.DataSet1.View1);

            this.reportViewer1.RefreshReport();
        }
    }
}
