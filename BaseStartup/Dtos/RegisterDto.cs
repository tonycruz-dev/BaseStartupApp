namespace BaseStartup.Dtos;

public class RegisterDto : LoginDto
{
    public string Email { get; set; } = string.Empty;
    public string NickName { get; set; } = String.Empty;
    public string Avatar { get; set; } = String.Empty;
}
