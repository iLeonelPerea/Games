using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BarritaGirando
{
    public class Score
    {
        private int puntos;
        private string nombre;

        public int Puntos
        {
            get { return puntos; }
            set { puntos = value; }
        }
        
        public string Nombre
        {
            get { return nombre; }
            set { nombre = value; }
        }

        public Score(string n, int s) {
            puntos = s;
            nombre = n;
        }
    }
}
