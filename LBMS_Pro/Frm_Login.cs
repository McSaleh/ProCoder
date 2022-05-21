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

        private void Frm_Login_Load(object sender, EventArgs e)
        {

        }

        private void BT_GetProfile_Click(object sender, EventArgs e)
        {
            using (ITikConnection connection = ConnectionFactory.CreateConnection(TikConnectionType.Api))
            {
                connection.Open("10.0.0.1", "admin", "ABURAID1993Professional");
               // connection.CreateParameter("name", "P100");
                //ITikCommand cmd = connection.CreateCommand("/tool/user-manager/profile/print");
                ITikCommand cmd1 = connection.CreateCommand("/tool/user-manager/profile/profile-limitation/print");
               // cmd.AddParameterAndValues("name", "P100");
               // ITikCommand cmd2= connection.CreateCommand("/tool/user-manager/profile/limitation/print");
               // DataTable pdt = DT_UM_Profiles(cmd);
                DataTable pldt = DT_UM_Profileslimitation(cmd1);
                DGV_Prof.DataSource = pldt;
            }
        }
        private DataTable DT_UM_Profiles(ITikCommand cmd)
        {
            DataTable dt = new DataTable("UM_Profiles");
            DataColumn[] data ={
                     new DataColumn("id")
                    ,new DataColumn("name")
                    ,new DataColumn("owner")
                    ,new DataColumn("name-for-users")
                    ,new DataColumn("validity")
                    ,new DataColumn("starts-at")
                    ,new DataColumn("price")
                    ,new DataColumn("override-shared-users")};           
            dt.Columns.AddRange(data);
            dt.Columns[0].Caption = "معرف";
            dt.Columns[1].Caption = "إسم البروفايل";
            dt.Columns[2].Caption = "المالك";
            dt.Columns[3].Caption = "الإسم";
            dt.Columns[4].Caption = "الصلاحية";
            dt.Columns[5].Caption = "البدء في";
            dt.Columns[6].Caption = "السعر";
            dt.Columns[7].Caption = "عدد المستخدمين في نفس الوقت";
            var profi = cmd.ExecuteList();
            foreach (var j in profi)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = j.GetResponseField(".id");
                dr["name"] = j.GetResponseField("name");
                dr["owner"] = j.GetResponseField("owner");
                dr["name-for-users"] = j.GetResponseField("name-for-users");
                dr["validity"] = j.GetResponseField("validity");
                dr["starts-at"] = j.GetResponseField("starts-at");
                dr["price"] = j.GetResponseField("price");
                dr["override-shared-users"] = j.GetResponseField("override-shared-users");
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private DataTable DT_UM_Profileslimitation(ITikCommand cmd)
        {
            DataTable dt = new DataTable("UM_Profileslimitation");
            DataColumn[] data ={
                     new DataColumn("id")
                    ,new DataColumn("profile")
                    ,new DataColumn("limitation")
                    ,new DataColumn("from-time")
                    ,new DataColumn("till-time")
                    ,new DataColumn("weekdays")};
            dt.Columns.AddRange(data);
            dt.Columns[0].Caption = "معرف";
            dt.Columns[1].Caption = "البروفايل";
            dt.Columns[2].Caption = "ملف التحديدات";
            dt.Columns[3].Caption = "من وقت";
            dt.Columns[4].Caption = "الى وقت";
            dt.Columns[5].Caption = "الفترة";
            var profi = cmd.ExecuteList();
            foreach (var j in profi)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = j.GetResponseField(".id");
                dr["profile"] = j.GetResponseField("profile");
                dr["limitation"] = j.GetResponseField("limitation");
                dr["from-time"] = j.GetResponseField("from-time");
                dr["till-time"] = j.GetResponseField("till-time");
                dr["weekdays"] = j.GetResponseField("weekdays");
                dt.Rows.Add(dr);
            }
            return dt;
        }
        private DataTable DT_UM_limitation(ITikCommand cmd)
        {
            DataTable dt = new DataTable("UM_Profileslimitation");
            DataColumn[] data ={
                     new DataColumn("id")
                    ,new DataColumn("name")
                    ,new DataColumn("owner")
                    ,new DataColumn("download-limit")
                    ,new DataColumn("upload-limit")
                    ,new DataColumn("transfer-limit")
                    ,new DataColumn("uptime-limit")};
            dt.Columns.AddRange(data);
            dt.Columns[0].Caption = "معرف";
            dt.Columns[1].Caption = "إسم البروفايل";
            dt.Columns[2].Caption = "المالك";
            dt.Columns[3].Caption = "التحميل";
            dt.Columns[4].Caption = "الرفع";
            dt.Columns[5].Caption = "النقل";
            dt.Columns[6].Caption = "الوقت المستخدم";
            var profi = cmd.ExecuteList();
            foreach (var j in profi)
            {
                DataRow dr = dt.NewRow();
                dr["id"] = j.GetResponseField(".id");
                dr["name"] = j.GetResponseField("name");
                dr["owner"] = j.GetResponseField("owner");
                dr["download-limit"] = j.GetResponseField("download-limit");
                dr["upload-limit"] = j.GetResponseField("upload-limit");
                dr["transfer-limit"] = j.GetResponseField("transfer-limit");
                dr["uptime-limit"] = j.GetResponseField("uptime-limit");
                dt.Rows.Add(dr);
            }
            return dt;
        }
        
    }
}
