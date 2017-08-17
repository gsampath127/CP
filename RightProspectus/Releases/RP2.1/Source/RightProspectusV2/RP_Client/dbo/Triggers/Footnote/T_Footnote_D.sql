-- Delete trigger for Footnote
CREATE TRIGGER T_Footnote_D
ON dbo.Footnote
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Footnote';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.FootnoteId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.FootnoteId,
			CONVERT(NVARCHAR(MAX), d.TaxonomyAssociationId ) AS TaxonomyAssociationId,
			CONVERT(NVARCHAR(MAX), d.TaxonomyAssociationGroupId) AS TaxonomyAssociationGroupId,
			CONVERT(NVARCHAR(MAX), d.LanguageCulture) AS LanguageCulture,
			CONVERT(NVARCHAR(MAX), d.[Text]) as [Text],
			CONVERT(NVARCHAR(MAX), d.[Order]) as [Order]
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'TaxonomyAssociationId' THEN d.TaxonomyAssociationId
			WHEN 'TaxonomyAssociationGroupId' THEN d.TaxonomyAssociationGroupId
			WHEN 'LanguageCulture' THEN d.LanguageCulture
			WHEN 'Text' THEN d.[Text]
			WHEN 'Order' THEN d.[Order]		
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('TaxonomyAssociationId'), ('TaxonomyAssociationGroupId'),
						 ('LanguageCulture'), ('Text'), ('Order')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.FootnoteId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;