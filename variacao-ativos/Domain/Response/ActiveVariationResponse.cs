using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VariacaoAtivos.Domain.Response
{
    public class ActiveVariationResponse
    {
        public int Dia { get; set; }
        public DateTime Data { get; set; }
        public decimal Valor { get; set; }
        public decimal VariacaoDMenosUM { get; set; }
        public DateTime VariacaoPrimeiraData { get; set; }
    }
}
