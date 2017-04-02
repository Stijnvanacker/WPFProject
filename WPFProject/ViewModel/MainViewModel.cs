using System;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Command;
using System.Windows;
using System.Windows.Input;
using WPFProject.Messages;
using WPFProject.util;
using GalaSoft.MvvmLight.Messaging;

namespace WPFProject.ViewModel
{
    
    public class MainViewModel : ViewModelBase
    {
        private ViewModelBase _currentViewModel;
        

        readonly static StartViewModel _startViewModel = new StartViewModel();
        readonly static AdminViewModel _adminViewModel = new AdminViewModel();
       

        //Commands
        public ICommand StartCommand { get; private set; }
        public ICommand AdminCommand { get; private set; }
        
        public ICommand CloseCommand { get; private set; }


        public ViewModelBase CurrentViewModel
        {
            get
            {
                return _currentViewModel;
            }
            set
            {
                if (_currentViewModel == value)
                    return;
                _currentViewModel = value;
                RaisePropertyChanged("CurrentViewModel");
            }
        }
        public MainViewModel()
        {
            CurrentViewModel = MainViewModel._startViewModel;
            InstantiateCommands();
            Messenger.Default.Register<NavigationMessage>(this, ShowViewModel);

        }

        private void InstantiateCommands()
        {
            CloseCommand = new RelayCommand(ExecuteCloseCommand);
            StartCommand = new RelayCommand(ExecuteStartCommand);
            AdminCommand = new RelayCommand(ExecuteAdminCommand);
        }

        private void ExecuteStartCommand()
        {
            
            Messenger.Default.Send(new NavigationMessage(new StartViewModel()) { });
        }

        private void ExecuteAdminCommand()
        {
            ShowViewModel(MainViewModel._adminViewModel);
        }

        private void ShowViewModel(ViewModelBase viewModel)
        {
            CurrentViewModel = viewModel;
        }
        private void ExecuteCloseCommand()
        {
            Application.Current.Shutdown();
        }

        

        private void ShowViewModel(NavigationMessage viewModelMessage)
        {
            ShowViewModel(viewModelMessage.ViewModel);
        }
        
    }
}