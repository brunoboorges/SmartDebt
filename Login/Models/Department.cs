using Login.Models.Enums;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;

namespace Login.Models
{
    public class Department
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public ICollection<Debt> Debts { get; set; } = new List<Debt>();


        string connectionString()
        {
            return ConfigurationManager.ConnectionStrings["Login"].ConnectionString;
        }

        




        public Department()
        {

        }

        public Department(int id, string name)
        {
            Id = id;
            Name = name;
        }

        public void AddDebt(Debt debt)
        {
            Debts.Add(debt);
        }
        public double TotalDebt (DateTime initial, DateTime final)
        {
            return Debts.Where(debt => debt.Date >= initial && debt.Date <= final).Sum(debt => debt.Amount);
        }






        List<Department> list = new List<Department>();

        // POPULAR LISTA
        public IEnumerable<Department> ShowDepartments(Department dep)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString()))
                {


                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader dr;

                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "select * from departments";
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        Department depart = new Department();
                        depart.Id = Convert.ToInt32(dr["id"]);
                        depart.Name = dr["Name"].ToString();
                        list.Add(depart);

                    }
                }
                

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }


            return list;
        }

        //DELETAR DA LISTA 

        public bool DeleteDepartment(int id)
        {


            try
            {
                using (SqlConnection con = new SqlConnection(connectionString()))
                {
                    SqlCommand cmd = new SqlCommand();
                   
                    

                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "DELETE from departments WHERE id='" + id + "'";
                    cmd.ExecuteNonQuery();
                    return true;

                    
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }


            return false;
            
        }

        public void PostarDepartamento(Department dep)
        {

            SqlConnection con = new SqlConnection(connectionString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            try
            {

                cmd.Connection = con;
                con.Open();

                cmd.CommandText = "insert into departments (Name) values ('" + dep.Name + "')";
                cmd.ExecuteNonQuery();


            }
            catch(Exception e)
            {
                Console.WriteLine(e.Message);
            }

        }

       
    }
}   