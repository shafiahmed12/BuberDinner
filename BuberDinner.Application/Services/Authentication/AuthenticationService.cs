using BuberDinner.Application.Common.Interfaces.Authentication;
using BuberDinner.Application.Common.Interfaces.Persistence;
using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Services.Authentication;

public class AuthenticationService : IAuthenticationService
{
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IUserRepository _userRepository;

    public AuthenticationService(
        IJwtTokenGenerator jwtTokenGenerator,
        IUserRepository userRepository)
    {
        _jwtTokenGenerator = jwtTokenGenerator;
        _userRepository = userRepository;
    }

    public AuthResult Login(string email, string password)
    {
        //1. Validate if user exists
        if(_userRepository.GetUserByEmail(email) is not User user)
        {
            throw new Exception("User with given email doesn't exist");
        }
        //2. Validate password is correct
        if(user.Password != password)
        {
            throw new Exception("Password is in correct");
        }
        //3. Create token
        var token = _jwtTokenGenerator.GenerateToken(user);
        return new AuthResult(
            user,
            token
            );
    }

    public AuthResult Register(string firstName, string lastName, string email, string password)
    {
        //1.Validate the user doesn't exist
        if(_userRepository.GetUserByEmail(email) is not null)
        {
            throw new Exception("User already exists");
        }
        //2.Create User & persist in Db
        var user = new User()
        {
            FirstName = firstName,
            LastName = lastName,
            Email = email,
            Password = password
        };
        _userRepository.Add(user);
       //Generate toknen
        var token = _jwtTokenGenerator.GenerateToken(user);
        
        return new AuthResult(
            user,
            token);
    }
}