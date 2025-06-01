using ConFinServer.Data;
using ConFinServer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ConFinServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstadoController : ControllerBase
    {
        //private static List<Estado> Lista = new List<Estado>();
        private readonly AppDbContext _appDbContext;

        public EstadoController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public List<Estado> GetEstados()
        {
            var lista = _appDbContext.Estado.OrderBy(e => e.Sigla).ToList();
            //select * from estado order by sigla asc

            return lista;
        }

        [HttpGet("{sigla}")]
        public ActionResult<Estado> GetEstado([FromRoute] string sigla)
        {
            var estadoExiste = _appDbContext.Estado.Where(x => x.Sigla == sigla).FirstOrDefault();
            if (estadoExiste != null)
            {
                return estadoExiste;
            }
            else
            {
                return NotFound("Estado não encontrado!"); ;
            }
        }

        //TRATAMENTOS ACIMA

        [HttpPost]
        public IActionResult PostEstado([FromBody] Estado estado)
        {
            try
            {
                _appDbContext.Estado.Add(estado);
                //insert into estado (sigla,nome) values("","")
                _appDbContext.SaveChanges();

                return Ok("Estado cadastrado com sucesso!");
            }
            catch (Exception ex)
            {
                return BadRequest("Erro ao incluir estado. " + ex.Message);
            }
            

        }

        [HttpPut]
        public IActionResult PutEstado([FromBody] Estado estado)
        {
            var estadoExiste = _appDbContext.Estado.Where(x => x.Sigla == estado.Sigla).FirstOrDefault();
            if (estadoExiste != null)
            {
                try
                {
                    estadoExiste.Nome = estado.Nome;
                    _appDbContext.Estado.Update(estadoExiste);
                    //update estado set nome = "" where sigla = ""
                    _appDbContext.SaveChanges();

                    return Ok("Estado alterado com sucesso!");
                }
                catch (Exception ex) {
                    return BadRequest("Erro ao alterar estado. " + ex.Message);
                }

                
            } else
            {
                return NotFound("Estado não encontrado!");
            }
            
            
        }

        //[HttpDelete]
        //public string DeleteEstado(Estado estado)
        //{
        //    var estadoExiste = Lista.Where(x => x.Sigla == estado.Sigla).FirstOrDefault();
        //    if (estadoExiste != null)
        //    {
        //        Lista.Remove(estadoExiste);
        //        return "Estado excluído com sucesso!";
        //    }
        //    else
        //    {
        //        return "Estado não encontrado!";
        //    }


        //}

        //[HttpDelete("Exclui2")]
        //public string DeleteEstado2([FromQuery] string sigla)
        //{
        //    var estadoExiste = Lista.Where(x => x.Sigla == sigla).FirstOrDefault();
        //    if (estadoExiste != null)
        //    {
        //        Lista.Remove(estadoExiste);
        //        return "Estado excluído com sucesso!";
        //    }
        //    else
        //    {
        //        return "Estado não encontrado!";
        //    }


        //}

        //[HttpDelete("Exclui3")]
        //public string DeleteEstado3([FromHeader] string sigla)
        //{
        //    var estadoExiste = Lista.Where(x => x.Sigla == sigla).FirstOrDefault();
        //    if (estadoExiste != null)
        //    {
        //        Lista.Remove(estadoExiste);
        //        return "Estado excluído com sucesso!";
        //    }
        //    else
        //    {
        //        return "Estado não encontrado!";
        //    }


        //}

        [HttpDelete("{sigla}")]
        public IActionResult DeleteEstado([FromRoute] string sigla)
        {
            var estadoExiste = _appDbContext.Estado.Where(x => x.Sigla == sigla).FirstOrDefault();
            if (estadoExiste != null)
            {
                try
                {
                    _appDbContext.Estado.Remove(estadoExiste);
                    //delete from estado where sigla = ""
                    _appDbContext.SaveChanges();

                    return Ok("Estado excluído com sucesso!");
                } catch (Exception ex)
                {
                    return BadRequest("Erro ao excluir estado. " + ex.Message);
                }
            }
            else
            {
                return NotFound("Estado não encontrado!");
            }


        }
    }
}
