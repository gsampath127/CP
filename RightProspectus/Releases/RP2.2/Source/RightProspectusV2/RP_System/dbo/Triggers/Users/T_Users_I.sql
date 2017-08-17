-- Insert trigger for Users
CREATE TRIGGER T_Users_I
ON dbo.Users
FOR INSERT
AS
BEGIN
	DECLARE @TableName NVARCHAR(128);
	DECLARE @ObjectId INT;
	DECLARE @CUDHistory dbo.TT_CUDHistory;
	DECLARE @CUDHistoryData dbo.TT_CUDHistoryData;

	SET	@TableName = 'Users';
	SET	@ObjectId = OBJECT_ID(@TableName);

	INSERT	INTO @CUDHistory (TableName, [Key], CUDType, UserId)
	SELECT	@TableName, i.UserId, 'I', i.ModifiedBy
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
	)
	INSERT	INTO @CUDHistoryData (ParentId, ColumnName, SqlDbType, NewValue)
	SELECT	c.Id, cn.ColumnName, sc.system_type_id,
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
		CROSS JOIN (VALUES('Email'),('EmailConfirmed'),('PasswordHash'),('SecurityStamp'),('PhoneNumber')
						,('PhoneNumberConfirmed'),('TwoFactorEnabled'),('LockOutEndDateUtc'),('LockoutEnabled'),('AccessFailedCount')
						,('UserName'),('FirstName'),('LastName')) AS cn(ColumnName)
		INNER JOIN @CUDHistory c
			ON	i.UserId = c.[Key]
		INNER JOIN sys.columns sc
			ON	sc.object_id = @ObjectId
			AND	sc.name = cn.ColumnName;
			
	EXEC CUDHistory_Insert @CUDHistory, @CUDHistoryData;
END;
