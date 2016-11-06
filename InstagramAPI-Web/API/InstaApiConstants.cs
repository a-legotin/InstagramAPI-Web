namespace InstagramApi.API
{
    public static class InstaApiConstants
    {
        public static string INSTAGRAM_URL = "https://www.instagram.com/";
        public static string GET_ALL_POSTFIX = "/?__a=1";
        public static string MAX_MEDIA_ID_POSTFIX = "/media/?max_id=";
        public static string MEDIA = "/media/";
        public static string P_SUFFIX = "p/";
        public static string ACCOUNTS_LOGIN = "/accounts/login/";
        public static string ACCOUNTS_LOGIN_AJAX = $"{ACCOUNTS_LOGIN}ajax/";
        public static string CSRFTOKEN = "csrftoken";

        public static string HEADER_XCSRFToken = "X-CSRFToken";
        public static string HEADER_XInstagramAJAX = "X-Instagram-AJAX";
        public static string HEADER_XRequestedWith = "X-Requested-With";
        public static string HEADER_XMLHttpRequest = "XMLHttpRequest";
    }
}