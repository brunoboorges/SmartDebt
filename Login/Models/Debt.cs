using Login.Models.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.ComponentModel.DataAnnotations;

namespace Login.Models
{
    public class Debt
    {
        public int Id { get; set; }

        [Display(Name ="Descrição")]
        public string Description { get; set; }

        [Display(Name = "Vencimento")]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:MM/dd/yyyy}")]
        public DateTime Date { get; set; }

        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]
        public double Amount { get; set; }
        public DebtStatus Status { get; set; }
        public List<Department> list { get; set; } = new List<Department>();

        [Display(Name = "Devedor")]
        public String Owner { get; set; }

        public Debt()
        {

        }

        public Debt(int id, DateTime date, double amount, DebtStatus status, String owner)
        {
            Id = id;
            Date =  date;
            Amount = amount;
            Status = status;
            Owner = owner;
        }

        string connectionString()
        {
            return ConfigurationManager.ConnectionStrings["Login"].ConnectionString;
        }

        //POPULAR DIVIDAS
        List<Debt> dividas = new List<Debt>();
        public IEnumerable<Debt> ShowDebts(Debt debt)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString()))
                {


                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader dr;

                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "select * from debtos";
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Debt debtos = new Debt();
                        debtos.Id = Convert.ToInt32(dr["id"]);
                        debtos.Description = dr["Description"].ToString();
                        debtos.Date = Convert.ToDateTime(dr["Date"]);
                        debtos.Amount = Convert.ToDouble(dr["Amount"]);
                        debtos.Owner = dr["Owner"].ToString();

                        dividas.Add(debtos);

                    }
                }


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return dividas;
        }

        //INSERIR DIVIDA
        public void InsertDebt(Debt debt)
        {
            String debito = debt.Date.ToString("MM/dd/yyyy");
            SqlConnection con = new SqlConnection(connectionString());
            SqlCommand cmd = new SqlCommand();
            

            try
            {
                
                cmd.Connection = con;
                con.Open();

                cmd.CommandText = "insert into debtos (Description, Amount, Date, Owner) values ('" + debt.Description+ "',  '" + debt.Amount + "', '" + Convert.ToDateTime(debito) + "', '" + debt.Owner + "' )";
                cmd.ExecuteNonQuery();


            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }
        public bool RemoveDebt(int id)
        {

            SqlConnection con = new SqlConnection(connectionString());
            SqlCommand cmd = new SqlCommand();


            try
            {

                cmd.Connection = con;
                con.Open();

                cmd.CommandText = "DELETE from debtos WHERE id='"+ id +"'";
                cmd.ExecuteNonQuery();

                return true;

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }

    }
}