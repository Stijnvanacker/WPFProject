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
    public class AdminArticleAddViewModel : ViewModelBase
    {


        //Commands
        public ICommand AddArticleCommand { get; private set; }
        public ICommand TerugCommand { get; private set; }
        //inladen database 
        private ArticleService articleService;
        private Article currentArticle;

        //Properties
        public Article CurrentArticle
        {
            get
            {
                return currentArticle;
            }
            set
            {
                if (currentArticle == value)
                    return;
                currentArticle = value;
                RaisePropertyChanged("CurrentArticle");
            }
        }
        //initialiseert adminArticleAddViewModel
        public AdminArticleAddViewModel()
        {
            articleService = new ArticleService();
            InstantiateCommands();
            CurrentArticle = new Article();
            CurrentArticle.Name = "articleNaam";
            CurrentArticle.Price = 100;
            currentArticle.MenuIndexX = 0;
            currentArticle.MenuIndexY = 0;
          

        }
        //Instantiatecommands
        private void InstantiateCommands()
        {
            AddArticleCommand = new RelayCommand<Article>(ExecuteAddArticleCommand);
            TerugCommand = new RelayCommand(ExecuteTerugCommand);
        }

        //voegt een artikel toe aan de list
        private void ExecuteAddArticleCommand(Article article)
        {
            if (article != null)
            {


                articleService.InsertArticle(article);

                Messenger.Default.Send(new NavigationMessage(new AdminArticleViewModel()));


            }
            else
            {
                MessageBox.Show("table is gelijk aan null");
            }
        }
        //keert terug naar hoofdscherm
        private void ExecuteTerugCommand()
        {
            Messenger.Default.Send(new NavigationMessage(new AdminArticleViewModel()));
        }

    }
}
