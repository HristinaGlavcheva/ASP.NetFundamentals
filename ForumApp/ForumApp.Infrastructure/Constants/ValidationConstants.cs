namespace ForumApp.Infrastructure.Constants
{
    /// <summary>
    /// Validation Constants
    /// </summary>
    public static class ValidationConstants
    {
        /// <summary>
        /// Maximak Post Title length
        /// </summary>
        public const int TitleMaxLength = 50;

        /// <summary>
        /// Minimal Post Title length
        /// </summary>
        public const int TitleMinLength = 10;

        /// <summary>
        /// Maximal Post Content Length
        /// </summary>
        public const int ContentMaxLength = 1500;

        /// <summary>
        /// Mimimal Post Content Length
        /// </summary>
        public const int ContentMinLength = 30;

        /// <summary>
        /// Require Error message text
        /// </summary>
        public const string RequireErrorMesage = "The {0} field is required";

        public const string StringLengthErrorMessage = "The {0} field should be between {2} and {1} characters long";
    }
}
