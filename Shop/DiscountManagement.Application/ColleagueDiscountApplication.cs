using _0_Framework.Application;
using DiscountManagement.Application.Contract.ColleagueDiscount;
using DiscountManagement.Domain.ColleagueDiscountAgg;

namespace DiscountManagement.Application
{
    public class ColleagueDiscountApplication : IColleagueDiscountApplication
    {
        private readonly IColleagueDiscountRepository _colleagueDiscountRepository;

        public ColleagueDiscountApplication(IColleagueDiscountRepository colleagueDiscountRepository)
        {
            _colleagueDiscountRepository = colleagueDiscountRepository;
        }

        public OperationResult Define(DefineColleagueDiscount command)
        {
            var operationResult = new OperationResult();
            if (_colleagueDiscountRepository.Exist(x =>
                    x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
            {
                return operationResult.Failed("تخفیف تکراری است");
            }

            var ColleagueDiscount = new ColleagueDiscount(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.Create(ColleagueDiscount);
            _colleagueDiscountRepository.SaveChange();

            return operationResult.Succeded();
        }

        public OperationResult Edit(EditColleagueDiscount command)
        {
            var operationResult = new OperationResult();
            if (_colleagueDiscountRepository.Exist(x =>
                    x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate && x.Id != command.Id))
            {
                return operationResult.Failed("تخفیف تکراری است");
            }

            var EditDiscount = _colleagueDiscountRepository.Get(command.Id);
            if (EditDiscount == null)
                return operationResult.Failed("رکوردی یافت نشد");

            EditDiscount.Edit(command.ProductId, command.DiscountRate);
            _colleagueDiscountRepository.SaveChange();

            return operationResult.Succeded();
        }

        public EditColleagueDiscount GetDetails(long id)
        {
            return _colleagueDiscountRepository.GetDetails(id);
        }

        public OperationResult Remove(long id)
        {
            var operationResult = new OperationResult();
            var removeDiscount = _colleagueDiscountRepository.Get(id);

            if (removeDiscount == null)
                return operationResult.Failed("رکوردی یافت نشد");

            removeDiscount.Remove(id);
            _colleagueDiscountRepository.SaveChange();

            return operationResult.Succeded();
        }

        public OperationResult Restore(long id)
        {
            var operationResult = new OperationResult();

            var RestoreDiscount = _colleagueDiscountRepository.Get(id);
            if (RestoreDiscount == null)
                return operationResult.Failed("رکوردی یافت نشد");

            RestoreDiscount.Restore(id);
            _colleagueDiscountRepository.SaveChange();

            return operationResult.Succeded();
        }

        public List<ColleagueDiscountViewModel> Search(ColleagueDiscountSearchModel searchModel)
        {
            return _colleagueDiscountRepository.Search(searchModel);
        }
    }
}
