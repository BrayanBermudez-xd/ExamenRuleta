using System;
using System.Collections.Generic;
using System.Text;


namespace JuegoRuleta
{
    internal class Apuesta
    {
        public decimal Monto { get; set; }

        public string Tipo { get; set; }

        public string Seleccion { get; set; }

        public int Multiplicador { get; set; }


        public Apuesta(
            decimal monto,
            string tipo,
            string seleccion,
            int multiplicador)
        {
            Monto = monto;
            Tipo = tipo;
            Seleccion = seleccion;
            Multiplicador = multiplicador;
        }


        public decimal CalcularPremio()
        {
            return Monto * Multiplicador;
        }


        public override string ToString()
        {
            return $"Tipo: {Tipo} | " +
                   $"Seleccion: {Seleccion} | " +
                   $"Monto: ${Monto:N2} | " +
                   $"Multiplicador: x{Multiplicador}";
        }
    }
}