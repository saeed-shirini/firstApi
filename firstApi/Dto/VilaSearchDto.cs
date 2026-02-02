using firstApi.Models;

namespace firstApi.Dto
{
    public class VilaSearchDto : PaginationBase
    {
        public List<VilaDto> Vilas { get; set; }


    }

    public class PaginationBase
    {
        public int PageId { get; set; }
        public string  Filter { get; set; }

        public int Take { get; set; }

        public int StartPage { get; set; }

        public int EndPage { get; set; }
        public int PageCount { get; set; }

        public VilaSearchDto Generate(IQueryable<Vila> data,int pageId,int take,string filter)
        { 
            if (!string.IsNullOrEmpty(filter))
                data = data.Where(v => v.Name.Contains(filter) || v.City.Contains(filter) || v.Street.Contains(filter));

            var skip = (pageId - 1) * take;
            PageCount = data.Count<Vila>() / take;
            if (data.Count<Vila>() % 2 > 0)
                PageCount++;
         
            data = data.Skip(skip).Take(take);
            var vilaSearchDto = new VilaSearchDto()
            {
                Vilas = data.Select(v => new VilaDto() { Id = v.Id,Name=v.Name,Address = v.Address,City=v.City,Street=v.Street,Mobile=v.Mobile}).ToList(),
                PageCount = PageCount,
                PageId = pageId,
                StartPage = (pageId - 2) < 0 ? 1 : pageId - 2,
                EndPage = (PageId + 2) >= PageCount ? PageCount : pageId + 2,
                Take = take,
                Filter = filter
            };
            return vilaSearchDto;
        }

    }
}
