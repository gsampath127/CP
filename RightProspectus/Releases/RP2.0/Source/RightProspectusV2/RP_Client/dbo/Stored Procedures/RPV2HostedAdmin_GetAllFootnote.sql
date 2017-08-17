-- =============================================
-- Author:		Noel Dsouza
-- Create date: 13th-Oct-2015
-- RPV2HostedAdmin_GetAllFootnote
-- =============================================
CREATE PROCEDURE dbo.RPV2HostedAdmin_GetAllFootnote
AS
BEGIN
  SELECT FootnoteId
      ,TaxonomyAssociationId
      ,TaxonomyAssociationGroupId
      ,LanguageCulture
      ,[Text]
      ,[Order]
      ,UtcModifiedDate as UtcLastModified
      ,ModifiedBy
  FROM dbo.Footnote
END