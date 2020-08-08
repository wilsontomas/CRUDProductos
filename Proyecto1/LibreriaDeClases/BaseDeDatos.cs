using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LibreriaDeClases
{
   public class BaseDeDatos
    {
        private SqlConnection Conection1 { get; set; }
      

        public SqlConnection Conection
        {
            get
            {
                if (Conection1==null)
                {
                    var conectionString = @"Data Source=DESKTOP-5QJ9ER4\SQLEXPRESS;Initial Catalog=Tienda;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";
                    Conection1 = new SqlConnection(conectionString);
                }
                return Conection1;
            }
           
        }

    }
}
