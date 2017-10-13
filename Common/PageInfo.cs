using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Common {
    public class PageInfo {

        #region 分页数据

        /// <summary>
        /// 数据总行数
        /// </summary>
        public int PageCount { get; set; }

        /// <summary>
        /// 当前页码
        /// </summary>
        public int PageIndex {
            get {
                if (pageIndex == 0)
                    pageIndex = 1;
                return pageIndex;
            }
            set {
                pageIndex = value;
            }
        }
        private int pageIndex;

        /// <summary>
        /// 每页行数
        /// </summary>
        public int PageSize {
            get {
                if (pageSize == 0)
                    pageSize = 10;
                return pageSize;
            }
            set {
                pageSize = value;
            }
        }
        private int pageSize;

        /// <summary>
        /// 页面跳转链接，带参数
        /// 用于分页跳转
        /// </summary>
        public string PageUrl { get; set; }

        /// <summary>
        /// 页面跳转链接，不带参数
        /// 用于删除时跳转
        /// </summary>
        public string GetPageUrl {
            get {
                // 判断是否为空
                if (PageUrl == null)
                    return "";

                // 检测是否有参数
                int index = PageUrl.LastIndexOf("?");
                // 去掉参数
                if (index > 0)
                    return PageUrl.Substring(0, index);
                return PageUrl;
            }
        }

        /// <summary>
        /// 下拉框名称
        /// 给上值后，配合分页方法，可以实现下拉框值修改时，数据实时更新
        /// </summary>
        public string PageDrop { get; set; }

        /// <summary>
        /// 中间页码数量，不填默认为8
        /// </summary>
        public int PageCent {
            get {
                if (pageCent == 0)
                    pageCent = 8;
                return pageCent;
            }
            set {
                pageCent = value;
            }
        }
        private int pageCent;

        /// <summary>
        /// 分页的HTML字符串
        /// </summary>
        public string PageContent { get; set; }

        /// <summary>
        /// 执行分页
        /// </summary>
        public void GetPageList() {
            PageContent = Utils.OutPageList(PageSize, PageIndex, PageCount, PageUrl, PageCent, PageDrop);
        }

        #endregion

        #region 时间参数

        #region 时间接收

        private DateTime? start_Time;
        /// <summary>
        /// 开始时间，时分秒为 0:0:0，并且不能大于End_Time
        /// </summary>
        public DateTime? Start_Time {
            get {
                // 允许开始时间为空
                if (start_Time == null)
                    return start_Time;

                // 若开始时间大于当前时间
                if (start_Time.Value > DateTime.Now)
                    // 开始时间为当前时间
                    start_Time = DateTime.Now;

                // 当结束时间不为空
                if (end_Time != null)
                    // 当开始时间大于结束时间时
                    if (start_Time > End_Time)
                        // 取结束时间当天的凌晨
                        start_Time = new DateTime(End_Time.Value.Year, End_Time.Value.Month, End_Time.Value.Day, 0, 0, 0);

                return start_Time;
            }
            set { start_Time = value; }
        }

        private DateTime? end_Time;
        /// <summary>
        /// 结束时间，时分秒为 23:59:59，并且不能大于当前时间
        /// </summary>
        public DateTime? End_Time {
            get {
                // 允许结束时间为空
                if (end_Time == null)
                    return end_Time;

                // 若结束时间大于当前时间
                if (end_Time.Value >= DateTime.Now)
                    // 结束时间为当前时间
                    end_Time = DateTime.Now;
                else {
                    // 获取结束时间的信息
                    int year = end_Time.Value.Year;
                    int month = end_Time.Value.Month;
                    int day = end_Time.Value.Day;

                    int hour = end_Time.Value.Hour;
                    int minute = end_Time.Value.Minute;
                    int second = end_Time.Value.Second;

                    // 当时分秒均为0时，为结束时间加上时分秒
                    if (hour == 0 && minute == 0 && second == 0) {
                        DateTime now = DateTime.Now;
                        // 若结束时间的年月日正好是当天
                        if (now.Year == year && now.Month == month && now.Day == day)
                            end_Time = now;
                        // 否则，给到结束时间那天，最后一秒
                        else
                            end_Time = new DateTime(year, month, day, 23, 59, 59);
                    }
                }

                return end_Time;
            }
            set { end_Time = value; }
        }

        #endregion

        #region 时间输出

        /// <summary>
        /// 时间字符串返回格式
        /// 若不设置，默认为"yyyy-MM-dd HH:mm:ss"
        /// </summary>
        public string Format {
            get {
                if (format == null)
                    format = "yyyy-MM-dd HH:mm:ss";
                return format;
            }
            set { format = value; }
        }
        private string format;

        /// <summary>
        /// 用于返回开始时间字符串
        /// </summary>
        public string GetStarTimeStr {
            get {
                if (Start_Time.HasValue)
                    return Start_Time.Value.ToString(Format);
                return "";
            }
        }

        /// <summary>
        /// 用于返回结束时间字符串
        /// </summary>
        public string GetEndTimeStr {
            get {
                if (End_Time.HasValue)
                    return End_Time.Value.ToString(Format);
                return "";
            }
        }

        #endregion

        #endregion
        
        #region 数据绑定方法

        /// <summary>
        /// 返回指定区间的日期，默认今天
        /// </summary>
        /// <param name="dateSection"></param>
        public void GetDateSection(DateSection dateSection = DateSection.Today) {

            // 判断枚举中，是否存在此项
            if (!Enum.IsDefined(typeof(DateSection), (int)dateSection))
                dateSection = DateSection.Today;

            // 日期
            DateTime Date = DateTime.Now;

            // 倒退的天数
            int BackDay = 0;

            switch (dateSection) {

                #region =====今天=====

                case DateSection.Today:
                    End_Time = Date;
                    Start_Time = new DateTime(Date.Year, Date.Month, Date.Day, 0, 0, 0, 0);
                    break;

                #endregion

                #region =====昨天=====

                case DateSection.Yesterday:
                    Date = DateTime.Now.AddDays(-1);

                    End_Time = new DateTime(Date.Year, Date.Month, Date.Day, 23, 59, 59, 999);
                    Start_Time = new DateTime(Date.Year, Date.Month, Date.Day, 0, 0, 0, 0, 0);
                    break;

                #endregion

                #region =====本周=====

                case DateSection.ThisWeek:
                    End_Time = Date;

                    // 获取今天是本周第几天
                    BackDay = Convert.ToInt32(Date.DayOfWeek.ToString("d"));

                    Date = DateTime.Now.AddDays(-BackDay);
                    Start_Time = new DateTime(Date.Year, Date.Month, Date.Day, 0, 0, 0, 0);
                    break;

                #endregion

                #region =====上周=====

                case DateSection.LastWeek:
                    BackDay = Convert.ToInt32(Date.DayOfWeek.ToString("d")) + 1;

                    // 到上周最后一天
                    Date = DateTime.Now.AddDays(-BackDay);
                    End_Time = new DateTime(Date.Year, Date.Month, Date.Day, 23, 59, 59, 999);

                    // 到上周第一天
                    Date = Date.AddDays(-6);
                    Start_Time = new DateTime(Date.Year, Date.Month, Date.Day, 0, 0, 0, 0);

                    break;

                #endregion

                #region =====本月=====

                case DateSection.ThisMonth:
                    End_Time = Date;
                    Start_Time = new DateTime(Date.Year, Date.Month, 1, 0, 0, 0, 0);
                    break;

                #endregion

                #region =====上月=====

                case DateSection.LastMonth:

                    BackDay = Date.Day;

                    // 到上月最后一天
                    Date = DateTime.Now.AddDays(-BackDay);
                    End_Time = new DateTime(Date.Year, Date.Month, Date.Day, 23, 59, 59, 999);

                    Start_Time = new DateTime(Date.Year, Date.Month, 1, 0, 0, 0, 0);
                    break;

                #endregion

                #region =====今年=====

                case DateSection.ThisYear:
                    End_Time = Date;
                    Start_Time = new DateTime(Date.Year, 1, 1, 0, 0, 0, 0);
                    break;

                #endregion

                #region =====去年=====

                case DateSection.LastYear:
                    BackDay = Date.DayOfYear;

                    // 到去年最后一天
                    Date = DateTime.Now.AddDays(-BackDay);
                    End_Time = new DateTime(Date.Year, Date.Month, Date.Day, 23, 59, 59, 999);

                    Start_Time = new DateTime(Date.Year, 1, 1, 0, 0, 0, 0);
                    break;

                #endregion

                default: break;
            }

        }

        /// <summary>
        /// 保证开始和结束时间绝对不为空
        /// </summary>
        /// <param name="DateLong">间隔长度，默认:7</param>
        /// <param name="dateFormat">间隔单位，默认:Day(天)</param>
        public void GetDateNow(int DateLong = 7, DateFormat dateFormat = DateFormat.Day) {

            // 校验是否存在此枚举
            if (Enum.IsDefined(typeof(DateFormat), (int)dateFormat))
                dateFormat = DateFormat.Day;

            // 初始化结束时间
            if (End_Time == null)
                End_Time = DateTime.Now;

            DateTime? star;

            // 有校验的时间
            star = new DateTime();
            star = Start_Time;
            ChangStar(ref star, End_Time, DateLong, dateFormat);
            Start_Time = star;

        }

        /// <summary>
        /// 根据结束时间，修改开始时间
        /// 若开始时间有值，则不改动
        /// </summary>
        /// <param name="Start">开始时间</param>
        /// <param name="End">结束时间</param>
        /// <param name="DateLong">间隔长度</param>
        /// <param name="dateFormat">间隔单位</param>
        private void ChangStar(ref DateTime? Start, DateTime? End, int DateLong, DateFormat dateFormat) {

            if (Start.HasValue)
                return;

            DateLong = 0 - DateLong;

            // 获取开始时间
            switch (dateFormat) {
                // 年份
                case DateFormat.Year:
                    Start = End.Value.AddYears(DateLong);
                    break;
                // 月份
                case DateFormat.Month:
                    Start = End.Value.AddMonths(DateLong);
                    break;
                // 天数
                case DateFormat.Day:
                    Start = End.Value.AddDays(DateLong);
                    break;
                // 小时
                case DateFormat.Hour:
                    Start = End.Value.AddHours(DateLong);
                    break;
                // 分钟
                case DateFormat.Minute:
                    Start = End.Value.AddMinutes(DateLong);
                    break;
                // 秒钟
                case DateFormat.Second:
                    Start = End.Value.AddSeconds(DateLong);
                    break;
            }

        }

        #endregion

    }
}

/// <summary>
/// 时间格式
/// </summary>
public enum DateFormat {
    /// <summary>
    /// 年份
    /// </summary>
    Year,
    /// <summary>
    /// 月份
    /// </summary>
    Month,
    /// <summary>
    /// 天数
    /// </summary>
    Day,
    /// <summary>
    /// 小时
    /// </summary>
    Hour,
    /// <summary>
    /// 分钟
    /// </summary>
    Minute,
    /// <summary>
    /// 秒钟
    /// </summary>
    Second
}

/// <summary>
/// 时间区间
/// </summary>
public enum DateSection {
    /// <summary>
    /// 今天
    /// </summary>
    Today,
    /// <summary>
    /// 昨天
    /// </summary>
    Yesterday,
    /// <summary>
    /// 本周，星期天为第一天
    /// </summary>
    ThisWeek,
    /// <summary>
    /// 上周，星期天为第一天
    /// </summary>
    LastWeek,
    /// <summary>
    /// 本月
    /// </summary>
    ThisMonth,
    /// <summary>
    /// 上月
    /// </summary>
    LastMonth,
    /// <summary>
    /// 今年
    /// </summary>
    ThisYear,
    /// <summary>
    /// 去年
    /// </summary>
    LastYear
}