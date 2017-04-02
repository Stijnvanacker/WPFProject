using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using WPFApplication.Business;
using WPFApplication.Model;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using WPFProject.Messages;

namespace WPFProject.ViewModel
{
    //Viewmodel voor het startscherm dat alle tafels toont
    class StartViewModel: ViewModelBase
    {
        private readonly TableService tableService;
        private ICollection<Table> tables;
        public RelayCommand<int> ShowKassaCommand { get; private set; }
        public ICollection<Table> Tables
        {
            get { return tables; }
            set
            {
                tables = value;
                RaisePropertyChanged("");
            }
        }

        public StartViewModel()
        {
            
            tableService = new TableService();
            InstantiateCommands();
            loadTables();
            RaisePropertyChanged("");
        }

        private void InstantiateCommands()
        {
            ShowKassaCommand = new RelayCommand<int>(ShowKassa);
        }

        private void loadTables()
        {
            Tables = tableService.GetAll();
            RaisePropertyChanged("");
        }

        private void ShowKassa(int tableId)
        {
            Messenger.Default.Send(new NavigationMessage(new KassaViewModel(tableId)) {  });
        }
    }
}
