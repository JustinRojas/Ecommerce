using Ecommerce.Servicio.Contrato;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;



using Ecommerce.Servicio.Contrato;
using Ecommerce.DTO;
using Ecommerce.Servicio.Implementacion;
namespace Ecommerce.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DashboardController : ControllerBase
    {
        private readonly IDashBoardService _dashboardServicio;

        public DashboardController(IDashBoardService dashboardServicio)
        {
            _dashboardServicio = dashboardServicio;
        }


        [HttpGet("Resumen")]
        public  IActionResult Resumen()
        {
            var response = new ResponseDTO<DashBoardDTO>();

            try
            {

                response.EsCorrecto = true;
                response.Resultado = _dashboardServicio.Resumen();
            }
            catch (Exception ex)
            {

                response.EsCorrecto = false;
                response.Mensaje = ex.Message;
            }
            return Ok(response);
        }


    }
}
