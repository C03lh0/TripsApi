using Journey.Application.UseCases.Trips.GetAll;
using Journey.Application.UseCases.Trips.Register;
using Journey.Communication.Requests;
using Journey.Exception.ExceptionsBase;
using Microsoft.AspNetCore.Mvc;

namespace Journey.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TripsController : ControllerBase
    {
        //Todo End point devolve uma resposta, nesse caso vai conter um status code (400, 404, 200 ...)
        //Para criação de recurso é utilizado o HttpPost
        [HttpPost]
        //[FromBody] - informa ao C# que ele vai ler as informações do corpo da requisição para montar o objeto.
        public IActionResult Register([FromBody] RequestRegisterTripJson request)
        {
            try
            {
                var registerTripUseCase = new RegisterTripUseCase();
                //Chamar funções de caso de uso como Execute. "Executa pra mim essa US."
                var tripRegisterResponse = registerTripUseCase.Execute(request);
                return Created(string.Empty, tripRegisterResponse);
            }
            catch (CRUDException ex)
            {
                return BadRequest(ex.Message);
            }
            catch
            {
                return StatusCode(StatusCodes.Status500InternalServerError, "Erro desconhecido");
            }
        }

        [HttpGet]
        public IActionResult GetAll()
        {
            var getAllTrips = new GetAllTripsUseCase();

            var reult = getAllTrips.Execute();

            return Ok(reult);
        }
    }
}
