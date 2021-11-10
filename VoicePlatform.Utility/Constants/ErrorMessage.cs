using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VoicePlatform.Utility.Constants
{
    public class ErrorMessage
    {
        //authenticate
        public const string LOGIN_FAIL = "ERR_LOGIN_FAILD";
        public const string TOKEN_INVALID = "TOKEN_INVALID";

        //register message
        public const string USERNAME_EXISTED = "Username existed";
        public const string EMAIL_EXISTED = "Email existed";
        public const string PHONE_EXISTED = "Phone existed";
        public const string USERNAME_NULL = "Username null";
        public const string EMAIL_NULL = "Email null";
        public const string PASSWORD_NULL = "Password null";
        public const string FIRSTNAME_NULL = "Firstname null";
        public const string LASTNAME_NULL = "Lastname null";
        public const string EMAIL_INVALID = "Email invalid";
        public const string PHONE_INVALID = "Phone invalid";
        public const string USERNAME_RANGE = "Username range";
        public const string PASSWORD_RANGE = "Password range";
        public const string FIRSTNAME_RANGE = "Firstname range";
        public const string LASTNAME_RANGE = "Lastname range";

        //post project message
        public const string PROJECT_NAME_NULL = "ProjectName null";
        public const string PROJECT_NAME_LENGTH = "ProjectName length";
        public const string PROJECT_AGE_RANGE = "ProjectAge range";
        public const string PROJECT_PRICE_RANGE = "ProjectPrice range";
        public const string MIN_AGE_RANGE = "MinAge range";
        public const string MAX_AGE_RANGE = "MaxAge range";
        public const string DESCRIPTION_NULL = "Description null";
        public const string VOICESTYLE_NULL = "VoiceStyle null";
        public const string GENDER_NULL = "Gender null";
        public const string COUNTRY_NULL = "Country null";
        public const string USAGEPURPOSE_NULL = "UsagePurpose null";

        //gender
        public const string GENDER_INVALID = "Gender invalid";

        //url
        public const string URL_INVALID = "Url invalid";

        //gender
        public const string GENDER_EXISTED = "Gender existed";
        public const string COUNTRY_EXISTED = "Country existed";
        public const string VOICESTYLE_EXISTED = "VoiceStyle existed";
        public const string USAGEPURPOSE_EXISTED = "UsagePurpose existed";
    }
}
