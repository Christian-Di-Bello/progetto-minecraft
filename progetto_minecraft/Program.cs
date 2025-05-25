namespace progetto_minecraft
{
    internal class Program
    {
        static string[] materialiDisponibili = { "Legno", "Pietra", "Ferro", "Oro", "Carbone", "Diamante" };
        static string[] materialiInventario = new string[4];
        static int[] quantitaInventario = new int[4];
        static Random generatoreCasuale = new Random();

        static void Main(string[] args)
        {
            int sceltaUtente = -1;
            while (sceltaUtente != 0)
            {
                MostraMenu();
                sceltaUtente = LeggiSceltaUtente();
                GestisciScelta(sceltaUtente);
            }

        }
        static void MostraMenu()
        {
            Console.WriteLine("\n--- MENU ---");
            Console.WriteLine("1. Mostra inventario ordinato");
            Console.WriteLine("2. Cerca un materiale");
            Console.WriteLine("3. Usa una ricetta");
            Console.WriteLine("0. Esci");
            Console.Write("Scelta: ");
        }

        static int LeggiSceltaUtente()
        {
            string input = Console.ReadLine();
            int scelta;
            if (!int.TryParse(input, out scelta) || scelta < 0 || scelta > 3)
            {
                Console.WriteLine("Input non valido. Inserisci un numero tra 0 e 3.");
                return -1;
            }
            return scelta;
        }

        static void GestisciScelta(int scelta)
        {
            switch (scelta)
            {
                case 1:
                    
                    break;
                case 2:
                    
                    break;
                case 3:
                   
                    break;
                case 0:
                    Console.WriteLine("Uscita...");
                    break;
                default:
                    break;
            }
        }

    }
}
