using System;
using System.Collections.Generic;
using System.Text;

namespace JuegoRuleta
{
    internal class Jugador
    {
        public decimal DineroInicial { get; private set; }

        public decimal Dinero { get; private set; }


        public Jugador(decimal dineroInicial)
        {
            DineroInicial = dineroInicial;
            Dinero = dineroInicial;
        }


        public void RestarDinero(decimal cantidad)
        {
            Dinero -= cantidad;
        }


        public void SumarDinero(decimal cantidad)
        {
            Dinero += cantidad;
        }


        public bool TieneDinero()
        {
            return Dinero > 0;
        }


        public decimal ObtenerGananciaPerdida()
        {
            return Dinero - DineroInicial;
        }


        public override string ToString()
        {
            return $"Dinero disponible: ${Dinero:N2}";
        }
    }
}