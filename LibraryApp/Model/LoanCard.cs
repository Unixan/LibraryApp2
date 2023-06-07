using System;
using System.Runtime.InteropServices.JavaScript;
using Microsoft.VisualBasic;

namespace LibraryApp.Model
{
    public class LoanCard
    {
        public DateOnly DateIssued { get; set; }

        public LoanCard()
        {
            DateIssued = new DateOnly(DateTime.Today.Year, DateTime.Today.Month, DateTime.Today.Day).AddYears(1);
        }
    }
}
