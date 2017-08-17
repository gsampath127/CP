-- Delete trigger for Users
CREATE TRIGGER T_Users_D
ON dbo.Users
FOR DELETE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Users';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType)
	SELECT	@TableName, d.UserId, 'D'
	FROM	deleted d;
	
	WITH deletedConverted AS
	(
		SELECT	d.UserId,
			CONVERT(NVARCHAR(MAX), d.Email) AS Email,
			CONVERT(NVARCHAR(MAX), d.EmailConfirmed) AS EmailConfirmed,
			CONVERT(NVARCHAR(MAX), d.PasswordHash) AS PasswordHash,
			CONVERT(NVARCHAR(MAX), d.SecurityStamp) AS SecurityStamp,
			CONVERT(NVARCHAR(MAX), d.PhoneNumber) AS PhoneNumber,
			CONVERT(NVARCHAR(MAX), d.PhoneNumberConfirmed) AS PhoneNumberConfirmed,
			CONVERT(NVARCHAR(MAX), d.TwoFactorEnabled) AS TwoFactorEnabled,
			CONVERT(NVARCHAR(MAX), d.LockOutEndDateUtc) AS LockOutEndDateUtc,
			CONVERT(NVARCHAR(MAX), d.LockoutEnabled) AS LockoutEnabled,
			CONVERT(NVARCHAR(MAX), d.AccessFailedCount) AS AccessFailedCount,
			CONVERT(NVARCHAR(MAX), d.UserName) AS UserName,
			CONVERT(NVARCHAR(MAX), d.FirstName) AS FirstName,
			CONVERT(NVARCHAR(MAX), d.LastName) AS LastName			
		FROM	deleted d
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, OldValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
	CASE cn.ColumnName
			WHEN 'Email' THEN d.Email
			WHEN 'EmailConfirmed' THEN d.EmailConfirmed
			WHEN 'PasswordHash' THEN d.PasswordHash
			WHEN 'SecurityStamp' THEN d.SecurityStamp
			WHEN 'PhoneNumber' THEN d.PhoneNumber
			WHEN 'PhoneNumberConfirmed' THEN d.PhoneNumberConfirmed
			WHEN 'TwoFactorEnabled' THEN d.TwoFactorEnabled
			WHEN 'LockOutEndDateUtc' THEN d.LockOutEndDateUtc
			WHEN 'LockoutEnabled' THEN d.LockoutEnabled
			WHEN 'AccessFailedCount' THEN d.AccessFailedCount
			WHEN 'UserName' THEN d.UserName
			WHEN 'FirstName' THEN d.FirstName
			WHEN 'LastName' THEN d.LastName
	END AS OldValue
	FROM deletedConverted d
		CROSS JOIN (VALUES('Email'),('EmailConfirmed'),('PasswordHash'),('SecurityStamp'),('PhoneNumber')
						,('PhoneNumberConfirmed'),('TwoFactorEnabled'),('LockOutEndDateUtc'),('LockoutEnabled'),('AccessFailedCount')
						,('UserName'),('FirstName'),('LastName')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	d.UserId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;

	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;