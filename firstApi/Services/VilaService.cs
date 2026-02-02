using AutoMapper;
using firstApi.Dto;
using firstApi.Models;

namespace firstApi.Services
{
    public class VilaService : IVilaService
    {

        private readonly DatabaseContext _context;
        public VilaService(DatabaseContext context)
        {
            _context = context;
          
        }

        public bool Create(Vila vila)
        {
            _context.Vilas.Add(vila);
            _context.SaveChanges();
            return true;
        }

        public bool Delete(int id)
        {
            var vila = GetById(id);
            if (vila != null)
            {
                var result = _context.Remove(vila);
                _context.SaveChanges();
                return true;
            }
            return false;
        }

        public List<Vila> GetAll()
        {
            var allVilas = _context.Vilas.ToList();
            return allVilas;
        }

        public Vila GetById(int id)
        {
            var vilaById = _context.Vilas.FirstOrDefault(v => v.Id == id);
            return vilaById;
        }


        public bool Update(int id, Vila model)
        {
            var vila = _context.Vilas.FirstOrDefault(v => v.Id == id);
            if(vila == null) {
                return false;
            }
            vila.Id = id;
            vila.Name = model.Name;
            vila.Address = model.Address;
            vila.City = model.City;
            vila.Street = model.Street;
            vila.Mobile = model.Mobile;
            _context.SaveChanges();
            return true;
        }

        public VilaSearchDto VilaSearch(int pageId,string filter,int take)
        {
            var allVilas = GetAll();
            var basePagination = new PaginationBase();

            var filterData = basePagination.Generate(allVilas.AsQueryable(), pageId, take,filter);
            return filterData;
        }
    }
}
