using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AppMerende
{
    public class Merenda
    {
        public string Nome { get; set; }
        public double Prezzo { get; set; }

        public override string ToString()
        {
            return $"{Nome}: {Prezzo.ToString()} Euro "; 
        }
    }
}
