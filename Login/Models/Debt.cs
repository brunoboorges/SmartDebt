using Login.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Debt
    {
        public int Id { get; set; }
        public string Description { get; set; }
        public DateTime Date { get; set; }
        public double Amount { get; set; }
        public DebtStatus Status { get; set; }
        public Department Department { get; set; }
        public Owner Owner { get; set; }

        public Debt()
        {

        }

        public Debt(int id, DateTime date, double amount, DebtStatus status, Department department, Owner owner)
        {
            Id = id;
            Date = date;
            Amount = amount;
            Status = status;
            Department = department;
            Owner = owner;
        }
    }
}