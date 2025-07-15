using System;
using System.Text.RegularExpressions;
using System.Collections.Generic;
using RecipeProject.Domain.Entities;
using RecipeProject.Application.Interfaces;
using BCrypt.Net;

namespace RecipeProject.Application.UseCases
{
    public class CreateUserUseCase
    {
        private readonly IUserRepository _userRepository;
        private readonly HashSet<string> _bannedEmailDomains = new HashSet<string>
        {
            "tempmail.com", "mailinator.com", "10minutemail.com"
        };
        private readonly HashSet<string> _commonPasswords = new HashSet<string>
        {
            "password", "12345678", "qwerty", "abc123", "111111", "123456789"
        };

        public CreateUserUseCase(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public void Execute(User user, string plainPassword)
        {
            if (user == null)
                throw new ArgumentNullException(nameof(user), "User object is required.");

            if (string.IsNullOrWhiteSpace(user.Name) || user.Name.Length > 100)
                throw new ArgumentException("Name is required and must be less than 100 characters.");
            if (!Regex.IsMatch(user.Name, @"^[a-zA-Z·ÈÌÛ˙¡…Õ”⁄¸‹Ò—\s\-]+$"))
                throw new ArgumentException("Name can only contain letters, spaces, and hyphens.");

            if (string.IsNullOrWhiteSpace(user.Email))
                throw new ArgumentException("Email is required.");
            user.Email = user.Email.Trim().ToLowerInvariant();
            if (!IsValidEmail(user.Email))
                throw new ArgumentException("Invalid email format.");
            if (user.Email.Contains(" "))
                throw new ArgumentException("Email cannot contain spaces.");
            if (IsBannedDomain(user.Email))
                throw new ArgumentException("Email domain is not allowed.");

            if (_userRepository.GetByEmail(user.Email) != null)
                throw new InvalidOperationException("Email is already registered.");

            if (!IsStrongPassword(plainPassword, user.Name, user.Email))
                throw new ArgumentException("Password must be at least 8 characters, contain a letter, a number, and a special character, and must not contain your name or email.");
            if (_commonPasswords.Contains(plainPassword.ToLowerInvariant()))
                throw new ArgumentException("Password is too common. Please choose a stronger password.");

            user.PasswordHash = BCrypt.Net.BCrypt.HashPassword(plainPassword);
            user.RegistrationDate = DateTime.UtcNow;

            _userRepository.Add(user);
        }

        private bool IsValidEmail(string email)
        {
            try
            {
                return Regex.IsMatch(email,
                    @"^[^@\s]+@[^@\s]+\.[^@\s]+$",
                    RegexOptions.IgnoreCase);
            }
            catch
            {
                return false;
            }
        }

        private bool IsBannedDomain(string email)
        {
            var atIdx = email.LastIndexOf('@');
            if (atIdx < 0) return true;
            var domain = email.Substring(atIdx + 1);
            return _bannedEmailDomains.Contains(domain);
        }

        private bool IsStrongPassword(string password, string userName, string email)
        {
            if (string.IsNullOrWhiteSpace(password) || password.Length < 8)
                return false;

            bool hasLetter = false, hasDigit = false, hasSpecial = false;
            foreach (var c in password)
            {
                if (char.IsLetter(c)) hasLetter = true;
                else if (char.IsDigit(c)) hasDigit = true;
                else if (!char.IsWhiteSpace(c)) hasSpecial = true;
            }

            if (!string.IsNullOrEmpty(userName) && password.ToLower().Contains(userName.ToLower()))
                return false;
            if (!string.IsNullOrEmpty(email) && password.ToLower().Contains(email.ToLower()))
                return false;

            return hasLetter && hasDigit && hasSpecial;
        }
    }
}