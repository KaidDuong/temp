namespace Rikkonbi.WebAPI.Helpers
{
    public static class Roles
    {
        public const string ADMIN = "Admin";
        public const string SALES = "Sales";
        public const string USER = "User";
    }

    public static class Policies
    {
        public const string ADMIN_OR_SALES = "AdminOrSales";
    }

    public static class BaseURIs
    {
#if (!DEBUG)
        public const string API_ROOT = "http://rikkonbi.com/api";
        public const string STATIC_RESOURCES = "http://rikkonbi.com/resources";
#else
        public const string API_ROOT = /*"https://localhost:44366*/"https://192.168.68.240:45456";
        public const string STATIC_RESOURCES = /*"https://localhost:44366*/"https://192.168.68.240:45456/resources";
#endif
    }

    public enum PAYMENT_STATUS
    {
        Unpaid = 1,
        Paid
    }
}