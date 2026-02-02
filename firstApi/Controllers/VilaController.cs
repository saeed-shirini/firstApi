using AutoMapper;
using firstApi.Dto;
using firstApi.Models;
using firstApi.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;

namespace firstApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class VilaController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly IVilaService _vila;

        public VilaController(IVilaService vila, IMapper mapper)
        {
            _vila = vila;
            _mapper = mapper;
        }

        /// <summary>
        ///    لیست تمام ویلاها
        /// </summary>
        /// <param></param>
        /// <returns></returns>
        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK,Type= typeof(VilaDto))]
        public IActionResult GetAll()
        {
            var result = _vila.GetAll();
            var vilaDto = _mapper.Map<List<VilaDto>>(result);
            return Ok(vilaDto);
        }

        /// <summary>
        ///    جزئیات یک ویلا
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("[action]/{id}", Name = "Details")]
        [ProducesResponseType(StatusCodes.Status200OK, Type = typeof(VilaDto))]
        [ProducesResponseType(404)]
        public IActionResult Details(int id)
        {
            var result = _vila.GetById(id);
            if (result == null)
            {
                return NotFound(result);
            }
            var vilaDto = _mapper.Map<VilaDto>(result);
            return Ok(vilaDto);
        }
        /// <summary>
        ///    ویلا بر اساس آدرس
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>

        [HttpGet("[action]/{id:int}")]
        [ProducesResponseType(StatusCodes.Status200OK,Type = typeof(Vila))]
        [ProducesResponseType(404)]
        public IActionResult GetVilaAddress(int id)
        {
            var result = _vila.GetById(id);
            if (result == null) { 
                return NotFound(result);
            }
           // var vilaDto = _mapper.Map<VilaDto>(result);

            return Ok(result.Address);
        }

        /// <summary>
        ///   ویلا بر اساس شماره موبایل
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        [HttpGet("[action]")]
        public IActionResult GetVilaMobile([FromQuery] int id)
        {
            var vilaById = _vila.GetById(id);
            if (vilaById == null) 
                return NotFound();
            return Ok(new { Id = vilaById.Id, Mobile = vilaById.Mobile });

        }

        /// <summary>
        ///     ایجاد یک ویلا
        /// </summary>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPost]
        [ProducesResponseType(StatusCodes.Status400BadRequest,Type = typeof(NoContent))]
        [ProducesResponseType(500)]
        [ProducesResponseType(StatusCodes.Status201Created,Type =typeof(VilaDto ))]
        public IActionResult Create([FromBody] VilaDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            var result = _mapper.Map<Vila>(model);
            if (_vila.Create(result))
            {
                return CreatedAtRoute("Details",new { id = result.Id},_mapper.Map<VilaDto>(result));
            }
           // var modelState = ModelState.AddModelError("","خطا در ارتباط با سرور به وجود آمده است");
            return StatusCode(500, "خطا در ارتباط با سرور به وجود آمده است");
        }

        /// <summary>
        ///     ویرایش اطلاعات ویلا
        /// </summary>
        /// <param name="id"></param>
        /// <param name="model"></param>
        /// <returns></returns>
        [HttpPatch("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent,Type = typeof(NoContent))]
        [ProducesResponseType(StatusCodes.Status400BadRequest, Type = typeof(VilaDto))]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public IActionResult Edit( int id,[FromBody] VilaDto model)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(model);
            }
            if(id != model.Id)
            {
                return NotFound();
            }
            var mapToVila = _mapper.Map<Vila>(model);
            var result = _vila.Update(id,mapToVila);
            if (!result)
            {
                ModelState.AddModelError("", "خطا در دخیره داده");
                return StatusCode(500, ModelState);
            }
            return NoContent();
        }

        [HttpDelete("[action]/{id}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(500)]
        public IActionResult Delete(int id)
        {
            var vila = _vila.GetById(id);
            if (vila == null)
            {
                return NotFound();
            }
            var result = _vila.Delete(id);
            if(result == false)    
            {
                ModelState.AddModelError("", "خطا در برقرای ارتباط با سرور");
                return StatusCode(500,ModelState);
            }    
             return NoContent();
        }

       [HttpGet("[action]")]
       public IActionResult VilaSearch(int pageId=1,string filter="",int Take=2)
        {
            if (pageId < 1 || Take < 1)
                return BadRequest();
            var filterData = _vila.VilaSearch(pageId,filter,Take);
            return Ok(filterData);
        }
    }
}
