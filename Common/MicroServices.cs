using Common.Model;

namespace Common
{
    public class MicroServices
    {
        public static readonly Microservice IdentitySuite = new Microservice
        {
            Id = "identity-suite",
            Url = "https://localhost:5001"
        };

        public static readonly Microservice IgniteMobileAdmin = new Microservice
        {
            Id = "ignite-mobile-admin",
            Url = "https://localhost:9001"
        };

        public static readonly Microservice IgniteMobileCustomer = new Microservice
        {
            Id = "ignite-mobile-customer",
            Url = "https://localhost:7001"
        };

        public static readonly Microservice Subscribtions = new Microservice
        {
            Id = "subscribtions",
            Url = "https://localhost:4001"
        };

        public static readonly Microservice Reporting = new Microservice
        {
            Id = "reporting",
            Url = "https://localhost:6001"
        };
    }
}
