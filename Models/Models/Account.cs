using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Models.Models
{
    public class Account
    {
   
        public int Id { get; set; }
        public string Name { get; set; }
        public double Balance { get; set; }

        public int ClientId { get; set; }
        public Client Client { get; set; }
    }
}
