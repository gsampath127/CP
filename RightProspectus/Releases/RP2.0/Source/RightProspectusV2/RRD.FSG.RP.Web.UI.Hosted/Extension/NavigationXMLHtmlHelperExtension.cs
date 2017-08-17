// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.Hosted
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 10-05-2015
// ***********************************************************************

using System.IO;
using System.Web;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Xsl;

/// <summary>
/// The Hosted namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.Hosted
{
    /// <summary>
    /// Class NavigationXMLHtmlHelperExtension.
    /// </summary>
    public static class NavigationXMLHtmlHelperExtension
    {
        /// <summary>
        /// Renders the XML.
        /// </summary>
        /// <param name="helper">The helper.</param>
        /// <param name="xml">The XML.</param>
        /// <param name="xsltPath">The XSLT path.</param>
        /// <returns>HtmlString.</returns>
        public static HtmlString RenderXml(this HtmlHelper helper, string xml, string xsltPath)
        {
            XmlReaderSettings settings = new XmlReaderSettings();
            settings.DtdProcessing = DtdProcessing.Parse;
            settings.ValidationType = ValidationType.DTD;

            XslCompiledTransform t = new XslCompiledTransform();
            t.Load(XmlReader.Create(new StringReader(xsltPath), settings));
            
            using (XmlReader reader = XmlReader.Create(new StringReader(xml), settings))
            {
                StringWriter writer = new StringWriter();
                t.Transform(reader, null , writer);
                HtmlString htmlString = new HtmlString(writer.ToString());
                return htmlString;
            }
        }
    }
}