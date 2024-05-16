using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using _0_Framework.Application;
using AccountManagement.Domain.RoleAgg;
using DomainManagement.Application.Contract.Role;
using Microsoft.EntityFrameworkCore.Storage.Json;
using Microsoft.IdentityModel.Protocols.OpenIdConnect;

namespace DomainManagement.Application
{
    public class RoleApplication : IRoleApplication
    {
        private readonly IRoleRepository _roleRepository;

        public RoleApplication(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public OperationResult Create(CreateRole command)
        {
            var operationResult = new OperationResult();

            if (_roleRepository.Exist(x => x.Name == command.Name))
                return operationResult.Failed(ValidationMessage.DuplicatedRecord);

            var newRole = new Role(command.Name, new List<Permission>());
            _roleRepository.Create(newRole);
            _roleRepository.SaveChange();

            return operationResult.Succeded();
        }

        public OperationResult Edit(EditRole command)
        {
            var operationResult = new OperationResult();

            var role = _roleRepository.Get(command.Id);

            if (role == null)
                return operationResult.Failed(ValidationMessage.RecordNotFound);

            if (_roleRepository.Exist(x => x.Name == command.Name && x.Id != command.Id))
                return operationResult.Failed(ValidationMessage.DuplicatedRecord);

            var permissions = new List<Permission>();

            if(command.Permissions != null)
                command.Permissions.ForEach(code=> permissions.Add(new Permission(code)));
            else
                permissions = new List<Permission>();

            role.Edit(command.Name, permissions);
            _roleRepository.SaveChange();
            return operationResult.Succeded();
        }

        public EditRole GetDetails(long id)
        {
            return _roleRepository.GetDetails(id);
        }

        public List<RoleViewModel> RoleList()
        {
            return _roleRepository.List();
        }
    }
}
