using DB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using tarea_mvc.Models;

namespace tarea_mvc.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibroController : ControllerBase
    {
        private TareaMvcContext _context;

        public LibroController(TareaMvcContext context)
        {
            _context = context;   
        }

        [HttpGet]
        [Route("get")]
        public IEnumerable<Libro> GetLibros() => _context.Libro.ToList();

        [HttpPost]
        [Route("store")]
        public ActionResult Create(Libro libros)
        {

            if (!ModelState.IsValid)
            {
                return BadRequest("No se ha podido registrar.");
            }

            _context.Libro.Add(libros);
            _context.SaveChanges();

            return Ok("Registro almacenado con éxito.");
        }

        [HttpGet]
        [Route("destroy")]
        public ActionResult Destroy(int? id)
        {

            if (_context.Libro is null)
            {
                return BadRequest("El parámetro id es requerido.");
            }

            var libro = _context.Libro.Find(id);
            if (libro is null)
            {
                return BadRequest("No se han encontrado datos relacionados con la búsqueda.");
               
            }

                _context.Libro.Remove(libro);
                _context.SaveChangesAsync();

                return Ok("Registro eliminado con éxito.");
        }

        [HttpPost]
        [Route("update")]
        public ActionResult Update(Libro libro)
        {

            if (libro.id != libro.id)
            {
                return BadRequest("El parámetro id es requerido.");
            }

            if (!ModelState.IsValid)
            {
                return BadRequest("Problema interno.");
            }

            try
            {
                _context.Update(libro);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

                return BadRequest("Problema interno con la fuente de Datos.");
            }

            return Ok("Registro actualizado con éxito.");
        }
    }
}
