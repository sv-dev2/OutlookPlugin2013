using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace OutlookAddIn
{
    public partial class MyMessageBox : Form
    {
        static MyMessageBox newMessageBox;
        public MyMessageBox()
        {
            InitializeComponent();
        }

        public void ShowBox(string txtMessage)
        {
            newMessageBox = new MyMessageBox();
            newMessageBox.LabelDomainName.Text = txtMessage;
            newMessageBox.ShowDialog();
        }

       


        private void btnCancel_Click(object sender, EventArgs e)
        {
            newMessageBox.Dispose();
        }

        private void btnYes_Click(object sender, EventArgs e)
        {
            newMessageBox.Dispose();
        }
    }
}
