using JWTLoginAuthorizationRoleBased.Generics;
using JWTLoginAuthorizationRoleBased.Model;

namespace JWTLoginAuthorizationRoleBased.Services
{
    public class CustomerService : ICustomerService
    {
        private readonly IGenericRepository<Customer> _customerRepo;

        public CustomerService(IGenericRepository<Customer> customerRepo)
        {
            _customerRepo = customerRepo;
        }

        public async Task<IEnumerable<Customer>> GetCustomers() => await _customerRepo.GetAll();

        public async Task<Customer> GetCustomerById(int id) => await _customerRepo.GetById(id);

        public async Task AddCustomer(Customer customer) => await _customerRepo.Add(customer);

        public async Task UpdateCustomer(Customer customer) => await _customerRepo.Update(customer);

        public async Task DeleteCustomer(int id) => await _customerRepo.Delete(id);
    }
}
