using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OLC2_Proyecto2.Gramatica
{
    class ErroresSin
    {
        public string descError = "", valEsp = "";
        public int col = 0, fil = 0;
        //Va a tener variables 
        public static ArrayList erroresSin = new ArrayList();

        public ErroresSin() { }

        public ErroresSin(string desc, int x, int y, string val)
        {
            this.descError = desc;
            this.col = y;
            this.fil = x;
            this.valEsp = val;
        }

        //----
        public void insertarErrSin(ErroresSin err)
        {
            erroresSin.Add(err);
        }

        public void limpiarArregloSin()
        {
            erroresSin.Clear();
        }
    }
}
