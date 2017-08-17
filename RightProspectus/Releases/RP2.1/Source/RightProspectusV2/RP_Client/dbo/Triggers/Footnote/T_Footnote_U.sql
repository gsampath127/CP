-- Update trigger for Footnote
CREATE TRIGGER T_Footnote_U
ON dbo.Footnote
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'Footnote';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.FootnoteId, 'U', i.ModifiedBy
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
	),
	deletedConverted AS
	(
		SELECT	d.FootnoteId,
			CONVERT(NVARCHAR(MAX), d.TaxonomyAssociationId ) AS TaxonomyAssociationId,
			CONVERT(NVARCHAR(MAX), d.TaxonomyAssociationGroupId) AS TaxonomyAssociationGroupId,
			CONVERT(NVARCHAR(MAX), d.LanguageCulture) AS LanguageCulture,
			CONVERT(NVARCHAR(MAX), d.[Text]) as [Text],
			CONVERT(NVARCHAR(MAX), d.[Order]) as [Order]
		FROM	deleted d
	),
	dictionary AS
	(
		SELECT	c.Id, cn.ColumnName, sc.system_type_id,
		CASE cn.ColumnName
			WHEN 'TaxonomyAssociationId' THEN d.TaxonomyAssociationId
			WHEN 'TaxonomyAssociationGroupId' THEN d.TaxonomyAssociationGroupId
			WHEN 'LanguageCulture' THEN d.LanguageCulture
			WHEN 'Text' THEN d.[Text]
			WHEN 'Order' THEN d.[Order]		
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'TaxonomyAssociationId' THEN i.TaxonomyAssociationId
			WHEN 'TaxonomyAssociationGroupId' THEN i.TaxonomyAssociationGroupId
			WHEN 'LanguageCulture' THEN i.LanguageCulture
			WHEN 'Text' THEN i.[Text]
			WHEN 'Order' THEN i.[Order]		
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.FootnoteId = d.FootnoteId
					CROSS JOIN (VALUES('TaxonomyAssociationId'), ('TaxonomyAssociationGroupId'),
						 ('LanguageCulture'), ('Text'), ('Order')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.FootnoteId = c.[Key]
			INNER JOIN sys.columns sc
				ON	sc.object_id = @ObjectId
				AND	sc.name = cn.ColumnName
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue, NewValue)
	SELECT	d.Id, d.ColumnName, d.system_type_id, d.OldValue, d.NewValue
	FROM	dictionary d
	WHERE	ISNULL(d.OldValue, d.NewValue) IS NOT NULL
		AND	ISNULL(d.OldValue, d.NewValue + 'X') != ISNULL(d.NewValue, d.OldValue + 'X')
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
