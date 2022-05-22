namespace BuggyCarsTestAutomation.FunctionalTests.Common
{
    class Constants
    {
        public static string GlobalBrowser { get; set; }
        public static bool IsBrowserHeadless { get; set; }
        public static string GlobalBaseUrl { get; set; }
        public static int DriverImplicitWait { get; set; }

        public static string InvalidCredentialsError = "Invalid username/password";
        public static string SuccessfulVoteMessage = "Thank you for your vote!";
        public static string SuccessfulProfileUpdateMessage = "The profile has been saved successful";
        public static string SuccessfulRegistrationMessage = "Registration is successful";
        public static string ExistingRegistrationError = "UsernameExistsException: User already exists";
        public static string MismatchedPasswordsError = "Passwords do not match";
    }
}
