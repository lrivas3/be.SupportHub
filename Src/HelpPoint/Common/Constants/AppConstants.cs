namespace HelpPoint.Common.Constants;

public class AppConstants
{
    public enum EstadosSolicitudes
    {
        ABIERTA = 1,
        APROBADA = 2,
        RECHAZADA = 3
    }

    public static class ErrorMessages
    {
        public static string RolesNotFoundMsg { get; set; } = "Roles for user not found";
    }
}
