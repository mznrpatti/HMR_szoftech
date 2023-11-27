using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMR_szoftech
{
    class Reservation
    {
        private Package package;
        private Guest guest;
        string date;

        public Reservation(Package package, Guest guest, string date)
        {
            this.package = package;
            this.guest = guest;
            this.date = date;
        }

        public string getDate()
        {
            return date;
        }

        public Guest getGuest()
        {
            return guest;
        }

        public Package getPackage()
        {
            return package;
        }

        public void printDatas()
        {
            Console.WriteLine($"szoba típusa: {package.getRoomType()}, vendégek száma: {package.getNumberOfGuests()}, a csomag ára: {package.getPackagePrice()}, dátum: {package.getStartDate()}-{package.getEndDate()}");
        }
    }
}
