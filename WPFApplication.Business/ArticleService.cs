using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WPFApplication.DataAccess;
using WPFApplication.Model;

namespace WPFApplication.Business
{
    public class ArticleService: BaseService
    {
        private readonly ArticleDataClass articleDataClass;


        public ArticleService()
        {
            this.articleDataClass = new ArticleDataClass(GetConnectionString());
        }

        //retourneert een artikel met een bepaald id
        public Article GetById(int id)
        {
            //Validation
            if (id == 0)
            {
                return null;
            }

            return articleDataClass.GetById(id);
        }

        //retourneert alle artikelen
        public ICollection<Article> GetAll()
        {
            return articleDataClass.GetAll();
        }

        //past een bestaand artikel aan
        public void UpdateArticle(Article article)
        {
            articleDataClass.UpdateArticle(article);
        }

        //voegt een nieuw artikel toe
        public void InsertArticle(Article article)
        {
            articleDataClass.InsertArticle(article);
        }

        //verwijdert een artikel
        public void DeleteArticle(Article article)
        {
            articleDataClass.DeleteArticle(article);
        }
    }
}
