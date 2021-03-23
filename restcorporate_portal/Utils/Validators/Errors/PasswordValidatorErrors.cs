using System;
namespace restcorporate_portal.Utils.Validators.Errors
{
    public static class PasswordValidatorErrors
    {
        public static readonly string RequireDigit = "PASSWORD_REQUIRE_DIGIT";
        public static readonly string NotEmpty = "PASSWORD_MUST_NOT_BE_EMPTY";
        public static readonly string RequireLength = "PASSWORD_REQUIRE_LENGTH_MORE_THEN_6_AND_LESS_THEN_20";
        public static readonly string ContainLatinChars = "PASSWORD_CONTAIN_LATIN_SYMBOLS";
    }
}
