using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Http;

using Microsoft.EntityFrameworkCore;
using apibackend.Models;

using Microsoft.AspNetCore.Cors;

namespace apibackend
{
    [EnableCors("ReglasCors")]
    [Route("api/[controller]")]
    [ApiController]
    public class ProductoController : ControllerBase
    {
        public readonly ApiwebnetContext _dbcontext;

        public ProductoController(ApiwebnetContext _context)
        {
            _dbcontext = _context;
        }

        [HttpGet]
        [Route("Lista")]
        public IActionResult Lista()
        {
            List<Producto> lista = new List<Producto>();

            try
            {
                lista = _dbcontext.Productos.Include(c => c.IdCategoriaNavigation).ToList();
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "ok", response = lista });
            }
            catch (Exception ex)
            {

                return StatusCode(StatusCodes.Status200OK, new { mensaje = ex.Message, response = lista });

            }
        }

        [HttpGet]
        [Route("Obtener/{idProducto:int}")]
        public IActionResult Obtener(int idProducto)
        {
            Producto oProducto = _dbcontext.Productos.Find(idProducto);

            if (oProducto == null)
            {
                return StatusCode(StatusCodes.Status200OK, new { mensaje = "No existe el producto", response = oProducto });
            }

            oProducto = _dbcontext.Productos.Include(c => c.IdCategoriaNavigation).Where(p => p.IdProducto == idProducto).FirstOrDefault();
            return Ok(oProducto);
        }
    
        [HttpPost]
        [Route("Guardar")]
        public IActionResult Guardar([FromBody] Producto objeto){
            _dbcontext.Productos.Add(objeto);
            _dbcontext.SaveChanges();

             return Ok(objeto);
        }

        [HttpPut]
        [Route("Actualizar/{idProducto:int}")]
        public IActionResult Actualizar([FromBody] Producto objeto, int idProducto){

            Producto oProducto = _dbcontext.Productos.Find(idProducto);

            if(oProducto==null){
                return BadRequest(new {mensaje= "Producto No Encontrado"});
            }

            oProducto.CodigoBarra = objeto.CodigoBarra is null ? oProducto.CodigoBarra : objeto.CodigoBarra;
            oProducto.Descripcion = objeto.Descripcion is null ? oProducto.Descripcion : objeto.Descripcion;
            oProducto.Marca = objeto.Marca is null ? oProducto.Marca : objeto.Marca;
            oProducto.IdCategoria = objeto.IdCategoria is null ? oProducto.IdCategoria : objeto.IdCategoria;
            oProducto.Precio = objeto.Precio is null ? oProducto.Precio : objeto.Precio;

            Console.WriteLine(oProducto);

            _dbcontext.Productos.Update(oProducto);
            _dbcontext.SaveChanges();

            return Ok(oProducto);
            
        }
    
        [HttpDelete]
        [Route("Eliminar/{idProducto:int}")]
        public IActionResult Eliminar(int idProducto){

            Producto oProducto = _dbcontext.Productos.Find(idProducto);

            if(oProducto == null){
                return Ok(new {mensaje= "Producto No Encontrado"});
            }

            _dbcontext.Productos.Remove(oProducto);
            _dbcontext.SaveChanges();

            return Ok(new {mensaje="Producto Elimindo", producto=oProducto});
        }

    }
}