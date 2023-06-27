using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Windows.Forms;
using System.IO;
using System.Text.RegularExpressions;

namespace LBMS_Pro
{
    public partial class Frm_Automation : UserControl
    {
        string fileEx = ".txt";
        public Frm_Automation()
        {
            InitializeComponent();
        }

        private void BT_GetFileSource_Click(object sender, EventArgs e)
        {
            OpenFileDialog open = new OpenFileDialog();
            open.Filter = "txt|*.txt|cfg|*.cfg|rsc|*.rsc|all files|*.*";
            if (open.ShowDialog() == DialogResult.OK)
            {
                TB_FileSursce.Text = open.FileName;
            }
        }

        private void BT_NSCFDir_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            if (fb.ShowDialog() == DialogResult.OK)
            {
                TB_NSFileDir.Text = fb.SelectedPath;
                Properties.Settings.Default.Save();
            }
        }

        private void BT_AddSAutoMat_Click(object sender, EventArgs e)
        {
            if (checkSAmation())
            {
                int num = Convert.ToInt32(UN_SAutoNUM.Value);
                int Snum = Convert.ToInt32(UN_SAutoFind.Value);
                int Rnum = Convert.ToInt32(NU_SAutoR.Value);
                AutoM_DGV.Rows.Add
                    (num, CB_SAutoUltimte.Checked
                    , CB_SAutoType.SelectedItem.ToString(), CB_SAutoDataType.SelectedItem.ToString()
                    , TB_SAutoFind.Text, Snum, CB_SAutoRMethod.SelectedItem.ToString()
                    , TB_SAutoR1.Text, CB_SAutoRCastum.Checked, Rnum, TB_SAutoR2.Text
                    );
            }
        }
        bool checkSAmation()
        {
            bool res = false;
            if (CB_SAutoType.SelectedItem != null)
            {
                if (CB_SAutoDataType.SelectedItem != null)
                {
                    if (CB_SAutoRMethod.SelectedItem != null)
                    {
                        return res = true;
                    }
                }
            }
            return res;
        }
        private void Frm_Automation_Load(object sender, EventArgs e)
        {
            CB_SAutoType.SelectedIndex = 0;
            CB_SAutoDataType.SelectedIndex = 0;
            CB_SAutoRMethod.SelectedIndex = 0;
            CB_FileExtation.SelectedIndex = 0;
        }

        private void BT_StartNSConfig_Click(object sender, EventArgs e)
        {
            StartAutoMation();
        }

        private void StartAutoMation()
        {
            if (checkSingleAM() &&  checkSourceFile() && nSConfigInfoFile())
            {
                try
                {
                    string dir = TB_FileSursce.Text;
                    if (File.Exists(dir))
                    {
                        string NSConfigFile = getNSConfigFile(dir);
                        if (NSConfigFile != "")
                        {
                            int AMFile = Convert.ToInt32(UN_FileNum.Value);
                            string fs = NSConfigFile;
                            string exprtedfile = "";
                            for (int i = 1; i <= AMFile; i++)
                            {
                                String[] frows = Regex.Split(fs, "\r\n");
                                foreach (DataGridViewRow row in AutoM_DGV.Rows)
                                {
                                    try
                                    {
                                        if (row == null) return;
                                        int SAMRun = Convert.ToInt32(row.Cells[0].Value);
                                        bool SArun = Convert.ToBoolean(row.Cells[1].Value);
                                        string SAType = row.Cells[2].Value.ToString();
                                        string SADataType = row.Cells[3].Value.ToString();
                                        string SATFind = row.Cells[4].Value.ToString();
                                        int SANUFind = Convert.ToInt32(row.Cells[5].Value);
                                        string SARepMeth = row.Cells[6].Value.ToString();
                                        string SATRep1 = row.Cells[7].Value.ToString();
                                        bool SACRnum = Convert.ToBoolean(row.Cells[8].Value);
                                        int SANURep = Convert.ToInt32(row.Cells[9].Value);
                                        string SATRep2 = row.Cells[10].Value.ToString();
                                        string replaced = "";
                                        int rep;
                                        switch (SARepMeth)
                                        {
                                            case "نص ثابت":
                                                replaced = SATRep1 + SATRep2;
                                                break;
                                            case "رقم متغير":
                                                if (SACRnum)
                                                {
                                                    rep = (SANURep - 1) + i;
                                                }
                                                else
                                                {
                                                    rep = i;
                                                }
                                                replaced = rep.ToString();
                                                break;
                                            case "نص + رقم ثابت":
                                                if (SACRnum)
                                                {
                                                    rep = (SANURep - 1) + i;
                                                }
                                                else
                                                {
                                                    rep = i;
                                                }
                                                replaced = SATRep1 + rep + SATRep2;
                                                break;
                                            case "نص + رقم متغير":
                                                if (SACRnum)
                                                {
                                                    rep = (SANURep - 1) + i;
                                                }
                                                else
                                                {
                                                    rep = i;
                                                }
                                                replaced = SATRep1 + rep + SATRep2;
                                                break;
                                        }
                                        //switch (SAType)
                                        //{
                                        //    case "":
                                        //        break;
                                        //}
                                        if (SArun)
                                        {
                                            for (int f = 0; f < frows.Length; f++)
                                            {
                                                if (frows[f].Contains(SATFind))
                                                {
                                                    frows[f] = frows[f].Replace(SATFind, replaced);
                                                }
                                            }
                                        }
                                        else
                                        {
                                            int round =0;
                                            for (int f = 0; f < frows.Length; f++)
                                            {
                                                if (round >= SAMRun)
                                                {
                                                    break;
                                                }
                                                if (frows[f].Contains(SATFind))
                                                {
                                                    frows[f] = frows[f].Replace(SATFind, replaced);
                                                    round++;
                                                }
                                            }
                                        }
                                    }
                                    catch (Exception ex)
                                    {
                                        string Err = string.Concat(new string[]
                                           { "رسالة الخطاء :",ex.Message,"\r\n"
                                                , "المصدر :" ,ex.Source,"\r\n"
                                                , "الهدف :" ,ex.StackTrace
                                           }); ;
                                        MessageBox.Show(Err, "خطاء في البيانات المدخلة");
                                    }
                                }
                                String[] res = new string[frows.Length];
                                for (int r = 0; r < frows.Length; r++)
                                {
                                    res[r] = frows[r] + "\r\n";
                                }
                                if (CB_ExportToOneFile.Checked)
                                {
                                    string finalFile = string.Concat(res);
                                    exprtedfile = exprtedfile + "\r\n" + finalFile;
                                }
                                else
                                {
                                    exprtedfile = "";
                                    exprtedfile = string.Concat(res);
                                    string fnam = TB_StartFileName.Text + "_" + i.ToString() + CB_FileExtation.SelectedItem;
                                    string fulldir = TB_NSFileDir.Text + "\\" + fnam;
                                    if (File.Exists(fulldir))
                                    {
                                        if (MessageBox.Show("هذا الملف موجود", "هل تريد إستبدال هذا الملف", MessageBoxButtons.YesNo) == DialogResult.Yes)
                                        {
                                            File.WriteAllText(fulldir, exprtedfile);
                                        }
                                    }
                                    else
                                    {
                                        File.WriteAllText(fulldir, exprtedfile);
                                    }
                                }
                               
                            }
                            if (CB_ExportToOneFile.Checked)
                            {
                                string fnam = TB_StartFileName.Text + CB_FileExtation.SelectedItem;
                                string fulldir = TB_NSFileDir.Text + "\\" + fnam;
                                if (File.Exists(fulldir))
                                {
                                    if (MessageBox.Show("هذا الملف موجود", "هل تريد إستبدال هذا الملف", MessageBoxButtons.YesNo)==DialogResult.Yes)
                                    {
                                        File.WriteAllText(fulldir, exprtedfile);
                                    }
                                }
                                else
                                {
                                    File.WriteAllText(fulldir, exprtedfile);
                                }
                            }
                            MessageBox.Show("تم إنشاء ملفات الإعدادات بنجاح");
                        }
                    }
                    else
                    {
                        MessageBox.Show("تعذر الحصول على ملف الإعداد");
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.ToString());
                }
            }
        }

        bool nSConfigInfoFile()
        {
            bool res = false;
            try
            {
                string dir = TB_NSFileDir.Text;
                if (!Directory.Exists(dir))
                {
                    Directory.CreateDirectory(dir);
                }
                return true;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
                TB_NSFileDir.Focus();
                return res;
            }
        }
        bool checkSourceFile()
        {
            if (TB_FileSursce.Text.Length > 0)
            {
                if (File.Exists(TB_FileSursce.Text))
                {
                    return true;
                }
                else
                {
                    MessageBox.Show("لم يتم العثور على ملف المصدر");
                    TB_FileSursce.Focus();
                    return false;
                }
            }
            else
            {
                MessageBox.Show("يجب تحديد ملف المصدر");
                TB_FileSursce.Focus();
                return false;
            }
        }
        bool checkSingleAM()
        {
            if (AutoM_DGV.Rows.Count>0)
            {
                return true;
            }
            else
            {
                MessageBox.Show("يجب إضافة مهام");
                return false;
            }
        }
        private string getNSConfigFile(string dir)
        {
            string res = "";
            try
            {
                res = File.ReadAllText(dir);
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.ToString());
            }
            return res;
        }
    }
}
