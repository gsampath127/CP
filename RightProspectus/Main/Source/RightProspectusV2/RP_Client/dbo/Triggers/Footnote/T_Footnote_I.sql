-- Insert trigger for Footnote
CREATE TRIGGER T_Footnote_I
ON dbo.Footnote
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Footnote';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.FootnoteId, 'I', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.FootnoteId,
		CONVERT(NVARCHAR(MAX), i.TaxonomyAssociationId ) AS TaxonomyAssociationId,
		CONVERT(NVARCHAR(MAX), i.TaxonomyAssociationGroupId) AS TaxonomyAssociationGroupId,
		CONVERT(NVARCHAR(MAX), i.LanguageCulture) AS LanguageCulture,
		CONVERT(NVARCHAR(MAX), i.[Text]) as [Text],
		CONVERT(NVARCHAR(MAX), i.[Order]) as [Order]
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'TaxonomyAssociationId' THEN i.TaxonomyAssociationId
		WHEN 'TaxonomyAssociationGroupId' THEN i.TaxonomyAssociationGroupId
		WHEN 'LanguageCulture' THEN i.LanguageCulture
		WHEN 'Text' THEN i.[Text]
		WHEN 'Order' THEN i.[Order]		
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('TaxonomyAssociationId'), ('TaxonomyAssociationGroupId'),
				 ('LanguageCulture'), ('Text'), ('Order')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.FootnoteId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
