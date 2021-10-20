using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.data.Models
{
    public class InfectadoDto
    {     
        public DateTime DataNascimento;
        public string Sexo { get; set; }
        public double Latitude { get; set; }
        public double Longitude { get; set; }
    } 
    
}