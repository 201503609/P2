using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC2_Proyecto2.Gramatica
{
    class Funciones
    {
        public string nombreF = "", overRide = "",tipoF ="", ambitoF ="", valorF = "";
        ParseTreeNode root;

        public Funciones(string nom, string over, string tip, string am, string val, ParseTreeNode r)
        {
            this.nombreF = nom;
            this.overRide = over;
            this.tipoF = tip;
            this.ambitoF = am;
            this.valorF = val;
            this.root = r;
        }
    }
}
