using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using tik4net;
using tik4net.Objects;
using tik4net.Objects.Ip.Hotspot;

namespace LBMS_Pro
{
    public partial class Frm_Login : Form
    {
        public Frm_Login()
        {
            InitializeComponent();
        }

        private void Button1_Click(object sender, EventArgs e)
        {
            using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
            {
                try
                {
                    connection.Open("10.0.0.1", "admin", "ABURAID1993Professional");
                    var users = connection.CreateCommand("/tool/user-manager/profile/print");
                    String[] s = Regex.Split(users.ExecuteScalar(), "\r\n");
                    //foreach (var user in hs)
                    //{
                    //    var ids = user.Id;
                    //    listBox1.Items.Add(ids);
                    //    // connection.Delete<HotspotActive>(user);
                    //}
                    foreach (string user in s)
                    {
                       // var ids = user;
                        listBox1.Items.Add(user);
                        // connection.Delete<HotspotActive>(user);
                    }

                }
                catch (Exception ex)
                {
                    RTB_Result.Text = ex.ToString();
                }      
                var hs = connection.LoadAll<tik4net.Objects.Ip.IpAddress>();
                foreach (var user in hs)
                {             
                    string add = "IP:" + user.Address + " Interface:" + user.Interface;
                    listBox1.Items.Add(add);
                }


            }
        }
    }
}
