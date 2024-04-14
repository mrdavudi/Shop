using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Domain;
using BlogManagement.Application.Contract.ArticleCategory;

namespace Blog.Management.Domain.ArticleCategoryAgg
{
    public interface IArticleCategoryRepository : IRepository<long, ArticleCategory>
    {
        public EditArticleCategory GetDetails(long id);
        public List<ArticleCategoryViewModel> Search(ArticleCategorySearchModel searchModel);
        public List<ArticleCategoryViewModel> GetArticleCategories();
        public string GetSlugBy(long categoryId);
    }
}
