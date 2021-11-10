using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using VoicePlatform.Data;
using VoicePlatform.Data.Repositories.Interfaces;
using VoicePlatform.Data.Requests;
using VoicePlatform.Data.Responses;
using VoicePlatform.Utility.Constants;

namespace VoicePlatform.Service.Helpers
{
    public class Validation
    {
        public Validation()
        {

        }

        public List<Error> ValidateCustomer(CustomerRequest customer, string purposes)
        {
            var errors = new List<Error>();


            if (string.IsNullOrEmpty(customer.Username))
            {
                errors.Add(addError(purposes, ErrorMessage.USERNAME_NULL));
            }
            else if (customer.Username.Length < 5 || customer.Username.Length > 35)
            {
                errors.Add(addError(purposes, ErrorMessage.USERNAME_RANGE));
            }

            if (string.IsNullOrEmpty(customer.Password))
            {
                errors.Add(addError(purposes, ErrorMessage.PASSWORD_NULL));
            }
            else if (customer.Password.Length < 5 && customer.Password.Length > 35)
            {
                errors.Add(addError(purposes, ErrorMessage.PASSWORD_RANGE));
            }

            if (string.IsNullOrEmpty(customer.Email))
            {
                errors.Add(addError(purposes, ErrorMessage.EMAIL_NULL));
            }
            else if (!new EmailAddressAttribute().IsValid(customer.Email))
            {
                errors.Add(addError(purposes, ErrorMessage.EMAIL_INVALID));
            }

            if (!string.IsNullOrEmpty(customer.Phone) && !Regex.Match(customer.Phone, @"(84|0[3|5|7|8|9])+([0-9]{8})$").Success)
            {
                errors.Add(addError(purposes, ErrorMessage.PHONE_INVALID));
            }

            if (string.IsNullOrEmpty(customer.FirstName))
            {
                errors.Add(addError(purposes, ErrorMessage.FIRSTNAME_NULL));
            }
            else if (customer.FirstName.Length < 1 || customer.FirstName.Length > 50)
            {
                errors.Add(addError(purposes, ErrorMessage.FIRSTNAME_RANGE));
            }

            if (string.IsNullOrEmpty(customer.LastName))
            {
                errors.Add(addError(purposes, ErrorMessage.LASTNAME_NULL));
            }
            else if (customer.LastName.Length < 1 || customer.LastName.Length > 50)
            {
                errors.Add(addError(purposes, ErrorMessage.LASTNAME_RANGE));
            }
            return errors;
        }
        
        public List<Error> ValidateArtist(ArtistRequest artist, string purposes)
        {
            var errors = new List<Error>();


            if (string.IsNullOrEmpty(artist.Username))
            {
                errors.Add(addError(purposes, ErrorMessage.USERNAME_NULL));
            }
            else if (artist.Username.Length < 5 || artist.Username.Length > 35)
            {
                errors.Add(addError(purposes, ErrorMessage.USERNAME_RANGE));
            }

            if (string.IsNullOrEmpty(artist.Password))
            {
                errors.Add(addError(purposes, ErrorMessage.PASSWORD_NULL));
            }
            else if (artist.Password.Length < 5 && artist.Password.Length > 35)
            {
                errors.Add(addError(purposes, ErrorMessage.PASSWORD_RANGE));
            }

            if (string.IsNullOrEmpty(artist.Email))
            {
                errors.Add(addError(purposes, ErrorMessage.EMAIL_NULL));
            }
            else if (!new EmailAddressAttribute().IsValid(artist.Email))
            {
                errors.Add(addError(purposes, ErrorMessage.EMAIL_INVALID));
            }

            if (!string.IsNullOrEmpty(artist.Phone) && !Regex.Match(artist.Phone, @"(84|0[3|5|7|8|9])+([0-9]{8})$").Success)
            {
                errors.Add(addError(purposes, ErrorMessage.PHONE_INVALID));
            }

            if (string.IsNullOrEmpty(artist.FirstName))
            {
                errors.Add(addError(purposes, ErrorMessage.FIRSTNAME_NULL));
            }
            else if (artist.FirstName.Length < 1 || artist.FirstName.Length > 50)
            {
                errors.Add(addError(purposes, ErrorMessage.FIRSTNAME_RANGE));
            }

            if (string.IsNullOrEmpty(artist.LastName))
            {
                errors.Add(addError(purposes, ErrorMessage.LASTNAME_NULL));
            }
            else if (artist.LastName.Length < 1 || artist.LastName.Length > 50)
            {
                errors.Add(addError(purposes, ErrorMessage.LASTNAME_RANGE));
            }
            return errors;
        }
        
        public List<Error> ValidateUpdateUser(UpdateUserRequest user, string purposes)
        {
            var errors = new List<Error>();

            if (string.IsNullOrEmpty(user.Email))
            {
                errors.Add(addError(purposes, ErrorMessage.EMAIL_NULL));
            }
            else if (!new EmailAddressAttribute().IsValid(user.Email))
            {
                errors.Add(addError(purposes, ErrorMessage.EMAIL_INVALID));
            }

            if (!string.IsNullOrEmpty(user.Phone) && !Regex.Match(user.Phone, @"(84|0[3|5|7|8|9])+([0-9]{8})$").Success)
            {
                errors.Add(addError(purposes, ErrorMessage.PHONE_INVALID));
            }

            if (string.IsNullOrEmpty(user.FirstName))
            {
                errors.Add(addError(purposes, ErrorMessage.FIRSTNAME_NULL));
            }
            else if (user.FirstName.Length < 1 || user.FirstName.Length > 50)
            {
                errors.Add(addError(purposes, ErrorMessage.FIRSTNAME_RANGE));
            }

            if (string.IsNullOrEmpty(user.LastName))
            {
                errors.Add(addError(purposes, ErrorMessage.LASTNAME_NULL));
            }
            else if (user.LastName.Length < 1 || user.LastName.Length > 50)
            {
                errors.Add(addError(purposes, ErrorMessage.LASTNAME_RANGE));
            }
            return errors;
        }

        public List<Error> ValidateProject(ProjectRequest project, string purposes)
        {
            var errors = new List<Error>();


            if (string.IsNullOrEmpty(project.Name))
            {
                errors.Add(addError(purposes, ErrorMessage.PROJECT_NAME_NULL));
            }
            else if (project.Name.Length < 3 || project.Name.Length > 50)
            {
                errors.Add(addError(purposes, ErrorMessage.PROJECT_NAME_LENGTH));
            }

            if (project.Description.Length.Equals(0))
            {
                errors.Add(addError(purposes, ErrorMessage.PROJECT_NAME_LENGTH));
            }

            if (project.MinAge > 65 || project.MinAge < 18)
            {
                errors.Add(addError(purposes, ErrorMessage.MIN_AGE_RANGE));
            }
            if (project.MaxAge > 65 || project.MaxAge < 18)
            {
                errors.Add(addError(purposes, ErrorMessage.MAX_AGE_RANGE));
            }
            if (project.MinAge > project.MaxAge)
            {
                errors.Add(addError(purposes, ErrorMessage.PROJECT_AGE_RANGE));
            }

            if (!project.ProjectCountries.Any())
            {
                errors.Add(addError(purposes, ErrorMessage.COUNTRY_NULL));
            }
            if (!project.ProjectUsagePurposes.Any())
            {
                errors.Add(addError(purposes, ErrorMessage.USAGEPURPOSE_NULL));
            }
            if (!project.ProjectVoiceStyles.Any())
            {
                errors.Add(addError(purposes, ErrorMessage.VOICESTYLE_NULL));
            }
            if (!project.ProjectGenders.Any())
            {
                errors.Add(addError(purposes, ErrorMessage.GENDER_NULL));
            }

            return errors;
        }

        public Error ValidateUrl(string url, string title)
        {
            if (url != null && !Regex.Match(url, @"^(?:http(s)?:\/\/)?[\w.-]+(?:\.[\w\.-]+)+[\w\-\._~:/?#[\]@!\$&'\(\)\*\+,;=.]+$").Success)
            {
                return new Error() { Title = title, Message = ErrorMessage.URL_INVALID };
            }

            return null;
        }
        
        public Error ValidatePassword(string password, string title)
        {
            if (string.IsNullOrEmpty(password))
            {
                return new Error() { Title = title, Message = ErrorMessage.PASSWORD_NULL };
            }
            else if (password.Length < 5 && password.Length > 35)
            {
                return new Error() { Title = title, Message = ErrorMessage.PASSWORD_RANGE };
            }
            return null;
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
