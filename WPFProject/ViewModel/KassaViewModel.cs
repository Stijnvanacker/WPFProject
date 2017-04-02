using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using GalaSoft.MvvmLight;
using WPFApplication.Business;
using WPFApplication.Model;
using System.Windows.Input;
using GalaSoft.MvvmLight.CommandWpf;
using GalaSoft.MvvmLight.Messaging;
using WPFProject.Messages;
using WPFProject.Views;

namespace WPFProject.ViewModel
{
    class KassaViewModel : ViewModelBase
    {
        private readonly OrderLineService orderLineService;
        private readonly TableService tableService;
        private readonly ArticleService articleService;
        private readonly OrderService orderService;
        private Table table;
        public Table Table
        {
            get { return table; }
            set
            {
                table = value;
                RaisePropertyChanged();
            }
        }

        private Order order;

        public Order Order
        {
            get { return order; }
            set
            {
                order = value;
                RaisePropertyChanged();
            }
        }

        private OrderLine orderLine;

        public OrderLine OrderLine
        {
            get { return orderLine; }
            set
            {
                orderLine = value;
                RaisePropertyChanged();
            }
        }

        
        private double totaal;
        //de totaalprijs van het order
        public double Totaal
        {
            get { return totaal; }
            set
            {
                totaal = value;
                RaisePropertyChanged();
            }
        }

        private ICollection<Article> articles;

        public ICollection<Article> Articles
        {
            get { return articles; }
            set
            {
                articles = value;
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
        //commands
        public ICommand AddCommand { get; private set; }
        public ICommand PlusOneCommand { get; private set; }
        public ICommand MinusOneCommand { get; private set; }
        public ICommand DeleteCommand { get; private set; }
        public ICommand BetalenCommand { get; private set; }
        public ICommand GoBackCommand { get; private set; }

        public KassaViewModel(int tableId)
        {
            tableService = new TableService();
            articleService = new ArticleService();
            orderLineService = new OrderLineService();
            orderService = new OrderService();
            InstantiateCommands();
            LoadTable(tableId);
            LoadOrder(tableId);
            LoadArticles();
            LoadOrderLines();
            CalcTotal();
        }

        public void InstantiateCommands()
        {
            AddCommand = new GalaSoft.MvvmLight.Command.RelayCommand<int>(ExecuteAddCommand);
            PlusOneCommand = new GalaSoft.MvvmLight.Command.RelayCommand<OrderLine>(ExecutePlusOneCommand);
            MinusOneCommand = new RelayCommand<OrderLine>(ExecuteMinusOneCommand);
            DeleteCommand = new RelayCommand<OrderLine>(ExecuteDeleteCommand);
            BetalenCommand = new RelayCommand(ExecuteBetalenCommand);
            GoBackCommand = new RelayCommand(GoBack);
        }

        //betalen van order, status van het order wordt veranderd van 0 naar 1
        //gaat over naar het detailscherm met de details over het order
        public void ExecuteBetalenCommand()
        {
            if (orderLines != null)
            {
                Order.Staat = 1;
                orderService.UpdateOrder(Order);
                Messenger.Default.Send(new NavigationMessage(new DetailViewModel(orderLines, totaal, Order)) { });
            }
            else
            {
                MessageBox.Show("Nog geen items!");
            }
        }

        //verwijderd een OrderLine, als het de laatste OrderLine is in de lijst
        //wordt het Order verwijderd
        public void ExecuteDeleteCommand(OrderLine orderLine)
        {
            if (orderLine != null)
            {
                Console.WriteLine(orderLine.Id);
                orderLineService.DeleteOrderLine(orderLine);
                
                LoadOrderLines();
                Console.WriteLine(Order.Id);
                Console.WriteLine(orderLines.Count);
                if (orderLines.Count == 0)
                {
                    Console.WriteLine("deleteorder");
                    orderService.DeleteOrder(Order);
                }
                CalcTotal();
                RaisePropertyChanged("");
            }
        }

        //aantal naar beneden van een bepaalde OrderLine
        public void ExecuteMinusOneCommand(OrderLine orderLine)
        {
            if (orderLine != null)
            {
                if (orderLine.Amount > 1)
                {
                    orderLine.Amount -= 1;
                    orderLineService.UpdateOrderLine(orderLine);
                    LoadOrderLines();
                    CalcTotal();
                    RaisePropertyChanged("");
                }
                
            }
        }

        //aantal naar boven van een bepaalde OrderLine
        public void ExecutePlusOneCommand(OrderLine orderLine)
        {
            if (orderLine != null)
            {
                orderLine.Amount += 1;
                orderLineService.UpdateOrderLine(orderLine);
                LoadOrderLines();
                CalcTotal();
                RaisePropertyChanged("");
            }
            
        }

        //voegt een artikel toe aan de lijst als het er nog niet in zit,
        //als het er wel in zit dan wordt het aantal met 1 verhoogd
        public void ExecuteAddCommand(int articleId)
        {
            
            LoadOrder(Table.Id);
            Console.WriteLine(articleId);
            OrderLine orderLine = orderLineService.GetByArticleId(articleId, Order.Id);
            if (orderLine != null)
            {
                LoadOrderLines();
                CalcTotal();
                ExecutePlusOneCommand(orderLine);
                RaisePropertyChanged("");
                
            }
            else
            {
                OrderLine newOrderLine = new OrderLine();
                Article article = articleService.GetById(articleId);
                newOrderLine.ArticleId = articleId;
                newOrderLine.OrderId = Order.Id;
                newOrderLine.Amount = 1;
                newOrderLine.Price = article.Price;
                newOrderLine.ArticleName = article.Name;
                newOrderLine.CreatedDate = DateTime.Now;
                orderLineService.InsertOrderLine(newOrderLine);
                LoadOrderLines();
                CalcTotal();
                RaisePropertyChanged("");
                
            }
        }

        
        private void LoadTable(int tableId)
        {
            Table = tableService.GetById(tableId);
        }

        //indien er al een lopen order is voor de tafel wordt deze hier opgehaald
        //als er nog geen lopend order is wordt er een nieuw order aangemaakt
        private void LoadOrder(int tableId)
        {
            Order = orderService.GetByTableId(tableId);
            if(Order == null)
            {
                Console.WriteLine("null");
                Order order = new Order();
                order.TableId = tableId;
                order.TableName = Table.Name;
                order.CreatedDate = DateTime.Now;
                order.Staat = 0;
                orderService.InsertOrder(order);
                Order = order;
            }
        }

        private void LoadArticles()
        {
            articles = articleService.GetAll();
        }

        //laadt alle OrderLines als er nog een openstaand order is van deze tafel
        private void LoadOrderLines()
        {
            if (Order != null)
            {
                orderLines = orderLineService.GetByOrderId(Order.Id);
            }
        }

        //berekend de totaalprijs van het Order
        private void CalcTotal()
        {
            double total = 0;
            if (orderLines!= null)
            { 
                foreach (var item in orderLines)
                {
                    total += item.Amount*item.Price;
                }
            }

            totaal = total;
        }

        //ga terug naar het startscherm
        private void GoBack()
        {
            Messenger.Default.Send(new NavigationMessage(new StartViewModel()) { });
        }
    }
}
