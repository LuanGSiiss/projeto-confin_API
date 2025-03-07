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
        private static List<Cidade> ListaCidades = new List<Cidade>();

        [HttpGet]
        public string Cidade()
        {
            return "Teste";
        }

        [HttpGet]
        [Route("Lista")]
        public List<Cidade> Lista()
        {
            return ListaCidades;
        }

        [HttpPost]
        public string IncluirLista([FromBody] Cidade cidade)
        {
            int novoCodigo;
            int ultimoCodigo;

            if (ListaCidades.Count == 0)
            {
                novoCodigo = 1;
            } else
            {
                ultimoCodigo = ListaCidades[ListaCidades.Count - 1].Codigo;
                novoCodigo = ultimoCodigo + 1;
            }

            cidade.Codigo = novoCodigo;
            ListaCidades.Add(cidade);
            return "Cidade Cadastrada com sucesso";
        }

        [HttpPut]
        public string AlterarEstado([FromBody] Cidade cidade)
        {
            var cidadeExiste = ListaCidades.Where(x => x.Codigo == cidade.Codigo).FirstOrDefault();
            if (cidadeExiste != null)
            {
                cidadeExiste.Nome = cidade.Nome;
                cidadeExiste.Estado = cidade.Estado;

                return "Cidade alterado com sucesso!";
            }
            else
            {
                return "Cidade não encontrado!";
            }


        }

        [HttpDelete("{codigo}")]
        public string ExcluirCidade([FromRoute] int codigo)
        {
            var cidadeExiste = ListaCidades.Where(x => x.Codigo == codigo).FirstOrDefault();
            if (cidadeExiste != null)
            {
                ListaCidades.Remove(cidadeExiste);
                return "Cidade excluído com sucesso!";
            }
            else
            {
                return "Cidade não encontrado!";
            }


        }
    }
}
