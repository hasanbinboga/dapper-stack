namespace NetFrame.Common.Extension
{
    /// <summary>
    /// Date extensions class
    /// </summary>
    public static class DateExtensions
    {
        /// <summary>
        /// Method for getting first day of month
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>First Day of month</returns>
		public static DateTime FirstDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1);
        }
        /// <summary>
        /// Method for getting last day of month.
        /// </summary>
        /// <param name="date">Date</param>
        /// <returns>Last Day Of Month</returns>
		public static DateTime LastDayOfMonth(this DateTime date)
        {
            return new DateTime(date.Year, date.Month, 1).AddMonths(1).AddDays(-1);
        }
    }
}
