using Check_Domain.Properties;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Runtime.InteropServices;
using System.Text;
using Office = Microsoft.Office.Core;
using Outlook = Microsoft.Office.Interop.Outlook;
using OutlookAddIn;
// TODO:  Follow these steps to enable the Ribbon (XML) item:

// 1: Copy the following code block into the ThisAddin, ThisWorkbook, or ThisDocument class.

//  protected override Microsoft.Office.Core.IRibbonExtensibility CreateRibbonExtensibilityObject()
//  {
//      return new Ribbon1();
//  }

// 2. Create callback methods in the "Ribbon Callbacks" region of this class to handle user
//    actions, such as clicking a button. Note: if you have exported this Ribbon from the Ribbon designer,
//    move your code from the event handlers to the callback methods and modify the code to work with the
//    Ribbon extensibility (RibbonX) programming model.

// 3. Assign attributes to the control tags in the Ribbon XML file to identify the appropriate callback methods in your code.  

// For more information, see the Ribbon XML documentation in the Visual Studio Tools for Office Help.


namespace Check_Domain
{
    [ComVisible(true)]
    public class Ribbon1 : Office.IRibbonExtensibility
    {
        private Office.IRibbonUI ribbon;

        public Ribbon1()
        {
        }

        #region IRibbonExtensibility Members

        public string GetCustomUI(string ribbonID)
        {
            return GetResourceText("Check_Domain.Ribbon1.xml");
        }

        #endregion

        #region Ribbon Callbacks
        //Create callback methods here. For more information about adding callback methods, visit http://go.microsoft.com/fwlink/?LinkID=271226

        public void Ribbon_Load(Office.IRibbonUI ribbonUI)
        {
            this.ribbon = ribbonUI;
        }

        #endregion

        #region Helpers

        private static string GetResourceText(string resourceName)
        {
            Assembly asm = Assembly.GetExecutingAssembly();
            string[] resourceNames = asm.GetManifestResourceNames();
            for (int i = 0; i < resourceNames.Length; ++i)
            {
                if (string.Compare(resourceName, resourceNames[i], StringComparison.OrdinalIgnoreCase) == 0)
                {
                    using (StreamReader resourceReader = new StreamReader(asm.GetManifestResourceStream(resourceNames[i])))
                    {
                        if (resourceReader != null)
                        {
                            return resourceReader.ReadToEnd();
                        }
                    }
                }
            }
            return null;
        }

        #endregion
        public void Button_Click(Office.IRibbonControl control)
        {
            try
            {

                Outlook.Recipient recipient = null;
                Outlook.Recipients recipients = null;

                Outlook.Application application = new Outlook.Application();
                Outlook.Explorer explorer = application.ActiveExplorer();
                Outlook.Inspector inspector = application.ActiveInspector();
                inspector.Activate();
                Outlook._MailItem mailItem = inspector.CurrentItem;

                //Outlook.Application outlookApplication = new Outlook.Application();
                //Outlook.MailItem mail = (Outlook.MailItem)outlookApplication.ActiveInspector().CurrentItem;

                if (mailItem != null)
                {

                    recipients = mailItem.Recipients;
                    recipients.ResolveAll();
                    String StrR = "";
                    const string PR_SMTP_ADDRESS =
                   "http://schemas.microsoft.com/mapi/proptag/0x39FE001E";
                    foreach (Outlook.Recipient recip in recipients)
                    {
                        Outlook.PropertyAccessor pa = recip.PropertyAccessor;
                        string smtpAddress =
                            pa.GetProperty(PR_SMTP_ADDRESS).ToString();

                        string[] strsplit = smtpAddress.Split('@');
                        if (!StrR.Contains(strsplit[1]))
                            StrR += strsplit[1] + Environment.NewLine;
                    }
                    if (StrR != string.Empty)
                    {
                        MyMessageBox ObjMyMessageBox = new MyMessageBox();
                        ObjMyMessageBox.ShowBox(StrR);
                    }



                    //recipient.Resolve();
                    //                DialogResult result = MessageBox.Show("Are you sure you want to send emails to the following domains " + StrR, "Varify Domains ",
                    //MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    //                if (result == DialogResult.Yes)
                    //                {
                    //                    //code for Yes
                    //                }
                    //                else
                    //                {

                    //                }


                }
            }
            catch (Exception ex)
            {

            }
        }


        public Bitmap imageSuper_GetImage(Office.IRibbonControl control)
        {
            return Resources.profile;
        }
    }
}
