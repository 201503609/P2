using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC2_Proyecto2.Gramatica
{
    class ErroresLex
    {
        public string descError = "";
        public int col = 0, fil = 0;
        //Va a tener variables 
        public static ArrayList erroresLex = new ArrayList();

        public ErroresLex() { }

        public ErroresLex(string desc, int x, int y)
        {
            this.descError = desc;
            this.col = y;
            this.fil = x;
        }

        //----
        public void insertarErrLex(ErroresLex err)
        {
            erroresLex.Add(err);
        }

        public void limpiarArregloLex()
        {
            erroresLex.Clear();
        }
    }
}
