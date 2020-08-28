using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Owner
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Relationship { get; set; }
        public ICollection<Debt> Debt { get; set; } = new List<Debt>();

        public Owner()
        {

        }

        public Owner(int id, string name, string relationship)
        {
            Id = id;
            Name = name;
            Relationship = relationship;
        }

        public void AddDebt(Debt debt)
        {
            Debt.Add(debt);
        }
        public void RemoveDebt (Debt debt)
        {
            Debt.Remove(debt);
        }

        public double TotalDebt(DateTime initial, DateTime final)
        {
            return Debt.Where(debt => debt.Date >= initial && debt.Date <= final).Sum(debt => debt.Amount); 
        }
    }
}