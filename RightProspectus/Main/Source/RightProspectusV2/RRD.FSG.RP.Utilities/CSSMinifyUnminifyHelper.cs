// ***********************************************************************
// Assembly         : RRD.FSG.RP.Utilities
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-16-2015
// ***********************************************************************
using System.Text.RegularExpressions;

namespace RRD.FSG.RP.Utilities
{
    /// <summary>
    /// Class CSSMinifyUnminifyHelper.
    /// </summary>
    public static class CSSMinifyUnminifyHelper
    {
        /// <summary>
        /// Minifies the CSS text.
        /// </summary>
        /// <param name="cssText">The CSS text.</param>
        /// <returns>System.String.</returns>
        public static string MinifyCSSText(string cssText)
        {
            cssText = cssText.Replace("&lt;br /&gt;", "");

            cssText = cssText.Replace("&amp;nbsp;", "");

            cssText = Regex.Replace(cssText, @"[a-zA-Z]+#", "#");
            cssText = Regex.Replace(cssText, @"[\n\r]+\s*", string.Empty);
            cssText = Regex.Replace(cssText, @"\s+", " ");
            cssText = Regex.Replace(cssText, @"\s?([:,;{}])\s?", "$1");
            cssText = cssText.Replace(";}", "}");
            cssText = Regex.Replace(cssText, @"([\s:]0)(px|pt|%|em)", "$1");

            // Remove comments from CSS
            cssText = Regex.Replace(cssText, @"/\*[\d\D]*?\*/", string.Empty);

            return cssText;
        }

        /// <summary>
        /// Uns the minify CSS text.
        /// </summary>
        /// <param name="cssText">The CSS text.</param>
        /// <returns>System.String.</returns>
        public static string UnMinifyCSSText(string cssText)
        {
            return cssText.Replace(".", "\n.")
                        .Replace("}#", "}\n#")
                        .Replace(";", ";\n")
                        .Replace("{", "{\n")
                        .Replace("}", "}\n")
                        .Replace(":", ": ");
        }
    }
}
