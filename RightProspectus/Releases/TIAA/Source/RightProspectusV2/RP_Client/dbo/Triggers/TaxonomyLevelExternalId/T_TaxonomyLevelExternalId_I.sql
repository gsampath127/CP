﻿-- Insert trigger for TaxonomyLevelExternalId
CREATE TRIGGER T_TaxonomyLevelExternalId_I
ON dbo.TaxonomyLevelExternalId
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'TaxonomyLevelExternalId';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key],SecondKey,Thirdkey, CUDType, UserId)
	SELECT	@TableName, i.[Level] ,i.TaxonomyId, i.ExternalId , 'I', i.ModifiedBy
	FROM	inserted i;

	WITH insertedConverted AS
	(
		SELECT	
		i.[Level] ,
		i.TaxonomyId,
		i.ExternalId,
		CONVERT(NVARCHAR(MAX), i.IsPrimary) AS IsPrimary
		FROM	inserted i
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
		WHEN 'IsPrimary' THEN i.IsPrimary
	END AS NewValue
	FROM insertedConverted i
		CROSS JOIN (VALUES('IsPrimary')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.[Level]  = c.[Key]
			AND i.TaxonomyId = c.[SecondKey]
			AND i.ExternalId = c.[ThirdKey]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;