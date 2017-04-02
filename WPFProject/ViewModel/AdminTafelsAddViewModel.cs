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
      public class AdminTafelsAddViewModel : ViewModelBase
      {

        //Commands
        public ICommand AddTableCommand { get; private set; }
        public ICommand TerugCommand { get; private set; }
        //inladen database 
        private TableService tblService;
        private Table currentTable;

        //Properties
        public Table CurrentTable
        {
            get
            {
                return currentTable;
            }
            set
            {
                if (currentTable == value)
                    return;
                currentTable = value;
                RaisePropertyChanged("CurrentTable");
            }
        }
        //constructor
        public AdminTafelsAddViewModel()
        {
            tblService = new TableService();
            InstantiateCommands();
            CurrentTable = new Table();
            CurrentTable.Name = "TableNaam";
            CurrentTable.Height = 100;
            CurrentTable.Width = 100;
            CurrentTable.PositionX = 10;
            CurrentTable.PositionY = 20;

        }

        //commands initialiseren
        private void InstantiateCommands()
        {
          AddTableCommand = new RelayCommand<Table>(ExecuteAddTableCommand);
            TerugCommand = new RelayCommand(ExecuteTerugCommand);
        }
        //toevoegen van een tafelscherm
          private void ExecuteAddTableCommand(Table tbl)
          {
              if (tbl != null)
              {

                  
                tblService.InsertTable(tbl);

                Messenger.Default.Send(new NavigationMessage(new AdminTafelsViewModel()));


            }
              else
              {
                  MessageBox.Show("table is gelijk aan null");
              }
          }

        //terugkeren naar menu
          private void ExecuteTerugCommand()
          {
            Messenger.Default.Send(new NavigationMessage(new AdminTafelsViewModel()));
        }






    }
}
