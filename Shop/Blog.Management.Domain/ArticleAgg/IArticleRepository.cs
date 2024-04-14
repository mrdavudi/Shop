using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using BlogManagement.Application.Contract.Article;

namespace Blog.Management.Domain.ArticleAgg
{
    public interface IArticleRepository : IRepository<long, Article>
    {
        public EditArticle GetDetails(long id);
        public List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
