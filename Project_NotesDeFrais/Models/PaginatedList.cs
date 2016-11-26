using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Project_NotesDeFrais.Models
{
    public class PaginatedList<T> : List<T>  
    {

        public int PageIndex { get; private set;}
        public int PageSize { get; private set; }
    
        public int TotaleCount { get; private set; }
        public int TotalPage { get; private set; }

        public PaginatedList(IQueryable<T> source, int? pageIndex, int pageSize)
        {
            PageSize = pageSize;
            var pageNumber = pageIndex != null ? (int)pageIndex : 0;

            TotaleCount = source.Count();
            TotalPage = (int)Math.Ceiling(TotaleCount / (double)PageSize);
            PageIndex = pageNumber;
            this.AddRange(source.Skip(PageIndex * PageSize).Take(PageSize));
        }
        public bool HasPreviousPage { get { return (PageIndex > 0); }}
        public bool HasNextPage { get { return (PageIndex + 1 < TotalPage); } } 
    }
}