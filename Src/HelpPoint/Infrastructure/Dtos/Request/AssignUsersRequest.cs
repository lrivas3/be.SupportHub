using System.ComponentModel.DataAnnotations;
using HelpPoint.Common.Constants;

namespace HelpPoint.Infrastructure.Dtos.Request;

public class AssignUsersRequest
{
    [Required(ErrorMessage = "Debe seleccionar al menos un usuario")]
    public List<string> Users { get; set; } = null!;
}
