using System.Text;

namespace FormPicker.Utils
{
    public static class StringUtil
    {
        private static readonly Encoding Encoding1252;
        private static readonly Encoding EncodingUTF8;

        static StringUtil()
        {
            Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
            Encoding1252 = Encoding.GetEncoding(1252);
            EncodingUTF8 = Encoding.UTF8;
        }

        public static string Enc1252ToUTF8(string? enc1252)
        {
            if (enc1252 == null) return string.Empty;
            return EncodingUTF8.GetString(Encoding1252.GetBytes(enc1252));
        }

        public static string UTF8ToEnc1252(string? utf8)
        {
            if (utf8 == null) return string.Empty;
            return Encoding1252.GetString(EncodingUTF8.GetBytes(utf8));
        }
    }
}
