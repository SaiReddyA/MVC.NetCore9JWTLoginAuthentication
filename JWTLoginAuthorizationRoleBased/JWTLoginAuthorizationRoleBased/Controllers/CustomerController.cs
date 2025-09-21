namespace JWTLoginAuthorizationRoleBased.Controllers
{
    [Route("[controller]")]
    public class CustomerController : Controller
    {
        private readonly ICustomerService _service;

        public CustomerController(ICustomerService service)
        {
            _service = service;
        }

        [Route("[action]")]
        public async Task<IActionResult> Index()
        {
            var customers = await _service.GetCustomers();
            return View(customers);
        }

        [Route("[action]")]
        public IActionResult Create() => View();

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Create(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _service.AddCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        [Route("[action]")]
        public async Task<IActionResult> Edit(int id)
        {
            var customer = await _service.GetCustomerById(id);
            return View(customer);
        }

        [HttpPost]
        [Route("[action]")]
        public async Task<IActionResult> Edit(Customer customer)
        {
            if (ModelState.IsValid)
            {
                await _service.UpdateCustomer(customer);
                return RedirectToAction(nameof(Index));
            }
            return View(customer);
        }

        [Route("[action]")]
        public async Task<IActionResult> Delete(int id)
        {
            var customer = await _service.GetCustomerById(id);
            return View(customer);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _service.DeleteCustomer(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
