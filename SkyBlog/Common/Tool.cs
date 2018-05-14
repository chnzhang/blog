using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Sockets;
using System.Text;

namespace SkyBlog.Common
{
    public static class Tool
    {

        /// <summary>
        /// 生成订单编号
        /// </summary>
        /// <param name="channel">下单渠道</param>
        /// <param name="pay">支付渠道</param>
        /// <param name="business">业务类型</param>
        /// <param name="userId">用户编号</param>
        /// <returns></returns>
        public static string GetOrderNumber(int channel, int pay, int business, int userId)
        {
            return "" + channel + pay + business + GetTimeStamp(DateTime.Now) + userId;
        }

        /// <summary>
        /// 生成工单编号
        /// </summary>
        /// <returns></returns>
        public static string GetWorkOrderNumber()
        {
            string id = System.Guid.NewGuid().ToString("N").ToUpper();
            if (id.Length > 19)
            {
                return id.Substring(0, 19).ToString();
            }
            else
            {
                return id;
            }
        }

        /// <summary>
        /// 获取时间戳
        /// </summary>
        /// <returns></returns>
        public static string GetTimeStamp(System.DateTime time)
        {
            long ts = ConvertDateTimeToInt(time);
            return ts.ToString();
        }

        /// <summary>  
        /// 将c# DateTime时间格式转换为Unix时间戳格式  
        /// </summary>  
        /// <param name="time">时间</param>  
        /// <returns>long</returns>  
        public static long ConvertDateTimeToInt(System.DateTime time)
        {
            System.DateTime startTime = TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            long t = (time.Ticks - startTime.Ticks) / 10000;   //除10000调整为13位      
            return t;
        }

        /// <summary>        
        /// 时间戳转为C#格式时间        
        /// </summary>        
        /// <param name=”timeStamp”></param>        
        /// <returns></returns>        
        private static DateTime ConvertStringToDateTime(string timeStamp)
        {
           
            DateTime dtStart =  TimeZoneInfo.ConvertTime(new DateTime(1970, 1, 1), TimeZoneInfo.Local);
            long lTime = long.Parse(timeStamp + "0000");
            TimeSpan toNow = new TimeSpan(lTime);
            return dtStart.Add(toNow);
        }

        /// <summary>
        /// 计算日期的间隔(静态类)
        /// </summary>
        public static class dateTimeDiff
        {
            /// <summary>
            /// 计算日期间隔
            /// </summary>
            /// <param name="d1">要参与计算的其中一个日期字符串</param>
            /// <param name="d2">要参与计算的另一个日期字符串</param>
            /// <returns>一个表示日期间隔的TimeSpan类型</returns>
            public static TimeSpan toResult(string d1, string d2)
            {
                try
                {
                    DateTime date1 = DateTime.Parse(d1);
                    DateTime date2 = DateTime.Parse(d2);
                    return toResult(date1, date2);
                }
                catch
                {
                    throw new Exception("parameter erro!");
                }
            }
            /// <summary>
            /// 计算日期间隔
            /// </summary>
            /// <param name="d1">要参与计算的其中一个日期</param>
            /// <param name="d2">要参与计算的另一个日期</param>
            /// <returns>一个表示日期间隔的TimeSpan类型</returns>
            public static TimeSpan toResult(DateTime d1, DateTime d2)
            {
                TimeSpan ts;
                if (d1 > d2)
                {
                    ts = d1 - d2;
                }
                else
                {
                    ts = d2 - d1;
                }
                return ts;
            }

            /// <summary>
            /// 计算日期间隔
            /// </summary>
            /// <param name="d1">要参与计算的其中一个日期字符串</param>
            /// <param name="d2">要参与计算的另一个日期字符串</param>
            /// <param name="drf">决定返回值形式的枚举</param>
            /// <returns>一个代表年月日的int数组，具体数组长度与枚举参数drf有关</returns>
            public static int[] toResult(string d1, string d2, diffResultFormat drf)
            {
                try
                {
                    DateTime date1 = DateTime.Parse(d1);
                    DateTime date2 = DateTime.Parse(d2);
                    return toResult(date1, date2, drf);
                }
                catch
                {
                    throw new Exception("parameter erro!");
                }
            }
            /// <summary>
            /// 计算日期间隔
            /// </summary>
            /// <param name="d1">要参与计算的其中一个日期</param>
            /// <param name="d2">要参与计算的另一个日期</param>
            /// <param name="drf">决定返回值形式的枚举</param>
            /// <returns>一个代表年月日的int数组，具体数组长度与枚举参数drf有关</returns>
            public static int[] toResult(DateTime d1, DateTime d2, diffResultFormat drf)
            {
                #region 数据初始化
                DateTime max;
                DateTime min;
                int year;
                int month;
                int tempYear, tempMonth;
                int day = -1;
                if (d1 > d2)
                {
                    max = d1;
                    min = d2;
                }
                else
                {
                    max = d2;
                    min = d1;
                    day = d2.Day - d1.Day;
                }
                tempYear = max.Year;
                tempMonth = max.Month;
                if (max.Month < min.Month)
                {
                    tempYear--;
                    tempMonth = tempMonth + 12;
                }
                year = tempYear - min.Year;
                month = tempMonth - min.Month;
                #endregion
                #region 按条件计算
                if (drf == diffResultFormat.dd)
                {
                    TimeSpan ts = max - min;
                    return new int[] { ts.Days };
                }
                if (drf == diffResultFormat.mm)
                {
                    return new int[] { month + year * 12 };
                }
                if (drf == diffResultFormat.yy)
                {
                    return new int[] { year };
                }
                return new int[] { year, month, day };
                #endregion
            }
        }
        /// <summary>
        /// 关于返回值形式的枚举
        /// </summary>
        public enum diffResultFormat
        {
            /// <summary>
            /// 年数和月数和日
            /// </summary>
            yymmdd,
            /// <summary>
            /// 年数和月数
            /// </summary>
            yymm,
            /// <summary>
            /// 年数
            /// </summary>
            yy,
            /// <summary>
            /// 月数
            /// </summary>
            mm,
            /// <summary>
            /// 天数
            /// </summary>
            dd,
        }



        public static class DateDiffTimespan
        {

            #region 返回时间差
            /// <summary>
            /// 返回时间差
            /// </summary>
            /// <param name="DateTime1">大日期</param>
            /// <param name="DateTime2">小日期</param>
            /// <returns></returns>
            public static string DateDiff(DateTime? DateTime1, DateTime? DateTime2)
            {
                string dateDiff = null;
                try
                {
                    if (DateTime1 == null || DateTime1.Equals(Convert.ToDateTime(null)) || DateTime2 == null || DateTime2.Equals(Convert.ToDateTime(null)))
                    {
                        dateDiff = "无";
                        return dateDiff;
                    }
                    else if (DateTime2 > DateTime1)
                    {
                        return "0天0时0分";
                    }

                    TimeSpan ts1 = new TimeSpan(Convert.ToDateTime(DateTime1).Ticks);
                    TimeSpan ts2 = new TimeSpan(Convert.ToDateTime(DateTime2).Ticks);
                    TimeSpan ts = ts1.Subtract(ts2).Duration();

                    if (ts.Days > 0)
                    {
                        dateDiff += ts.Days.ToString() + "天";
                    }
                    if (ts.Hours > 0)
                    {
                        dateDiff += ts.Hours.ToString() + "时";
                    }
                    if (ts.Minutes > 0)
                    {
                        dateDiff += ts.Minutes.ToString() + "分";
                    }

                }
                catch
                {

                }
                return dateDiff;
            }
            #endregion
        }

        /// <summary>
        /// 返回年龄
        /// </summary>
        /// <param name="birthday">生日</param>
        public static int GetAge(DateTime birthday)
        {
            return DateTime.Now.Year - birthday.Year;
        }

        /// <summary>
        /// 取得本机 IP Address
        /// </summary>
        /// <returns></returns>
        public static List<string> GetHostIPAddress()
        {
            List<string> lstIPAddress = new List<string>();
            System.Net.IPHostEntry IpEntry = System.Net.Dns.GetHostEntry(System.Net.Dns.GetHostName());
            foreach (System.Net.IPAddress ipa in IpEntry.AddressList)
            {
                if (ipa.AddressFamily == System.Net.Sockets.AddressFamily.InterNetwork)
                    lstIPAddress.Add(ipa.ToString());
            }
            return lstIPAddress;
        }


        #region 从网卡获取ip设置信息  
        /// <summary>  
        /// 从网卡获取ip设置信息  
        /// </summary>  
        /// <param name="sender"></param>  
        /// <param name="e"></param>  
        public static string GetNetWorkIPAddress()
        {
            System.Net.NetworkInformation.NetworkInterface[] nics = System.Net.NetworkInformation.NetworkInterface.GetAllNetworkInterfaces();
            foreach (System.Net.NetworkInformation.NetworkInterface adapter in nics)
            {
                bool Pd1 = (adapter.NetworkInterfaceType == System.Net.NetworkInformation.NetworkInterfaceType.Ethernet); //判断是否是以太网连接  
                if (Pd1)
                {
                    System.Net.NetworkInformation.IPInterfaceProperties ip = adapter.GetIPProperties();     //IP配置信息  

                    //获取单播地址集  
                    System.Net.NetworkInformation.UnicastIPAddressInformationCollection ipCollection = ip.UnicastAddresses;
                    var ipv4 = ipCollection.Where(w => w.Address.AddressFamily == AddressFamily.InterNetwork).FirstOrDefault();
                    if (ipv4 != null)
                    {
                        return ipv4.Address.ToString();
                    }
                    else
                    {
                        return ipCollection.Where(w => w.Address.AddressFamily == AddressFamily.InterNetworkV6).FirstOrDefault().Address.ToString();
                    }
                  
                    ////未获取到ipv4
                    //if (ip.UnicastAddresses.Count > 0)
                    //{
                    //    //ipv6 地址优先
                    //   return ip.UnicastAddresses[0].Address.ToString();//IP地址 
                    //}
                    //if (ip.GatewayAddresses.Count > 0)
                    //{
                    //    txtGateWay.Text = ip.GatewayAddresses[0].Address.ToString();//默认网关  
                    //}
                    //int DnsCount = ip.DnsAddresses.Count;
                    //Console.WriteLine("DNS服务器地址：");
                    //if (DnsCount > 0)
                    //{
                    //    try
                    //    {
                    //        txtDNS1.Text = ip.DnsAddresses[0].ToString(); //主DNS  
                    //        txtDNS2.Text = ip.DnsAddresses[1].ToString(); //备用DNS地址  
                    //    }
                    //    catch (Exception er)
                    //    {
                    //        throw er;
                    //    }
                    //}
                }
            }

            return "127.0.0.1";
        }
        #endregion

        #region 判断是否是正确的ip地址  
        /// <summary>  
        /// 判断是否是正确的ip地址  
        /// </summary>  
        /// <param name="ipaddress"></param>  
        /// <returns></returns>  
        public static bool IsIpaddress(string ipaddress)
        {
            string[] nums = ipaddress.Split('.');
            if (nums.Length != 4) return false;
            foreach (string num in nums)
            {
                if (Convert.ToInt32(num) < 0 || Convert.ToInt32(num) > 255) return false;
            }
            return true;
        }
        #endregion

    }
}
