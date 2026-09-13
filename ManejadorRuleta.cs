using System;
using System.Collections.Generic;
using System.Text;


namespace JuegoRuleta
{
    internal class ManejadorRuleta
    {
        private List<Giro> ListaGiros;

        private Random Random;

        private List<int> NumerosNegros;


        public Jugador Jugador { get; set; }


        public ManejadorRuleta()
        {
            ListaGiros = new List<Giro>();

            Random = new Random();

            NumerosNegros = new List<int>()
            {
                2, 4, 6, 8, 10, 11, 13, 15, 17,
                20, 22, 24, 26, 28, 29, 31, 33, 35
            };

            Jugador = new Jugador(300);
        }


        public bool ValidarMonto(decimal monto)
        {
            if (monto <= 0)
            {
                return false;
            }

            if (monto % 10 != 0)
            {
                return false;
            }

            if (monto > Jugador.Dinero)
            {
                return false;
            }

            return true;
        }


        public Apuesta CrearApuestaNumero(
            decimal monto,
            int numero)
        {
            Apuesta apuesta = new Apuesta(
                monto,
                "Numero",
                numero.ToString(),
                10
            );

            return apuesta;
        }


        public Apuesta CrearApuestaColor(
            decimal monto,
            string color)
        {
            Apuesta apuesta = new Apuesta(
                monto,
                "Color",
                color,
                5
            );

            return apuesta;
        }


        public Apuesta CrearApuestaParidad(
            decimal monto,
            string paridad)
        {
            Apuesta apuesta = new Apuesta(
                monto,
                "Paridad",
                paridad,
                2
            );

            return apuesta;
        }


        public Giro GirarRuleta()
        {
            int numero = Random.Next(0, 37);

            string color = ObtenerColor(numero);

            Giro giro = new Giro(
                numero,
                color
            );

            ListaGiros.Add(giro);

            return giro;
        }


        private string ObtenerColor(int numero)
        {
            if (numero == 0)
            {
                return "Sin color";
            }


            if (NumerosNegros.Contains(numero))
            {
                return "Negro";
            }


            return "Rojo";
        }


        public bool RealizarApuesta(
            Apuesta apuesta,
            out Giro giro,
            out decimal premio)
        {
            premio = 0;


            if (!ValidarMonto(apuesta.Monto))
            {
                giro = null;

                return false;
            }


            Jugador.RestarDinero(
                apuesta.Monto
            );


            giro = GirarRuleta();


            bool gano = EsApuestaGanadora(
                apuesta,
                giro
            );


            if (gano)
            {
                premio = apuesta.CalcularPremio();

                Jugador.SumarDinero(
                    premio
                );
            }


            return gano;
        }


        private bool EsApuestaGanadora(
            Apuesta apuesta,
            Giro giro)
        {
            if (apuesta.Tipo == "Numero")
            {
                int numeroElegido =
                    Convert.ToInt32(apuesta.Seleccion);

                return giro.Numero == numeroElegido;
            }


            if (apuesta.Tipo == "Color")
            {
                return giro.Color == apuesta.Seleccion;
            }


            if (apuesta.Tipo == "Paridad")
            {
               
                 

                if (giro.Numero == 0)
                {
                    return false;
                }


                if (apuesta.Seleccion == "Par")
                {
                    return giro.Numero % 2 == 0;
                }


                if (apuesta.Seleccion == "Impar")
                {
                    return giro.Numero % 2 != 0;
                }
            }


            return false;
        }


        public void ListarGiros()
        {
            Console.WriteLine();
            Console.WriteLine("HISTORIAL DE GIROS");
            Console.WriteLine("==================");


            if (ListaGiros.Count == 0)
            {
                Console.WriteLine(
                    "Todavia no se ha realizado ningun giro."
                );

                return;
            }


            for (int i = 0;
                 i < ListaGiros.Count;
                 i++)
            {
                Console.WriteLine(
                    $"Giro #{i + 1}: {ListaGiros[i]}"
                );
            }
        }


        public int CantidadGiros()
        {
            return ListaGiros.Count;
        }
    }
}