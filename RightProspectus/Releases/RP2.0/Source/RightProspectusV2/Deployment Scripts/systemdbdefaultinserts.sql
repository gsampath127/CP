/*
This script was created by Visual Studio on 12/3/2015 at 4:02 PM.
Run this script on CHEFBU-MWE-SQL8.na.ad.rrd.com.rpv2systemdbtest (NA\rr173192) to make it the same as acwin-sqlt01.ecomad.int\sql003.RPv2SystemDB (NA\rr173192).
This script performs its actions in the following order:
1. Disable foreign-key constraints.
2. Perform DELETE commands. 
3. Perform UPDATE commands.
4. Perform INSERT commands.
5. Re-enable foreign-key constraints.
Please back up your target database before running this script.
*/
SET NUMERIC_ROUNDABORT OFF
GO
SET XACT_ABORT, ANSI_PADDING, ANSI_WARNINGS, CONCAT_NULL_YIELDS_NULL, ARITHABORT, QUOTED_IDENTIFIER, ANSI_NULLS ON
GO
/*Pointer used for text / image updates. This might not be needed, but is declared here just in case*/
DECLARE @pv binary(16)
BEGIN TRANSACTION
ALTER TABLE [dbo].[TemplateText] DROP CONSTRAINT [fk_TemplateText]
ALTER TABLE [dbo].[TemplateFeature] DROP CONSTRAINT [fk1_TemplateFeature]
ALTER TABLE [dbo].[TemplatePageFeature] DROP CONSTRAINT [fk1_TemplatePageFeature]
ALTER TABLE [dbo].[TemplatePageFeature] DROP CONSTRAINT [fk2_TemplatePageFeature]
ALTER TABLE [dbo].[TemplateNavigation] DROP CONSTRAINT [fk_TemplateNavigation1]
ALTER TABLE [dbo].[TemplatePageNavigation] DROP CONSTRAINT [fk_TemplatePageNavigation1]
ALTER TABLE [dbo].[TemplatePageNavigation] DROP CONSTRAINT [fk_TemplatePageNavigation2]
ALTER TABLE [dbo].[TemplatePage] DROP CONSTRAINT [fk1_TemplatePage]
ALTER TABLE [dbo].[TemplatePage] DROP CONSTRAINT [fk2_TemplatePage]
ALTER TABLE [dbo].[TemplatePageText] DROP CONSTRAINT [fk_TemplatePageText1]
ALTER TABLE [dbo].[TemplatePageText] DROP CONSTRAINT [fk_TemplatePageText2]
SET IDENTITY_INSERT [dbo].[Template] ON
INSERT INTO [dbo].[Template] ([TemplateId], [Name], [Description]) VALUES (1, N'Default', N'Default Template with Grid Vew')
SET IDENTITY_INSERT [dbo].[Template] OFF
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 1, N'TAL_CSSFile', N'TAL_CSSFile', 0, N'.cssTALMainDiv{background-color:#fff;width:1000px;display:table;table-layout:fixed;margin:auto;height:550px;border-radius:20px;margin-top:21px}.cssTALProductDetailsMainDiv{border-radius:20px;margin-left:40px;margin-right:40px;height:300px;margin-top:10px;border-color:#d3d3d3;border-style:solid;border-width:1px}.cssTALProductHeaderTextDiv{background-color:#0085cf;border-top-left-radius:17px;border-top-right-radius:17px;padding-left:12px;font-weight:700;font-size:16px;color:#fff;height:25px;padding-top:5px}.cssTALProductLinksPlaceHolderDiv{line-height:2.0}', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 1, N'TAL_ProductHeaderText', N'TaxonomyAssociationLink_ProductHeaderText', 1, N'Products', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 2, N'TAHD_CSSFile', N'TAHD_CSSFile', 0, N'.cssTAHDMainDiv{margin:21px auto;height:100%;border-radius:20px;width:1000px;background-color:#fff;color:#464646}.cssTAHDProductDetailsMainDiv{border-radius:20px;margin-left:40px;margin-right:40px;margin-top:10px;border-color:#d3d3d3;border-style:solid;border-width:1px}.cssTAHDUnderlyingFundDetailsMainDiv{border-radius:20px;margin-left:40px;margin-right:40px;margin-top:10px;border-color:#d3d3d3;border-style:solid;border-width:1px}.cssTAHDProductHeaderTextDiv{background-color:#0085cf;border-top-left-radius:17px;border-top-right-radius:17px;padding-left:12px;font-weight:700;font-size:16px;color:#fff;height:25px;padding-top:5px}.cssTAHDUnderlyingFundHeaderTextDiv{background-color:#0085cf;border-top-left-radius:17px;border-top-right-radius:17px;padding-left:12px;font-weight:700;font-size:16px;color:#fff;height:25px;padding-top:5px}.cssTAHDProductDocumentGridDiv{display:table;table-layout:fixed;width:100%;margin-bottom:20px}.cssTAHDUnderlyingFundDocumentGridDiv{display:table;table-layout:fixed;width:100%;margin-bottom:20px}.cssTAHDProductDocumentGridHeader{padding-top:5px;height:30px;display:table-row;width:100%;table-layout:fixed;padding-left:10px;padding-bottom:8px;font-weight:700;color:#036}.cssTAHDUnderlyingFundDocumentGridHeader{padding-top:5px;height:30px;display:table-row;width:100%;table-layout:fixed;padding-left:10px;padding-bottom:8px;font-weight:700;color:#036}.cssTAHDProductDocumentGridProductNameColumn{width:60%}.cssTAHDProductDocumentGridDocTypeColumn{width:20%}.cssTAHDProductDocumentGridItem{padding-top:5px;height:25px;display:table-row;table-layout:fixed;width:100%;background-color:#ECEDEE}.cssTAHDProductDocumentGridItemProductNameColumn{width:60%;padding-top:5px;padding-bottom:5px}.cssTAHDProductDocumentGridItemDocTypeColumn{width:20%;padding-top:5px;padding-bottom:5px}.cssTAHDUnderlyingFundDocumentGridFundNameColumn{width:40%;padding-top:5px;padding-bottom:5px}.cssTAHDUnderlyingFundDocumentGridDocTypeColumn{width:12%;padding-top:5px;padding-bottom:5px}.cssTAHDUnderlyingFundDocumentGridItem{background-color:#ECEDEE;padding-top:5px;height:30px;display:table-row;table-layout:fixed;width:100%}.cssTAHDUnderlyingFundDocumentGridItemFundNameColumn{width:40%;padding-top:8px;padding-bottom:8px}.cssTAHDUnderlyingFundDocumentGridItemDocTypeColumn{width:12%;padding-top:5px;padding-bottom:5px}.cssTAHDUnderlyingFundDocumentGridAlternateItem{background-color:#fff;padding-top:5px;height:30px;display:table-row;width:100%}.cssTAHDUnderlyingFundDocumentGridAlternateItemFundNameColumn{width:40%;padding-top:8px;padding-bottom:8px}.cssTAHDUnderlyingFundDocumentGridAlternateItemDocTypeColumn{width:12%;padding-top:5px;padding-bottom:5px}.cssTAHDDocumentNAText{}.cssTAHDGlossary{display:none;margin-left:40px}.cssTAHDFooter{margin:40px}.cssTAHDFootnotesHeaderText{font-size:20px;font-weight:bold}.cssTAHDFootnotesItems{margin-top:20px}.cssTADHFootnotesItemText{}.cssDocumentTypeLinkImage{}.cssDocumentTypeLinkText{}.cssXBRLLinkImage{}.cssXBRLLinkText{}.cssDocumentTypeLinkNAImage{}.cssDocumentTypeLinkNAText{}', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 2, N'TAHD_DocumentNotAvailableText', N'TaxonomyAssociationHierarchy_DocumentNotAvailableText', 1, N'NA', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 2, N'TAHD_FootnotesHeaderText', N'TAHD_FootnotesHeaderText', 1, N'Footnotes', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 2, N'TAHD_Glossary', N'TAHD_Glossary', 1, N'Glossary', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 2, N'TAHD_ProductDocumentNotAvailableText', N'TAHD_ProductDocumentNotAvailableText', 1, N'NA', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 2, N'TAHD_ProductGridProductNameColumnText', N'TaxonomyAssociationHierarchy_ProductGridProductNameColumnText', 1, N'Product Name', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 2, N'TAHD_ProductHeaderText', N'TaxonomyAssociationHierarchy_ProductHeaderText', 1, N'Product Documents', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 2, N'TAHD_UnderlyingFundGridFundNameColumnText', N'TaxonomyAssociationHierarchy_UnderlyingFundGridFundNameColumnText', 1, N'Fund Name', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 2, N'TAHD_UnderlyingFundHeaderText', N'TaxonomyAssociationHierarchy_UnderlyingFundHeaderText', 1, N'Underlying Fund Documents', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 3, N'TADF_CSSFile', N'TADF_CSSFile', 0, N'.cssTADFBody{overflow-y:hidden}.cssTADFMainDiv{background-color:#fff;display:table;margin:auto;height:100%;width:100%;padding:18px 0 0 0}.cssTADFFundName{display:table-cell;padding-right:20px;font-size:16px;font-weight:bold;font-family:Verdana,Geneva,sans-serif;color:#1C4877;width:50%;text-align:right;vertical-align:middle}.TADFDocumentTypeNavPlaceHolderDiv{color:#fff;margin-top:25px}.cssTADFPDFPlaceHolderDiv{position:absolute;width:100%;left:0;margin-top:0}.cssTADFPDFFrame{border-style:none;width:100%}.cssTADFDocumentNotAvailableText{margin-top:20px}.cssTADFDocumentNotAvailablePlaceHolderDiv{position:absolute;background:#FFF;height:100%;width:100%}#NavMenu{color:#FFF !important;margin:0;padding:0}#NavMenu ul{position:absolute;left:-9999px;top:-9999px}#NavMenu li.top{display:inline-block;line-height:25px;padding-left:10px;padding-right:10px;height:25px;vertical-align:middle;font-family:Verdana,Geneva,sans-serif;font-weight:bold;text-decoration:none;font-size:11px;min-width:50px;text-align:center;background-color:#5B9BD5}#NavMenu li:hover li{display:block;float:left;line-height:25px;padding-left:10px;padding-right:10px;height:25px;vertical-align:middle;font-family:Verdana,Geneva,sans-serif;font-weight:bold;text-decoration:none;font-size:11px;min-width:50px;text-align:center}#NavMenu li:hover{position:relative}#NavMenu li:hover ul{left:0;top:25px;padding-top:2px;background:#ffffff;padding-left:0 !important;z-index:100}.cssMenuTdActive{}.cssMenuTdInActive{}.cssMenuTdActive a{color:#FFF;text-decoration:none}.cssMenuTdInActive a{color:#FFF;text-decoration:none}.cssMenuTdActive a:hover{color:#FFF;text-decoration:none}.cssMenuTdInActive a:hover{color:#FFF;text-decoration:none}', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 3, N'TADF_DocumentNotAvailableText', N'TADF_DocumentNotAvailableText', 1, N'This document is not currently available. Please try again later.', N'TADF_DocumentNotAvailableText')
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 4, N'TAD_CSSFile', N'TAD_CSSFile', 0, N'.cssTADMainDiv{margin:auto;height:100%;width:1024px;padding:18px 0 0 0;background-color:#fff}.TADHeaderMenuMainDiv{display:none}.cssTADHeaderText{font-size:12px;font-family:Verdana,Geneva,sans-serif;display:table;width:100%;margin-bottom:15px;margin-top:15px}.cssTADUnderlyingFundDocumentGridDiv{display:table;table-layout:fixed;width:100%;margin-bottom:20px}.cssTADUnderlyingFundDocumentGridHeader{height:47px;display:table-row;width:100%;table-layout:fixed;background:#336699;color:#fff;font-family:Verdana,Geneva,sans-serif;padding-left:10px}.cssTADUnderlyingFundDocumentGridFundNameColumn{width:40%;vertical-align:middle;font-size:17px}.cssTADUnderlyingFundDocumentGridDocTypeColumn{width:10%;text-align:center;vertical-align:middle;border-left:2px;border-left-style:solid;border-left-color:#FFF}.cssTADUnderlyingFundDocumentGridItem{background-color:#ECEDEE;height:20px;display:table-row;table-layout:fixed;width:100%}.cssTADUnderlyingFundDocumentGridItemFundNameColumn{width:40%}.cssTADUnderlyingFundDocumentGridItemDocTypeColumn{width:10%;border-left:2px;border-left-style:solid;border-left-color:#FFF}.cssTADUnderlyingFundDocumentGridAlternateItemFundNameColumn{width:40%}.cssTADUnderlyingFundDocumentGridAlternateItemDocTypeColumn{width:10%;border-left:2px;border-left-style:solid;border-left-color:#FFF}.cssTADUnderlayingFundGridNAText{text-align:center}.cssTADUnderlyingFundDocumentGridAlternateItem{background-color:#fff;height:20px;display:table-row;width:100%}.cssTADFooter{margin-top:20px}.cssTADFootnotesHeaderText{font-size:20px;font-weight:bold}.cssTADFootnotesItems{margin-top:20px}.cssTADFootnotesItemText{}.TADHeaderMenuPlaceHolderDiv{color:#fff;margin-top:15px}#NavMenu{color:#FFF !important;margin:0;padding:0}#NavMenu ul{position:absolute;left:-9999px;top:-9999px}#NavMenu li.top{display:table-cell;line-height:25px;padding-left:10px;padding-right:10px;height:25px;vertical-align:middle;color:#1C4877;font-family:Verdana,Geneva,sans-serif;font-weight:bold;text-decoration:none;font-size:11px;min-width:100px;text-align:center}#NavMenu li:hover li{display:block;float:left;line-height:30px;padding-left:10px;padding-right:10px;height:25px;vertical-align:middle;color:#1C4877;font-family:Verdana,Geneva,sans-serif;font-weight:bold;text-decoration:none;font-size:11px;min-width:100px;text-align:center}#NavMenu li:hover{position:relative}#NavMenu li:hover ul{left:0;top:25px;padding-top:2px;background:#ffffff;padding-left:0 !important;z-index:100}.cssMenuTdActive{color:#e5f8ff;font-family:Verdana,Geneva,sans-serif;font-weight:bold;text-decoration:none}.cssMenuTdInActive{}.cssMenuTdActive a{color:#FFF;text-decoration:none}.cssMenuTdActive a:hover{color:#FFF;text-decoration:underline}.cssMenuTdInActive a:hover{color:#FFF;text-decoration:underline}.cssDocumentTypeLinkImage{}.cssDocumentTypeLinkText{}.cssXBRLLinkImage{}.cssXBRLLinkText{}', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 4, N'TAD_FootnotesHeaderText', N'TAD_FootnotesHeaderText', 1, N'Footnotes', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 4, N'TAD_NAText', N'TaxonomyAssociationDocuments_NAText', 1, N'NA', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 4, N'TAD_UnderlayingFundGridFundNameColumnText', N'TaxonomyAssociationDocuments_UnderlayingFundGridFundNameColumnText', 1, N'Underlying Funds', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 4, N'TAD_UnderlyingFundDocumentsHelpText', N'TaxonomyAssociationDocuments_UnderlyingFundDocumentsHelpText', 1, N'Underlying fund documents help text', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 5, N'XBRL_CSSFile', N'XBRL_CSSFile', 0, N'.cssXBRLMainDiv{background-color:#fff;display:table;margin:auto;height:100%;width:100%;padding:18px 0 0 0}.cssXBRLFundName{display:table-cell;padding-right:20px;font-size:16px;font-weight:bold;font-family:Verdana,Geneva,sans-serif;color:#1C4877;width:50%;text-align:right;vertical-align:middle}.cssXBRLContentDiv{}.cssXBRLContentMainTable{margin-left:5px;margin-top:25px}.cssXBRLPlaceHolder{}.cssXBRLViewerPlaceHolder{list-style-image:none;list-style-type:disc}.cssXBRLZipFilesHeaderText{font-size:13px;font-weight:bold}.cssXBRLViewerHeaderText{font-size:13px;font-weight:bold}', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 5, N'XBRL_DatedText', N'XBRL_XBRLViewerDatedText', 0, N'Dated', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 5, N'XBRL_DateFormat', N'XBRL_XBRLDateFormat', 0, N'MM\/dd\/yyyy', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 5, N'XBRL_FiledText', N'XBRL_XBRLViewerFiledText', 0, N'Filed', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 5, N'XBRL_ViewerHeaderText', N'XBRL_XBRLViewerHeaderText', 1, N'XBRL Viewer', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 5, N'XBRL_ViewerNotReadyMessage', N'XBRL_XBRLViewerNotReadyMessage', 1, N'XBRL Viewer rendering will be available soon.', NULL)
INSERT INTO [dbo].[TemplatePageText] ([TemplateId], [PageId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, 5, N'XBRL_ZipFilesHeaderText', N'XBRL_ZipFilesHeaderText', 1, N'Zip Files', NULL)
SET IDENTITY_INSERT [dbo].[VerticalMarkets] ON
INSERT INTO [dbo].[VerticalMarkets] ([VerticalMarketId], [MarketName], [ConnectionStringName], [DatabaseName], [MarketDescription], [UtcModifiedDate], [ModifiedBy]) VALUES (1, N'US', N'USVerticalMarketDBInstance', N'db1029', N'U.S.A', '20150901 13:50:09.607', 1)
SET IDENTITY_INSERT [dbo].[VerticalMarkets] OFF
SET IDENTITY_INSERT [dbo].[Page] ON
INSERT INTO [dbo].[Page] ([PageId], [Name], [Description]) VALUES (1, N'TAL', N'Taxonomy Association Link')
INSERT INTO [dbo].[Page] ([PageId], [Name], [Description]) VALUES (2, N'TAHD', N'Taxonomy AssociationHierarchy Documents')
INSERT INTO [dbo].[Page] ([PageId], [Name], [Description]) VALUES (3, N'TADF', N'Taxonomy Specific Document Frame')
INSERT INTO [dbo].[Page] ([PageId], [Name], [Description]) VALUES (4, N'TAD', N'Taxonomy Association Documents')
INSERT INTO [dbo].[Page] ([PageId], [Name], [Description]) VALUES (5, N'XBRL', N'XBRL')
SET IDENTITY_INSERT [dbo].[Page] OFF
INSERT INTO [dbo].[TemplatePage] ([TemplateId], [PageId]) VALUES (1, 1)
INSERT INTO [dbo].[TemplatePage] ([TemplateId], [PageId]) VALUES (1, 2)
INSERT INTO [dbo].[TemplatePage] ([TemplateId], [PageId]) VALUES (1, 3)
INSERT INTO [dbo].[TemplatePage] ([TemplateId], [PageId]) VALUES (1, 4)
INSERT INTO [dbo].[TemplatePage] ([TemplateId], [PageId]) VALUES (1, 5)
SET IDENTITY_INSERT [dbo].[Roles] ON
INSERT INTO [dbo].[Roles] ([RoleId], [Name], [UtcModifiedDate], [ModifiedBy]) VALUES (1, N'Admin', '20150919 01:21:27.080', 1)
INSERT INTO [dbo].[Roles] ([RoleId], [Name], [UtcModifiedDate], [ModifiedBy]) VALUES (2, N'User', '20150919 01:21:36.117', 1)
SET IDENTITY_INSERT [dbo].[Roles] OFF
SET IDENTITY_INSERT [dbo].[Reports] ON
INSERT INTO [dbo].[Reports] ([ReportId], [ReportName], [ReportDescription]) VALUES (1, N'ActivityReport', N'ActivityReport')
INSERT INTO [dbo].[Reports] ([ReportId], [ReportName], [ReportDescription]) VALUES (2, N'ErrorReport', N'ErrorReport')
INSERT INTO [dbo].[Reports] ([ReportId], [ReportName], [ReportDescription]) VALUES (3, N'PrintRequestReport', N'PrintRequestReport')
SET IDENTITY_INSERT [dbo].[Reports] OFF
INSERT INTO [dbo].[TemplatePageNavigation] ([TemplateId], [PageId], [NavigationKey], [Name], [XslTransform], [DefaultNavigationXml], [Description]) VALUES (1, 3, N'TADF_DocumentTypeMenu', N'Document Type Menu', CONVERT(xml,N'<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" version="1.0" exclude-result-prefixes="msxsl"><xsl:template match="/"><ul id="NavMenu"><xsl:for-each select="menuRoot/menuItem"><xsl:if test="@type=''link''"><li class="top"><xsl:attribute name="class"><xsl:value-of select="@cssClass"/></xsl:attribute><xsl:attribute name="id"><xsl:value-of select="@id"/></xsl:attribute><a><xsl:attribute name="title"><xsl:value-of select="@toolTip"/></xsl:attribute><xsl:attribute name="onclick"><xsl:value-of select="@onclick"/></xsl:attribute><xsl:attribute name="href"><xsl:value-of select="@href"/></xsl:attribute><xsl:attribute name="target"><xsl:value-of select="@target"/></xsl:attribute><xsl:value-of select="@displayName"/></a></li></xsl:if><xsl:if test="@type=''menuDropDown''"><li class="top"><xsl:attribute name="class"><xsl:value-of select="@cssClass"/></xsl:attribute><a><xsl:value-of select="@displayName"/></a><ul><xsl:for-each select="menuItem"><li><xsl:attribute name="class"><xsl:value-of select="@cssClass"/></xsl:attribute><xsl:attribute name="id"><xsl:value-of select="@id"/></xsl:attribute><a><xsl:attribute name="title"><xsl:value-of select="@toolTip"/></xsl:attribute><xsl:attribute name="onclick"><xsl:value-of select="@onclick"/></xsl:attribute><xsl:attribute name="href"><xsl:value-of select="@href"/></xsl:attribute><xsl:attribute name="target"><xsl:value-of select="@target"/></xsl:attribute><xsl:value-of select="@displayName"/></a></li></xsl:for-each></ul></li></xsl:if></xsl:for-each></ul></xsl:template></xsl:stylesheet>',1), CONVERT(xml,N'<menuRoot xmlns="http://rightprospectus.com/hostedSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"><menuItem xsi:type="menuFeature" featureType="documentTypes"/></menuRoot>',1), N'Document Type Menu')
INSERT INTO [dbo].[TemplatePageNavigation] ([TemplateId], [PageId], [NavigationKey], [Name], [XslTransform], [DefaultNavigationXml], [Description]) VALUES (1, 4, N'TAD_HeaderMenu', N'Header Menu', CONVERT(xml,N'<xsl:stylesheet xmlns:xsl="http://www.w3.org/1999/XSL/Transform" xmlns:msxsl="urn:schemas-microsoft-com:xslt" version="1.0" exclude-result-prefixes="msxsl"><xsl:template match="/"><ul id="NavMenu"><xsl:for-each select="menuRoot/menuItem"><xsl:if test="@type=''link''"><li class="top"><xsl:attribute name="class"><xsl:value-of select="@cssClass"/></xsl:attribute><xsl:attribute name="id"><xsl:value-of select="@id"/></xsl:attribute><a><xsl:attribute name="title"><xsl:value-of select="@toolTip"/></xsl:attribute><xsl:attribute name="onclick"><xsl:value-of select="@onclick"/></xsl:attribute><xsl:attribute name="href"><xsl:value-of select="@href"/></xsl:attribute><xsl:attribute name="target"><xsl:value-of select="@target"/></xsl:attribute><xsl:value-of select="@displayName"/></a></li></xsl:if><xsl:if test="@type=''menuDropDown''"><li class="top"><xsl:attribute name="class"><xsl:value-of select="@cssClass"/></xsl:attribute><a><xsl:value-of select="@displayName"/></a><ul><xsl:for-each select="menuItem"><li><xsl:attribute name="class"><xsl:value-of select="@cssClass"/></xsl:attribute><xsl:attribute name="id"><xsl:value-of select="@id"/></xsl:attribute><a><xsl:attribute name="title"><xsl:value-of select="@toolTip"/></xsl:attribute><xsl:attribute name="onclick"><xsl:value-of select="@onclick"/></xsl:attribute><xsl:attribute name="href"><xsl:value-of select="@href"/></xsl:attribute><xsl:attribute name="target"><xsl:value-of select="@target"/></xsl:attribute><xsl:value-of select="@displayName"/></a></li></xsl:for-each></ul></li></xsl:if></xsl:for-each></ul></xsl:template></xsl:stylesheet>',1), CONVERT(xml,N'<menuRoot xmlns="http://rightprospectus.com/hostedSchema" xmlns:xsi="http://www.w3.org/2001/XMLSchema-instance"><menuItem xsi:type="menuLink" displayName="FUND LITERATURE" toolTip="FUND LITERATURE" href="#" isRPPage="true"/></menuRoot>',1), N'Header Menu')
INSERT INTO [dbo].[TemplatePageFeature] ([TemplateId], [PageId], [Key], [Description]) VALUES (1, 3, N'RequestMaterial', N'DescRequestMaterial')
INSERT INTO [dbo].[TemplateFeature] ([TemplateId], [Key], [Description]) VALUES (1, N'XBRL', N'DescXBRL')
INSERT INTO [dbo].[TemplateText] ([TemplateId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, N'cssFile', N'css File name', 0, N'body{font-family:arial,tahoma,sans-serif;background-color:#0085cf;font-size:13px;color:#464646;margin:0}.cssClientLogoPlaceHolder{padding-left:41px}.cssClientLogo{padding-top:30px;padding-bottom:30px;height:50px}.cssDocumentNotAvailableItemDiv{border-radius:20px;margin-left:40px;margin-right:40px;height:300px;margin-top:10px}.cssDocumentNotAvailableGoBackButtonDiv{margin-top:20px}.cssTaxonomyDataNotAvailableMainDiv{background-color:#fff;width:1000px;display:table;margin:auto;height:100%;border-radius:20px;margin-top:21px}.cssTaxonomyDataNotAvailableItemDiv{border-radius:20px;margin-left:40px;margin-right:40px;height:300px;margin-top:10px}.cssTaxonomyDataNotAvailableGoBackButtonDiv{margin-top:20px}.td{padding-left:10px;padding-right:10px;display:table-cell;table-layout:fixed;vertical-align:middle}.cssLinks{color:#464646;text-decoration:none;cursor:pointer}.cssLinks:hover{text-decoration:underline;cursor:pointer}.cssHeaderTable{display:table;width:100%;border-bottom-style:solid;border-bottom-width:2px}.cssHeaderTableRow{display:table-row;width:100%}.cssHeaderTableCell{display:table-cell}', N'Default css file for Default template')
INSERT INTO [dbo].[TemplateText] ([TemplateId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, N'DocumentNotAvailableText', N'DocumentNotAvailableText', 1, N'This document is not currently available. Please try again later.', N'This document is not currently available. Please try again later.')
INSERT INTO [dbo].[TemplateText] ([TemplateId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, N'LogoText', N'Logo Text', 1, N'Client Logo', N'Client Logo')
INSERT INTO [dbo].[TemplateText] ([TemplateId], [ResourceKey], [Name], [IsHtml], [DefaultText], [Description]) VALUES (1, N'TaxonomyNotAvailableText', N'TaxonomyNotAvailableText', 1, N'Taxonomy Data is not currently available. Please try again later.', N'Taxonomy Data is not currently available. Please try again later.')
INSERT INTO [dbo].[FrequencyType] ([EnumKey], [EnumText]) VALUES (1, N'RunOnce')
INSERT INTO [dbo].[FrequencyType] ([EnumKey], [EnumText]) VALUES (2, N'EveryXDays')
INSERT INTO [dbo].[FrequencyType] ([EnumKey], [EnumText]) VALUES (3, N'Weekly')
INSERT INTO [dbo].[FrequencyType] ([EnumKey], [EnumText]) VALUES (4, N'Monthly')
INSERT INTO [dbo].[FrequencyType] ([EnumKey], [EnumText]) VALUES (5, N'Quarterly')
INSERT INTO [dbo].[FrequencyType] ([EnumKey], [EnumText]) VALUES (6, N'BiAnnually')
INSERT INTO [dbo].[FrequencyType] ([EnumKey], [EnumText]) VALUES (7, N'Annually')
ALTER TABLE [dbo].[TemplateText]
    ADD CONSTRAINT [fk_TemplateText] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId])
ALTER TABLE [dbo].[TemplateFeature]
    ADD CONSTRAINT [fk1_TemplateFeature] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId])
ALTER TABLE [dbo].[TemplatePageFeature]
    ADD CONSTRAINT [fk1_TemplatePageFeature] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId])
ALTER TABLE [dbo].[TemplatePageFeature]
    ADD CONSTRAINT [fk2_TemplatePageFeature] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([PageId])
ALTER TABLE [dbo].[TemplateNavigation]
    ADD CONSTRAINT [fk_TemplateNavigation1] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId])
ALTER TABLE [dbo].[TemplatePageNavigation]
    ADD CONSTRAINT [fk_TemplatePageNavigation1] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId])
ALTER TABLE [dbo].[TemplatePageNavigation]
    ADD CONSTRAINT [fk_TemplatePageNavigation2] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([PageId])
ALTER TABLE [dbo].[TemplatePage]
    ADD CONSTRAINT [fk1_TemplatePage] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId])
ALTER TABLE [dbo].[TemplatePage]
    ADD CONSTRAINT [fk2_TemplatePage] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([PageId])
ALTER TABLE [dbo].[TemplatePageText]
    ADD CONSTRAINT [fk_TemplatePageText1] FOREIGN KEY ([TemplateId]) REFERENCES [dbo].[Template] ([TemplateId])
ALTER TABLE [dbo].[TemplatePageText]
    ADD CONSTRAINT [fk_TemplatePageText2] FOREIGN KEY ([PageId]) REFERENCES [dbo].[Page] ([PageId])
COMMIT TRANSACTION