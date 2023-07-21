using BCrypt.Net;

public class CustomPasswordHasher : ICustomPasswordHasher
{
  public string HashPassword(string password)
  {
    // Generate a salt and hash the password using BCrypt
    return BCrypt.Net.BCrypt.HashPassword(password);
  }

  public bool VerifyPassword(string password, string hashedPassword)
  {
    // Verify the provided password with the stored hashed password using BCrypt
    return BCrypt.Net.BCrypt.Verify(password, hashedPassword);
  }
}
