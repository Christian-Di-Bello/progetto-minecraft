using System.ComponentModel.Design;

namespace progetto_minecraft
{
    internal class Program
    {
        static string[] materialiDisponibili = { "Legno", "Pietra", "Ferro", "Oro", "Carbone", "Diamante" };
        //un array di stringhe che contiene tutti i materiali che possono essere generati
        static string[] materialiInventario = new string[4];
        //un array di stringhe che serve per contenere i materiali generati
        static int[] quantitaInventario = new int[4];
        //un array di interi che indica la quantita di un materiale nel inventario
        static Random generatoreCasuale = new Random();
        //variabile per generare valori randomici

        static void Main(string[] args)
        {
            InizializzaInventario(); //chiama la funzione InizializzaInventario per inizializzare inventario
            int sceltaUtente;
            do //ciclo do while per il programma, esce se l'utente inserisce 0
            {
                sceltaUtente = MostraMenu();
                GestisciScelta(sceltaUtente);
            } while (sceltaUtente != 0);

        }
        static int MostraMenu() //funzione per mostrare il menu e chiedere input dell' utente
        {
            int scelta;
            Console.WriteLine("\n--- MENU ---");
            Console.WriteLine("1. Mostra inventario ordinato");
            Console.WriteLine("2. Cerca un materiale");
            Console.WriteLine("3. Usa una ricetta");
            Console.WriteLine("0. Esci");
            Console.Write("Scelta: ");
            string input = Console.ReadLine();
            int.TryParse(input, out scelta);
            return scelta;
        }

        static void GestisciScelta(int scelta) //funzione che gestisce input del utente e reindirizza nel operazione richiesta
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
                    Console.WriteLine("input non valido. inserisci un numero tra 0 e 3.");
                    break;
            }
        }
        static void InizializzaInventario() //inizializza l'inventario, inserendo i materiali e la quantita
        {
            for (int i = 0; i < 4; i++) //ciclo for che serve per aggiungere il materiali e quantita a ogni posizione dei array
            {
                string materialeCasuale; //una variabile temporanea che contiene il materiale generato//una variabile temporanea che contiene il materiale generato
                bool giaPresente; //una variabile che indica se il materiale generato e gia presente

                do //ciclo che genera il materiale e controlla se il materiale e gia presente
                {
                    materialeCasuale = materialiDisponibili[generatoreCasuale.Next(materialiDisponibili.Length)];
                    giaPresente = false;

                    for (int j = 0; j < i; j++) //controllo se il materiale e gia presente
                    {
                        if (materialiInventario[j] == materialeCasuale)
                        {
                            giaPresente = true;
                            break;
                        }
                    }
                } while (giaPresente);
                //in caso il materiale non e presente salva il materiale nel array e genera la quantita che varia da 1 a 64
                materialiInventario[i] = materialeCasuale;
                quantitaInventario[i] = generatoreCasuale.Next(1, 64);
            }
        }

        static void MostraInventarioOrdinato() //funzione che stampa tutti i materiali nel inventario in ordine decrescente in base alla quantita
        {
            string[] materialiOrdinati = new string[4];
            int[] quantitaOrdinata = new int[4];

            for (int i = 0; i < 4; i++) //ciclo che ordina inventario
            {
                materialiOrdinati[i] = materialiInventario[i];
                quantitaOrdinata[i] = quantitaInventario[i];
            }

            for (int i = 0; i < materialiOrdinati.Length - 1; i++) //bubble sort
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
            for (int i = 0; i < 4; i++) //ciclo per stampare l'inventario ordinato
            {
                if (!(materialiOrdinati[i] == null || materialiOrdinati[i] == ""))
                    Console.WriteLine(materialiOrdinati[i] + " --> " + quantitaOrdinata[i]);
            }
        }

        static void CercaMateriale() //funzione che cerca un materiale in base al nome
        {
            Console.Write("Inserisci il nome del materiale da cercare: ");
            string nomeMateriale = Console.ReadLine();
            bool trovato = false;

            for (int i = 0; i < 4; i++) //un ciclo che cerca il materiale nel array
            {
                if (!(materialiInventario[i] == null || materialiInventario[i] == ""))
                {
                    if (materialiInventario[i] == nomeMateriale) //se ritorna True allora e stato trovato il materiale nell' inventario e stampa il nome, la posizione e la quantita
                    {
                        trovato = true;
                        Console.WriteLine(materialiInventario[i] + " trovato in posizione " + i + " con quantità: " + quantitaInventario[i]);
                        break;
                    }
                }
            }

            if (!trovato)  //in caso non e stato trovato il materiale
                Console.WriteLine("Materiale non trovato nell'inventario.");
        }

        static void AvviaRicetta() //la funzione per simulare la crafting table
        { 
            //stampa una lista di ricette disponibili
            Console.WriteLine("\n--- Ricette Disponibili ---");
            Console.WriteLine("1. Acciaio = Ferro + Carbone");
            Console.WriteLine("2. Picozza = Legno + Pietra");
            Console.WriteLine("3. Anello = Oro + Diamante");
            Console.Write("Scegli una ricetta (1 o 2 o 3): ");

            string input = Console.ReadLine();
            int sceltaRicetta;
            if (!int.TryParse(input, out sceltaRicetta) || (sceltaRicetta != 1 && sceltaRicetta != 2 && sceltaRicetta > 3)) //in caso utente inserisce una ricetta non esistente
            {
                Console.WriteLine("Scelta non valida.");
                return;
            }

            string materiale1, materiale2, creato;
            //un if annidato che salva nelle variabili sopra i materiali che servono e il risultato della ricetta

            if (sceltaRicetta == 1)
            {
                materiale1 = "Ferro";
                materiale2 = "Carbone";
                creato = "Acciaio";
            }
            else if (sceltaRicetta == 2)
            {
                materiale1 = "Legno";
                materiale2 = "Pietra";
                creato = "Picozza";
            }
            else
            {
                materiale1 = "Oro";
                materiale2 = "Diamante";
                creato = "Anello";
            }

            int indice1 = -1;
            int indice2 = -1;

            for (int i = 0; i < 4; i++) //controlla se ci sono abbastanza risorse per fare la ricetta
            {
                if (materialiInventario[i] == materiale1)
                    indice1 = i;
                if (materialiInventario[i] == materiale2)
                    indice2 = i;
            }

            if (indice1 == -1 || indice2 == -1) //in caso non e stato trovato 1 delle 2 risorse
            {
                Console.WriteLine("Uno o entrambi i materiali richiesti non sono presenti.");
                return;
            }

            if (quantitaInventario[indice1] > 0 && quantitaInventario[indice2] > 0) //controlla se la quantita e maggiore di 0
            {
                //rimuove dal inventario 1 di entrambi le risorse
                quantitaInventario[indice1]--;
                quantitaInventario[indice2]--;

                bool aggiunto = false;
                for (int i = 0; i < 4; i++) //cerca se esiste il materiale creato e aggiunge 1
                {
                    if (materialiInventario[i] == creato)
                    {
                        quantitaInventario[i]++;
                        aggiunto = true;
                        break;
                    }
                }

                if (!aggiunto) //in caso il materiale non e presente nell' inventario
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
                    Console.WriteLine("Hai creato: " + creato);
                }
                else
                {
                    Console.WriteLine("Inventario pieno.");
                }
            }
            else
            {
                Console.WriteLine("Materiali insufficienti.");
            }
        }

    }
}




