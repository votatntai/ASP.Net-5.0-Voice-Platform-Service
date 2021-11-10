using System.Collections;
using VoicePlatform.Utilities.Constants;

namespace VoicePlatform.Data.Application
{
    public class Response
    {
        public int? TotalRow { get; set; } = 0;

        public object Data { get; set; } = 0;


        public static Response OK()
        {
            return new Response
            {
            };
        }

        public static Response OK(object data, int? totalRow = 0)
        {
            if (! (data is ICollection))
            {
                totalRow = 1;
            }

            return new Response
            {
                Data = data,
                TotalRow = totalRow
            };
        }

        public static Response Created()
        {
            return new Response
            {
            };
        }

        public static Response Created(object data, int? totalRow = 0)
        {
            totalRow = 1;

            if (data is ICollection d)
            {
                totalRow = d.Count;
            }

            return new Response
            {
                Data = data,
                TotalRow = totalRow
            };
        }

        public static Response NotFound()
        {
            return new Response
            {
            };
        }

        public static Response NotFound(string message = null)
        {
            return new Response
            {
            };
        }

        public static Response BadRequest(string message = null)
        {
            return new Response
            {
            };
        }
        
        public static Response BadRequest(object data, int? totalRow = 0)
        {
            if (data is ICollection d)
            {
                totalRow = d.Count;
            }

            return new Response
            {
                Data = data,
                TotalRow = totalRow
            };
        }

        public static Response Unauthorized()
        {
            return new Response
            {
            };
        }

        public static Response Forbidden()
        {
            return new Response
            {
            };
        }
    }
}
