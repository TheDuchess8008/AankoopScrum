using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrulariaAankoopData.Models
{
    public class ActiecodeWijzigenViewModel
    {
        public int ActiecodeId { get; set; }
        public string Naam { get; set; } = null!;
        public DateTime GeldigVanDatum { get; set; }
        public DateTime GeldigTotDatum { get; set; }
        public bool IsEenmalig { get; set; }
    }
}
