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
            Console.WriteLine("Ezek az alapadatok!");
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
