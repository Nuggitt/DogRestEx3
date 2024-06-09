using DogLib;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace DogRest.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DogsController : ControllerBase
    {
        private readonly DogsRepository _dogsRepository = new DogsRepository();

        public DogsController(DogsRepository dogsRepository)
        {
            _dogsRepository = dogsRepository;
        }


        // GET: api/<DogsController>
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet]
        public ActionResult<IEnumerable<Dog>> Get()
        {
            IEnumerable<Dog> dogs = _dogsRepository.GetAll();

            if (dogs.Any())
            {
                return Ok(dogs);
            }
            else
            {
                return NotFound();
            }
        }

        // GET api/<DogsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpGet("{id}")]
        public ActionResult<Dog> Get(int id)
        {
            Dog? dog = _dogsRepository.GetById(id);

            if (dog != null)
            {
                return Ok(dog);
            }
            else
            {
                return NotFound();
            }
        }


        // POST api/<DogsController>
        [ProducesResponseType(StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [HttpPost]
        public ActionResult<Dog> Post([FromBody] Dog dog)
        {
            try
            {
                Dog createdDog = _dogsRepository.Add(dog);
                return Created("/", dog);

            }
            catch (Exception ex) when (ex is ArgumentException || ex is ArgumentNullException)
            {
                return BadRequest(ex.Message);
            }

        }


        // DELETE api/<DogsController>/5
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [HttpDelete("{id}")]
        public ActionResult<Dog> Delete(int id)
        {
            Dog? deletedDog = _dogsRepository.Delete(id);
            if (deletedDog != null)
            {
                return Ok(deletedDog);
            }
            else
            {
                return NotFound();
            }

        }
    }
}
