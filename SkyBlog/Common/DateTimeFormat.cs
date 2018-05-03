namespace System
{
    public static class DateTimeFormat
    {
        /// <summary>
        /// 得到年月日 时分秒 格式化日期 24小时制
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetYMDHMS(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss");
        }
     
        /// <summary>
        /// 得到年月日 时分秒 格式化日期 24小时制 上下午
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetYMDHMSTT(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd HH:mm:ss tt");
        }


        /// <summary>
        /// 得到年月日 时分秒 格式化日期 12小时制止
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetYMDhms(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd hh:mm:ss");
        }

        /// <summary>
        /// 得到年月日 时分秒 格式化日期 12小时制 上下午
        /// </summary>
        /// <param name="dt"></param>
        /// <returns></returns>
        public static string GetYMDhmsTT(this DateTime dt)
        {
            return dt.ToString("yyyy-MM-dd hh:mm:ss tt");
        }

   
   
    }
}
