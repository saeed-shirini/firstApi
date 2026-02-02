using firstApi.Controllers;
using firstApi.Dto;
using firstApi.Models;

namespace firstApi.Services
{
    public interface IVilaService
    {
        List<Vila> GetAll();

        bool Create(Vila vila);

        Vila GetById(int id);

        bool Update(int id,Vila model);

        bool Delete(int id);

        VilaSearchDto VilaSearch(int pageId, string filter, int take);
    }
}
