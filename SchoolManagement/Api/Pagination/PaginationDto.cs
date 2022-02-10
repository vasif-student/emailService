using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.Pagination
{
    public class PaginationDto<T>
    {
        public PaginationDto(IQueryable<T> items, int itemCount, int pageNumber)
        {
            if (pageNumber < 0) pageNumber = 1;
            if (itemCount < 0) itemCount = 10;

            this.Items = items.Skip((pageNumber - 1) * itemCount).Take(itemCount).ToList();
            this.CurrentPage = pageNumber;
            this.TotalPages = (int)Math.Ceiling((decimal)items.Count() / itemCount);
            this.HasNext = CurrentPage < TotalPages;
            this.HasPrevious = CurrentPage > 1;
        }

        public List<T> Items { get; set; }
        public int TotalPages { get; set; }
        public int CurrentPage { get; set; }
        public bool HasNext { get; set; }
        public bool HasPrevious { get; set; }
    }
}
