using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Irony.Ast;
using Irony.Parsing;

namespace OLC2_Proyecto2.Gramatica
{
    class Sintactico : Grammar
    {
        public static bool analizar(String cadena)
        {
            Gramatica g = new Gramatica();
            LanguageData l = new LanguageData(g);
            Parser p = new Parser(l);
            ParseTree arbol = p.Parse(cadena);
            ParseTreeNode raiz = arbol.Root;

            if(raiz == null)
                return false;
            else
                return true;
            
        }
    }
}
