using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;



namespace SmartApi.Login.Models
{
    public class User : Authorization
    {

        public int Id { get; set; }
        public string Nome { get; set; }
        public string Password { get; set; }
        public string SecondPass { get; set; }
    public string CEP { get; set; }


        public User()
        {

        }
        public User(int id, string name, string firstPass, string secondPass)
        {
            Id = id;
            Nome = name;
            Password = firstPass;
            SecondPass = secondPass;
        }

    
        

        public bool ValidarLogin(User user)
        {
            Authorization auto = new Authorization();

            string method = "ValidarLogin";

            user.Auth(this, method);

            
            


            return true;
        }


    }
}