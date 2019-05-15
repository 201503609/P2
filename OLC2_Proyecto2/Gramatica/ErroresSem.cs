using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC2_Proyecto2.Gramatica
{
    class ErroresSem
    {
        public string descError = "";
        public int col = 0, fil = 0;
        //Va a tener variables 
        public static ArrayList erroreSeman = new ArrayList();

        public ErroresSem() { }

        public ErroresSem(string desc, int x, int y)
        {
            this.descError = desc;
            this.col = y;
            this.fil = x;
        }

        //----
        public void insertarErrSem(ErroresSem err)
        {
            erroreSeman.Add(err);
        }

        public void limpiarArregloSem()
        {
            erroreSeman.Clear();
        }
    }
}
