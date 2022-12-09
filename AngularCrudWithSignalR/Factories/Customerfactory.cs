using AngularCrudWithSignalR.Attributes;
using AngularCrudWithSignalR.Data.Entites;
using AngularCrudWithSignalR.Models;
using AngularCrudWithSignalR.Services;

namespace AngularCrudWithSignalR.Factories
{
    public partial class Customerfactory : ICustomerfactory
    {

        #region fields

        private readonly ICustomerService _customerService;
        private readonly IAddressService _addressService;
        private readonly IStateService _stateService;
        private readonly ICountryService _countryService;
        private readonly IDownloadService _downloadService;
        private readonly IGenericAttributeService _genericAttributeService;

        #endregion

        #region ctor

        public Customerfactory(
            ICustomerService customerService, IAddressService addressService,
            IStateService stateService,
            ICountryService countryService,
             IGenericAttributeService genericAttributeService, IDownloadService downloadService)
        {
            _customerService = customerService;
            _addressService = addressService;
            _stateService = stateService;
            _countryService = countryService;
            _genericAttributeService = genericAttributeService;
            _downloadService = downloadService;
        }

        #endregion

        public async Task<IList<CustomerModel>> PrepareAllCustomer()
        {
            var customerListModel = new List<CustomerModel>();

            var customers = await _customerService.GetCustomersAsync();

            foreach (var cust in customers)
            {
                var customerModel = new CustomerModel();
                var customerFirstName = await _genericAttributeService.GetGenericAttributeAsync(CustomerAttributes.FirstName, cust.Id);
                var customerLastName = await _genericAttributeService.GetGenericAttributeAsync(CustomerAttributes.LastName, cust.Id);
                customerModel.Fullname = $"{customerFirstName.Value} {customerLastName.Value}";
                customerModel.Email = cust.Email;
                customerModel.FileId = (await _downloadService.GetDownloadById(cust.PictureId))?.Id ?? 0;
                customerModel.Gender = (await _genericAttributeService.GetGenericAttributeAsync(CustomerAttributes.Gender, cust.Id))?.Value ?? string.Empty;
                customerModel.Phone = (await _genericAttributeService.GetGenericAttributeAsync(CustomerAttributes.Phone, cust.Id))?.Value ?? string.Empty; ;
                customerModel.DOB = (await _genericAttributeService.GetGenericAttributeAsync(CustomerAttributes.DOB, cust.Id))?.Value ?? string.Empty; ;
                customerModel.Id = cust.Id;
                customerListModel.Add(customerModel);
            }

            return customerListModel;
        }

        public async Task<CustomerModel> PrepareCustomerById(int customerId)
        {
            var cust = await _customerService.GetCustomerById(customerId);

            var customerModel = new CustomerModel();
            var customerFirstName = await _genericAttributeService.GetGenericAttributeAsync(CustomerAttributes.FirstName, cust.Id);
            var customerLastName = await _genericAttributeService.GetGenericAttributeAsync(CustomerAttributes.LastName, cust.Id);
            customerModel.Fullname = $"{customerFirstName.Value} {customerLastName.Value}";

            customerModel.FirstName = customerFirstName.Value;
            customerModel.LastName = customerLastName.Value;

            customerModel.Email = cust.Email;
            customerModel.FileId = (await _downloadService.GetDownloadById(cust.PictureId))?.Id ?? 0;
            customerModel.FileName = (await _downloadService.GetDownloadById(cust.PictureId))?.FileName ?? string.Empty;
            customerModel.Gender = (await _genericAttributeService.GetGenericAttributeAsync(CustomerAttributes.Gender, cust.Id))?.Value ?? string.Empty;
            customerModel.Phone = (await _genericAttributeService.GetGenericAttributeAsync(CustomerAttributes.Phone, cust.Id))?.Value ?? string.Empty; ;
            customerModel.DOB = (await _genericAttributeService.GetGenericAttributeAsync(CustomerAttributes.DOB, cust.Id))?.Value ?? string.Empty; ;
            customerModel.Id = cust.Id;

            return customerModel;
        }

        public async Task AddCustomerAsync(CustomerModel model)
        {
            if (model is null)
                throw new ArgumentNullException();

            var customer = new Customer()
            {
                Email = model?.Email ?? string.Empty,
                PassWord = EncodePasswordToBase64(model?.Password ?? string.Empty),
                PictureId = model?.FileId ?? 0
            };

            await _customerService.InsertCustomerAsync(customer);

            await _genericAttributeService.InsertGenericAttributeAsync(new GenericAttribute()
            {
                CustomerId = customer.Id,
                Key = CustomerAttributes.Gender,
                Value = model?.Gender ?? string.Empty
            });
            await _genericAttributeService.InsertGenericAttributeAsync(new GenericAttribute()
            { CustomerId = customer.Id, Key = CustomerAttributes.FirstName, Value = model?.FirstName ?? string.Empty });
            await _genericAttributeService.InsertGenericAttributeAsync(new GenericAttribute()
            { CustomerId = customer.Id, Key = CustomerAttributes.LastName, Value = model?.LastName ?? string.Empty });
            await _genericAttributeService.InsertGenericAttributeAsync(new GenericAttribute()
            { CustomerId = customer.Id, Key = CustomerAttributes.DOB, Value = model?.DOB ?? string.Empty });
            await _genericAttributeService.InsertGenericAttributeAsync(new GenericAttribute()
            { CustomerId = customer.Id, Key = CustomerAttributes.Phone, Value = model?.Phone ?? string.Empty });

        }

        public static string EncodePasswordToBase64(string password)
        {
            try
            {
                if (string.IsNullOrEmpty(password))
                    return string.Empty;

                byte[] encData_byte = new byte[password.Length];
                encData_byte = System.Text.Encoding.UTF8.GetBytes(password);
                string encodedData = Convert.ToBase64String(encData_byte);
                return encodedData;
            }
            catch (Exception ex)
            {
                throw new Exception("Error in base64Encode" + ex.Message);
            }
        }

        public async Task DeleteCustomerAsync(int id)
        {
            var customer = await _customerService.GetCustomerById(id);

            if (customer is null)
                return;

            await _genericAttributeService.DeleteAllGenericAttributeAsync(customer.Id);
            var file = await _downloadService.GetDownloadById(customer.PictureId);
            if (file != null)
                await _downloadService.DeleteDownloadAsync(file);

            await _customerService.DeleteCustomerAsync(customer);
        }

        public async Task UpdateCustomerAsync(CustomerModel model)
        {
            var customer = await _customerService.GetCustomerById(model.Id);

            if (customer is null)
                return;

            customer.Email = model.Email;
            customer.PictureId = model.FileId;

            await _customerService.UpdateCustomerAsync(customer);

            var dict = new Dictionary<string, string>();

            dict.Add(CustomerAttributes.Gender, model.Gender);
            dict.Add(CustomerAttributes.LastName, model.LastName);
            dict.Add(CustomerAttributes.FirstName, model.FirstName);
            dict.Add(CustomerAttributes.DOB, model.DOB);
            dict.Add(CustomerAttributes.Phone, model.Phone);

            await _genericAttributeService.UpdateAllGenericAttribute(customer.Id, dict);


        }
    }

}