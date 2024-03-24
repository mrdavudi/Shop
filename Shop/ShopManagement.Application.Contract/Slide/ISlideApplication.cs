using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;

namespace ShopManagement.Application.Contract.Slide
{
    public interface ISlideApplication
    {
        public OperationResult Create(CreateSlide command);
        public OperationResult Edit(EditSlide command);
        public OperationResult Remove(long id);
        public OperationResult Restore(long id);
        public EditSlide GetDetails(long id);
        public List<SlideViewModel> GetList();

    }
}
