using ConFinServer.Data;
using ConFinServer.Model;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Drawing;

namespace ConFinServer.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CidadeController : ControllerBase
    {
        //private static List<Cidade> ListaCidades = new List<Cidade>();
        private readonly AppDbContext _appDbContext;

        public CidadeController(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        [HttpGet]
        public List<Cidade> GetCidade()
        {
            var lista = _appDbContext.Cidade.OrderBy(x => x.Codigo).ToList();
            
            return lista;
        }

        //[HttpGet]
        //[Route("Lista")]
        //public List<Cidade> Lista()
        //{
        //    return ListaCidades;
        //}

        [HttpPost]
        public IActionResult PostCidade([FromBody] Cidade cidade)
        {
            var lista = _appDbContext.Cidade.OrderBy(x => x.Codigo).ToList();

            int novoCodigo;
            int ultimoCodigo;

            if (lista.Count == 0)
            {
                novoCodigo = 1;
            }
            else
            {
                ultimoCodigo = lista[lista.Count - 1].Codigo;
                novoCodigo = ultimoCodigo + 1;
            }

            try
            {
                cidade.Codigo = novoCodigo;
                _appDbContext.Cidade.Add(cidade);
                _appDbContext.SaveChanges();

                return Ok("Cidade Cadastrada com sucesso");
            }
            catch (Exception ex) 
            {
                return BadRequest("Erro ao incluir Cidade. " + ex.Message);
            }
            
        }

        [HttpPut]
        public IActionResult PutEstado([FromBody] Cidade cidade)
        {
            var cidadeExiste = _appDbContext.Cidade.Where(x => x.Codigo == cidade.Codigo).FirstOrDefault();
            if (cidadeExiste != null)
            {
                try
                {
                    cidadeExiste.Nome = cidade.Nome;
                    cidadeExiste.Estado = cidade.Estado;
                    _appDbContext.Cidade.Update(cidadeExiste);
                    _appDbContext.SaveChanges();

                    return Ok("Cidade alterado com sucesso!");
                }
                catch (Exception ex) 
                {
                    return BadRequest("Erro ao alterar Cidade. " + ex.Message);
                }
            }
            else
            {
                return NotFound("Cidade não encontrado!");
            }


        }

        [HttpDelete("{codigo}")]
        public IActionResult DeleteCidade([FromRoute] int codigo)
        {
            var cidadeExiste = _appDbContext.Cidade.Where(x => x.Codigo == codigo).FirstOrDefault();
            if (cidadeExiste != null)
            {

                try
                {
                    _appDbContext.Cidade.Remove(cidadeExiste);
                    _appDbContext.SaveChanges();

                    return Ok("Cidade excluído com sucesso!");
                }
                catch (Exception ex)
                { 
                    return BadRequest("Erro ao excluir Cidade. " + ex.Message);
                }
            }
            else
            {
                return NotFound("Cidade não encontrado!");
            }

        }
    }
}
