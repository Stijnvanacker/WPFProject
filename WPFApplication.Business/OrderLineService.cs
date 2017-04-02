using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApplication.DataAccess;
using WPFApplication.Model;

namespace WPFApplication.Business
{
    public class OrderLineService: BaseService
    {
        private readonly OrderLineDataClass orderLineDataClass;


        public OrderLineService()
        {
            this.orderLineDataClass = new OrderLineDataClass(GetConnectionString());
        }

        //retourneert een OrderLine met een bepaald id
        public OrderLine GetById(int id)
        {
            //Validation
            if (id == 0)
            {
                return null;
            }

            return orderLineDataClass.GetById(id);
        }

        //retourneert alle OrderLines van een bepaald Order(via id van order)
        public ICollection<OrderLine> GetByOrderId(int orderId)
        {
            //Validation
            if (orderId == 0)
            {
                return null;
            }
            return orderLineDataClass.getByOrderId(orderId);
        }

        //retourneert alle OrderLines van een bepaald artikel en order
        public OrderLine GetByArticleId(int articleId, int orderId)
        {
            //Validation
            if (orderId == 0 || articleId == 0)
            {
                return null;
            }
            return orderLineDataClass.GetByArticleId(articleId, orderId);
        }

        //retourneert alle OrderLines
        public ICollection<OrderLine> GetAll()
        {
            return orderLineDataClass.GetAll();
        }

        //maakt een nieuwe OrderLine aan
        public void InsertOrderLine(OrderLine orderLine)
        {
            orderLineDataClass.InsertOrderLine(orderLine);
        }

        //past een bestaande OrderLine aan
        public void UpdateOrderLine(OrderLine orderLine)
        {
            orderLineDataClass.UpdateOrderLine(orderLine);
        }

        public void DeleteOrderLine(OrderLine orderLine)
        {
            orderLineDataClass.DeleteOrderLine(orderLine);
        }
    }
}
