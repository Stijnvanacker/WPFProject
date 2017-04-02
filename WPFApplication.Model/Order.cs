using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApplication.Model
{
    public class Order
    {
        public int Id { get; set; }
        public int TableId { get; set; }
        public string TableName { get; set; }
        public DateTime CreatedDate { get; set; }
        public int Staat { get; set; }
    }
}
