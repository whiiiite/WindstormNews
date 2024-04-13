namespace NewsApp.Extentions
{
    public static class StringExtensions
    {
        public static string FixImagePath(this string path)
        {
            if(path == null) return string.Empty;
            return path.Replace('\\', '/').Replace("wwwroot/", "");
        }
    }
}
