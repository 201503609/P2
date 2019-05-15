using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC2_Proyecto2.Gramatica
{
    class Nodo
    {
        public ParseTreeNode root;
        public Ambito ambito;

        public Nodo(ParseTreeNode root, Ambito ambito)
        {
            this.root = root;
            this.ambito = ambito;
        }

    }

}
