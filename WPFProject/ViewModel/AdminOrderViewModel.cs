using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using WPFApplication.Business;
using WPFApplication.Model;
using WPFProject.Messages;

namespace WPFProject.ViewModel
{
    public class AdminOrderViewModel : ViewModelBase
    {
        //commands
        public ICommand clickOnOrderLine { get; private set; }
        public ICommand OpenTerug { get; private set; }
        public ICommand OpenMaandOverzicht { get; private set; }
        public ICommand SelectDate { get; private set; }
        
        //properties
        private Order currentOrder;
        private OrderService orderService;
        private OrderLineService orderLineService;
        public Order CurrentOrder
        {
            get { return currentOrder; }
            set
            {
                currentOrder = value;
                RaisePropertyChanged();
            }
        }

        private ICollection<Order> orders;
        public ICollection<Order> Orders
        {
            get { return orders; }
            set
            {
                orders = value;
                RaisePropertyChanged();
            }
        }

        private ICollection<OrderLine> orderLines;
        public ICollection<OrderLine> OrderLines
        {
            get { return orderLines; }
            set
            {
                orderLines = value;
                RaisePropertyChanged();
            }
        }

        private double totalOrderLines;
        public double TotalOrderLines
        {
            get
            {
                if (OrderLines != null)
                {
                    double tempTot = 0.0;
                    foreach (OrderLine o in OrderLines)
                    {
                        tempTot += o.Price*o.Amount;
                    }
                    totalOrderLines = tempTot;

                }
                else
                {
                    totalOrderLines = 0.0;
                }
                return totalOrderLines;
            }
            set
            {
                totalOrderLines = value;
                RaisePropertyChanged();
            }
        }

        private double monthTotal;
        public double MonthTotal
        {
            get
            {
                if (Orders != null)
                {
                    double tempTot = 0.0;

                    foreach (Order o in Orders)
                    {
                        ICollection<OrderLine> orderLinesForMonth = orderLineService.GetByOrderId(o.Id);

                        foreach (OrderLine ol in orderLinesForMonth)
                        {
                            tempTot += ol.Price*ol.Amount;
                        }


                    }

                    monthTotal = tempTot;

                }
                else
                {
                    monthTotal = 0.0;
                }
                return monthTotal;
            }
            set
            {
                monthTotal = value;
                RaisePropertyChanged();
            }
        }





        //opbouwen van adminorderviewmodel
        public AdminOrderViewModel()
        {
            orderService = new OrderService();
            orderLineService = new OrderLineService();
            InstantiateCommands();
            LoadOrders();
        }
        //laad all orders
        public void LoadOrders()
        {
             orders = orderService.GetAll();
        }
        //initialiseert alle commands
        public void InstantiateCommands()
        {
            clickOnOrderLine = new RelayCommand<Order>(executeClickOnOrderLine);
            OpenTerug = new RelayCommand(executeOpenTerug);     
            SelectDate = new RelayCommand<int>(executeSelectDate);
        }

        //zorg ervoor dat je op een orderline kan klikken
        public void executeClickOnOrderLine(Order order)
        {
            if (order != null)
            {
                OrderLines = orderLineService.GetByOrderId(order.Id);
                RaisePropertyChanged("OrderLines");
                RaisePropertyChanged("TotalOrderLines");
            }

        }
        //zorgt ervoor dat je terug kan naar menu
        public void executeOpenTerug()
        {
            Messenger.Default.Send(new NavigationMessage(new AdminMenuViewModel()));
        }
      
        //bij het selecteren van maand verschijnen alle orders van deze maand
        public void executeSelectDate(int index)
        {


            if (index == 0)
            {
                LoadOrders();
            }
            else
            {

                Orders = orderService.GetOrdersByMonthAndCurrentYear(index, DateTime.Now.Year);
                
                if(OrderLines != null)
                {
                    OrderLines = null;
                }
            
               
                RaisePropertyChanged("MonthTotal");
                RaisePropertyChanged("OrderLines");
                RaisePropertyChanged("Orders");
               RaisePropertyChanged("TotalOrderLines");
            }

            
        }

    }
}
