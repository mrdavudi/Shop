
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using DiscountManagement.Application.Contract.CustomerDiscount;
using DiscountManagement.Domain.CustomerDiscountAgg;
using DiscountManagement.Infrastructure.Repository;


namespace DiscountManagement.Application
{
    public class CustomerDiscountApplication : ICustomerDiscountApplication
    {
        private readonly ICustomerDiscountRepository _customerDiscountRepository;

        public CustomerDiscountApplication(ICustomerDiscountRepository customerDiscountRepository)
        {
            _customerDiscountRepository = customerDiscountRepository;
        }

        public OperationResult Define(DefineCustomerDiscount command)
        {
            var operationResult = new OperationResult();

            if (_customerDiscountRepository.Exist(x =>
                    x.ProductId == command.ProductId && x.DiscountRate == command.DiscountRate))
                return operationResult.Failed("تخفیف مورد نظر تکراری است");

            var StartDate = command.StartDate.ToGeorgianDateTime();
            var EndDate = command.EndDate.ToGeorgianDateTime();

            var discount = new CustomerDiscount(command.ProductId, command.DiscountRate, StartDate, EndDate, command.Reason);
            _customerDiscountRepository.Create(discount);
            _customerDiscountRepository.SaveChange();

            return operationResult.Succeded();
        }

        public OperationResult Edit(EditCustomerDiscount command)
        {
            var operationResult = new OperationResult();

            var discount = _customerDiscountRepository.Get(command.Id);
            if (discount == null)
                return operationResult.Failed("اطلاعات موجود نیست");

            var StartDate = command.StartDate.ToGeorgianDateTime();
            var EndDate = command.EndDate.ToGeorgianDateTime();

            discount.Edit(command.ProductId, command.DiscountRate, StartDate, EndDate, command.Reason);
            _customerDiscountRepository.SaveChange();

            return operationResult.Succeded();
        }

        public EditCustomerDiscount GetDetails(long id)
        {
            return _customerDiscountRepository.GetDetails(id);
        }

        public List<CustomerDiscountViewModel> Search(CustomerDiscountSearchModel searchMode)
        {
            return _customerDiscountRepository.Search(searchMode);
        }
    }
}