namespace NetFrame.Core
{
    /// <summary>
    /// The class where the pagination information is kept
    /// </summary>
    public class Page
    {
        /// <summary>
        /// requested page number
        /// </summary>
        public int PageNumber { get; set; }
        /// <summary>
        /// information on how many data will be on the pages
        /// </summary>
        public int PageSize { get; set; }

        /// <summary>
        /// Information on how many entities there are in total
        /// </summary>
        public int RowCount { get; set; }


        /// <summary>
        /// Total number of pages
        /// </summary>
        public int TotalPageCount
        {
            get
            {
                if (PageSize > 0)
                {
                    var mod = RowCount % PageSize;
                    return (RowCount - mod) / PageSize + mod > 0 ? 1 : 0;
                }
                return 0;
            }
        }

        /// <summary>
        /// Default constructor
        /// Default params PageNumber = 1 , PageSize = 10;
        /// </summary>
        public Page(int rowCount, int pageNumber = 1, int pageSize = 10)
        {
            PageNumber = pageNumber;
            PageSize = pageSize;
            RowCount = rowCount;
        }



        /// <summary>
        /// Number of records to skip to fetch records listed on the page
        /// 
        /// Example: If Pagesize is 10, 20 records must be skipped 
        /// to access the records on page 3.
        /// </summary>
        public int Skip => (PageNumber - 1) * PageSize;
    }
}
