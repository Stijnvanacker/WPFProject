using System;
using System.Collections.Generic;
using System.Data.SqlTypes;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApplication.Model
{
    public class OrderLine
    {
        public int Id { get; set; }
        public int OrderId { get; set; }
        public int ArticleId { get; set; }
        public string ArticleName { get; set; }
        public float Price { get; set; }
        public int Amount { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
