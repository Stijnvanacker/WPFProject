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
    public class AdminArticleViewModel : ViewModelBase
    {

        private ArticleService articleService;
        private ICollection<Article> articles;
        private Article currentArticle;

        //Commands
        public ICommand DisplayTextCommand { get; private set; }
        public ICommand btnWijzigenClick { get; private set; }
        public ICommand btnToevoegenClick { get; private set; }
        public ICommand btnVerwijderenClick { get; private set; }
        public ICommand TerugCommand { get; private set; }
        

        //Properties
        public ICollection<Article> Articles
        {
            get { return articles; }
            set
            {
                articles = value;
                RaisePropertyChanged("Articles");
            }
        }


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


        public AdminArticleViewModel()
        {
            articleService = new ArticleService();
            LoadArticles();
            InstantiateCommands();
        }

        //laden van alle artikelen
        public void LoadArticles()
        {
            articles = articleService.GetAll();
        }

        //commands initialiseren 
        private void InstantiateCommands()
        {
            DisplayTextCommand = new RelayCommand<Article>(ExecuteDisplayTextCommand);
            btnWijzigenClick = new RelayCommand<Article>(ExecuteWijzigenCommand);
            btnToevoegenClick = new RelayCommand<Article>(ExecuteToevoegenCommand);
            btnVerwijderenClick = new RelayCommand<Article>(ExecuteVerwijderenCommand);
            TerugCommand = new RelayCommand(ExecuteTerugCommand);
        }

        //uitvoeren van command om current article weer te geven 
        private void ExecuteDisplayTextCommand(Article article)
        {
            CurrentArticle = article;
        }
        //uitvoeren van command om tafel te wijzigen 
        private void ExecuteWijzigenCommand(Article article)
        {           
            articleService.UpdateArticle(article);
            MessageBox.Show("Tafel Gewijzigd");
        }

        //Openen van Addwindow
        private void ExecuteToevoegenCommand(Article article)
        {
            Messenger.Default.Send(new NavigationMessage(new AdminArticleAddViewModel()));
        }
        //Verwijderen van het article
        private void ExecuteVerwijderenCommand(Article article)
        {
            articleService.DeleteArticle(article);
            LoadArticles();
            RaisePropertyChanged("Articles");
        }

        //Terugkeren naar menu 
        private void ExecuteTerugCommand()
        {
            Messenger.Default.Send(new NavigationMessage(new AdminMenuViewModel()));
        }

    }
}
