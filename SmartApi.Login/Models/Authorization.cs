using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Data.SqlClient;
using System.Configuration;
using System.ComponentModel.DataAnnotations;
using System.Configuration;
using System.Data.SqlClient;
using SmartApi.Login;

namespace SmartApi.Login.Models
{
    public class Authorization 
    {
        

        public bool Auth(User user, string methodVerification)
        {
            return false;
        }
    }
}