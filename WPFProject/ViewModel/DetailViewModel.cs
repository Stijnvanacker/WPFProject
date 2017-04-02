using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;
using WPFApplication.Model;
using WPFProject.Messages;
using System.Windows.Input;
using GalaSoft.MvvmLight.Command;

namespace WPFProject.ViewModel
{
    //scherm dat na het kassascherm komt met de details van het Order
    public class DetailViewModel: ViewModelBase
    {
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

        private double totaal;
        public double Totaal
        {
            get { return totaal; }
            set
            {
                totaal = value;
                RaisePropertyChanged();
            }
        }

        public ICommand StartSchermCommand { get; private set; }

        public DetailViewModel(ICollection<OrderLine> orderLines, double totaal, Order order)
        {
            this.orderLines = orderLines;
            this.totaal = totaal;
            this.order = order;
            InstantiateCommands();
        }

        public void InstantiateCommands()
        {
            StartSchermCommand = new RelayCommand(StartScherm);
        }

        private void StartScherm()
        {
            Messenger.Default.Send(new NavigationMessage(new StartViewModel()) { });
        }
    }
}
