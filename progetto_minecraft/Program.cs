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
            InizializzaInventario();
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
                    MostraInventarioOrdinato();
                    break;
                case 2:
                    CercaMateriale();
                    break;
                case 3:
                    AvviaRicetta();
                    break;
                case 0:
                    Console.WriteLine("Uscita...");
                    break;
                default:
                    break;
            }
        }
        static void InizializzaInventario()
        {
            for (int i = 0; i < 4; i++)
            {
                string materialeCasuale;
                bool giaPresente;

                do
                {
                    materialeCasuale = materialiDisponibili[generatoreCasuale.Next(materialiDisponibili.Length)];
                    giaPresente = false;

                    for (int j = 0; j < i; j++)
                    {
                        if (materialiInventario[j] == materialeCasuale)
                        {
                            giaPresente = true;
                            break;
                        }
                    }
                } while (giaPresente);

                materialiInventario[i] = materialeCasuale;
                quantitaInventario[i] = generatoreCasuale.Next(5, 21);
            }
        }

        static void MostraInventarioOrdinato()
        {
            string[] materialiOrdinati = new string[4];
            int[] quantitaOrdinata = new int[4];

            for (int i = 0; i < 4; i++)
            {
                materialiOrdinati[i] = materialiInventario[i];
                quantitaOrdinata[i] = quantitaInventario[i];
            }

            for (int i = 0; i < materialiOrdinati.Length - 1; i++)
            {
                for (int j = i + 1; j < materialiOrdinati.Length; j++)
                {
                    if (quantitaOrdinata[j] > quantitaOrdinata[i])
                    {
                        int tempQ = quantitaOrdinata[i];
                        quantitaOrdinata[i] = quantitaOrdinata[j];
                        quantitaOrdinata[j] = tempQ;

                        string tempM = materialiOrdinati[i];
                        materialiOrdinati[i] = materialiOrdinati[j];
                        materialiOrdinati[j] = tempM;
                    }
                }
            }

            Console.WriteLine("\n--- Inventario Ordinato ---");
            for (int i = 0; i < 4; i++)
            {
                if (!(materialiOrdinati[i] == null || materialiOrdinati[i] == ""))
                    Console.WriteLine(materialiOrdinati[i] + " --> " + quantitaOrdinata[i]);
            }
        }

        static void CercaMateriale()
        {
            Console.Write("Inserisci il nome del materiale da cercare: ");
            string nomeMateriale = Console.ReadLine();
            bool trovato = false;

            for (int i = 0; i < 4; i++)
            {
                if (!(materialiInventario[i] == null || materialiInventario[i] == ""))
                {
                    if (materialiInventario[i] == nomeMateriale)
                    {
                        trovato = true;
                        Console.WriteLine(materialiInventario[i] + " trovato in posizione " + i + " con quantità: " + quantitaInventario[i]);
                        break;
                    }
                }
            }

            if (!trovato)
                Console.WriteLine("Materiale non trovato nell'inventario.");
        }

        static void AvviaRicetta()
        {
            Console.WriteLine("\n--- Ricette Disponibili ---");
            Console.WriteLine("1. Acciaio = Ferro + Carbone");
            Console.WriteLine("2. Picozza = Legno + Pietra");
            Console.Write("Scegli una ricetta (1 o 2): ");

            string input = Console.ReadLine();
            int sceltaRicetta;
            if (!int.TryParse(input, out sceltaRicetta) || (sceltaRicetta != 1 && sceltaRicetta != 2))
            {
                Console.WriteLine("Scelta non valida.");
                return;
            }

            string materiale1, materiale2, creato;

            if (sceltaRicetta == 1)
            {
                materiale1 = "Ferro";
                materiale2 = "Carbone";
                creato = "Acciaio";
            }
            else
            {
                materiale1 = "Legno";
                materiale2 = "Pietra";
                creato = "Picozza";
            }

            int indice1 = -1;
            int indice2 = -1;

            for (int i = 0; i < 4; i++)
            {
                if (materialiInventario[i] == materiale1)
                    indice1 = i;
                if (materialiInventario[i] == materiale2)
                    indice2 = i;
            }

            if (indice1 == -1 || indice2 == -1)
            {
                Console.WriteLine("Uno o entrambi i materiali richiesti non sono presenti.");
                return;
            }

            if (quantitaInventario[indice1] > 0 && quantitaInventario[indice2] > 0)
            {
                quantitaInventario[indice1]--;
                quantitaInventario[indice2]--;

                bool aggiunto = false;
                for (int i = 0; i < 4; i++)
                {
                    if (materialiInventario[i] == creato)
                    {
                        quantitaInventario[i]++;
                        aggiunto = true;
                        break;
                    }
                }

                if (!aggiunto)
                {
                    for (int i = 0; i < 4; i++)
                    {
                        if (quantitaInventario[i] == 0 || materialiInventario[i] == null || materialiInventario[i] == "")
                        {
                            materialiInventario[i] = creato;
                            quantitaInventario[i] = 1;
                            aggiunto = true;
                            break;
                        }
                    }
                }

                if (!aggiunto)
                {
                    Console.WriteLine("Inventario pieno.");
                }
                else
                {
                    Console.WriteLine("Hai creato: " + creato);
                }
            }
            else
            {
                Console.WriteLine("Materiali insufficienti.");
            }
        }


    }
}


