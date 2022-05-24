﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.Net;
using System.Reflection;
using System.IO;
using System.Text.RegularExpressions;

namespace LBMS_Pro
{
    public partial class FrmMain : Form
    {
        bool canMove;
       //bool canResize;
        Point cpoint;
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
        private string routsInSameServer;
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
            cpoint = new Point(this.Location.X, this.Location.Y);
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
            string routeLBISS = "";
            routsInSameServer = "";
            switch (mode)
            {
                case Mode.NTHBridge:
                    if (CB_ScrLB_insameServer.Checked)
                    {
                        int le = 1;
                        foreach (DataGridViewRow row in DGV_Main.Rows)
                        {
                            a1 = a1 + "\r\n" + AddAddress(row.Cells["C_interface"].Value.ToString(), row.Cells["C_WlanIPAddr"].Value.ToString(), row.Cells["C_WlanNetIP"].Value.ToString());
                            p1 = p1 + "\r\n" + AddPppoe(row.Cells["C_interface"].Value.ToString(), row.Cells["C_pppoeClint"].Value.ToString(), row.Cells["C_PPPOEUSER"].Value.ToString(), row.Cells["C_C_PPPOEPASS"].Value.ToString());
                            n1 = n1 + "\r\n" + AddNatBridge(row.Cells["C_pppoeClint"].Value.ToString());
                            m2 = m2 + "\r\n" + AddSingleMangle(row.Cells["C_WlanNetIP"].Value.ToString() + "/24", outinf);
                            if (le == 1)
                            {
                                routeLBISS = row.Cells["C_pppoeClint"].Value.ToString();
                            }
                            else
                            {
                                routeLBISS = routeLBISS + "," + row.Cells["C_pppoeClint"].Value.ToString();
                            }
                            le++;  
                        }
                        routsInSameServer = AddRoutsLBISS(routeLBISS);
                        Address = a1;
                    }
                    else
                    {
                        foreach (DataGridViewRow row in DGV_Main.Rows)
                        {
                            a1 = a1 + "\r\n" + AddAddress(row.Cells["C_interface"].Value.ToString(), row.Cells["C_WlanIPAddr"].Value.ToString(), row.Cells["C_WlanNetIP"].Value.ToString());
                            p1 = p1 + "\r\n" + AddPppoe(row.Cells["C_interface"].Value.ToString(), row.Cells["C_pppoeClint"].Value.ToString(), row.Cells["C_PPPOEUSER"].Value.ToString(), row.Cells["C_C_PPPOEPASS"].Value.ToString());
                            n1 = n1 + "\r\n" + AddNatBridge(row.Cells["C_pppoeClint"].Value.ToString());
                            m2 = m2 + "\r\n" + AddSingleMangle(row.Cells["C_WlanNetIP"].Value.ToString() + "/24", outinf);
                        }
                        Address = "\r\n" + "add address=" + TB_IPout.Text + " interface=" + outinf + " network=" + TB_OUTSub.Text + a1;
                    }
                    Pppoe = p1;
                    Nat = AddSingleNat(TB_OUTSub.Text + "/24") + n1;
                    Mangle = m2;
                    break;
                case Mode.PCC:
                    if (CB_ScrLB_insameServer.Checked)
                    {
                        int le1 = 1;
                        foreach (DataGridViewRow row in DGV_Main.Rows)
                        {
                            a1 = a1 + "\r\n" + AddAddress(row.Cells["C_interface"].Value.ToString(), row.Cells["C_WlanIPAddr"].Value.ToString(), row.Cells["C_WlanNetIP"].Value.ToString());
                            n1 = n1 + "\r\n" + AddNatPCC(row.Cells["C_interface"].Value.ToString());
                            ma = ma + "\r\n" + AddSingleManglePCCAcsept(row.Cells["C_WlanNetIP"].Value.ToString() + "/24", outinf);
                            mio = mio + "\r\n" + AddSingleManglePCCInputOut(row.Cells["C_interface"].Value.ToString(), row.Cells["C_interface"].Value.ToString() + "_Con");
                            m2 = m2 + "\r\n" + AddSingleManglePCC(outinf, row.Cells["C_interface"].Value.ToString() + "_Con");
                            r1 = r1 + "\r\n" + AddRoutsPCCMr(row.Cells["C_WlanIPGetway"].Value.ToString(), row.Cells["C_interface"].Value.ToString() + "_Con");
                            r2 = r2 + "\r\n" + AddRoutsPCCDis(row.Cells["C_WlanIPGetway"].Value.ToString(), (row.Index + 1).ToString());
                            if (le1 == 1)
                            {
                                routeLBISS = row.Cells["C_WlanIPGetway"].Value.ToString();
                            }
                            else
                            {
                                routeLBISS = routeLBISS + "," + row.Cells["C_WlanIPGetway"].Value.ToString();
                            }
                            le1++;
                        }
                        routsInSameServer = AddRoutsLBISS(routeLBISS);
                        Address = a1;
                    }
                    else
                    {
                        foreach (DataGridViewRow row in DGV_Main.Rows)
                        {
                            a1 = a1 + "\r\n" + AddAddress(row.Cells["C_interface"].Value.ToString(), row.Cells["C_WlanIPAddr"].Value.ToString(), row.Cells["C_WlanNetIP"].Value.ToString());
                            n1 = n1 + "\r\n" + AddNatPCC(row.Cells["C_interface"].Value.ToString());
                            ma = ma + "\r\n" + AddSingleManglePCCAcsept(row.Cells["C_WlanNetIP"].Value.ToString() + "/24", outinf);
                            mio = mio + "\r\n" + AddSingleManglePCCInputOut(row.Cells["C_interface"].Value.ToString(), row.Cells["C_interface"].Value.ToString() + "_Con");
                            m2 = m2 + "\r\n" + AddSingleManglePCC(outinf, row.Cells["C_interface"].Value.ToString() + "_Con");
                            r1 = r1 + "\r\n" + AddRoutsPCCMr(row.Cells["C_WlanIPGetway"].Value.ToString(), row.Cells["C_interface"].Value.ToString() + "_Con");
                            r2 = r2 + "\r\n" + AddRoutsPCCDis(row.Cells["C_WlanIPGetway"].Value.ToString(), (row.Index + 1).ToString());
                        }
                        Address = "\r\n" + "add address=" + TB_IPout.Text + " interface=" + outinf + " network=" + TB_OUTSub.Text + a1;
                    }
                    Nat = AddSingleNat(TB_OUTSub.Text + "/24") + n1;
                    Mangle=ma+mio;
                    Mangle1 = m2;
                    Routs = r1 + r2 + "\r\n" + routsInSameServer;
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
                    r = "/ip route" + r1 + "\r\n" + routsInSameServer;
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
                    r = "/ip route" + Routs + "\r\n" + routsInSameServer;
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
                        view["C_pppoeClint", e.RowIndex].Value = "pppoe-out" + Convert.ToString(e.RowIndex + 1);
                        view["C_PPPOEUSER", e.RowIndex].Value = "";
                        view["C_C_PPPOEPASS", e.RowIndex].Value = "";

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
                view.EndEdit();
                view.ReadOnly = false;
        }
        private int miniSize()
        {
            ///Solution1
            List<double> dsize = new List<double>();
            double m = 99999999;
            for (int i = 0; i < DGV_Main.Rows.Count; i++)
            {
                m = Math.Min(m, Convert.ToDouble(DGV_Main["C_DataSize", i].Value));
            }
            return Convert.ToInt32(m);
        }
        private int Nth()
        {
            List<int> dnth = new List<int>();
            foreach (DataGridViewRow row in DGV_Main.Rows)
            {
                dnth.Add(Convert.ToInt32(row.Cells["C_index"].Value));
            }
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
        private string AddRoutsBridgeLBISS(string in_Gateway)
        {
            return string.Concat(new string[]
                   {
                   "add dst-address=0.0.0.0/0 gateway="
                   ,in_Gateway
                   ," distance=1"
                   ," check-gateway=ping"
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
        private string AddRoutsLBISS(string in_Gateway)
        {
            return string.Concat(new string[]
                   {
                   "add dst-address=0.0.0.0/0 gateway="
                   ,in_Gateway
                   ," distance=1"
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
            if (MessageBox.Show("هل تريد الخروج من البرنامج","سيتم إغلاق البرنامج", MessageBoxButtons.YesNo) == DialogResult.Yes)
            {
                this.Close();
            }           
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
            string interval =timespane(DTP_LG_interval.Value);
            if (String.IsNullOrEmpty(interval))
            {
                MessageBox.Show("لا يمكن ان يكون الوقت فارغا");
                DTP_LG_interval.Focus();
            }
            else
            {
                SaveFileDialog sfd = new SaveFileDialog();
                sfd.DefaultExt = "*.txt";
                sfd.FileName = "duplicate-mac.txt";
                sfd.Title = "إحفظ ملف الماكات المتكررة لأجهزة LG";
                if (sfd.ShowDialog() == DialogResult.OK)
                {
                    string s = Properties.Resources.duplicate_mac;
                    String[] rows = Regex.Split(s, "\r\n");
                    StreamWriter writer = new StreamWriter(sfd.OpenFile());
                    for (int i = 0; i < rows.Length; i++)
                    {
                        writer.WriteLine(rows[i]);
                    }
                    writer.Dispose();
                    writer.Close();
                    TB_Result.Text = _cms.SecSLGDuobleMac(interval);
                }
            }
            
        }
        private string timespane(DateTime time)
        {
            String[] res = new string[3];
            if (time.Hour != 0)
            {
                res[0] = time.Hour + "h";
            }
            else
            {
                res[0] = "";
            }
            if (time.Minute != 0)
            {
                res[1] = time.Minute + "m";
            }
            else
            {
                res[1] = "";
            }
            if (time.Second != 0)
            {
                res[2] = time.Second + "s";
            }
            else
            {
                res[2] = "";
            }
            return string.Concat(res);
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
        private bool vaildataMikS(Mode mod)
        {
            bool res = false;
            switch (mod)
            {
                case Mode.MikSMBackUp:
                    res = false;
                    break;
                case Mode.MikSMBackupToEmail:
                    res = false;
                    break;
                case Mode.MikSMBackUpRSC:
                    if (MikSBRSC_ScaName.Text.Length > 0)
                    {
                        CheckText(MikSBRSC_ScaName);
                        if (MikSBRSC_FileName.Text.Length > 0)
                        {
                            CheckText(MikSBRSC_FileName);
                            res = true;
                        }
                        else
                        {
                            MessageBox.Show("لا يمكن ان يكون إسم الملف فارغا");
                            MikSBRSC_FileName.Focus();
                        }
                    }
                    else
                    {
                        MessageBox.Show("لا يمكن ان يكون إسم المهمة فارغا");
                        MikSBRSC_ScaName.Focus();
                    }
                    break;
                case Mode.MikSMBackupRSCToEmail:
                    res = false;
                    break;
                case Mode.MikSMLogBackup:
                    res = false;
                    break;
                case Mode.MikSMLogBackupToEmail:
                    res = false;
                    break;
                case Mode.MikSQDAutoSpeed:
                    res = false;
                    break;
                case Mode.MikSQTAutoSpeed:
                    res = false;
                    break;
                case Mode.MikSUMBackUp:
                    res = false;
                    break;
                case Mode.MikSUMBackupToEmail:
                    res = false;
                    break;
            }
            return res;
        }
        private void BT_MikSBRSC_Click(object sender, EventArgs e)
        {
            if (vaildataMikS(Mode.MikSMBackUpRSC))
            {
                DateTime _date = MikSBRSC_StartDate.Value;
                DateTime date = new DateTime(_date.Year, _date.Month, _date.Day);
                TB_Result.Text = _cms.MikSMBackUpRSC(MikSBRSC_ScaName.Text, MikSBRSC_FileName.Text, date.ToString("MMM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture), MikSBRSC_StartTime.Value.ToString("HH:mm:ss"), MikSBRSC_interval.Value.ToString() + "d", MikSBRSC_CanInterval.Checked,CB_MikSBRSC_overwrite.Checked);
            }   
        }

        private void MikSBRSC_ScaName_Validated(object sender, EventArgs e)
        {
            CheckText(sender);
        }

        private void MikSBRSC_FileName_Validated(object sender, EventArgs e)
        {
            CheckText(sender);
        }
        private void CheckText(object sender)
        {
            TextBox tb = sender as TextBox;
                string s = tb.Text;
                s.Replace(" ", "");
                s.Replace(".", "");
                s.Replace(",", "");
                s.Replace(">", "");
                s.Replace("<", "");
                s.Replace("}", "");
                s.Replace("{", "");
                s.Replace("]", "");
                s.Replace("[", "");
                s.Replace("/", "");
                s.Replace(@"\", "");
                tb.Text = s;
        }

        private void BT_MikSBlockPornSites_Click(object sender, EventArgs e)
        {
            TB_Result.Text = "/ip dns set servers=1.1.1.3,1.0.0.3";
        }

        private void BT_MikSB_Click(object sender, EventArgs e)
        {

            DateTime _date = MikSB_StartDate.Value;
            DateTime date = new DateTime(_date.Year, _date.Month, _date.Day);
            TB_Result.Text = _cms.MikSMBackUp(MikSB_ScName.Text,MikSB_fileName.Text, date.ToString("MMM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture), MikSB_StartTime.Value.ToString("HH:mm:ss"), MikSB_interval.Value.ToString() + "d", CB_MikSB_Caninterval.Checked, CB_MikSB_overwrite.Checked,CB_MikSB_DontEncrypt.Checked);
        }

        private void BT_MikSUMBackUp_Click(object sender, EventArgs e)
        {
            DateTime _date = MikSUMB_StartDate.Value;
            DateTime date = new DateTime(_date.Year, _date.Month, _date.Day);
            TB_Result.Text = _cms.MikSUMBackUp(MikSUMB_Schname.Text, MikSUMB_Filename.Text, date.ToString("MMM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture), MikSUMB_StartTime.Value.ToString("HH:mm:ss"), MikSUMB_Interval.Value.ToString() + "d", MikSUMB_CanInterval.Checked, MikSUMB_overwrite.Checked);
        }

        private void Lin_Face_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel link = sender as LinkLabel;
            System.Diagnostics.Process.Start(link.Text);
        }

        private void Lin_Tik_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel link = sender as LinkLabel;
            System.Diagnostics.Process.Start(link.Text);
        }

        private void Lin_Twi_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel link = sender as LinkLabel;
            System.Diagnostics.Process.Start(link.Text);
        }

        private void Lin_You_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            LinkLabel link = sender as LinkLabel;
            System.Diagnostics.Process.Start(link.Text);
        }

        private void BT_MikSRUWS_Click(object sender, EventArgs e)
        {
            DateTime _date = MikSUMB_StartDate.Value;
            DateTime date = new DateTime(_date.Year, _date.Month, _date.Day);
            TB_Result.Text = _cms.MikSUMBackUp(MikSUMB_Schname.Text, MikSUMB_Filename.Text, date.ToString("MMM/dd/yyyy", System.Globalization.CultureInfo.InvariantCulture), MikSUMB_StartTime.Value.ToString("HH:mm:ss"), MikSUMB_Interval.Value.ToString() + "d", MikSUMB_CanInterval.Checked, MikSUMB_overwrite.Checked);
        }

        private void BT_SecSNetCut_Click(object sender, EventArgs e)
        {
            TB_Result.Text = _cms.SecSNetCut();
        }

        private void BT_SecSDFC_Click(object sender, EventArgs e)
        {

        }

        private void PL_Up_MouseDown(object sender, MouseEventArgs e)
        {
            
            if (e.Button == MouseButtons.Left)
            {
                canMove = true;
                cpoint = new Point((MousePosition.X-this.Location.X),(MousePosition.Y-this.Location.Y));
            }
        }

        private void PL_Up_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button == MouseButtons.Left)
            {
                canMove = false;
            }
        }

        private void PL_Up_MouseMove(object sender, MouseEventArgs e)
        {
            if (canMove)
            {
                int x;
                int y;
                x = MousePosition.X - cpoint.X;
                y = MousePosition.Y - cpoint.Y;
                //if (this.Location.X >= 0 && this.Location.X < (Screen.PrimaryScreen.WorkingArea.X) - 100)
                //{
                //    x = MousePosition.X - cpoint.X;
                //}
                //else
                //{
                //    x = this.Location.X;
                //}
                //if (this.Location.Y >= 0 && this.Location.Y < (Screen.PrimaryScreen.WorkingArea.Y) - 100)
                //{
                //    y = MousePosition.Y - cpoint.Y;
                //}
                //else
                //{
                //    y = this.Location.Y;
                //}
                if (y <= 0)
                {
                    y = this.Location.Y;
                }
                if (x <= 0)
                {
                    x = this.Location.X;
                }
                this.Location = new Point(x, y);
            }
        }

        private void FrmMain_Move(object sender, EventArgs e)
        {
            //int x = 0;
            //int y = 0;
            //if (this.Location.X < 0)
            //{
            //    this.Location.X = 0;
            //}
        }

        private void FrmMain_MouseMove(object sender, MouseEventArgs e)
        {
            //label16.Text = "M=X: " + MousePosition.X + " X: " + MousePosition.Y;
        }

        private void TP_SecScr_MouseMove(object sender, MouseEventArgs e)
        {
            //label16.Text = "M=X: " + MousePosition.X + " X: " + MousePosition.Y;
        }

        private void VLAN_Num_ValueChanged(object sender, EventArgs e)
        {
            CheckIP_Rate(sender);
        }
        private void CheckIP_Rate(object sender)
        {
            NumericUpDown um = sender as NumericUpDown;
            int vlannum = Convert.ToInt32(um.Value);
            DGV_VLANRate.Rows.Clear();
            if (vlannum <= 254)
            {
                string ir = "IP_Rate 1";
                VLIPRate.Items.Add(ir);
                DGV_VLANRate.Rows.Add(("10"), ir, "0", "0", "/24", vlannum, ir, 1);
            }
            else
            {
                int s = 1;
                int io = vlannum;
                while (io > 0)
                {
                    string ir = "IP_Rate " + s;
                    VLIPRate.Items.Add(ir);
                    if (io >= 254)
                    {
                        DGV_VLANRate.Rows.Add((s + "0"), ir, "0", "0", "/24", 254, ir, s);
                    }
                    else
                    {
                        DGV_VLANRate.Rows.Add((s + "0"), ir, "0", "0", "/24", io, ir, s);
                    }
                    s++;
                    io = io - 254;
                }
            }
        }
        private void BT_MTAddVlan_Click(object sender, EventArgs e)
        {
            int VLNUM = Convert.ToInt32(VLAN_Num.Value);
            int VLID = Convert.ToInt32(NUD_VlanID.Value);
            if ((VLNUM + VLID) <= 4095)
            {
                string intface = TB_BridgeInterFace.Text;
                string hsProfile = TB_HSSprofile.Text;
                string vlanNa = TB_VlanName.Text;
                string res = "";
                foreach (DataGridViewRow row in DGV_VLANRate.Rows)
                {
                    int ir = Convert.ToInt32(row.Cells["IPNUM"].Value);
                    for (int i = 1; i <= ir; i++)
                    {
                        string ip = row.Cells["C_IP1"].Value.ToString() + "." + i + "."+ row.Cells["C_IP3"].Value.ToString();
                        string netm = row.Cells["C_Mask"].Value.ToString();
                        string vlann;
                        if (i >= 100)
                        {
                            vlann = vlanNa + i;
                        }
                        else
                        {
                            if (i < 10)
                            {
                                vlann = vlanNa +"00"+ i;
                            }
                            else
                            {
                                vlann = vlanNa + "0" + i;
                            }
                        }
                        if (CB_MUT_HSserverForVL.Checked)
                        {
                            string hsserver = "HS_" + vlann;
                            string poolna = "pool_" + vlann;
                            string dhpserver = "dhp_" + vlann;
                            res = res + "\r\n" + _cms.MultiVlan(intface, VLID.ToString(), vlann, ip, netm, hsserver, hsProfile, poolna, dhpserver);
                        }
                        else
                        {
                            res = res + "\r\n" + _cms.MultiVlanNoHS(intface, VLID.ToString(), vlann, ip, netm);
                        }                      
                        VLID++;
                    }
                }
                TB_Result.Text = res;
            }
            else
            {
                MessageBox.Show("يجب ان يكون VLanID اقل من 4095");
                NUD_VlanID.Focus();
            }
           
        }

        private void CB_MultiUSECVLANID_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            NUD_VlanID.ReadOnly = !cb.Checked;
        }

        private void CB_MUT_EditDGVlan_CheckedChanged(object sender, EventArgs e)
        {
            CheckBox cb = sender as CheckBox;
            DGV_VLANRate.ReadOnly = !cb.Checked;
        }

        private void BT_ChekIpRate_Click(object sender, EventArgs e)
        {
            CheckIP_Rate(VLAN_Num);
        }

        private void PictureBox1_DoubleClick(object sender, EventArgs e)
        {
           // Frm_Login _Login = new Frm_Login();
           // _Login.Show();
        }

        private void PictureBox1_Click(object sender, EventArgs e)
        {

        }

        private void BT_WordWrap_Click(object sender, EventArgs e)
        {
            TB_Result.WordWrap = !TB_Result.WordWrap;
        }

        private void BT_SelectAll_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(TB_Result.Text);
        }

        private void BT_SaveScripts_Click(object sender, EventArgs e)
        {
            if (!String.IsNullOrEmpty(TB_Result.Text))
            {
                try
                {
                    SaveFileDialog sfd = new SaveFileDialog();
                    sfd.Filter = "ملف نصي| *.txt | MikrotikScript | *.rsc";
                    sfd.FileName = "ProCoderSecript";
                    sfd.Title = "إحفظ الإسكربت الى ملف";
                    if (sfd.ShowDialog() == DialogResult.OK)
                    {
                        string s = TB_Result.Text;
                        String[] rows = Regex.Split(s, "\r\n");
                        StreamWriter writer = new StreamWriter(sfd.OpenFile());
                        for (int i = 0; i < rows.Length; i++)
                        {
                            writer.WriteLine(rows[i]);
                        }
                        writer.Dispose();
                        writer.Close();
                        MessageBox.Show("تم حفظ الملف بنجاح");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
                
            }
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
       , MikSBlockPornSites
       , SecSFreedom
       , SecSFreedom1
       , SecSNetCut
       , SecSRemoveHost
       , SecSLGDuobleMac
       , SecSClosePorts
       , SecSDogWatch
    }
}
