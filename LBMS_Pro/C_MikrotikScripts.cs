using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.IO;
using System.Reflection;

namespace LBMS_Pro
{
    class C_MikrotikScripts
    {
        #region MikS
        public string QDAutoSpeed(string netspeed)
        {
            return string.Concat(new string[]
                    {
                   "/system script"
                   ,"\r\n"
                   ,@"add dont-require-permissions=no name=ProAutoSpeed owner=admin source=""{\r\"
                   ,"\r\n"
                   ,@"\n:local linkSpeed ",netspeed,@" ;\r\"
                   ,"\r\n"
                   ,@"\n\r\"
                   ,"\r\n"
                   ,@"\n:local activeCount [/ip hotspot active print count-only] ;\r\"
                   ,"\r\n"
                   ,@"\n\r\"
                   ,"\r\n"
                   ,@"\n:local DownSpeedPerOne ((\$linkSpeed * 1200) / \$activeCount );\r\"
                   ,"\r\n"
                   ,@"\n\r\"
                   ,"\r\n"
                   ,@"\n:local UpSpeedPerOne (\$DownSpeedPerOne /4) ;\r\"
                   ,"\r\n"
                   ,@"\r\n"
                   ,"\r\n"
                   ,@"\n/queue simple remove [find] ;\r\"
                   ,"\r\n"
                   ,@"\r\n\"
                   ,@"\n:local addr;\r\"
                   ,"\r\n"
                   ,@"\n:local uname;\r\"
                   ,"\r\n"
                   ,@"\n:foreach i in=[/ip hotspot active find] do={\r\"
                   ,"\r\n"
                   ,@"\n:set addr [/ip hotspot active get \$i address];\r\"
                   ,"\r\n"
                   ,@"\n:set uname [/ip hotspot active get \$i user];\r\"
                   ,"\r\n"
                   ,@"\n/queue simple add name=\$uname target=\$addr  max-limit=\""\$UpSpeedPerOn\"
                   ,"\r\n"
                   ,@"e\\4B/\$DownSpeedPerOne\\4B\""  ;\r\"
                   ,"\r\n"
                   ,@"\n}}"""
                   ,"\r\n"
                   ,@"/ip hotspot user profile set [ find default=yes ] on-login=""/system script run ProAutoSpeed ; "" on-logout="" / system script run ProAutoSpeed;"" "
                   ,"\r\n"
                    }); ;
        }
        public string _QTAutoSpeed(string netspeed ,string ip,bool V6)
        {
            return string.Concat(new string[]
                    {
                   "/ip firewall mangle"
                   ,"\r\n"
                   ,"add action=mark-packet chain=prerouting dst-address=!"
                   ,ip
                   ," new-packet-mark=Pro-down passthrough=no"
                   ,"\r\n"
                   ,"/queue type"
                   ,"\r\n"
                   ,"add kind=pcq name=Pro-down pcq-burst-time=30s pcq-classifier=dst-address"
                   ,"\r\n"
                   ,"add kind=pcq name=Pro-up pcq-burst-time=30s pcq-classifier=src-address"
                   ,"\r\n"
                   ,"/system script"
                   ,"\r\n"
                   ,@"add name=ProLimitSpeed owner=admin policy=ftp,reboot,read,write,policy,test,password,sniff,sensitive source=""{\r\"
                   ,"\r\n"
                   ,@"\n:local i;\"
                   ,"\r\n"
                   ,@"\n\r\"
                   ,@"\n:local c ",netspeed,@";\r\"
                   ,@"\n\r\"
                   ,@"\n\r\"
                   ,@"\n\r\"
                   ,@"\n\r\"
                   ,@"\n\r\"
                   ,@"\n:local linkSpeed ",netspeed,@" ;\r\"
                   ,"\r\n"
                   ,@"\n\r\"
                   ,"\r\n"
                   ,@"\n:local activeCount [/ip hotspot active print count-only] ;\r\"
                   ,"\r\n"
                   ,@"\n\r\"
                   ,"\r\n"
                   ,@"\n:local DownSpeedPerOne ((\$linkSpeed * 1200) / \$activeCount );\r\"
                   ,"\r\n"
                   ,@"\n\r\"
                   ,"\r\n"
                   ,@"\n:local UpSpeedPerOne (\$DownSpeedPerOne /4) ;\r\"
                   ,"\r\n"
                   ,@"\r\n"
                   ,"\r\n"
                   ,@"\n/queue simple remove [find] ;\r\"
                   ,"\r\n"
                   ,@"\r\n\"
                   ,@"\n:local addr;\r\"
                   ,"\r\n"
                   ,@"\n:local uname;\r\"
                   ,"\r\n"
                   ,@"\n:foreach i in=[/ip hotspot active find] do={\r\"
                   ,"\r\n"
                   ,@"\n:set addr [/ip hotspot active get \$i address];\r\"
                   ,"\r\n"
                   ,@"\n:set uname [/ip hotspot active get \$i user];\r\"
                   ,"\r\n"
                   ,@"\n/queue simple add name=\$uname target=\$addr  max-limit=\""\$UpSpeedPerOn\"
                   ,"\r\n"
                   ,@"e\\4B/\$DownSpeedPerOne\\4B\""  ;\r\"
                   ,"\r\n"
                   ,@"\n}}"""
                   ,"\r\n"
                   ,@"/ip hotspot user profile set [ find default=yes ] on-login=""/system script run ProAutoSpeed ; "" on-logout="" / system script run ProAutoSpeed;"" "
                   ,"\r\n"
                    }); ;
        }
        public string QTAutoSpeed(string netspeed ,string ip, bool VR)
        {
            string Script;
            string s  = Properties.Resources.MikSQTAS;
            String[] rows = Regex.Split(s, "\r\n");
            for (int i = 0; i < rows.Length; i++)
            {
                if (rows[i].Contains("10.0.0.1"))
                {
                    rows[i] = rows[i].Replace("10.0.0.1", ip);
                    break;
                }
            }
            for (int i = 0; i < rows.Length; i++)
            {
                if (rows[i].Contains("1212"))
                {
                    rows[i] = rows[i].Replace("1212", netspeed);
                    break;
                }
            }
            String[] res=new string[rows.Length];
            for (int i = 0; i < rows.Length; i++)
            {
                res[i] = rows[i] + "\r\n";
            }
            Script= string.Concat(res);

            string V6 = string.Concat(new string[]
            {
                "/queue tree"
                ,"\r\n"
                ,"add name=Pro-down packet-mark=Pro-down parent=global queue=Pro-down"
                ,"\r\n"
                ,"add name=Pro-up packet-mark=Pro-up parent=global queue=Pro-up"
            });
            string V5 = string.Concat(new string[]
           {
                "/queue tree"
                ,"\r\n"
                ,"add name=Pro-down packet-mark=Pro-down parent=global-out queue=Pro-down"
                ,"\r\n"
                ,"add name=Pro-up packet-mark=Pro-up parent=global-out queue=Pro-up"
           });
            if (VR == true)
            {
                Script=Script+"\r\n" + V5;
            }
            else
            {
                Script = Script + "\r\n" + V6;
            }
            return Script;
        }
        public string MikSMBackUpRSC(string namescheduler,string nfile,string startdate,string starttime,string interval,bool can_interval,bool overwrite)
        {
            string Script;
            string s;
            if (overwrite)
            {
                s = Properties.Resources.MikSBrsc;
                String[] rows = Regex.Split(s, "\r\n");
                if (can_interval)
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        if (rows[i].Contains("1d"))
                        {
                            rows[i] = rows[i].Replace("1d", interval);
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        if (rows[i].Contains(" interval=1d"))
                        {
                            rows[i] = rows[i].Replace(" interval=1d", "");
                            break;
                        }
                    }
                }
                for (int i = 0; i < rows.Length; i++)
                {
                    if (rows[i].Contains("SNA"))
                    {
                        rows[i] = rows[i].Replace("SNA", namescheduler);
                        break;
                    }
                }
                for (int i = 0; i < rows.Length; i++)
                {
                    if (rows[i].Contains("FNA"))
                    {
                        rows[i] = rows[i].Replace("FNA", nfile);
                        break;
                    }
                }
                for (int i = 0; i < rows.Length; i++)
                {
                    if (rows[i].Contains("SSD"))
                    {
                        rows[i] = rows[i].Replace("SSD", startdate);
                        break;
                    }
                }
                for (int i = 0; i < rows.Length; i++)
                {
                    if (rows[i].Contains("SST"))
                    {
                        rows[i] = rows[i].Replace("SST", starttime);
                        break;
                    }
                }
                String[] res = new string[rows.Length];
                for (int i = 0; i < rows.Length; i++)
                {
                    res[i] = rows[i] + "\r\n";
                }
                Script = string.Concat(res);
            }
            else
            {
                string inter;
                if (can_interval)
                {
                    inter = " interval=" + interval;
                }
                else
                {
                    inter = "";
                }
                Script = string.Concat(new string[]
                   {
                   "/system scheduler"
                   ,"\r\n"
                   ,@"add"
                   ,inter
                   ," name="
                   ,namescheduler
                   ,@" on-event=\"
                   ,"\r\n"
                   ,@"""delay 5;\r\"
                   ,"\r\n"
                   ,@"\nexport file="
                   ,nfile,@".rsc\r\"
                   ,"\r\n"
                   ,@"\n"" policy=\"
                   ,"\r\n"
                   ,@"ftp,reboot,read,write,policy,test,password,sniff,sensitive,romon \"
                   ,"\r\n"
                   ,@"start-date="
                   ,startdate
                   ," start-time="
                   ,starttime
                   });
            }
            return Script;
        }
        public string MikSMBackUp(string namescheduler,string nfile,string startdate,string starttime,string interval,bool can_interval,bool overwrite,bool dontencrypt)
        {
            string Script;
            string s;
            if (overwrite)
            {
                s = Properties.Resources.MikSMBU;
                String[] rows = Regex.Split(s, "\r\n");
                if (can_interval)
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        if (rows[i].Contains("1d"))
                        {
                            rows[i] = rows[i].Replace("1d", interval);
                            break;
                        }
                    }
                }
                else
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        if (rows[i].Contains(" interval=1d"))
                        {
                            rows[i] = rows[i].Replace(" interval=1d", "");
                            break;
                        }
                    }
                }
                if (dontencrypt)
                {
                    for (int i = 0; i < rows.Length; i++)
                    {
                        if (rows[i].Contains("dont-encrypt=no"))
                        {
                            rows[i] = rows[i].Replace("dont-encrypt=no", "dont-encrypt=yes");
                            break;
                        }
                    }
                }
                for (int i = 0; i < rows.Length; i++)
                {
                    if (rows[i].Contains("SNA"))
                    {
                        rows[i] = rows[i].Replace("SNA", namescheduler);
                        break;
                    }
                }
                for (int i = 0; i < rows.Length; i++)
                {
                    if (rows[i].Contains("FNA"))
                    {
                        rows[i] = rows[i].Replace("FNA", nfile);
                        break;
                    }
                }
                for (int i = 0; i < rows.Length; i++)
                {
                    if (rows[i].Contains("SSD"))
                    {
                        rows[i] = rows[i].Replace("SSD", startdate);
                        break;
                    }
                }
                for (int i = 0; i < rows.Length; i++)
                {
                    if (rows[i].Contains("SST"))
                    {
                        rows[i] = rows[i].Replace("SST", starttime);
                        break;
                    }
                }
                String[] res = new string[rows.Length];
                for (int i = 0; i < rows.Length; i++)
                {
                    res[i] = rows[i] + "\r\n";
                }
                Script = string.Concat(res);
            }
            else
            {
                string inter;
                string delay = @"""delay 5;\r\";
                string encrypt;
                if (dontencrypt)
                {
                    encrypt = "yes";
                }
                else
                {
                    encrypt = "no";
                }
                if (can_interval)
                {
                    inter = " interval=" + interval;
                }
                else
                {
                    inter = "";
                }
                Script= string.Concat(new string[]
                       {
                       "/system scheduler"
                       ,"\r\n"
                       ,@"add"
                       ,inter
                       ," name="
                       ,namescheduler
                       ,@" on-event=\"
                       ,"\r\n"
                       ,delay
                       ,"\r\n"
                       ,@"\nsystem backup save name="
                       ,nfile," dont-encrypt=",encrypt,@"\r\"
                       ,"\r\n"
                       ,@"\n"" policy=\"
                       ,"\r\n"
                       ,@"ftp,reboot,read,write,policy,test,password,sniff,sensitive,romon \"
                       ,"\r\n"
                       ,@"start-date="
                       ,startdate
                       ," start-time="
                       ,starttime
                       });
            }
            return Script;
            //string inter;
            //string delay = @"""delay 5;\r\";
            //string _name;
            //string encrypt = "no";
            //if (dontencrypt)
            //{
            //    encrypt = "yes";
            //}
            //else
            //{
            //    encrypt = "no";
            //}
            //if (can_interval)
            //{
            //    inter = " interval=" + interval;
            //}
            //else
            //{
            //    inter = "";
            //    //return string.Concat(new string[]
            //    //    {
            //    //   "/system scheduler"
            //    //   ,"\r\n"
            //    //   ,"add name="
            //    //   ,namescheduler
            //    //   ,@" on-event=\"
            //    //   ,"\r\n"
            //    //   ,@"""delay 5;\r\"
            //    //   ,"\r\n"
            //    //   ,@"\nexport file="
            //    //   ,nfile,@".rsc\r\"
            //    //   ,"\r\n"
            //    //   ,@"\n"" policy=\"
            //    //   ,"\r\n"
            //    //   ,@"ftp,reboot,read,write,policy,test,password,sniff,sensitive,romon \"
            //    //   ,"\r\n"
            //    //   ,@"start-date="
            //    //   ,startdate
            //    //   ," start-time="
            //    //   ,starttime
            //    //    });
            //}
            //if (overwrite)
            //{
            //    delay = string.Concat(new string[]
            //   {
            //         @""":local fname \""",nfile,@"\",@""";\r\"
            //        ,"\r\n"
            //        ,@"\n:local date [/system clock get date];\r\"
            //        ,"\r\n"
            //        ,@"\n:local months {\""jan\""=\""01\"";",@"\""feb\""=\""02\"";\""mar\""=\""03\"";\""apr\""=\""04\"
            //        ,@"\"";\""may\""=\""05\"";\""jun\""=\""06\"";\""jul\""=\""07\"";\""aug\""=\""08\"";\""sep\""=\""0\"
            //        ,"\r\n"
            //        ,@"9\"";\""oct\""=10;\""nov\""=11;\""dec\""=12};\r\"
            //        ,"\r\n"
            //        ,@"\n:local day [:tonum [:pick \$date 4 6]];\r\"
            //        ,"\r\n"
            //        ,@"\n:local year [:tonum [:pick \$date 7 11]];\r\"
            //        ,"\r\n"
            //        ,@"\n:local month [:pick \$date 0 3];\r\"
            //        ,"\r\n"
            //        ,@"\n:local mm (:\$months->\$month);\r\"
            //        ,"\r\n"
            //        ,@"\n:local newdate \""\$fname-\$year-\$mm-\$day\"";\r\"
            //        ,"\r\n"
            //        ,@"\ndelay 5;\r\"
            //   });
            //    _name = @"\$newdate";
            //}
            //else
            //{
            //    delay = @"""delay 5;\r\";
            //    _name = nfile;
            //}
            //return string.Concat(new string[]
            //       {
            //       "/system scheduler"
            //       ,"\r\n"
            //       ,@"add"
            //       ,inter
            //       ," name="
            //       ,namescheduler
            //       ,@" on-event=\"
            //       ,"\r\n"
            //       ,delay
            //       ,"\r\n"
            //       ,@"\nsystem backup save name="
            //       ,_name," dont-encrypt=",encrypt,@"\r\"
            //       ,"\r\n"
            //       ,@"\n"" policy=\"
            //       ,"\r\n"
            //       ,@"ftp,reboot,read,write,policy,test,password,sniff,sensitive,romon \"
            //       ,"\r\n"
            //       ,@"start-date="
            //       ,startdate
            //       ," start-time="
            //       ,starttime
            //       });
        }
        #endregion
        #region SecS
        public string SecSFreedomBU(string outinf ,string time)
        {
            string Script;
            string s  = Properties.Resources.SecSFrDoBU;
            String[] rows = Regex.Split(s, "\r\n");
            for (int i = 0; i < rows.Length; i++)
            {
                if (rows[i].Contains("oin"))
                {
                    rows[i] = rows[i].Replace("oin", outinf);
                    break;
                }
            }
            for (int i = 0; i < rows.Length; i++)
            {
                if (rows[i].Contains("5m"))
                {
                    rows[i] = rows[i].Replace("5m", time);
                }
            }
            String[] res=new string[rows.Length];
            for (int i = 0; i < rows.Length; i++)
            {
                res[i] = rows[i] + "\r\n";
            }
            Script= string.Concat(res);
            return Script;
        }
        public string SecSLGDuobleMac(string time)
        {
            string Script;
            string s  = Properties.Resources.SecLGDmac;
            String[] rows = Regex.Split(s, "\r\n");
            for (int i = 0; i < rows.Length; i++)
            {
                if (rows[i].Contains("2m"))
                {
                    rows[i] = rows[i].Replace("2m", time);
                    break;
                }
            }
            String[] res=new string[rows.Length];
            for (int i = 0; i < rows.Length; i++)
            {
                res[i] = rows[i] + "\r\n";
            }
            Script= string.Concat(res);
            return Script;
        }
        #endregion

        private void replaceString(String filename, String search, String replace)
        {
            StreamReader sr = new StreamReader(filename);
            String[] rows = Regex.Split(sr.ReadToEnd(), "\r\n");
            sr.Close();

            StreamWriter sw = new StreamWriter(filename);
            for (int i = 0; i < rows.Length; i++)
            {
                if (rows[i].Contains(search))
                {
                    rows[i] = rows[i].Replace(search, replace);
                }
                sw.WriteLine(rows[i]);
            }
            sw.Close();
        }
    }
}
