using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TemelEntityKurallari
{
    public class Product
    {
        // Ef core, her tablonun default olarak bir primary key kolonu olması gerektiğini kabul eder. Bu kolonu temsil eden bir property tanımlamadığınız takdirde hata verir.
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public double UnitPrice { get; set; }
    }
}
