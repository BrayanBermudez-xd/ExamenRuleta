using System;
using System.Collections.Generic;
using System.Text;

namespace JuegoRuleta
{
    internal class Giro
    {
        public int Numero { get; set; }

        public string Color { get; set; }


        public Giro(int numero, string color)
        {
            Numero = numero;
            Color = color;
        }


        public string ObtenerParidad()
        {
            if (Numero == 0)
            {
                return "Sin paridad";
            }

            if (Numero % 2 == 0)
            {
                return "Par";
            }

            return "Impar";
        }


        public override string ToString()
        {
            return $"Numero: {Numero} | Color: {Color} | {ObtenerParidad()}";
        }
    }
}