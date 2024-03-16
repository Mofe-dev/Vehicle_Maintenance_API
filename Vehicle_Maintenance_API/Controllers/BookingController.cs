using Microsoft.AspNetCore.Mvc;
using Vehicle_Maintenance_API.Context;
using Vehicle_Maintenance_API.Dto;
using Vehicle_Maintenance_API.Interfaces;

namespace Vehicle_Maintenance_API.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class BookingController: Controller
    {
        private readonly ApplicationDbContext _context;

        private IMasterEntityRepository _MasterEntityRepository;

        #region Constructor Declarations
        public BookingController(ApplicationDbContext context, IMasterEntityRepository MasterEntityRepository)
        {
            _context = context;
            this._MasterEntityRepository = MasterEntityRepository;
        }

        #endregion


        [HttpPost]
        [Route("SaveBooking")]
        public JsonResult SaveBooking([FromBody] BookingDto booking)
        {
            try
            {
                ModelResponse objResult = new();



                if (_MasterEntityRepository == null)
                {
                    objResult.Status = new { Code = 500, Message = "Repositorio no inicializado." };
                    objResult.Info = new { Datetime = DateTime.Now, AcceptedUser = false };
                    return Json(objResult);
                }

                var _result = _MasterEntityRepository.SaveBooking(booking);

                objResult.Data = _result;
                objResult.Status = new { Code = 200, Message = "Ok" };
                objResult.Info = new { Datetime = DateTime.Now, AcceptedUser = true };

                return Json(objResult); ;

            }
            catch (Exception e)
            {
                ModelResponse objresult = new ModelResponse();
                objresult.Status = new { Code = 406, Message = e.Message };
                objresult.Info = new { Datetime = DateTime.Now, AcceptedUser = true };
                return Json(objresult);
            }
        }


    }
}
