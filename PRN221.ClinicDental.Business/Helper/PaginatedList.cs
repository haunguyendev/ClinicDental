using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PRN221.ClinicDental.Business.Helper
{
    public class PaginatedList<T>
    {
        public List<T> Items { get; private set; }
        public int TotalPages { get; private set; }
        public int CurrentPage { get; private set; }
        public int PageSize { get; private set; }

        public PaginatedList(List<T> items, int count, int pageNumber, int pageSize)
        {
            Items = items;
            TotalPages = (int)Math.Ceiling(count / (double)pageSize);
            CurrentPage = pageNumber;
            PageSize = pageSize;
        }

        public bool HasPreviousPage => CurrentPage > 1;
        public bool HasNextPage => CurrentPage < TotalPages;

        public static PaginatedList<T> Create(IQueryable<T> source, int pageNumber, int pageSize)
        {
            var count = source.Count();
            var items = source.Skip((pageNumber - 1) * pageSize).Take(pageSize).ToList();
            return new PaginatedList<T>(items, count, pageNumber, pageSize);
        }
    }

}
