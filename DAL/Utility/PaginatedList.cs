using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using DTO.DTOs.Responses;

namespace DAL.Utility
{
    public class PaginatedList<T> : PaginationInfo
    {
        public List<T> Datas { get; set; }

        public PaginatedList(List<T> items, int count, int pageIndex, int pageSize)
        {
            if (pageIndex != 0)
            {
                PageIndex = pageIndex;
                TotalPageCount = (int)Math.Ceiling(count / (double)pageSize);
            }
            else
            {
                PageIndex = 1;
                TotalPageCount = 1;
            }
            TotalRecordCount = count;
            Datas = items;      
        }

        public static async Task<PaginatedList<T>> CreateAsync(IQueryable<T> source, int pageIndex, int pageSize)
        {
            var count = await source.CountAsync();
            List<T> items;
            if (pageIndex != 0)
            {
                items = await source.Skip((pageIndex - 1) * pageSize).Take(pageSize).ToListAsync();
            }
            else
            {
                items = await source.ToListAsync();
            }
            PaginatedList<T> response = new PaginatedList<T>(items, count, pageIndex, pageSize);
            return response;
        }
    }

}
