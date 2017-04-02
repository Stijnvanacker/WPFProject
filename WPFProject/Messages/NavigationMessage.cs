using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GalaSoft.MvvmLight;
using GalaSoft.MvvmLight.Messaging;

namespace WPFProject.Messages
{
    //klasse voor het navigeren tussen de verschillende schermen
    public class NavigationMessage: GenericMessage<ViewModelBase>
    {

        public NavigationMessage(ViewModelBase viewModel):base(viewModel)
		{
            ViewModel = viewModel;
        }

        public ViewModelBase ViewModel { get; set; }

    }
}
