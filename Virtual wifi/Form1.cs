using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Windows.Forms;
using System.Diagnostics;
using System.Security.Principal;
using VirtualRouterHost;

namespace Virtual_wifi
{
    public partial class Form1 : Form
    {
        Process newprocess = new Process();
        VirtualRouterHost.VirtualRouterHost virtualRouterHost;
        List<ConnectedPeer> connectedPeersList;
        Thread bgthread;

        public Form1()
        {
            newprocess.StartInfo.UseShellExecute = false;
            newprocess.StartInfo.CreateNoWindow = true;
            newprocess.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            InitializeComponent();
        }

        public bool IsUserAdminstrator()
        {
            bool isAdmin;

            try
            {
                WindowsIdentity user = WindowsIdentity.GetCurrent();
                WindowsPrincipal principal = new WindowsPrincipal(user);
                isAdmin = principal.IsInRole(WindowsBuiltInRole.Administrator);
            }

            catch (UnauthorizedAccessException)
            {
                isAdmin = false;
            }

            catch (Exception)
            {
                isAdmin = false;
            }
            return isAdmin;
        }

        public void Process_start_1()
        {
            Process_progressBar.Increment(25);
            newprocess.StartInfo.FileName = "netsh";
            newprocess.StartInfo.Arguments = "wlan stop hostednetwork";
            try
            {
                using (Process execute = Process.Start(newprocess.StartInfo))
                {
                    execute.WaitForExit();
                    Process_progressBar.Increment(25);
                    Process_start_2();
                }
            }
            catch
            {
                //nothing
            }
        }

        public void Process_start_2()
        {
            newprocess.StartInfo.FileName = "netsh";
            newprocess.StartInfo.Arguments = "wlan set hostednetwork mode=allow ssid=" + SSID_textBox.Text + " key=" + Password_textBox.Text;
            try
            {
                using (Process execute = Process.Start(newprocess.StartInfo))
                {
                    execute.WaitForExit();
                    Process_progressBar.Increment(25);
                    Process_start_3();
                }
            }
            catch
            {
                //nothing
            }
        }

        public void Process_start_3()
        {
            newprocess.StartInfo.FileName = "netsh";
            newprocess.StartInfo.Arguments = "wlan start hostednetwork";
            try
            {
                using (Process execute = Process.Start(newprocess.StartInfo))
                {
                    execute.WaitForExit();
                    int selectedIndex = comboBox1.SelectedIndex;
                    var conn = virtualRouterHost.GetSharableConnections();
                    List<SharableConnection> listOfSharableConnections = conn.ToList();
                    virtualRouterHost.Start(listOfSharableConnections[selectedIndex]);
                    Process_progressBar.Increment(25);
                    button_panel.Visible = true;
                    Play_Stop_button.Text = "Stop";
                    SSID_textBox.Enabled = false;
                    Password_textBox.Enabled = false;
                    comboBox1.Enabled = false;
                    bgthread = new Thread(() => ConnectedPeersTracker());
                    bgthread.Start();
                }
            }
            catch
            {
                //nothing
            }
        }

        public void Process_stop()
        {
            newprocess.StartInfo.FileName = "netsh";
            newprocess.StartInfo.Arguments = "wlan stop hostednetwork";
            try
            {
                Process_progressBar.Increment(50);
                using (Process execute = Process.Start(newprocess.StartInfo))
                {
                    execute.WaitForExit();
                    Process_progressBar.Increment(50);
                    button_panel.Visible = true;
                    Play_Stop_button.Text = "Start";
                    SSID_textBox.Enabled = true;
                    Password_textBox.Enabled = true;
                    comboBox1.Enabled = true;
                    bgthread.Abort();
                }
            }
            catch
            {
                //nothing
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (Play_Stop_button.Text == "Start")
            {
                if (SSID_textBox.TextLength < 1)
                {
                    MessageBox.Show("SSID Must be 1 character or more", "SSID Error");
                    return;
                }

                else if (Password_textBox.TextLength < 8)
                {
                    MessageBox.Show("Password Must be 8 characters or more", "Password Error");
                    return;
                }

                else if (comboBox1.SelectedItem.ToString()=="None" || comboBox1.SelectedItem.ToString()=="")
                {
                    MessageBox.Show("Select an adapter", "Adapter Error");
                    
                    return;
                }

                button_panel.Visible = false;
                Process_start_1();
            }
            else
            {
                button_panel.Visible = false;
                Process_stop();
            }
        }
       
        private void Form1_Load(object sender, EventArgs e)
        {
            virtualRouterHost = new VirtualRouterHost.VirtualRouterHost();
            connectedPeersList = new List<ConnectedPeer>();
            

            foreach (var conn in virtualRouterHost.GetSharableConnections())
            {
                comboBox1.Items.Add(conn.Name);
            }

            if (!IsUserAdminstrator())
            {
                MessageBox.Show("Run as Administrator", "Administrator Privilages Required", MessageBoxButtons.OK, MessageBoxIcon.Asterisk);
                System.Environment.Exit(0);
            }
        }

        private void label3_Click(object sender, EventArgs e)
        {

        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        
        public void ConnectedPeersTracker()
        {
            //while (true)
            //{
            //    foreach (var connPeers in virtualRouterHost.GetConnectedPeers())
            //    {
            //        listBox1.Items.Add(connPeers.MacAddress.ToString());
            //    }
            //    Thread.Sleep(200);
            //    listBox1.Items.Clear();
            //    //listBox1.
            //}
            var peers = virtualRouterHost.GetConnectedPeers();
            while (true)
            {
                label6.Text = "Peers Connected (" + peers.Count().ToString() + "):";
                foreach (var p in peers)
                {
                    listBox1.Items.Add(p.MacAddress.ToString());
                }
                Thread.Sleep(200);
                listBox1.Items.Clear();
            }
        }

        //private bool isPeerAlreadyConnected(ConnectedPeer peer)
        //{
        //    foreach (var element in listBox1.Items)
        //    {
        //        var elem;// = element as PeerDevice;
        //        if (elem != null)
        //        {
        //            if (elem.Peer.MacAddress.ToLowerInvariant() == peer.MacAddress.ToLowerInvariant())
        //            {
        //                return true;
        //            }
        //        }
        //    }
        //    return false;
        //}

        private void listBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void exListBox1_SelectedIndexChanged(object sender, EventArgs e)
        {

        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (MessageBox.Show("Really Quit?", "Exit", MessageBoxButtons.OKCancel) == DialogResult.OK)
            {
                Process_stop();
                Application.Exit();

            }
        }

        private void helpToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("How to Use the Program : ", "Help", MessageBoxButtons.OK);
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("Created by : \n\tNikhil N\n\tNikith Shetty", "About", MessageBoxButtons.OK);
            //AboutBox.ActiveForm.Activate();
        }
    }
}
