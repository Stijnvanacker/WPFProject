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
    public class AdminTafelsViewModel : ViewModelBase
    {

        private TableService tableService;
        private ICollection<Table> tables;
        private Table currentTable;
        
        //Commands
        public ICommand DisplayTextCommand { get; private set; }
        public ICommand btnWijzigenClick { get; private set; }
        public ICommand btnToevoegenClick { get; private set; }
        public ICommand btnVerwijderenClick { get; private set; }
        public ICommand TerugCommand { get; private set; }

        //Properties
        public ICollection<Table> Tables
        {
            get { return tables; }
            set
            {
                tables = value;
                RaisePropertyChanged("Tables");
            }
        }

       
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
        public AdminTafelsViewModel()
        {
            tableService = new TableService();
            LoadTables();
            InstantiateCommands();

        }
        //laden van alle tafels
        public void LoadTables()
        {
            tables = tableService.GetAll();
        }
        //commands initialiseren
        private void InstantiateCommands()
        {
            DisplayTextCommand = new RelayCommand<Table>(ExecuteDisplayTextCommand);
            btnWijzigenClick = new RelayCommand<Table>(ExecuteWijzigenCommand);
            btnToevoegenClick = new RelayCommand(ExecuteToevoegenCommand);
            btnVerwijderenClick = new RelayCommand<Table>(ExecuteVerwijderenCommand);
            TerugCommand = new RelayCommand(ExecuteTerugCommand);
        }
        //current text command weergeven
        private void ExecuteDisplayTextCommand(Table tbl)
        {
            CurrentTable = tbl;
            tableService.UpdateTable(tbl);

        }
        //wijzigen command om tafel te wijzigen
        private void ExecuteWijzigenCommand(Table tbl)
        {
            
            tableService.UpdateTable(tbl);
            MessageBox.Show("Tafel Gewijzigd");
        }

        //Form openen om tafe toe te voegen
        private void ExecuteToevoegenCommand()
        {

            Messenger.Default.Send(new NavigationMessage(new AdminTafelsAddViewModel()));
        }
        //Verwijderen van tafel
        private void ExecuteVerwijderenCommand(Table tbl)
        {
            tableService.DeleteTable(tbl);
            
            LoadTables();
            RaisePropertyChanged("Tables");
            
        }
        //terugkeren
        private void ExecuteTerugCommand()
        {
            Messenger.Default.Send(new NavigationMessage(new AdminMenuViewModel()));
        }



    }
}
