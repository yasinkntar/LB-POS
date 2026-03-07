namespace LB_POS.Data.Commons
{
    public static class LocalizationExtension
    {
        public static string Localize(this object _, string ar, string en)
        {
            var lang = Thread.CurrentThread.CurrentCulture.TwoLetterISOLanguageName;

            return lang == "en" ? en : ar;
        }
    }
}
