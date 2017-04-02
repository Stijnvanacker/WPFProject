using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApplication.DataAccess;
using WPFApplication.Model;

namespace WPFApplication.Business
{
    public class OrderService : BaseService
    {
        private readonly OrderDataClass orderDataClass;


        public OrderService()
        {
            this.orderDataClass = new OrderDataClass(GetConnectionString());
        }

        //retourneert een order met een bepaald id
        public Order GetById(int id)
        {
            //Validation
            if (id == 0)
            {
                return null;
            }

            return orderDataClass.GetById(id);
        }

        //retourneert het order van een bepaalde tafel waarbij de status 0 is (lopend)
        public Order GetByTableId(int tableId)
        {
            //Validation
            if (tableId == 0)
            {
                return null;
            }
            return orderDataClass.GetByTableId(tableId);
        }

        //retourneert alle orders
        public ICollection<Order> GetAll()
        {
            return orderDataClass.GetAll();
        }

        //voegt een nieuw order toe
        public void InsertOrder(Order order)
        {
            orderDataClass.InsertOrder(order);
        }

        //past een bestaand order aan
        public void UpdateOrder(Order order)
        {
            orderDataClass.UpdateOrderLine(order);
        }

        //verwijderd een bepaald order
        public void DeleteOrder(Order order)
        {
            orderDataClass.DeleteOrder(order);
        }

        //retourneert de orders van een specifieke maand en jaar
        public ICollection<Order> GetOrdersByMonthAndCurrentYear(int month, int year)
        {
            return orderDataClass.GetOrdersByMonthAndCurrentYear(month, year);
        }
    }

}
