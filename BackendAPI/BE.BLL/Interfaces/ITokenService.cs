namespace BackendAPI.BE.BLL.Interfaces;
using UserModel = BackendAPI.BE.API.DTO.UserDTO;
public interface ITokenService
{
    string CreateAccessToken(UserModel user);
    string GenerateRandomStringToken();
}