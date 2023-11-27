using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMR_szoftech
{
    static class Visitor
    {
        static public void registration()
        {
            Program.registration();
        }

        static public void showBasicDatas()
        {
            Console.Clear();
            Console.WriteLine("Üdvözöljük a Hotel oldalán!");
            Console.WriteLine("Hotelünk általános információit az alábbiakban olvashatja:\n");
            Console.WriteLine("Helyszín:\n\tBudapest, Hess András tér 1-3, 1014");
            Console.WriteLine("Hotel kapacitása: \n\t150 fő");
            Console.WriteLine("Földszinten található: \n\telőtér\n\ttársalgó\n\trecepció");
            Console.WriteLine("Első szinten található:\n\tcsaládi szobák");
            Console.WriteLine("Második szinten található:\n\tegyágyas szobák");
            Console.WriteLine("Harmadik szinten található:\n\tfranciaágyas szobák\n");
            Console.WriteLine("\nMinden szoba egy fürdőszobával és egy külön helységben elhelyezett illemhellyel van felszerelve.\n");
            Console.WriteLine("Elérhetőségek:\n\ttelefonszám: +36/30/204/3933\n\temail-cím: besthotel@gmail.com\n");
            Console.WriteLine("Szerettel várjuk a pihenni vágyókat!\nSok szeretettel: a Hotel csapata!");
            back();
        }

        static private void back()
        {
            Console.Write("Nyomja meg az <Enter>-t a visszalépéshez!");
            while (Console.ReadKey().Key != ConsoleKey.Enter) { };
            Console.Clear();
            Program.visitor();
        }
    }
}
