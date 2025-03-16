using System.ComponentModel.DataAnnotations;
using System.Configuration;

namespace ConFinServer.Model
{
    public class Cidade
    {
        [Key]
        [Range(0, int.MaxValue, ErrorMessage = "Código não pode ser negativo")]
        public int Codigo { get; set; }

        [Required(ErrorMessage = "Nome é obrigatório")]
        [StringLength(100
                     , MinimumLength = 2
                     , ErrorMessage = "Informe de 2 a 100 caracteres"
            )]
        public string Nome { get; set; }

        [StringLength(2
                     , MinimumLength = 2
                    , ErrorMessage = "Obrigatório informar 2 caracteres")]
        public string Estado { get; set; }
    }
}
