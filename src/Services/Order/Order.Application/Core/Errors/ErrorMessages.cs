using Shared.Core.Primitives;

namespace Order.Application.Core.Errors
{
    public static class ErrorMessages
    {
        public static class General
        {
            public static Error UnProcessableRequest => new Error("General.UnProcessableRequest", "The server could not process the request.");
            public static Error ServerError => new Error("General.ServerError", "The server encountered an unrecoverable error.");
        }

        public static class Order
        {
            public static Error NotExist => new Error("Order.NotExist", "The order is not exist.");
            public static Error StatusNotExist => new Error("Order.StatusNotExist", "The status is not exist.");
        }

        public static class Product
        {
            public static Error NotExist => new Error("Product.NotExist", "The product is not exist.");
        }

        public static class Address
        {
            public static Error NotExist => new Error("Address.NotExist", "The address is not exist.");
        }
    }
}
