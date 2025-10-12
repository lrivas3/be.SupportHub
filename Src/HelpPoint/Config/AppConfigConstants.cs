namespace HelpPoint.Config;

public static class AppConfigConstants
{
    public static class RolesConstants
    {
        public const string Admin           = "Admin";
        public const string AdminNormalized = "ADMIN";
        public static readonly Guid AdminId = Guid.Parse("01956042-3344-70a8-99d7-7a337595c1ea");

        public const string AreaManager           = "AreaManager";
        public const string AreaManagerNormalized = "AREAMANAGER";
        public static readonly Guid AreaManagerId = Guid.Parse("01956042-3344-7953-b575-59d8f088a283");

        public const string SupportStaff           = "SupportStaff";
        public const string SupportStaffNormalized = "SUPPORTSTAFF";
        public static readonly Guid SupportStaffId = Guid.Parse("01956042-3344-7a85-8117-020290a145f9");

    }
    public static readonly int CODIGO_SP_REQUEST_RECHAZADA = 3;

}
