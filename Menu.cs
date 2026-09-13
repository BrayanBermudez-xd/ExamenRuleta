using System;
using System.Collections.Generic;
using System.Text;

namespace JuegoRuleta
{
    internal class Menu
    {
        private string Titulo;

        private string[] Opciones;


        public ManejadorRuleta Manejador { get; set; }


        public Menu(
            string titulo,
            string[] opciones)
        {
            Titulo = titulo;

            Opciones = opciones;

            Manejador =
                new ManejadorRuleta();
        }


        public void MostrarMenu()
        {
            bool continuar = true;


            while (
                continuar &&
                Manejador.Jugador.TieneDinero()
            )
            {
                Console.Clear();

                Console.WriteLine(Titulo);
                Console.WriteLine(
                    new string('=', Titulo.Length)
                );

                Console.WriteLine();

                Console.WriteLine(
                    $"Saldo actual: " +
                    $"${Manejador.Jugador.Dinero:N2}"
                );

                Console.WriteLine();


                for (int i = 0;
                     i < Opciones.Length;
                     i++)
                {
                    Console.WriteLine(
                        $"{i + 1}. {Opciones[i]}"
                    );
                }


                Console.WriteLine("0. Retirarse");

                Console.WriteLine();

                Console.Write(
                    "Selecciona una opcion: "
                );


                string opcion =
                    Console.ReadLine();


                switch (opcion)
                {
                    case "1":

                        MostrarApostar();

                        break;


                    case "2":

                        MostrarHistorial();

                        break;


                    case "3":

                        MostrarSaldo();

                        break;


                    case "0":

                        continuar = false;

                        break;


                    default:

                        Console.WriteLine();

                        Console.WriteLine(
                            "Opcion invalida."
                        );

                        Pausar();

                        break;
                }
            }


            if (!Manejador.Jugador.TieneDinero())
            {
                Console.Clear();

                Console.WriteLine(
                    "TE HAS QUEDADO SIN DINERO"
                );

                Console.WriteLine();

                Console.WriteLine(
                    "El juego ha terminado automaticamente."
                );

                Pausar();
            }


            MostrarResultadoFinal();
        }


        private void MostrarApostar()
        {
            Console.Clear();

            Console.WriteLine(
                "REALIZAR APUESTA"
            );

            Console.WriteLine(
                "================"
            );

            Console.WriteLine();

            Console.WriteLine(
                $"Saldo disponible: " +
                $"${Manejador.Jugador.Dinero:N2}"
            );

            Console.WriteLine();

            Console.WriteLine(
                "1. Apostar a un numero (x10)"
            );

            Console.WriteLine(
                "2. Apostar a un color (x5)"
            );

            Console.WriteLine(
                "3. Apostar a Par o Impar (x2)"
            );

            Console.WriteLine();


            int tipo =
                PedirValorEnteroEntre(
                    "Tipo de apuesta",
                    1,
                    3
                );


            decimal monto =
                PedirMonto();


            Apuesta apuesta = null;


            switch (tipo)
            {
                case 1:

                    int numero =
                        PedirValorEnteroEntre(
                            "Numero (0 - 36)",
                            0,
                            36
                        );


                    apuesta =
                        Manejador.CrearApuestaNumero(
                            monto,
                            numero
                        );

                    break;


                case 2:

                    string color =
                        PedirColor();


                    apuesta =
                        Manejador.CrearApuestaColor(
                            monto,
                            color
                        );

                    break;


                case 3:

                    string paridad =
                        PedirParidad();


                    apuesta =
                        Manejador.CrearApuestaParidad(
                            monto,
                            paridad
                        );

                    break;
            }


            Console.WriteLine();

            Console.WriteLine(
                "APUESTA REALIZADA"
            );

            Console.WriteLine(
                "================="
            );

            Console.WriteLine(apuesta);


            Console.WriteLine();

            Console.WriteLine(
                "Girando la ruleta..."
            );


            bool gano =
                Manejador.RealizarApuesta(
                    apuesta,
                    out Giro giro,
                    out decimal premio
                );


            Console.WriteLine();

            Console.WriteLine(
                "RESULTADO DE LA RULETA"
            );

            Console.WriteLine(
                "====================="
            );

            Console.WriteLine(giro);


            Console.WriteLine();


            if (gano)
            {
                Console.WriteLine(
                    "¡GANASTE!"
                );

                Console.WriteLine(
                    $"Premio: ${premio:N2}"
                );
            }
            else
            {
                Console.WriteLine(
                    "Perdiste la apuesta."
                );
            }


            Console.WriteLine();

            Console.WriteLine(
                $"Saldo actual: " +
                $"${Manejador.Jugador.Dinero:N2}"
            );


            Pausar();
        }


        private decimal PedirMonto()
        {
            while (true)
            {
                Console.WriteLine();

                Console.Write(
                    "Cantidad a apostar: $"
                );


                string entrada =
                    Console.ReadLine();


                decimal monto;


                if (!decimal.TryParse(
                    entrada,
                    out monto))
                {
                    Console.WriteLine(
                        "Ingresa una cantidad valida."
                    );

                    continue;
                }


                if (monto <= 0)
                {
                    Console.WriteLine(
                        "La cantidad debe ser mayor a $0."
                    );

                    continue;
                }


                if (monto % 10 != 0)
                {
                    Console.WriteLine(
                        "La apuesta debe ser multiplo de $10."
                    );

                    continue;
                }


                if (monto >
                    Manejador.Jugador.Dinero)
                {
                    Console.WriteLine(
                        "No tienes suficiente dinero."
                    );

                    continue;
                }


                return monto;
            }
        }


        private int PedirValorEnteroEntre(
            string mensaje,
            int minimo,
            int maximo)
        {
            while (true)
            {
                Console.Write(
                    $"{mensaje}: "
                );


                string entrada =
                    Console.ReadLine();


                int valor;


                if (!int.TryParse(
                    entrada,
                    out valor))
                {
                    Console.WriteLine(
                        "Debes ingresar un numero entero."
                    );

                    continue;
                }


                if (
                    valor < minimo ||
                    valor > maximo
                )
                {
                    Console.WriteLine(
                        $"Ingresa un valor entre " +
                        $"{minimo} y {maximo}."
                    );

                    continue;
                }


                return valor;
            }
        }


        private string PedirColor()
        {
            while (true)
            {
                Console.WriteLine();

                Console.WriteLine(
                    "1. Rojo"
                );

                Console.WriteLine(
                    "2. Negro"
                );

                Console.WriteLine();

                Console.Write(
                    "Selecciona un color: "
                );


                string opcion =
                    Console.ReadLine();


                if (opcion == "1")
                {
                    return "Rojo";
                }


                if (opcion == "2")
                {
                    return "Negro";
                }


                Console.WriteLine(
                    "Opcion invalida."
                );
            }
        }


        private string PedirParidad()
        {
            while (true)
            {
                Console.WriteLine();

                Console.WriteLine(
                    "1. Par"
                );

                Console.WriteLine(
                    "2. Impar"
                );

                Console.WriteLine();

                Console.Write(
                    "Selecciona una opcion: "
                );


                string opcion =
                    Console.ReadLine();


                if (opcion == "1")
                {
                    return "Par";
                }


                if (opcion == "2")
                {
                    return "Impar";
                }


                Console.WriteLine(
                    "Opcion invalida."
                );
            }
        }


        private void MostrarHistorial()
        {
            Console.Clear();

            Manejador.ListarGiros();

            Pausar();
        }


        private void MostrarSaldo()
        {
            Console.Clear();

            Console.WriteLine(
                "INFORMACION DEL JUGADOR"
            );

            Console.WriteLine(
                "======================="
            );

            Console.WriteLine();

            Console.WriteLine(
                $"Dinero inicial: " +
                $"${Manejador.Jugador.DineroInicial:N2}"
            );

            Console.WriteLine(
                $"Dinero actual: " +
                $"${Manejador.Jugador.Dinero:N2}"
            );


            decimal diferencia =
                Manejador.Jugador
                    .ObtenerGananciaPerdida();


            if (diferencia > 0)
            {
                Console.WriteLine(
                    $"Ganancia actual: " +
                    $"${diferencia:N2}"
                );
            }
            else if (diferencia < 0)
            {
                Console.WriteLine(
                    $"Perdida actual: " +
                    $"${Math.Abs(diferencia):N2}"
                );
            }
            else
            {
                Console.WriteLine(
                    "No has ganado ni perdido dinero."
                );
            }


            Pausar();
        }


        private void MostrarResultadoFinal()
        {
            Console.Clear();

            decimal resultado =
                Manejador.Jugador
                    .ObtenerGananciaPerdida();


            Console.WriteLine(
                "RESULTADO FINAL"
            );

            Console.WriteLine(
                "==============="
            );

            Console.WriteLine();

            Console.WriteLine(
                $"Dinero inicial: " +
                $"${Manejador.Jugador.DineroInicial:N2}"
            );

            Console.WriteLine(
                $"Dinero final: " +
                $"${Manejador.Jugador.Dinero:N2}"
            );

            Console.WriteLine(
                $"Giros realizados: " +
                $"{Manejador.CantidadGiros()}"
            );

            Console.WriteLine();


            if (resultado > 0)
            {
                Console.WriteLine(
                    $"Ganaste ${resultado:N2}."
                );
            }
            else if (resultado < 0)
            {
                Console.WriteLine(
                    $"Perdiste " +
                    $"${Math.Abs(resultado):N2}."
                );
            }
            else
            {
                Console.WriteLine(
                    "Terminaste con el mismo dinero " +
                    "con el que comenzaste."
                );
            }


            Console.WriteLine();

            Console.WriteLine(
                "Gracias por jugar."
            );
        }


        private void Pausar()
        {
            Console.WriteLine();

            Console.Write(
                "Presiona ENTER para continuar..."
            );

            Console.ReadLine();
        }
    }
}