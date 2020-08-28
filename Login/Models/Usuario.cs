using Login.Models;
using System;
using System.Collections.Generic;
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

        
        
        public string Nome { get; set; }
        public string Password { get; set; }
        public string SecondPass { get; set; }

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

                else { 
                
                if(firstPass.Length < 5)
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

        
    }
}