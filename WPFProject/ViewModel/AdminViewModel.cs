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
using WPFProject.Messages;
using WPFProject.util;

namespace WPFProject.ViewModel
{
   

    class AdminViewModel : ViewModelBase
    {
        private string pinCode;
        //Commands
        public ICommand GetPinCommand { get; private set; }
        
        //Properties
        public string PinCode
		{
			get
			{
				return pinCode;
			}
			set
			{
				if (pinCode == value)
					return;
				pinCode = value;
				RaisePropertyChanged("PinCode");
			}
		}

        public AdminViewModel()
        {
         InstantiateCommands();
        }

   

        private void InstantiateCommands()
        {
         
          GetPinCommand =  new RelayCommand<string>(ExecuteGetPinCommand);
        }

        //deze methode kijkt of de pin correct is 
        private void ExecuteGetPinCommand(string text)
        {
            PinCode = text;
            System.Diagnostics.Debug.Write("CheckPin :" + text);
            HashService hashService = new HashService();

            if (hashService.GetPinFromConfigAndCheck(text))
            {
                //goeie pin doorgaan naar volgende scherm met overzich
                System.Diagnostics.Debug.Write("PIN OK :" + text);
                Messenger.Default.Send(new NavigationMessage(new AdminMenuViewModel()));
            }
            else
            {
                //foutieve pin, foutmelding weergeven en vragen opnieuw te proberen.
                System.Diagnostics.Debug.Write("PIN NIET OK :" + text);
                MessageBox.Show("Pin niet ok, probeer opnieuw!", "Pin niet ok", MessageBoxButton.OK,
                    MessageBoxImage.Error);

            }


        }
    }
}
