using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC2_Proyecto2.Gramatica
{
    class Arreglos
    {
        public string nombre, tipo, ambito, pertence;
        public static ArrayList arreglos = new ArrayList();

        //Constructor
        public Arreglos(string n, string t, string a, string p, ArrayList val1, ArrayList val2, ArrayList val3)
        {
            this.ambito = a;
            this.tipo = t;
            this.nombre = n;
            this.pertence = p;
        }

      
    }
}
