using System;

namespace JuegoRuleta
{
    internal class Program
    {
        static void Main(string[] args)
        {
            string titulo = "JUEGO DE RULETA";

            string[] opciones =
            {
                "Realizar apuesta",
                "Ver historial de giros",
                "Ver saldo"
            };

            Menu menu = new Menu(titulo, opciones);

            menu.MostrarMenu();
        }
    }
}