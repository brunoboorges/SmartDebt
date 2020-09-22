using Login.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Login.Models
{
    public class Usuario
    {
        string connectionString()
        {
            return ConfigurationManager.ConnectionStrings["Login"].ConnectionString;
        }


        public int Id { get; set; }
        public string Nome { get; set; }
        public string Password { get; set; }
        public string SecondPass { get; set; }
        public int OtId { get; set; }
        [Display(Name = "Outras Receitas")]
        public string OtReceitasNome { get; set; }
        [Display(Name = "Total Outras Receitas")]
        [DataType(DataType.Currency)]
        public double OtTotal { get; set; }
        [Display(Name = "Valor")]
        [DataType(DataType.Currency)]
        public double OutrasReceitas { get; set; }
        public double Receita { get; set; }

        public Usuario()
        {

        }

        public Usuario(string nome, string password)
        {
            Nome = nome;
            Password = password;
        }




        //CADASTRAR ----------------------------------------------------
        public int SignUp(Usuario user)
        {

            SqlConnection con = new SqlConnection(connectionString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            try
            {
                cmd.Connection = con;
                con.Open();

                string firstPass = user.Password;
                string secondPass = user.SecondPass;
                string login = "";

                cmd.CommandText = "select * from usuarios where usuario='" + user.Nome + "'";


                dr = cmd.ExecuteReader();


                if (dr.Read())
                {
                    login = dr["usuario"].ToString();

                }
                if (user.Nome.Length < 5)
                {
                    return 4;
                }
                dr.Dispose();
                //SE JA EXISTIR USUARIO, RETORNE: "JÁ EXISTE ESTE NOME DE USUARIO."
                if (login == user.Nome)
                {
                    return 1;
                }

                else
                {

                    if (firstPass.Length < 5)
                    {
                        return 5;
                    }

                    if (firstPass == secondPass)
                    {

                        cmd.CommandText = "insert into usuarios (usuario, senha) values ('" + user.Nome + "', '" + user.Password + "' )";
                        dr = cmd.ExecuteReader();
                        //SE LOGIN E SENHA PASSAR RETORNE: "USUARIO CADASTRADO COM SUCESSO"!
                        return 2;

                    }
                    else
                    {
                        //SE SENHA NÃO PASSAR, RETORN: "SENHAS NÃO COMBINAM"
                        return 3;

                    }
                }


            }



            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return 4;
        }


        //LOGAR ----------------------------------------------------
        public bool Login(Usuario user)
        {

            SqlConnection con = new SqlConnection(connectionString());
            SqlCommand cmd = new SqlCommand();
            SqlDataReader dr;

            string userDb = "";
            string senhaDb = "";

            try
            {


                cmd.Connection = con;
                con.Open();

                cmd.CommandText = "select * from usuarios where usuario='" + user.Nome + "' and senha='" + user.Password + "'";
                dr = cmd.ExecuteReader();

                while (dr.Read())
                {
                    userDb = dr["usuario"].ToString();
                    senhaDb = dr["senha"].ToString();
                }
                if (user.Nome == userDb && user.Password == senhaDb)
                {
                    return true;
                }
                else
                {
                    return false;
                }

            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return false;
        }
        //ADICIONAR RECEITAS
        public void InserirReceita(Usuario user, string nome)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader dr;


                    cmd.CommandText = "UPDATE usuarios SET receita=('" + user.Receita + "') where usuario = '" + nome + "'";
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        string receita = dr["receita"].ToString();
                    }

                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }



        }
        //LER OUTRAS RECEITAS
        List<Usuario> otReceitas = new List<Usuario>();
        public IEnumerable<Usuario> LerOutrasReceitas(Usuario user, string nome)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();
                    SqlDataReader dr;


                    cmd.CommandText = "select * from otReceitas";
                    dr = cmd.ExecuteReader();
                    while (dr.Read())
                    {
                        Usuario usuario = new Usuario();
                        usuario.OtId = Convert.ToInt32(dr["id"]);
                        usuario.OutrasReceitas = Convert.ToDouble(dr["valor"]);
                        usuario.OtReceitasNome = dr["description"].ToString();
                        otReceitas.Add(usuario);
                    }
                    return otReceitas;
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }



        }
        //ADICIONAR OUTRAS RECEITAS

        public void InserirOutrasReceitas(Usuario user)
        {

            try
            {
                using (SqlConnection con = new SqlConnection(connectionString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "INSERT INTO otReceitas (description, valor) values ('" + user.OtReceitasNome + "', '" + user.OutrasReceitas + "')";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }



        }

        //PUXAR RECEITAS
        public double ReceitaMensal(string user)
        {
            double receita = 0;
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader dr;
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "select * from usuarios where usuario='" + user + "' ";
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {
                        receita = Convert.ToDouble(dr["receita"]);
                        return receita;
                    }

                }
            }
            catch (Exception e)
            {

                throw new Exception(e.Message);
            }


            return receita;
        }
        //EXCLUIR OT RECEITAS
        public void OtDelete(int id)
        {
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString()))
                {
                    SqlCommand cmd = new SqlCommand();
                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "DELETE FROM otReceitas where id = '" + id + "'";
                    cmd.ExecuteNonQuery();
                }
            }
            catch (Exception)
            {

                throw;
            }
        }


        //SOMAR OUTRAS RECEITAS
        public double SumOtReceitas(Usuario user)
        {
            
            List<Usuario> lista = new List<Usuario>();
            try
            {
                using (SqlConnection con = new SqlConnection(connectionString()))
                {

                    SqlCommand cmd = new SqlCommand();
                    SqlDataReader dr;

                    cmd.Connection = con;
                    con.Open();

                    cmd.CommandText = "select valor from otReceitas";
                    dr = cmd.ExecuteReader();

                    while (dr.Read())
                    {

                        Usuario usuario = new Usuario();
                        usuario.OutrasReceitas = Convert.ToDouble(dr["valor"]);
                        lista.Add(usuario);

                        Receita = Receita + usuario.OutrasReceitas;


                    }
                }
            }
            catch (Exception e)
            {
                throw new Exception(e.Message);
            }

            return Receita;
        }







    }
}