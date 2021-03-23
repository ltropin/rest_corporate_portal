using System;
namespace restcorporate_portal.Utils.Validators.Errors
{
    public static class NamesValidatorErrors
    {
        public static readonly string FirstNotEmpty = "FIRST_NAME_MUST_BE_NOT_EMPTY";
        public static readonly string LastNotEmpty = "LAST_NAME_MUST_BE_NOT_EMPTY";
        public static readonly string FirstMoreLess = "FIRST_NAME_MUST_BE_MORE_THAN_3_AND_LESS_THAN_25";
        public static readonly string LastMoreLess = "LAST_NAME_MUST_BE_MORE_THAN_3_AND_LESS_THAN_25";
        public static readonly string FirstIsUpper = "FIRST_NAME_MUST_BE_START_UPPER";
        public static readonly string LastIsUpper = "Last_NAME_MUST_BE_START_UPPER";
    }
}
