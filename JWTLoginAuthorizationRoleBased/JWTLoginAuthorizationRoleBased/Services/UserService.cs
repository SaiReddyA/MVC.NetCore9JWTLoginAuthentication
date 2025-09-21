namespace JWTLoginAuthorizationRoleBased.Services
{
    public class UserService : IUserService
    {
        private readonly IConfiguration _config;

        private List<User> _users = new()
    {
        new User { Id = 1, Name = "Admin", Gender = "M", DateOfBirth = new DateTime(1990,1,1), Email = "admin@example.com", PhoneNumber = "1234567890", Password = "admin123", Role = "Admin" },
        new User { Id = 2, Name = "User", Gender = "F", DateOfBirth = new DateTime(1995,5,5), Email = "user@example.com", PhoneNumber = "0987654321", Password = "user123", Role = "User" }
    };

        public UserService(IConfiguration config)
        {
            _config = config;
        }

        public User Authenticate(string email, string password)
        {
            return _users.FirstOrDefault(u => u.Email == email && u.Password == password);
        }

        public string GenerateJwtToken(User user)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var key = Encoding.ASCII.GetBytes(_config["Jwt:Key"]);
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new[]
                {
                new Claim(ClaimTypes.Name, user.Email),
                new Claim(ClaimTypes.Role, user.Role)
            }),
                Expires = DateTime.UtcNow.AddHours(1),
                SigningCredentials = new SigningCredentials(new SymmetricSecurityKey(key), SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }

}
