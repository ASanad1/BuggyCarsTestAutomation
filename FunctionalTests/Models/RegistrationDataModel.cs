using System;

namespace BuggyCarsTestAutomation.FunctionalTests.Models
{
    class RegistrationDataModel
    {
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Password { get; set; }
        public string ConfirmPassword { get; set; }

        public void GenerateRandomUsername() => Username = Guid.NewGuid().ToString();
    }
}
