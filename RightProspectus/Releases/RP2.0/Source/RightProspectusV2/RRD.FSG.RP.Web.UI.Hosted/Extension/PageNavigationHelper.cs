// ***********************************************************************
// Assembly         : RRD.FSG.RP.Web.UI.Hosted
// Author           : 
// Created          : 11-09-2015
//
// Last Modified By : 
// Last Modified On : 11-12-2015
// ***********************************************************************

using RRD.FSG.RP.Model.Entities.HostedPages;
using System.IO;
using System.Xml.Linq;

/// <summary>
/// The Hosted namespace.
/// </summary>
namespace RRD.FSG.RP.Web.UI.Hosted
{
    /// <summary>
    /// Class PageNavigationHelper.
    /// </summary>
    public static class PageNavigationHelper
    {
        /// <summary>
        /// Gets the final navigation XML.
        /// </summary>
        /// <param name="NavigationXML">The navigation XML.</param>
        /// <returns>XDocument.</returns>
        public static XDocument GetFinalNavigationXML(string NavigationXML)
        {
            XDocument xDocumentNavigationXMLFinal = new XDocument();

            XDocument xDocDBNavigationXML = XDocument.Load(new StringReader(NavigationXML));
            XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");

            XElement xElementNavigationXMLFinal = new XElement("menuRoot");
            foreach (XElement element in xDocDBNavigationXML.Elements("{" + xDocDBNavigationXML.Root.Name.Namespace + "}menuRoot").Elements("{" + xDocDBNavigationXML.Root.Name.Namespace + "}menuItem"))
            {
                switch (element.Attribute(xsi + "type").Value)
                {
                    case "menuLink":
                        xElementNavigationXMLFinal.Add(new XElement("menuItem",
                                                           new XAttribute("type", "link"), new XAttribute("displayName", element.Attribute("displayName").Value), new XAttribute("toolTip", element.Attribute("toolTip").Value),
                                                           new XAttribute("onclick", ""),
                                                           new XAttribute("href", element.Attribute("href").Value), new XAttribute("target", "blank"),
                                                           new XAttribute("cssClass", element.Attribute("isRPPage") != null ? (element.Attribute("isRPPage").Value == "true" ? "top cssMenuTdActive" : "top cssMenuTdInActive") : "top cssMenuTdInActive")));

                        break;
                    case "menuDropDown":

                        XElement dropDownMenuElement = new XElement("menuItem",
                                                            new XAttribute("type", "menuDropDown"),
                                                            new XAttribute("displayName", element.Attribute("displayName").Value),
                                                            new XAttribute("cssClass", "top cssMenuTdInActive"));

                        foreach (XElement childElement in element.Elements("{" + element.Name.Namespace + "}menuItem"))
                        {
                            dropDownMenuElement.Add(new XElement("menuItem",
                                    new XAttribute("type", "link"),
                                    new XAttribute("displayName", childElement.Attribute("displayName").Value),
                                    new XAttribute("toolTip", childElement.Attribute("toolTip").Value),
                                    new XAttribute("onclick", ""),
                                    new XAttribute("href", childElement.Attribute("href").Value), new XAttribute("target", "blank"),
                                    new XAttribute("cssClass", "cssMenuTdInActive")));
                        }

                        xElementNavigationXMLFinal.Add(dropDownMenuElement);

                        break;
                }
            }
            xDocumentNavigationXMLFinal.Add(xElementNavigationXMLFinal);
            return xDocumentNavigationXMLFinal;
        }

        /// <summary>
        /// Gets the final navigation XML.
        /// </summary>
        /// <param name="navigationXML">The navigation XML.</param>
        /// <param name="customer">The customer.</param>
        /// <param name="site">The site.</param>
        /// <param name="externalID1">The external i d1.</param>
        /// <param name="isInternalTAID">if set to <c>true</c> [is internal taid].</param>
        /// <param name="taxonomyAssociationData">The taxonomy association data.</param>
        /// <param name="showXBRL">if set to <c>true</c> [show XBRL].</param>
        /// <returns>XDocument.</returns>
        public static XDocument GetFinalNavigationXML(string navigationXML, string customer, string site, string externalID1, bool? isInternalTAID, TaxonomyAssociationData taxonomyAssociationData, bool showXBRL)
        {
            XDocument xDocumentNavigationXMLFinal = new XDocument();

            XDocument xDocDBNavigationXML = XDocument.Load(new StringReader(navigationXML));
            XNamespace xsi = XNamespace.Get("http://www.w3.org/2001/XMLSchema-instance");
           
            XElement xElementNavigationXMLFinal = new XElement("menuRoot");
            foreach (XElement element in xDocDBNavigationXML.Elements("{" + xDocDBNavigationXML.Root.Name.Namespace + "}menuRoot").Elements("{" + xDocDBNavigationXML.Root.Name.Namespace + "}menuItem"))
            {
                switch (element.Attribute(xsi + "type").Value)
                {
                    case "menuFeature":
                        if (element.Attribute("featureType").Value == "documentTypes")
                        {
                            foreach (var item in taxonomyAssociationData.DocumentTypes)
                            {
                                if (item.VerticalMarketID == "XBRL")
                                {
                                    if (showXBRL)
                                    {
                                        xElementNavigationXMLFinal.Add(new XElement("menuItem",
                                                               new XAttribute("type", "link"), new XAttribute("displayName", item.DocumentTypeLinkText.Replace("newline", "<br>")), new XAttribute("toolTip", item.DocumentTypeDescriptionOverride),
                                                               new XAttribute("id", "MenuTdID" + item.VerticalMarketID),
                                                               new XAttribute("onclick", "loadXBRL('MenuTdID" + item.VerticalMarketID + "');" +
                                                                                           "trackSiteActivity('" + customer + "','" + site + "','" + externalID1 + "','" + item.DocumentTypeId + "','" + isInternalTAID + "');"),
                                                                new XAttribute("href", "#"), new XAttribute("target", ""),
                                                                new XAttribute("cssClass", "top cssMenuTdInActive")));
                                    }
                                }
                                else
                                {
                                    xElementNavigationXMLFinal.Add(new XElement("menuItem",
                                                                new XAttribute("type", "link"), new XAttribute("displayName", item.DocumentTypeLinkText.Replace("newline", "<br>")), new XAttribute("toolTip", item.DocumentTypeDescriptionOverride),
                                                                new XAttribute("id", "MenuTdID" + item.VerticalMarketID),
                                                                new XAttribute("onclick", "openpdf('MenuTdID" + item.VerticalMarketID + "','" + item.ContentURI + "');" +
                                                                                            "trackSiteActivity('" + customer + "','" + site + "','" + externalID1 + "','" + item.DocumentTypeId + "','" + isInternalTAID + "');"),
                                                                new XAttribute("href", "#"), new XAttribute("target", ""),
                                                                new XAttribute("cssClass", "top cssMenuTdInActive")));
                                }
                            }                            
                        }
                        break;
                    case "menuLink":
                        xElementNavigationXMLFinal.Add(new XElement("menuItem",
                                                           new XAttribute("type", "link"), new XAttribute("displayName", element.Attribute("displayName").Value), new XAttribute("toolTip", element.Attribute("toolTip").Value),
                                                           new XAttribute("onclick", ""),
                                                           new XAttribute("href", element.Attribute("href").Value), new XAttribute("target", "blank"),
                                                           new XAttribute("cssClass", element.Attribute("isRPPage") != null ? (element.Attribute("isRPPage").Value == "true" ? "top cssMenuTdActive" : "top cssMenuTdInActive") : "top cssMenuTdInActive")));

                        break;
                    case "menuDropDown":

                        XElement dropDownMenuElement = new XElement("menuItem",
                                                            new XAttribute("type", "menuDropDown"),
                                                            new XAttribute("displayName", element.Attribute("displayName").Value),
                                                            new XAttribute("cssClass", "top cssMenuTdInActive"));

                        foreach (XElement childElement in element.Elements("{" + element.Name.Namespace + "}menuItem"))
                        {
                            dropDownMenuElement.Add(new XElement("menuItem",
                                    new XAttribute("type", "link"),
                                    new XAttribute("displayName", childElement.Attribute("displayName").Value),
                                    new XAttribute("toolTip", childElement.Attribute("toolTip").Value),
                                    new XAttribute("onclick", ""),
                                    new XAttribute("href", childElement.Attribute("href").Value), new XAttribute("target", "blank"),
                                    new XAttribute("cssClass", "cssMenuTdInActive")));
                        }

                        xElementNavigationXMLFinal.Add(dropDownMenuElement);

                        break;
                }                
            }
            xDocumentNavigationXMLFinal.Add(xElementNavigationXMLFinal);
            return xDocumentNavigationXMLFinal;
        }
    }
}