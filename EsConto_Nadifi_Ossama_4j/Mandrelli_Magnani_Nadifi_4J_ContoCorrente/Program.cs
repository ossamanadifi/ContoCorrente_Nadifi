using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mandrelli_Magnani_Nadifi_4J_ContoCorrente
{
    class Program
    {
        static void Main(string[] args)
        {
            Banca DB = new Banca("Deutsche Bank", "viale Principe Amedeo");


            Console.WriteLine("Benvenuto alla Deutsche Bank. Crea un profilo per usufruire dei nostri servizi.");
            Console.WriteLine("Nome : ");
            string nome = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Cognome : ");
            string cognome = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Codice Fiscale : ");
            string codiceFiscale = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Indirizzo : ");
            string indirizzo = Convert.ToString(Console.ReadLine());
            Console.WriteLine("Numero di Cellulare : ");
            string telefono = Convert.ToString(Console.ReadLine());
            

            Intestatario A = new Intestatario(nome, cognome, codiceFiscale, indirizzo, telefono);
            Console.WriteLine("Buongiorno " + nome + " " + cognome + ". Inserisca i dati del suo conto.");

            Console.WriteLine("Iban :");
            string iban = Convert.ToString(Console.ReadLine());

            Console.WriteLine("Saldo :");
            double saldo = Convert.ToDouble(Console.ReadLine());

            ContoCorrente AC = new ContoCorrente(iban, saldo);

            double importo = 0;
            int minMov =  0;
            double tassa = 0.50;
            int maxMov = 50;
            int scelta;
            DateTime dataMov = DateTime.Today;
            DB.AddConto(AC);
            Console.WriteLine("Intestatario: " + A.getNome() + " " + A.getCognome() + "\nAbita in via: " + A.getIndirizzo() + "\nNumero di telefono: " + A.getTelefono());
            Console.WriteLine("Stampa del saldo di " + A.getNome() + " " + A.getCognome() + ": " + AC.getSaldo());

            Console.WriteLine("Benvenuto. Che cosa vuole fare?");

            do
            {

                Console.WriteLine("1)Prelievo");
                Console.WriteLine("2)Versamento");
                Console.WriteLine("3)Bonifico");
                Console.WriteLine("4)Stampa saldo del conto");
                Console.WriteLine("5)Esci");

                scelta = Convert.ToInt32(Console.ReadLine());
                switch (scelta)
                {
                    case 1:

                            Console.WriteLine("Inserisci l'importo del prelievo(Deve essere minore del saldo)");
                            importo = Convert.ToDouble(Console.ReadLine());

                        if (minMov > maxMov)
                        {
                            importo = importo + tassa;
                        }
                        if (AC.getSaldo() > importo)
                        {
                            Prelievo P = new Prelievo(importo,dataMov, AC, null);
                            AC.AddMovimenti(P);
                            P.Sommare(AC);
                            Console.WriteLine("Prelievo riuscito");
                            Console.WriteLine(" ");
                            minMov++;
                        }
                        else
                        {

                            Console.WriteLine("Non è possibile effettuare un prelievo con importo maggiore al saldo");
                            Console.WriteLine(" ");
                        }

                        minMov++;
                        break;
                    case 2:
                        Console.WriteLine("Inserisci l'importo del versamento");
                        importo = Convert.ToDouble(Console.ReadLine());
                        if (minMov > maxMov)
                        {
                            importo = importo + tassa;
                        }
                        Versamento V = new Versamento(importo, dataMov, AC, null);

                        AC.AddMovimenti(V);
                        V.Sommare(AC);
                        minMov++;
                        Console.WriteLine("Versamento riuscito");
                        Console.WriteLine(" ");
                        break;
                    case 3:
                        Console.WriteLine("Inserisci l'Iban del destinatario");
                        string ibanTo = Convert.ToString(Console.ReadLine());
                        ContoCorrente AC2 = new ContoCorrente(ibanTo,0);
                        DB.AddConto(AC2);
                        Console.WriteLine("Inserisci l'importo del bonifico");
                        importo = Convert.ToDouble(Console.ReadLine());
                        if (minMov > maxMov)
                        {
                            importo = importo + tassa;
                        }
                        if (AC.getSaldo() > importo)
                        {
                            Bonifico B = new Bonifico(AC, importo, dataMov, null, null);
                            AC.AddMovimenti(B);
                            B.EseguiBonifico(AC, AC2);
                            Console.WriteLine("Bonifico riuscito");
                            Console.WriteLine(" ");
                            minMov++;
                        }
                        else
                        {

                            Console.WriteLine("Non è possibile effettuare un bonifio con importo maggiore al saldo");
                            Console.WriteLine(" ");
                        }


                        break;
                    case 4:
                        Console.WriteLine("Saldo: " + AC.getSaldo());
      
                        Console.WriteLine(" ");
                        break;
                    default:
                        Console.WriteLine("Inserisci una delle azioni elencate");
                        Console.WriteLine(" ");
                        break;
                }
            } while (scelta != 5);
                        Console.WriteLine("Grazie per aver usufruito dei nostri servizi, buona giornata");

            Console.ReadLine();
        }
    }
}
