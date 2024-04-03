using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using ShopManagement.Application.Contract.Slide;
using ShopManagement.Domain.SlideAgg;

namespace ShopManagement.Application
{
    public class SlideApplication : ISlideApplication
    {
        private readonly IFileUploader _fileUploader;
        private readonly ISlideRepository _slideRepository;

        public SlideApplication(ISlideRepository slideRepository, IFileUploader fileUploader)
        {
            _slideRepository = slideRepository;
            _fileUploader = fileUploader;
        }

        public OperationResult Create(CreateSlide command)
        {
            var operationResult = new OperationResult();

            var pictureName = _fileUploader.Upload(command.Picture, "Slides");
            var slide = new Slide(pictureName, command.PictureAlt, command.PictureTitle, command.Heading,
                command.Text, command.Title, command.BtnText, command.Link);
            _slideRepository.Create(slide);
            _slideRepository.SaveChange();

            return operationResult.Succeded();
        }

        public OperationResult Edit(EditSlide command)
        {
            var operationResult = new OperationResult();

            var slide = _slideRepository.Get(command.Id);
            if (slide == null)
                return operationResult.Failed("هیچ رکوردی یافت نشد");

            var pictureName = _fileUploader.Upload(command.Picture, "Slides");

            slide.Edit(pictureName, command.PictureAlt, command.PictureTitle, command.Heading,
                command.Text, command.Title, command.BtnText, command.Link);
            _slideRepository.SaveChange();

            return operationResult.Succeded();
        }

        public EditSlide GetDetails(long id)
        {
            return _slideRepository.GetDetails(id);
        }

        public List<SlideViewModel> GetList()
        {
            return _slideRepository.GetList();
        }

        public OperationResult Remove(long id)
        {
            var operationResult = new OperationResult();
            var slide = _slideRepository.Get(id);

            if (slide == null)
                return operationResult.Failed("هیچ رکوردی یافت نشد");
            slide.Remove(id);
            _slideRepository.SaveChange();

            return operationResult.Succeded();
        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();
            var slide = _slideRepository.Get(id);

            if (slide == null)
                return operationResult.Failed("هیچ رکوردی یافت نشد");
            slide.Restore(id);
            _slideRepository.SaveChange();

            return operationResult.Succeded();
        }
    }
}
