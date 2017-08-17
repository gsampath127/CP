-- Update trigger for Users
CREATE TRIGGER T_Users_U
ON dbo.Users
FOR UPDATE
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = N'Users';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.UserId, 'U', i.ModifiedBy
	FROM	inserted i;
	
	WITH insertedConverted AS
	(
		SELECT	i.UserId,
			CONVERT(NVARCHAR(MAX), i.Email) AS Email,
			CONVERT(NVARCHAR(MAX), i.EmailConfirmed) AS EmailConfirmed,
			CONVERT(NVARCHAR(MAX), i.PasswordHash) AS PasswordHash,
			CONVERT(NVARCHAR(MAX), i.SecurityStamp) AS SecurityStamp,
			CONVERT(NVARCHAR(MAX), i.PhoneNumber) AS PhoneNumber,
			CONVERT(NVARCHAR(MAX), i.PhoneNumberConfirmed) AS PhoneNumberConfirmed,
			CONVERT(NVARCHAR(MAX), i.TwoFactorEnabled) AS TwoFactorEnabled,
			CONVERT(NVARCHAR(MAX), i.LockOutEndDateUtc) AS LockOutEndDateUtc,
			CONVERT(NVARCHAR(MAX), i.LockoutEnabled) AS LockoutEnabled,
			CONVERT(NVARCHAR(MAX), i.AccessFailedCount) AS AccessFailedCount,
			CONVERT(NVARCHAR(MAX), i.UserName) AS UserName,
			CONVERT(NVARCHAR(MAX), i.FirstName) AS FirstName,
			CONVERT(NVARCHAR(MAX), i.LastName) AS LastName			
		FROM	inserted i
	),
	deletedConverted AS
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
	),
	dictionary AS
	(
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
		END AS OldValue,
		CASE cn.ColumnName
			WHEN 'Email' THEN i.Email
			WHEN 'EmailConfirmed' THEN i.EmailConfirmed
			WHEN 'PasswordHash' THEN i.PasswordHash
			WHEN 'SecurityStamp' THEN i.SecurityStamp
			WHEN 'PhoneNumber' THEN i.PhoneNumber
			WHEN 'PhoneNumberConfirmed' THEN i.PhoneNumberConfirmed
			WHEN 'TwoFactorEnabled' THEN i.TwoFactorEnabled
			WHEN 'LockOutEndDateUtc' THEN i.LockOutEndDateUtc
			WHEN 'LockoutEnabled' THEN i.LockoutEnabled
			WHEN 'AccessFailedCount' THEN i.AccessFailedCount
			WHEN 'UserName' THEN i.UserName
			WHEN 'FirstName' THEN i.FirstName
			WHEN 'LastName' THEN i.LastName
		END AS NewValue
		FROM insertedConverted i
			INNER JOIN deletedConverted d
				ON	i.UserId = d.UserId
					CROSS JOIN (VALUES('Email'),('EmailConfirmed'),('PasswordHash'),('SecurityStamp'),('PhoneNumber')
						,('PhoneNumberConfirmed'),('TwoFactorEnabled'),('LockOutEndDateUtc'),('LockoutEnabled'),('AccessFailedCount')
						,('UserName'),('FirstName'),('LastName')) AS cn(ColumnName)
			INNER JOIN @CUDHistory c
				ON	i.UserId = c.[Key]
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
