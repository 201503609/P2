using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC2_Proyecto2.Gramatica
{
    class NodoFuncion
    {
        public string nombreF = "";
        public int cantidadPar = 0;
        public ParseTreeNode root;
        public Ambito ambito;

        public NodoFuncion(string nombreF, ParseTreeNode root, Ambito ambito)
        {
            this.nombreF = nombreF;
            this.root = root;
            this.ambito = ambito;
        }

    }
}
