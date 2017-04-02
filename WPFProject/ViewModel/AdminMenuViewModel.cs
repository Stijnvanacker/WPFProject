using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using GalaSoft.MvvmLight.Messaging;
using WPFProject.Messages;

namespace WPFProject.ViewModel
{
    public class AdminMenuViewModel : ViewModelBase
    {
        //Commands
        public ICommand OpenTafelsAdmin { get; private set; }
        public ICommand OpenArtikelsAdmin { get; private set; }
        public ICommand OpenOrdersAdmin { get; private set; }

        //Constructor AdminViewModel
        public AdminMenuViewModel()
        {
            InstantiateCommands();
        }
        //Initialiseren van commands
        private void InstantiateCommands()
        {

            OpenTafelsAdmin = new RelayCommand<string>(ExecuteOpenTafelsAdmin);
            OpenArtikelsAdmin = new RelayCommand<string>(ExecuteOpenArtikelsAdmin);
            OpenOrdersAdmin = new RelayCommand<string>(ExecuteOpenOrdersAdmin);
        }

        //deze methode zorgt ervoor dat tafels openen
        private void ExecuteOpenTafelsAdmin(string text)
        {
            Messenger.Default.Send(new NavigationMessage(new AdminTafelsViewModel()));
        }
        //deze methode zorgt ervoor dat AdminArticle opent
        private void ExecuteOpenArtikelsAdmin(string text)
        {
            Messenger.Default.Send(new NavigationMessage(new AdminArticleViewModel()));
        }
        //openen van AdminOrderViewModel
        private void ExecuteOpenOrdersAdmin(string text)
        {
            Messenger.Default.Send(new NavigationMessage(new AdminOrderViewModel()));
        }


      }
    }
