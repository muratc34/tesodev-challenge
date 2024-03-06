using Shared.Core.Primitives;

namespace Shared.Errors
{
    public static class ErrorMessages
    {
        public static class General
        {
            public static Error UnProcessableRequest => new Error("General.UnProcessableRequest", "The server could not process the request.");
            public static Error ServerError => new Error("General.ServerError", "The server encountered an unrecoverable error.");
        }

        public static class Customer
        {
            public static Error NotExist => new Error("Customer.NotExist", "The customer is not exist.");
        }
    }
}
