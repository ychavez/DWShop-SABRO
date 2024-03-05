namespace TaskAsync.Extensions
{
    public static class StringExtensions
    {
        /// <summary>
        /// Le agrega el Hola y una fecha al final
        /// </summary>
        /// <param name="s"></param>
        /// <returns></returns>
        public static string ToSaludo(this string s) => $"Hola {s} {DateTime.Now}";

    }
}
