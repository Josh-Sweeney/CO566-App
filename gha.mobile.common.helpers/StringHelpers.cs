using System.Linq;
using System.Text;
using Xamarin.Forms;

namespace gha.mobile.common.helpers
{
    public static class StringHelpers
    {
        public static Color ToColor(this string color)
        {
            var arrColorFragments = color?.Split(',').Select(sFragment =>
            {
                int.TryParse(sFragment, out int fragment);
                return fragment;
            }).ToArray();

            switch (arrColorFragments?.Length)
            {
                case 3:
                    return System.Drawing.Color.FromArgb(arrColorFragments[0], arrColorFragments[1],
                        arrColorFragments[2]);
                case 4:
                    return System.Drawing.Color.FromArgb(arrColorFragments[0], arrColorFragments[1],
                        arrColorFragments[2], arrColorFragments[3]);
                default:
                    return System.Drawing.Color.Transparent;
            }
        }

        public static string concatenate(this string s, string t)
        {
            return string.Concat(s, t);
        }

        public static string Clean(this string s)
        {
            return new string(s.Where(x => char.IsWhiteSpace(x) || char.IsLetterOrDigit(x)).ToArray());
        }

        public static bool ToBool(this string s)
        {
            switch (s.ToLower())
            {
                case "ok":
                case "yes":
                case "y":
                case "true":
                case "t":
                case "1":
                    return true;
                default:
                    return false;
            }
        }

        public static byte[] ToBytes(this string s)
        {
            return Encoding.UTF8.GetBytes(s);
        }

        public static string ToString(this byte[] b)
        {
            return Encoding.UTF8.GetString(b);
        }

        public static string ParsePlantsForFilter(this string s)
        {
            /*
             * Convert a string from
             *   Plant1~Plant2~Plant3
             *   ~Plant1~Plant2~Plant3
             *   Plant1~Plant2~Plant3~
             *   ~Plant1~Plant2~Plant3~
             * To
             *   Plant1 eq 'Plant1' or Plant1 eq 'Plant1' or Plant1 eq 'Plant3')
             */
            
            s.Trim('~');
            s = s.Replace("~", "' or Plant1 eq '");
            s = "Plant1 eq '" + s;
            s += "'";

            return string.Compare(s, "Plant1 eq ''", true) == 0 ? "Plant1 eq 'MfgSys'" : s;
        }
    }
}
