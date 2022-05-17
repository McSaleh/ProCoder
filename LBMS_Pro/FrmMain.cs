using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Reflection;


namespace LBMS_Pro
{
    public partial class FrmMain : Form
    {
        private Mode mode;
        private int NUQ = 2;
        private string Pppoe;
        private string Address;
        private string Nat;
        private string Mangle;
        private string Mangle1;
        private string Routs;
        private string outinf="Local";
        private bool isProssess = false;
        C_MikrotikScripts _cms;
        public FrmMain()
        {
            InitializeComponent();
            C_WlanIPAddr.ValueType = typeof(IPAddress);
            mode = Mode.NTHBridge;
            CB_BType.SelectedIndex = 0;
            Pppoe = "";
            Address = "";
            Nat = "";
            Mangle = "";
            Routs = "";
            _cms = new C_MikrotikScripts();
            LA_Version.Text = ProductVersion;
            RendomLink();
        }
        private void RendomLink()
        {
            Random rnd = new Random();
            int link = rnd.Next(1, 4);
            switch (link)
            {
                case 1:
                    System.Diagnostics.Process.Start(Lin_Face.Text);
                    break;
                case 2:
                    System.Diagnostics.Process.Start(Lin_Tik.Text);
                    break;
                case 3:
                    System.Diagnostics.Process.Start(Lin_Twi.Text);
                    break;
                case 4:
                    System.Diagnostics.Process.Start(Lin_You.Text);
                    break;
                default:
                    System.Diagnostics.Process.Start(Lin_Tik.Text);
                    break;
            }
        }
        private void CheckData()
        {
            Pppoe = "";
            Address = "";
            Nat = "";
            Mangle = "";
            Routs = "";
            NUQ = Convert.ToInt32(NUP_NRQ.Value);
            outinf = OUT_Name.Text;
            int res = 0;
            foreach (DataGridViewRow row in DGV_Main.Rows)
            {
                try
                {
                    res = Convert.ToInt32(row.Cells["C_SLC"].Value);
                    if (res > 0)
                    {
                        switch (row.Cells["C_DataTYPE"].Value.ToString())
                        {
                            case "كيلو بايت":
                                row.Cells["C_DataSize"].Value = res;
                                break;
                            case "ميجا بايت":
                                row.Cells["C_DataSize"].Value = res * 1024;
                                break;
                            case "جيجا بايت":
                                row.Cells["C_DataSize"].Value = (res * 1024) * 1024;
                                break;
                            default:
                                row.Cells["C_DataSize"].Value = "0";
                                break;
                        }
                    }
                    else
                    {
                        row.Cells["C_DataSize"].Value = "0";
                    }
                }
                catch
                {
                    res = 0;
                    row.Cells["C_DataSize"].Value = "0";
                }
            }
            int Adv = miniSize() / NUQ;
            if (Adv >= 1)
            {
                foreach (DataGridViewRow row in DGV_Main.Rows)
                {
                    row.Cells["C_index"].Value = Convert.ToInt32(Convert.ToInt32(row.Cells["C_DataSize"].Value) / Adv);
                }
            }
            else
            {
                MessageBox.Show("تأكد من إدخال سرعة الخطوط بالطريقة الصحيحة");
            }

        }
        private void BuildSingle()
        {
            string a1 = "";
            string p1 = "";
            string n1 = "";
            string ma = "";
            string mio = "";
            string m2 = "";
            string r1 = "";
            string r2 = "";
            switch (mode)
            {
                case Mode.NTHBridge:
                    foreach (DataGridViewRow row in DGV_Main.Rows)
                    {
                        a1 = a1 + "\r\n" + AddAddress(row.Cells["C_interface"].Value.ToString(), row.Cells["C_WlanIPAddr"].Value.ToString(), row.Cells["C_WlanNetIP"].Value.ToString());
                        p1 = p1 + "\r\n" + AddPppoe(row.Cells["C_interface"].Value.ToString(), row.Cells["C_pppoeClint"].Value.ToString(), row.Cells["C_PPPOEUSER"].Value.ToString(), row.Cells["C_C_PPPOEPASS"].Value.ToString());
                        n1 = n1 + "\r\n" + AddNatBridge(row.Cells["C_pppoeClint"].Value.ToString());
                        m2 = m2 + "\r\n" + AddSingleMangle(row.Cells["C_WlanNetIP"].Value.ToString() + "/24", outinf);
                    }

                    Address = "\r\n" + "add address=" + TB_IPout.Text + " interface=" + outinf + " network=" + TB_OUTSub.Text + a1;
                    Pppoe = p1;
                    Nat = AddSingleNat(TB_OUTSub.Text + "/24") + n1;
                    Mangle = m2;
                    break;
                case Mode.PCC:
                    foreach (DataGridViewRow row in DGV_Main.Rows)
                    {
                        a1 = a1 + "\r\n" + AddAddress(row.Cells["C_interface"].Value.ToString(), row.Cells["C_WlanIPAddr"].Value.ToString(), row.Cells["C_WlanNetIP"].Value.ToString());
                        n1 = n1 + "\r\n" + AddNatPCC(row.Cells["C_interface"].Value.ToString());
                        ma = ma + "\r\n" + AddSingleManglePCCAcsept(row.Cells["C_WlanNetIP"].Value.ToString() + "/24", outinf);
                        mio = mio + "\r\n" + AddSingleManglePCCInputOut(row.Cells["C_interface"].Value.ToString() , row.Cells["C_interface"].Value.ToString()+"_Con");
                        m2 = m2 + "\r\n" + AddSingleManglePCC(outinf, row.Cells["C_interface"].Value.ToString() + "_Con");
                        r1 = r1 + "\r\n" + AddRoutsPCCMr(row.Cells["C_WlanIPGetway"].Value.ToString() , row.Cells["C_interface"].Value.ToString() + "_Con");
                        r2 = r2 + "\r\n" + AddRoutsPCCDis(row.Cells["C_WlanIPGetway"].Value.ToString() , (row.Index+1).ToString());
                    }

                    Address = "\r\n" + "add address=" + TB_IPout.Text + " interface=" + outinf + " network=" + TB_OUTSub.Text + a1;
                    Nat = AddSingleNat(TB_OUTSub.Text + "/24") + n1;
                    Mangle=ma+mio;
                    Mangle1 = m2;
                    Routs = r1 + r2;
                    break;
            }

        }
        private void BuildMulti() 
        {
            int _nth = Nth();
            int ib = _nth;
            int il = 1;
            string m1 = "";
            string r1 = "";
            string p = "";
            string n = "";
            string r = "";
            string m = "";
            switch (mode)
            {
                case Mode.NTHBridge:
                    while (il <= ib)
                    {
                        foreach (DataGridViewRow row in DGV_Main.Rows)
                        {
                            if (Convert.ToInt32(row.Cells["C_index"].Value) >= 1)
                            {
                                string mk = "W" + (row.Index + 1) + "C" + row.Cells["C_index"].Value.ToString();
                                m1 = m1 + "\r\n" + AddMangle(mk, outinf, _nth, il);
                                r1 = r1 + "\r\n" + AddRoutsBridge(row.Cells["C_pppoeClint"].Value.ToString(), mk, "1");
                                row.Cells["C_index"].Value = (Convert.ToInt32(row.Cells["C_index"].Value) - 1);
                                il++;
                            }
                        }
                    }
                    p = "/interface pppoe-client"  + Pppoe;
                    Pppoe = p;
                    n = "/ip firewall nat" + "\r\n" + Nat;
                    Nat = n;
                    m = "/ip firewall mangle" + Mangle + m1;
                    Mangle = m;
                    r = "/ip route" + r1;
                    Routs = r;
                    TB_Result.Text = "/ip address" + Address + "\r\n" + Pppoe + "\r\n" + Nat + "\r\n" + Mangle + "\r\n" + Routs;
                    break;
                case Mode.PCC:
                    while (il <= ib)
                    {
                        foreach (DataGridViewRow row in DGV_Main.Rows)
                        {
                            if (Convert.ToInt32(row.Cells["C_index"].Value) >= 1)
                            {
                                string mk = row.Cells["C_interface"].Value.ToString() + "_Con";
                                m1 = m1 + "\r\n" + AddManglePCC1(mk, outinf, _nth, il-1);
                                row.Cells["C_index"].Value = (Convert.ToInt32(row.Cells["C_index"].Value) - 1);
                                il++;
                            }
                        }
                    }
                    n = "/ip firewall nat" + "\r\n" + Nat;
                    Nat = n;
                    m = "/ip firewall mangle" + Mangle + m1 + Mangle1;
                    Mangle = m;
                    r = "/ip route" + Routs;
                    Routs = r;
                    TB_Result.Text = "/ip address"+ Address +"\r\n" + Nat + "\r\n" + Mangle + "\r\n" + Routs;
                    break;
            } 
        }
        private void BuildScript()
        {
            CheckData();
            BuildSingle();
            BuildMulti();
        }

        //[DefaultValue(Mode.NTHBridge)]
        //public Mode _MODE
        //{
        //    get
        //    {
        //        return this.mode;
        //    }
        //    set
        //    {
        //        this.mode = value; ;
        //    }
        //}
        private void DGV_Main_CellContentClick(object sender, DataGridViewCellEventArgs e)
        {

        }

        private void DGV_Main_DataError(object sender, DataGridViewDataErrorEventArgs e)
        {

            int row = e.RowIndex + 1;
            string errorMessage = "خطاء في البيانات المدخلة.\n" + "الحقل: " + row + "\n" + "الخطأ : " + e.Exception.Message;
            MessageBox.Show(errorMessage, "Data Error");
        }

        private void DGV_Main_RowsAdded(object sender, DataGridViewRowsAddedEventArgs e)
        {
                DataGridView view = sender as DataGridView;
                DataGridViewComboBoxColumn cbc = sender as DataGridViewComboBoxColumn;
                view["C_interface", e.RowIndex].Value = "WLAN" + Convert.ToString(e.RowIndex + 1);
                switch (mode)
                {
                    case Mode.NTHBridge:
                        view["C_pppoeClint", e.RowIndex].Value = "pppoe-out" + Convert.ToString(e.RowIndex + 1);
                        view["C_PPPOEUSER", e.RowIndex].Value = "";
                        view["C_C_PPPOEPASS", e.RowIndex].Value = "";

                        view["C_WlanIPAddr", e.RowIndex].Value = "192.168." + Convert.ToString(e.RowIndex + 1) + ".10/24";
                        view["C_WlanNetIP", e.RowIndex].Value = "192.168." + Convert.ToString(e.RowIndex + 1) + ".0";
                        view["C_WlanIPGetway", e.RowIndex].Value = "192.168." + Convert.ToString(e.RowIndex + 1) + ".1";
                        break;
                    case Mode.PCC:
                        view["C_WlanIPAddr", e.RowIndex].Value = "192.168." + Convert.ToString(e.RowIndex + 1) + ".10/24";
                        view["C_WlanNetIP", e.RowIndex].Value = "192.168." + Convert.ToString(e.RowIndex + 1) + ".0";
                        view["C_WlanIPGetway", e.RowIndex].Value = "192.168." + Convert.ToString(e.RowIndex + 1) + ".1";
                        break;
                }
                view["C_SPEEDLINE", e.RowIndex].Value = "";
                view["C_SLC", e.RowIndex].Value = "2";
                view["C_DataTYPE", e.RowIndex].Value = "ميجا بايت";
                view["C_DataSize", e.RowIndex].Value = "0";
                view["C_NUMREQUSTE", e.RowIndex].Value = "1";
                view["C_index", e.RowIndex].Value = "0";
        }
        private int miniSize()
        {
            ///Solution1
            List<double> dsize = new List<double>();
            //foreach (DataGridViewRow row in  DGV_Main.Rows)
            //{
            //    dsize.Add(Convert.ToDouble(row.Cells["C_DataSize"].Value));
            //}
            ///Solution2
            double m = 99999999;
            for (int i = 0; i < DGV_Main.Rows.Count; i++)
            {
                m = Math.Min(m, Convert.ToDouble(DGV_Main["C_DataSize", i].Value));
            }
            return Convert.ToInt32(m);
           // return Convert.ToInt32(dsize.Min());
        }
        private int Nth()
        {
            ///Solution1
            List<int> dnth = new List<int>();
            foreach (DataGridViewRow row in DGV_Main.Rows)
            {
                dnth.Add(Convert.ToInt32(row.Cells["C_index"].Value));
            }
            ///Solution2
            //double m = 99999999;
            //for (int i = 0; i < DGV_Main.Rows.Count; i++)
            //{
            //    m = Math.Min(m, Convert.ToDouble(DGV_Main["C_DataSize", i].Value));
            //}
            //return Convert.ToInt32(m);
            return Convert.ToInt32(dnth.Sum());
        }
        private string AddPppoe(string in_Face,string pppoeName,string user ,string pass)
        {
            return string.Concat(new string[]
                  {
                   "add comment=+967-779537981 disabled=no interface="
                    ,in_Face
                    ," name="
                    ,pppoeName
                    ," password="
                    ,pass
                    ," user="
                    ,user
                  }); ;
        } private string AddAddress(string in_Face, string ip,string netmask)
        {
            return string.Concat(new string[]
                  {
                  "add address="
                  ,ip
                  ," interface="
                  ,in_Face
                  ," network="
                  ,netmask
                  }); ;
        }
        private string AddNatBridge(string out_if)
        {
            return string.Concat(new string[]
                  {
                   "add action=masquerade chain=srcnat out-interface="
                   ,out_if
                  }); ;
        }
        private string AddNatPCC(string out_if)
        {
            return string.Concat(new string[]
                  {
                   "add chain = srcnat out-interface="
                   ,out_if
                   ," action = masquerade"
                  }); ;
        }
        private string AddSingleNat(string srs_add)
        {
            return string.Concat(new string[]
                  {
                   "add action=masquerade chain=srcnat src-address="
                   ,srs_add
                  }); ;
        }
        private string AddMangle(string conMark, string in_if, int nth, int cnth)
        {
            return string.Concat(new string[]
                  {
                   @"add action=mark-connection chain=prerouting connection-state=new \"
                  ,"\r\nin-interface="
                  ,in_if
                  ," new-connection-mark="
                  ,conMark
                  ," nth="
                  ,nth.ToString()
                  ,","
                  ,cnth.ToString()
                  ," passthrough=yes\r\n"
                  ,"add action=mark-routing chain=prerouting connection-mark="
                  ,conMark
                  ,@" in-interface=\"
                  ,"\r\n"
                  ,in_if
                  ," new-routing-mark="
                  ,conMark
                  ," passthrough=yes"
                  }); ;
        }
        private string AddManglePCC(string conMark, string in_if, int nth, int cnth)
        {
            return string.Concat(new string[]
                  {
                   @"add action=mark-connection chain=prerouting connection-state=new \"
                  ,"\r\nin-interface="
                  ,in_if
                  ," new-connection-mark="
                  ,conMark
                  ," nth="
                  ,nth.ToString()
                  ,","
                  ,cnth.ToString()
                  ," passthrough=yes\r\n"
                  ,"add action=mark-routing chain=prerouting connection-mark="
                  ,conMark
                  ,@" in-interface=\"
                  ,"\r\n"
                  ,in_if
                  ," new-routing-mark="
                  ,conMark
                  ," passthrough=yes"
                  }); ;
        }
        private string AddManglePCC1(string conMark, string in_if, int nth, int cnth)
        {
            return string.Concat(new string[]
                  {
                   "add chain=prerouting dst-address-type=!"
                   ,in_if
                   ," in-interface="
                   ,in_if
                   ," per-connection-classifier=both-addresses:"
                   ,nth.ToString()
                  ,"/"
                  ,cnth.ToString()
                  ," action=mark-connection new-connection-mark="
                  ,conMark
                  ," passthrough=yes"
                  }); ;
        }
        private string AddSingleMangle(string dsip, string in_if)
        {
            return string.Concat(new string[]
                  {
                   "add action=accept chain=prerouting dst-address="
                   ,dsip
                   ," in-interface="
                   ,in_if
                  }); ;
        }
        private string AddSingleManglePCCAcsept(string dsip, string in_if)
        {
            return string.Concat(new string[]
                  {
                   "add action=accept chain=prerouting dst-address="
                   ,dsip
                   ," in-interface="
                   ,in_if
                  }); ;
        }
        private string AddSingleManglePCCInputOut(string in_if, string markcon)
        {
            return string.Concat(new string[]
                  {
                     "add chain=input in-interface="
                    ,in_if
                    ," action=mark-connection new-connection-mark="
                    ,markcon
                    ,"\r\n"
                    ,"add chain=output connection-mark="
                    ,markcon
                    ," action=mark-routing new-routing-mark="
                    ,markcon
                  }); ;
        }
        private string AddSingleManglePCC(string out_if, string markcon)
        {
            return string.Concat(new string[]
                  {
                     "add chain=prerouting connection-mark="
                     ,markcon
                     ," in-interface="
                     ,out_if
                     ," action=mark-routing new-routing-mark="
                     ,markcon
                  }); ;
        }
        private string AddRoutsBridge(string in_Gateway,string routMark,string dist)
        {
            return string.Concat(new string[]
                  {
                   "add distance="
                   ,dist
                   ," gateway="
                   ,in_Gateway
                   ," routing-mark="
                   ,routMark 
                  }); ;
        }
        private string AddRoutsPCC(string in_Gateway,string routMark,string dist)
        {
            return string.Concat(new string[]
                  {
                   "add dst-address=0.0.0.0/0 gateway="
                   ,in_Gateway
                   ," routing-mark="
                   ,routMark 
                   ," distance="
                   ,dist
                   ," check-gateway=ping"
                  }); ;
        }
        private string AddRoutsPCCMr(string in_Gateway,string routMark)
        {
            return string.Concat(new string[]
                  {
                   "add dst-address=0.0.0.0/0 gateway="
                   ,in_Gateway
                   ," routing-mark="
                   ,routMark 
                   ," check-gateway=ping"
                  }); ;
        }
        private string AddRoutsPCCDis(string in_Gateway,string dist)
        {
            return string.Concat(new string[]
                  {
                   "add dst-address=0.0.0.0/0 gateway="
                   ,in_Gateway
                   ," distance="
                   ,dist
                   ," check-gateway=ping"
                  }); ;
        }
        private void DGV_Main_CellValidated(object sender, DataGridViewCellEventArgs e)
        {
            if (isProssess)
            {
                DataGridView view = sender as DataGridView;
                if (view.CurrentRow.Cells[e.ColumnIndex] == view.CurrentRow.Cells["C_SLC"] || view.CurrentRow.Cells[e.ColumnIndex] == view.CurrentRow.Cells["C_DataTYPE"])
                {
                    int res = 0;
                    try
                    {
                        res = Convert.ToInt32(view.CurrentRow.Cells["C_SLC"].Value);
                        if (res > 0)
                        {
                            switch (view.CurrentRow.Cells["C_DataTYPE"].Value.ToString())
                            {
                                case "كيلو بايت":
                                    view.CurrentRow.Cells["C_DataSize"].Value = res;
                                    break;
                                case "ميجا بايت":
                                    view.CurrentRow.Cells["C_DataSize"].Value = res * 1024;
                                    break;
                                case "جيجا بايت":
                                    view.CurrentRow.Cells["C_DataSize"].Value = (res * 1024) * 1024;
                                    break;
                                default:
                                    view.CurrentRow.Cells["C_DataSize"].Value = "0";
                                    break;
                            }
                        }
                        else
                        {
                            view.CurrentRow.Cells["C_DataSize"].Value = "0";
                        }
                    }
                    catch
                    {
                        res = 0;
                        view.CurrentRow.Cells["C_DataSize"].Value = "0";
                    }
                }
            }
        }

        private void BT_CS_Click(object sender, EventArgs e)
        {
            isProssess = true;
            DGV_Main.ReadOnly = true;
            DGV_Main.EndEdit();
            BuildScript();
            DGV_Main.ReadOnly = false;
            isProssess = false;
        }

        private void BT_Add_Click(object sender, EventArgs e)
        {
            DGV_Main.ReadOnly = true;
            DGV_Main.Rows.Add();
            DGV_Main.ReadOnly = false;
        }

        private void CB_BType_SelectedIndexChanged(object sender, EventArgs e)
        {
            ComboBox CB = sender as ComboBox;
            if (CB.SelectedIndex == 0)
            {
                mode = Mode.NTHBridge;
            }
            else
            {
                mode = Mode.PCC;
            }
        }

        private void BT_BALANCE_Click(object sender, EventArgs e)
        {
            if (TC_Main.SelectedTab != TC_Main.TabPages["TP_LBS"])
            {
                TC_Main.SelectedTab = TC_Main.TabPages["TP_LBS"];
            }         
        }

        private void BT_MikScr_Click(object sender, EventArgs e)
        {
            if (TC_Main.SelectedTab != TC_Main.TabPages["TP_MikScr"])
            {
                TC_Main.SelectedTab = TC_Main.TabPages["TP_MikScr"];
            }
        }

        private void BT_Exit_Click(object sender, EventArgs e)
        {
            this.Close();
        }
        private void BT_SecScr_Click(object sender, EventArgs e)
        {
            if (TC_Main.SelectedTab != TC_Main.TabPages["TP_SecScr"])
            {
                TC_Main.SelectedTab = TC_Main.TabPages["TP_SecScr"];
            }
        }

        private void BT_MikSQDAS_Click(object sender, EventArgs e)
        {
            TB_Result.Text = _cms.QDAutoSpeed(NU_SpeedNet.Value.ToString());
        }

        private void CB_RBV_CheckedChanged(object sender, EventArgs e)
        {

        }

        private void BT_MikSQTAS_Click(object sender, EventArgs e)
        {
            TB_Result.Text = _cms.QTAutoSpeed(NU_SpeedNetQT.Value.ToString(),MTB.Text, Convert.ToBoolean(CB_RBV.CheckState));
        }

        private void BT_SecFBU_Click(object sender, EventArgs e)
        {
            TB_Result.Text = _cms.SecSFreedomBU(MTB_OUTinf.Text,(TBlouk.Value.ToString())+"m");
        }

        private void Panel2_Paint(object sender, PaintEventArgs e)
        {

        }

        private void PL_DeveloperInfo_Paint(object sender, PaintEventArgs e)
        {

        }

        private void BT_InfoPage_Click(object sender, EventArgs e)
        {

            if (TC_Main.SelectedTab != TC_Main.TabPages["TP_Info"])
            {
                TC_Main.SelectedTab = TC_Main.TabPages["TP_Info"];
            }  
        }

        private void BT_info_Click(object sender, EventArgs e)
        {
            if (TC_Main.SelectedTab != TC_Main.TabPages["TP_Info"])
            {
                TC_Main.SelectedTab = TC_Main.TabPages["TP_Info"];
            }
        }

        private void BT_SecSLGDuobleMac_Click(object sender, EventArgs e)
        {
            TB_Result.Text = _cms.SecSFreedomBU(MTB_OUTinf.Text, (TBlouk.Value.ToString()) + "m");
        }

        private void BT_MultiTask_Click(object sender, EventArgs e)
        {
            if (TC_Main.SelectedTab != TC_Main.TabPages["TP_MultiTask"])
            {
                TC_Main.SelectedTab = TC_Main.TabPages["TP_MultiTask"];
            }
        }

        private void GB_QueueTree_Enter(object sender, EventArgs e)
        {

        }

        private void GroupBox6_Enter(object sender, EventArgs e)
        {

        }
    }
    public enum Mode
    {
         NTHBridge
       , PCC
       , MikS
       , MikSQDAutoSpeed
       , MikSQTAutoSpeed
       , MikSMBackUp
       , MikSMBackUpRSC
       , MikSUMBackUp
       , MikSMLogBackup
       , MikSMBackupToEmail
       , MikSMBackupRSCToEmail
       , MikSUMBackupToEmail
       , MikSMLogBackupToEmail
       , MikSUMRemoveSection
       , MikSUMRebuildDataBase
       , SecSFreedom
       , SecSFreedom1
       , SecSNetCut
       , SecSRemoveHost
       , SecSLGDuobleMac
       , SecSClosePorts
       , SecSDogWatch
    }
}
