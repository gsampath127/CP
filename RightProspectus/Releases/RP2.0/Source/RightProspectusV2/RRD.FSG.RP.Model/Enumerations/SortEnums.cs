// ***********************************************************************
// Assembly         : RRD.FSG.RP.Model
// Author           : 
// Created          : 10-05-2015
//
// Last Modified By : 
// Last Modified On : 11-16-2015
// ***********************************************************************


/// <summary>
/// The Model namespace.
/// </summary>
namespace RRD.FSG.RP.Model
{
    /// <summary>
    /// Defines basic sort columns shared by all entity types.
    /// </summary>
    public enum SortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the Name value.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,


    }

    /// <summary>
    /// Defines audited sort entity sort columns.
    /// </summary>
    public enum AuditedSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the Name value.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 4,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 5
    }

    /// <summary>
    /// Defines sort columns shared by all taxonomy entities.
    /// </summary>
    public enum TaxonomyEntitySortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the Name value.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,

        /// <summary>
        /// Sort on the Hierarchical Level value.
        /// </summary>
        Level = 4,

        /// <summary>
        /// Sort on the Taxonomy Id value.
        /// </summary>
        TaxonomyId = 5,

        /// <summary>
        /// Sort on the Market Specific Id value.
        /// </summary>
        MarketId = 6
    }

    /// <summary>
    /// Defines Fund specific sort columns.
    /// </summary>
    public enum FundSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the Name value.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,

        /// <summary>
        /// Sort on the Hierarchical Level value.
        /// </summary>
        Level = 4,

        /// <summary>
        /// Sort on the Taxonomy Id value.
        /// </summary>
        TaxonomyId = 5,

        /// <summary>
        /// Sort on the Market Specific Id value.
        /// </summary>
        MarketId = 6,

        /// <summary>
        /// Sort on the Company Id value.
        /// </summary>
        CompanyId = 7
    }

    /// <summary>
    /// Defines vertical market specific sort columns.
    /// </summary>
    public enum VerticalMarketSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the Name value.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 4,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 5,

        /// <summary>
        /// Sort on the Connection String Name value.
        /// </summary>
        ConnectionStringName = 6,

        /// <summary>
        /// Sort on the Database Name value.
        /// </summary>
        DatabaseName = 7
    }
    /// <summary>
    /// Defines User specific sort columns.
    /// </summary>
    public enum UserSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the UserName value.
        /// </summary>
        UserName = 2,

        /// <summary>
        /// Sort on the FirstName value.
        /// </summary>
        FirstName = 3,

        /// <summary>
        /// Sort on the LastName value.
        /// </summary>
        LastName = 4,

        /// <summary>
        /// Sort on the Email value.
        /// </summary>
        Email = 5,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 6,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 7,

    }
    /// <summary>
    /// Defines Roles specific sort columns.
    /// </summary>
    public enum RolesSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,
        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,
        /// <summary>
        /// Sort on the UserName value.
        /// </summary>
        Name = 2,
        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 3,
        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 4,

    }
    /// <summary>
    /// Defines client specific sort columns.
    /// </summary>
    public enum ClientSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the ClientName value.
        /// </summary>
        ClientName = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 4,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 5,

        /// <summary>
        /// Sort on the Connection String Name value.
        /// </summary>
        ClientConnectionStringName = 6,

        /// <summary>
        /// Sort on the Database Name value.
        /// </summary>
        ClientDatabaseName = 7,

        /// <summary>
        /// Sort on the Vertical Market Id value.
        /// </summary>
        VerticalMarketId = 8,

        /// <summary>
        /// Sort on the Vertical Market Name value.
        /// </summary>
        VerticalMarketName = 9,

        /// <summary>
        /// Sort on the Vertical Market Database value.
        /// </summary>
        VerticalMarketDatabaseName = 10
    }
    /// <summary>
    /// Defines TemplatePage specific sort columns.
    /// </summary>
    public enum TemplatePageSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the ClientName value.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 4,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 5,

        /// <summary>
        /// Sort on the PageId Name value.
        /// </summary>
        PageId = 6,

        /// <summary>
        /// Sort on the TemplateID value.
        /// </summary>
        TemplateID = 7,

        /// <summary>
        /// Sort on the TemplateName value.
        /// </summary>
        TemplateName = 8,

        /// <summary>
        /// Sort on the PageName Name value.
        /// </summary>
        PageName = 9

    }
    /// <summary>
    /// Defines TemplatePageText specific sort columns.
    /// </summary>
    public enum TemplatePageTextSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the ClientName value.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 4,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 5,

        /// <summary>
        /// Sort on the TemplateId Name value.
        /// </summary>
        TemplateId = 6,

        /// <summary>
        /// Sort on the PageId Name value.
        /// </summary>
        PageId = 7,

        /// <summary>
        /// Sort on the ResourceKey value.
        /// </summary>
        ResourceKey = 8,

        /// <summary>
        /// Sort on the IsHtml value.
        /// </summary>
        IsHtml = 9,

        /// <summary>
        /// Sort on the DefaultText value.
        /// </summary>
        DefaultText = 10

    }
    /// <summary>
    /// Defines TemplateText specific sort columns.
    /// </summary>
    public enum TemplateTextSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the ClientName value.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 4,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 5,

        /// <summary>
        /// Sort on the TemplateId Name value.
        /// </summary>
        TemplateId = 6,

        /// <summary>
        /// Sort on the ResourceKey value.
        /// </summary>
        ResourceKey = 7,

        /// <summary>
        /// Sort on the IsHtml value.
        /// </summary>
        IsHtml = 8,

        /// <summary>
        /// Sort on the DefaultText value.
        /// </summary>
        DefaultText = 9

    }
    /// <summary>
    /// Defines taxonomy association specific sort columns.
    /// </summary>
    public enum TaxonomyAssociationSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the Name value.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 4,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 5,

        /// <summary>
        /// Sort on the Hierarchical Level value.
        /// </summary>
        Level = 6,

        /// <summary>
        /// Sort on the Taxonomy Id value.
        /// </summary>
        TaxonomyId = 7
    }
    /// <summary>
    /// Defines TaxonomyLevelExternalId specific sort columns.
    /// </summary>
    public enum TaxonomyLevelExternalIdSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the Level value.
        /// </summary>
        Level = 2,

        /// <summary>
        /// Sort on the TaxonomyId value.
        /// </summary>
        TaxonomyName = 3,

        /// <summary>
        /// Sort on the TaxonomyId value.
        /// </summary>
        ExternalId = 4,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 5,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 6,

        /// <summary>
        /// Sort on the IsPrimary value.
        /// </summary>
        IsPrimary = 7


    }
    /// <summary>
    /// Defines PageTextSortColumn specific sort columns.
    /// </summary>
    public enum PageTextSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the Name value.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 4,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 5,

        /// <summary>
        /// Sort on the PageName value.
        /// </summary>
        PageName = 6,

        /// <summary>
        /// Sort on the SiteID value.
        /// </summary>
        SiteID = 7,

        /// <summary>
        /// Sort on the SiteName value.
        /// </summary>
        SiteName = 8,

        /// <summary>
        /// Sort on the ResourceKey value.
        /// </summary>
        ResourceKey = 9,

        /// <summary>
        /// Sort on the Text value.
        /// </summary>
        Text = 10,

        /// <summary>
        /// Sort on the IsProofing Id value.
        /// </summary>
        IsProofing = 11,
        /// <summary>
        /// Sort on the PageName value.
        /// </summary>
        PageDescription = 12,
    }


    /// <summary>
    /// Defines PageNavigationSortColumn specific sort columns.
    /// </summary>
    public enum PageNavigationSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the Page value.
        /// </summary>
        PageName = 2,

        /// <summary>
        /// Sort on the LanguageCulture value.
        /// </summary>
        Languageculture = 3,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 4,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 5,

        /// <summary>
        /// Sort on the ResourceKey value.
        /// </summary>
        NavigationKey = 6,

        /// <summary>
        /// Sort on the Currentversion value.
        /// </summary>
        CurrentVersion = 7,
        /// <summary>
        /// Sort on the IsProofing Id value.
        /// </summary>
        IsProofing = 8,
        /// <summary>
        /// Sort on the SiteId value.
        /// </summary>
        SiteID = 9,
        /// <summary>
        /// Sort on the PageDescription value.
        /// </summary>
        PageDescription = 10
    }


    /// <summary>
    /// Defines SiteTextSortColumn specific sort columns.
    /// </summary>
    public enum SiteTextSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the Name value.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 4,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 5,

        /// <summary>
        /// Sort on the SiteID value.
        /// </summary>
        SiteID = 6,

        /// <summary>
        /// Sort on the SiteName value.
        /// </summary>
        SiteName = 7,

        /// <summary>
        /// Sort on the ResourceKey value.
        /// </summary>
        ResourceKey = 8,

        /// <summary>
        /// Sort on the Text value.
        /// </summary>
        Text = 9,

        /// <summary>
        /// Sort on the IsProofing Id value.
        /// </summary>
        IsProofing = 10
    }

    /// <summary>
    /// Defines ReportScheduleSortColumn specific sort columns.
    /// </summary>
    public enum ReportScheduleSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the Name value.
        /// </summary>
        ReportName = 2,

        /// <summary>
        /// Sort on the Frequency Type value.
        /// </summary>
        FrequencyType = 3,

        /// <summary>
        /// Sort on the Frequency Interval value.
        /// </summary>
        FrequencyInterval = 4,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 5,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 6,

        /// <summary>
        /// Sort on the UtcFirstScheduleRunDate value.
        /// </summary>
        UtcFirstScheduledRunDate = 7,

        /// <summary>
        /// Sort on the UtcNextScheduleRunDate value.
        /// </summary>
        UtcNextScheduledRunDate = 8,

        /// <summary>
        /// Sort on the  UtcLastScheduleRunDate value.
        /// </summary>
        UtcLastScheduledRunDate = 9,

        /// <summary>
        /// Sort on the UtcLastActualRunDate value.
        /// </summary>
        UtcLastActualRunDate = 10,

        /// <summary>
        /// Sort on the IsEnabled Id value.
        /// </summary>
        IsEnabled = 11,

        /// <summary>
        /// Sort on the FrequencyDescription Id value.
        /// </summary>
        FrequencyDescription = 12
    }

    /// <summary>
    /// Defines DocumentTypeExternalIdSortColumn specific sort columns.
    /// </summary>
    public enum ClientDocumentSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the Name value.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 4,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 5,

        /// <summary>
        /// Sort on the ClientDocumentId value.
        /// </summary>
        ClientDocumentId = 6,

        /// <summary>
        /// Sort on the ClientDocumentId value.
        /// </summary>
        ClientDocumentTypeId = 7,

        /// <summary>
        /// Sort on the FileName value.
        /// </summary>
        FileName = 8,

        /// <summary>
        /// Sort on the MimeType value.
        /// </summary>
        MimeType = 9,


        /// <summary>
        /// Sort on the isPrivate value.
        /// </summary>
        IsPrivate = 10,


        /// <summary>
        /// Sort on the ContentUri value.
        /// </summary>
        ContentUri = 11,

    }

    /// <summary>
    /// Defines DocumentTypeExternalIdSortColumn specific sort columns.
    /// </summary>
    public enum DocumentTypeExternalIdSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the Name value.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 4,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 5,

        /// <summary>
        /// Sort on the DocumentTypeId value.
        /// </summary>
        DocumentTypeId = 6,

        /// <summary>
        /// Sort on the DocumentTypeName value.
        /// </summary>
        DocumentTypeName = 7,

        /// <summary>
        /// Sort on the ExternalId value.
        /// </summary>
        ExternalId = 8,

        /// <summary>
        /// Sort on the Modified By Name value.
        /// </summary>
        ModifiedByName = 9,
        /// <summary>
        /// sort on IsPrimary
        /// </summary>
        IsPrimary=10,

    }

    /// <summary>
    /// Defines CUDHistorySortColumn specific sort columns.
    /// </summary>
    public enum CUDHistorySortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the TableName.
        /// </summary>
        TableName = 1,

        /// <summary>
        /// Sort on the CUD Type.
        /// </summary>
        CUDType = 2,

        /// <summary>
        /// Sort on the CUDDate.
        /// </summary>
        UtcCUDDate = 3,

        /// <summary>
        /// Sort on the ColumnName.
        /// </summary>
        ColumnName = 4,

    }

    /// <summary>
    /// Defines SiteSortColumn specific sort columns.
    /// </summary>
    public enum SiteSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the Name value.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 4,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 5,

        /// <summary>
        /// Sort on the SiteId value.
        /// </summary>
        SiteId = 6,

        /// <summary>
        /// Sort on the TemplateName value.
        /// </summary>
        TemplateName = 7,

        /// <summary>
        /// Sort on the DefaultPageName value.
        /// </summary>
        DefaultPageName = 8,

        /// <summary>
        /// Sort on the PageDescription value.
        /// </summary>
        PageDescription = 9,

        /// <summary>
        /// Sort on the ParentSiteId value.
        /// </summary>
        ParentSiteId = 10,

    }
    /// <summary>
    /// Defines DocumentTypeExternalIdSortColumn specific sort columns.
    /// </summary>
    public enum ClientDocumentTypeSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the Name value.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 4,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 5,



    }
    /// <summary>
    /// Defines UrlRewriteSortColumn specific sort columns.
    /// </summary>
    public enum UrlRewriteSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the MatchPattern value.
        /// </summary>
        MatchPattern = 2,

        /// <summary>
        /// Sort on the RewriteFormat value.
        /// </summary>
        RewriteFormat = 3,

        /// <summary>
        /// The pattern name
        /// </summary>
        PatternName = 4,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 5,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 6

    }
    /// <summary>
    /// Defines StaticResource specific sort columns.
    /// </summary>
    public enum StaticResourceSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the FileName value.
        /// </summary>
        FileName = 2,

        /// <summary>
        /// Sort on the Size value.
        /// </summary>
        Size = 3,

        /// <summary>
        /// The MIME type
        /// </summary>
        MimeType = 4,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 5,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 6

    }
    /// <summary>
    /// Defines Taxonomy specific sort columns.
    /// </summary>
    public enum TaxonomySortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the TaxonomyId value.
        /// </summary>
        TaxonomyId = 2,

        /// <summary>
        /// Sort on the TaxonomyName value.
        /// </summary>
        TaxonomyName = 3,

        /// <summary>
        /// The level
        /// </summary>
        Level = 4
    }

    /// <summary>
    /// Defines Site Feature specific sort columns.
    /// </summary>

    public enum SiteFeatureSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the SiteId value.
        /// </summary>
        SiteId = 2,

        /// <summary>
        /// Sort on the SiteKey value.
        /// </summary>

        SiteKey = 3,

        /// <summary>
        /// Sort on the FeatureMode value.
        /// </summary>

        FeatureMode = 4





    }


    /// <summary>
    /// Defines Page Feature specific sort columns.
    /// </summary>

    public enum PageFeatureSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the SiteId value.
        /// </summary>
        SiteId = 2,

        /// <summary>
        /// Sort on the PageId value.
        /// </summary>
        PageName = 3,

        /// <summary>
        /// Sort on the SiteKey value.
        /// </summary>

        PageKey = 4,

        /// <summary>
        /// Sort on the FeatureMode value.
        /// </summary>
        FeatureMode = 5,
        /// <summary>
        /// Sort on the PageDescription value.
        /// </summary>
        PageDescription = 7
    }

    /// <summary>
    /// Defines Template  Feature specific sort columns.
    /// </summary>

    public enum TemplateFeatureSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the ClientName value.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 4,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 5,

        /// <summary>
        /// Sort on the TemplateId Name value.
        /// </summary>
        TemplateId = 6,

        /// <summary>
        /// Sort on the TemplateFeatureKey value.
        /// </summary>
        FeatureKey = 7,

        /// <summary>
        /// Sort on the TemplateFeatureDescription value.
        /// </summary>
        FeatureDescription = 8


    }


    /// <summary>
    /// Enum TemplatePageFeatureSortColumn
    /// </summary>
    public enum TemplatePageFeatureSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the ClientName value.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 4,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 5,

        /// <summary>
        /// Sort on the TemplateId Name value.
        /// </summary>
        TemplateId = 6,
        /// <summary>
        /// Sort on the PageId value.
        /// </summary>
        PageId = 7,

        /// <summary>
        /// Sort on the TemplateFeatureKey value.
        /// </summary>
        FeatureKey = 8,

        /// <summary>
        /// Sort on the TemplateFeatureDescription value.
        /// </summary>
        FeatureDescription = 9


    }

    /// <summary>
    /// Defines Vertical XML Export specific sort columns.
    /// </summary>

    public enum VerticalXmlExportSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the VerticalXmlExportId value.
        /// </summary>
        VerticalXmlExportId = 1,

        /// <summary>
        /// Sort on the ExportDate value.
        /// </summary>
        ExportDate = 2,

        /// <summary>
        /// Sort on the ExportBy value.
        /// </summary>
        Status = 3,
        /// <summary>
        /// Sort on the ExportedByName value.
        /// </summary>
        ExportedByName = 4,
        /// <summary>
        /// Sort on the ExportDescription value.
        /// </summary>
        ExportDescription = 5


    }

    /// <summary>
    /// Defines Vertical XML Export specific sort columns.
    /// </summary>

    public enum VerticalXmlImportSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the VerticalXmlExportId value.
        /// </summary>
        VerticalXmlImportId = 1,

        /// <summary>
        /// Sort on the ExportDate value.
        /// </summary>
        ImportDate = 2,

        /// <summary>
        /// Sort on the Status value.
        /// </summary>
        Status = 3,
        /// <summary>
        /// Sort on the ImportDescription value.
        /// </summary>
        ImportDescription = 4,
        /// <summary>
        /// Sort on the ImportedByName value.
        /// </summary>
        ImportedByName = 5


    }

    /// <summary>
    /// Enum SiteNavigationSortColumn
    /// </summary>
    public enum SiteNavigationSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the NavigationKey value.
        /// </summary>
        NavigationKey = 2,

        /// <summary>
        /// Sort on the PageId value.
        /// </summary>
        PageName = 3,

        /// <summary>
        /// The modified by
        /// </summary>
        ModifiedBy = 4,
        /// <summary>
        /// Sort on the Text value.
        /// </summary>
        Text = 5,
        /// <summary>
        /// Sort on the IsProofing Id value.
        /// </summary>
        IsProofing = 6,
        /// <summary>
        /// Sort on the PageDescription value.
        /// </summary>
        PageDescription = 7,
    }

    /// <summary>
    /// Defines Client Document Group specific sort columns.
    /// </summary>
    public enum ClientDocumentGroupSortColumn
    {
        /// <summary>
        /// Sort column is unspecified. No sort is performed.
        /// </summary>
        Unspecified = 0,

        /// <summary>
        /// Sort on the Key value.
        /// </summary>
        Key = 1,

        /// <summary>
        /// Sort on the ClientName value.
        /// </summary>
        Name = 2,

        /// <summary>
        /// Sort on the Description value.
        /// </summary>
        Description = 3,

        /// <summary>
        /// Sort on the Last Modified value.
        /// </summary>
        LastModified = 4,

        /// <summary>
        /// Sort on the Modified By value.
        /// </summary>
        ModifiedBy = 5,

        /// <summary>
        /// Sort on the ClientDocumentGroupId value.
        /// </summary>
        ClientDocumentGroupId = 6,

        /// <summary>
        /// Sort on the ParentClientDocumentGroupId value.
        /// </summary>
        ParentClientDocumentGroupId = 7,

        /// <summary>
        /// Sort on the CssClass value.
        /// </summary>
        CssClass = 8

    }

}
