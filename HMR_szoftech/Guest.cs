using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HMR_szoftech
{
    class Guest: User
    {
        private string identityCardNummber;
        private List<Reservation> reservations;
        private DateTime birthDate;

        public Guest(string userName, string name, string rank, string identityCardNumber, DateTime birthDate): base(userName, name, rank)
        {
            this.identityCardNummber = identityCardNumber;
            this.birthDate = birthDate;
            reservations = new List<Reservation>();
        }
    }
}
