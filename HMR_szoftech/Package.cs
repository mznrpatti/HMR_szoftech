using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMR_szoftech
{
    class Package
    {
        private string roomType;
        private int numberOfGuests;
        private int packagePrice;
        private string startDate;
        private string endDate;

        public Package(string roomType, int numberOfGuests, int packagePrice, string startDate, string endDate)
        {
            this.roomType = roomType;
            this.numberOfGuests = numberOfGuests;
            this.packagePrice = packagePrice;
            this.startDate = startDate;
            this.endDate = endDate;
        }

        public string getRoomType() {  return roomType; }

        public int getNumberOfGuests() {  return numberOfGuests; }

        public int getPackagePrice() {  return packagePrice; }

        public string getStartDate() { return startDate; }  

        public string getEndDate() { return endDate; }  

        public void printPackageDatas()
        {
            Console.WriteLine($"szoba típusa: {roomType}, vendégek száma: {numberOfGuests}, csomag ára: {packagePrice}, dátum: {startDate}-{endDate}");
        }

    }
}
