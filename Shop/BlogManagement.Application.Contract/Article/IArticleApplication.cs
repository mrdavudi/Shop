using System.Runtime.InteropServices;
using _0_Framework.Application;

namespace BlogManagement.Application.Contract.Article
{
    public interface IArticleApplication
    {
        public OperationResult Create(CreateArticle command);
        public OperationResult Edit(EditArticle command);
        public EditArticle GetDetails(long id);
        public List<ArticleViewModel> Search(ArticleSearchModel searchModel);
    }
}
