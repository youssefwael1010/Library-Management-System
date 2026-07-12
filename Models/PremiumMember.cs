using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sprint1
{
    public class PremiumMember : Member
    {
        public override int MaxBorrowLimit { get; }

        public override int LoanDays { get; }
        public PremiumMember(int id, string name, string email) : base(id, name, email)
        {
            MaxBorrowLimit = 10;
            LoanDays = 30;
        }
        public override string GetInfo()
        {
            return $"Premium Member: {Name}, ID : {Id}, Email: {Email}, Join Date: {JoinDate.ToShortDateString()}";

        }
    }
}
