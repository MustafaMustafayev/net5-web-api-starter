using System;
namespace DAL.Utility
{
    public class PaginationInfo
    {
        public int PageIndex { get; set; }
        public int TotalPageCount { get; set; }
        public int TotalRecordCount { get; set; }

        public bool HasPreviousPage
        {
            get
            {
                return (PageIndex > 1);
            }
        }

        public bool HasNextPage
        {
            get
            {
                return (PageIndex < TotalPageCount);
            }
        }
    }
}
