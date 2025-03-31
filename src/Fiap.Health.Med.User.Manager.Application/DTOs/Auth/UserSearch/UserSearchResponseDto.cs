using Fiap.Health.Med.Infra.Enums;

namespace Fiap.Health.Med.User.Manager.Application.DTOs.Auth.UserSearch
{
    public class UserSearchResponseDto
    {
        public required string Username { get; set; }
        public required string HashPassword { get; set; }
        public EUserType UserType { get; set; }
    }
}
