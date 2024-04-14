using _0_Framework.Application;
using Blog.Management.Domain.ArticleAgg;
using Blog.Management.Domain.ArticleCategoryAgg;
using BlogManagement.Application.Contract.Article;

namespace BlogManagement.Application
{
    public class ArticleApplication : IArticleApplication
    {
        private readonly IArticleRepository _articleRepository;
        private readonly IFileUploader _fileUploader;
        private readonly IArticleCategoryRepository _articleCategoryRepository;

        public ArticleApplication(IArticleRepository articleRepository, IFileUploader fileUploader, IArticleCategoryRepository articleCategoryRepository)
        {
            _articleRepository = articleRepository;
            _fileUploader = fileUploader;
            _articleCategoryRepository = articleCategoryRepository;
        }

        public OperationResult Create(CreateArticle command)
        { 
            var operationResult = new OperationResult();
            var checkArticlExist = _articleRepository.Exist(x => x.Title == command.Title);

            if (checkArticlExist)
                return operationResult.Failed(ValidationMessage.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var articleCategorySlug = _articleCategoryRepository.GetSlugBy(command.CategoryId);
            var path = $"Articles/{articleCategorySlug}/{slug}";
            var pictureName = _fileUploader.Upload(command.Picture, path);

            var newArticle = new Article(command.Title, command.ShortDescription, command.Description, pictureName,
                command.PictureAlt, command.PictureTitle, command.PublishDate.ToGeorgianDateTime(), slug, command.Keywords,
                command.MetaDescription, command.CanonicalAddress, command.CategoryId);

            _articleRepository.Create(newArticle);
            _articleRepository.SaveChange();
            return operationResult.Succeded();
        }

        public OperationResult Edit(EditArticle command)
        {
            var operationResult = new OperationResult();
            var editArticle = _articleRepository.Get(command.Id);

            if (editArticle == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            if(_articleRepository.Exist(x => x.Title == command.Title && x.Id != command.Id))
                return operationResult.Failed(ValidationMessage.DuplicatedRecord);

            var slug = command.Slug.Slugify();
            var articleCategorySlug = _articleCategoryRepository.GetSlugBy(command.CategoryId);
            var path = $"Articles/{articleCategorySlug}/{slug}";
            var pictureName = _fileUploader.Upload(command.Picture, path);

            editArticle.Edit(command.Title, command.ShortDescription, command.Description, pictureName, command.PictureAlt,
                    command.PictureTitle, command.PublishDate.ToGeorgianDateTime(), slug, command.Keywords,
                    command.MetaDescription, command.CanonicalAddress, command.CategoryId);

            _articleRepository.SaveChange();
            return operationResult.Succeded();
        }

        public EditArticle GetDetails(long id)
        {
            return _articleRepository.GetDetails(id);
        }

        public List<ArticleViewModel> Search(ArticleSearchModel searchModel)
        {
            return _articleRepository.Search(searchModel);
        }
    }
}
