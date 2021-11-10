using AutoMapper;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using VoicePlatform.Data;
using VoicePlatform.Data.Application;
using VoicePlatform.Data.Entities;
using VoicePlatform.Data.Repositories.Interfaces;
using VoicePlatform.Data.Requests;
using VoicePlatform.Data.Responses;
using VoicePlatform.Service.Helpers;
using VoicePlatform.Service.Interfaces;
using VoicePlatform.Utility.Constants;
using VoicePlatform.Utility.Enums;

namespace VoicePlatform.Service.Implementations
{
    public class CustomerService : BaseService, ICustomerService
    {
        private readonly ICustomerRepository _customerRepository;
        private readonly IUserRepository _userRepository;
        private readonly IGenderRepository _genderRepository;
        private readonly IMapper _mapper;

        public CustomerService(IUnitOfWork unitOfWork, IMapper mapper) : base(unitOfWork)
        {
            _customerRepository = unitOfWork.Customer;
            _userRepository = unitOfWork.User;
            _genderRepository = unitOfWork.Gender;
            _mapper = mapper;
        }

        public async Task<object> GetAllCustomer(
            Pagination pagination, Dictionary<AdminFilterCustomer, string> filter,
            Dictionary<AdminSortCustomer, bool> sort, string searchString)
        {
            var customer = _customerRepository.GetAll()
                .Include(x => x.GenderNavigation).AsEnumerable();
            if (customer.Any())
            {
                if (!string.IsNullOrEmpty(searchString))
                {
                    customer = customer.Where(x => x.FirstName.Contains(searchString) ||
                    x.LastName.Contains(searchString, StringComparison.OrdinalIgnoreCase));
                }

                if (filter.Any())
                {
                    foreach (var fil in filter)
                    {
                        switch (fil.Key)
                        {
                            case AdminFilterCustomer.Status:
                                Enum.TryParse(fil.Value, out UserStatus customerStatus);
                                switch (customerStatus)
                                {
                                    case UserStatus.Activated:
                                        customer = customer.Where(x => x.Status.Equals((int)UserStatus.Activated));
                                        break;
                                    case UserStatus.Banned:
                                        customer = customer.Where(x => x.Status.Equals((int)UserStatus.Banned));
                                        break;
                                    default:
                                        break;
                                }
                                break;
                            case AdminFilterCustomer.Gender:
                                customer = customer.Where(x => x.GenderNavigation.Name.Equals(fil.Value));
                                break;
                            default:
                                break;
                        }
                    }
                }
                if (sort.Any())
                {
                    var sortBy = sort.FirstOrDefault();
                    switch (sortBy.Key)
                    {
                        case AdminSortCustomer.Name:
                            if (sortBy.Value)
                            {
                                customer = customer.OrderBy(x => x.FirstName);
                            }
                            else
                            {
                                customer = customer.OrderByDescending(x => x.FirstName);
                            }
                            break;
                        case AdminSortCustomer.Email:
                            if (sortBy.Value)
                            {
                                customer = customer.OrderBy(x => x.Email);
                            }
                            else
                            {
                                customer = customer.OrderByDescending(x => x.Email);
                            }
                            break;
                        case AdminSortCustomer.Gender:
                            if (sortBy.Value)
                            {
                                customer = customer.OrderBy(x => x.GenderNavigation.Name);
                            }
                            else
                            {
                                customer = customer.OrderByDescending(x => x.GenderNavigation.Name);
                            }
                            break;
                        case AdminSortCustomer.Status:
                            if (sortBy.Value)
                            {
                                customer = customer.OrderBy(x => x.Status);
                            }
                            else
                            {
                                customer = customer.OrderByDescending(x => x.Status);
                            }
                            break;
                        default:
                            break;
                    }
                }
                List<CustomerResponse> listCustomer = new List<CustomerResponse>();
                customer.ToList().ForEach(x => listCustomer.Add(_mapper.Map<CustomerResponse>(x)));
                
                var total = listCustomer.Count;
                var data = listCustomer.Skip((pagination.PageNumber - 1) * pagination.PageSize).Take(pagination.PageSize).ToList();
                return Response.OK(data, total);
            }

            return null;
        }

        public async Task<CustomerResponse> GetCustomerById(Guid id)
        {
            var customer = await _customerRepository.GetMany(x => x.Id.Equals(id)).ToListAsync();
            return _mapper.Map<CustomerResponse>(customer.FirstOrDefault(), opt => opt.AfterMap((dest, src) => src.Gender = _genderRepository.FirstOrDefault(x => x.Id.Equals(customer.FirstOrDefault().Gender)).Name));
        }

        public async Task<object> RegisterCustomer(CustomerRequest customer)
        {
            List<Error> errors = new List<Error>();
            var errorTitle = "RegisterCustomer";
            if (_customerRepository.GetMany(x => x.Username.Equals(customer.Username)).Any())
            {
                errors.Add(addError(errorTitle, ErrorMessage.USERNAME_EXISTED));
            }
            if (_customerRepository.GetMany(x => x.Email.Equals(customer.Email)).Any())
            {
                errors.Add(addError(errorTitle, ErrorMessage.EMAIL_EXISTED));
            }
            if (_customerRepository.GetMany(x => x.Phone.Equals(customer.Phone)).Any())
            {
                errors.Add(addError(errorTitle, ErrorMessage.PHONE_EXISTED));
            }
            if (_genderRepository.GetMany(x => x.Name.Equals(customer.Gender)).Select(x => x.Id).FirstOrDefault() == Guid.Empty)
            {
                errors.Add(addError(errorTitle, ErrorMessage.GENDER_INVALID));
            }
            if (!errors.Any())
            {
                var newCustomer = _mapper.Map<Customer>(customer, opt => opt.AfterMap((src, dest) =>
                {
                    dest.Id = Guid.NewGuid();
                    dest.Avatar = string.Empty;
                    dest.Gender = _genderRepository.GetMany(x => x.Name.Equals(customer.Gender)).Select(x => x.Id).FirstOrDefault();
                    dest.Status = (int)UserStatus.Activated;
                    dest.CreateDate = DateTime.UtcNow;
                    dest.Role = (int)Role.Customer;
                }));
                try
                {
                    _customerRepository.Add(newCustomer);
                    await _unitOfWork.SaveChanges();
                }
                catch (Exception)
                {
                    return null;
                }
                return _mapper.Map<CustomerResponse>(newCustomer, opt => opt.AfterMap((src, dest) => dest.Gender = customer.Gender));
            }
            else
            {
                return errors;
            }
        }

        public async Task<bool> UpdateAvatar(Guid id, string url)
        {
            url = url ?? string.Empty;
            var customer = _customerRepository.FirstOrDefault(x => x.Id.Equals(id));
            customer.Avatar = url;
            try
            {
                customer.UpdateDate = DateTime.UtcNow;
                _customerRepository.Update(customer);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        public async Task<bool> UpdatePassword(string email, string password)
        {
            var customer = _customerRepository.FirstOrDefault(x => x.Email.Equals(email));
            if (customer is null)
            {
                return false;
            }
            customer.Password = password;
            try
            {
                customer.UpdateDate = DateTime.UtcNow;
                _customerRepository.Update(customer);
                await _unitOfWork.SaveChanges();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }

        public async Task<object> UpdateCustomer(UpdateUserRequest cust, Guid id)
        {
            List<Error> errors = new List<Error>();
            var errorTitle = "UpdateCustomer";
            if (_customerRepository.GetMany(x => x.Email.Equals(cust.Email) && !x.Id.Equals(id)).Any())
            {
                errors.Add(addError(errorTitle, ErrorMessage.EMAIL_EXISTED));
            }
            if (_customerRepository.GetMany(x => x.Phone.Equals(cust.Phone) && !x.Id.Equals(id)).Any())
            {
                errors.Add(addError(errorTitle, ErrorMessage.PHONE_EXISTED));
            }
            if (_genderRepository.GetMany(x => x.Name.Equals(cust.Gender)).Select(x => x.Id).FirstOrDefault() == Guid.Empty)
            {
                errors.Add(addError(errorTitle, ErrorMessage.GENDER_INVALID));
            }
            if (!errors.Any())
            {
                var customer = _customerRepository.FirstOrDefault(x => x.Id.Equals(id));
                try
                {
                    customer.FirstName = cust.FirstName;
                    customer.LastName = cust.LastName;
                    customer.Email = cust.Email;
                    customer.Phone = cust.Phone;
                    customer.Gender = _genderRepository.GetMany(x => x.Name.Equals(cust.Gender)).Select(x => x.Id).FirstOrDefault();
                    customer.UpdateDate = DateTime.UtcNow;
                    _customerRepository.Update(customer);
                    await _unitOfWork.SaveChanges();
                }
                catch (Exception)
                {
                    return null;
                }
                return _mapper.Map<CustomerResponse>(customer, opt => opt.AfterMap((src, dest) => dest.Gender = cust.Gender));
            }
            else
            {
                return errors;
            }
        }

        public async Task<bool> UpdateStatusCustomer(Guid userId, Guid adminId, UserStatus status)
        {
            var customer = _customerRepository.FirstOrDefault(x => x.Id.Equals(userId));
            try
            {
                customer.Status = (int)status;
                customer.UpdateDate = DateTime.UtcNow;
                customer.UpdateBy = _userRepository.FirstOrDefault(x => x.Id.Equals(adminId)).Id;
                _customerRepository.Update(customer);
                await _unitOfWork.SaveChanges();
            }
            catch (Exception)
            {
                return false;
            }
            return true;
        }

        private Error addError(string title, string message)
        {
            return new Error()
            {
                Title = title,
                Message = message
            };
        }

    }
}
