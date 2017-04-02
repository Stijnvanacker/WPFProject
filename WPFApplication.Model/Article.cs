using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WPFApplication.Model
{
    public class Article
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public float Price { get; set; }
        public int MenuIndexX { get; set; }
        public int MenuIndexY { get; set; }
    }
}
