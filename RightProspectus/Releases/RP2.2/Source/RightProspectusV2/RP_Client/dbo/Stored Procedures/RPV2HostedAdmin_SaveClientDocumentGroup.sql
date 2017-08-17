
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveClientDocumentGroup]
	@ClientDocumentGroupId INT, 
	@Name NVARCHAR(100), 
	@Description NVARCHAR(100),
	@ParentClientDocumentGroupId INT,
	@CssClass VARCHAR(50),
	@ModifiedBy INT,
	@ClientDocumentGroupClientDocument TT_ClientDocumentGroupClientDocument READONLY
AS
BEGIN
	IF @ClientDocumentGroupId = 0
		BEGIN
			DECLARE @identityClientDocumentGroupId INT
			INSERT INTO ClientDocumentGroup
					(
						Name,
						[Description],
						ParentClientDocumentGroupId,
						CssClass,
						UtcModifiedDate,
						ModifiedBy
					)
				VALUES
					(
						@Name,
						@Description,
						@ParentClientDocumentGroupId,
						@CssClass,
						GETUTCDATE(),
						@ModifiedBy
					)
			SELECT @identityClientDocumentGroupId = SCOPE_IDENTITY()

			INSERT INTO ClientDocumentGroupClientDocument 
					(
						ClientDocumentGroupId,
						ClientDocumentId,
						[Order],
						UtcModifiedDate,
						ModifiedBy
					)
					SELECT 
						@identityClientDocumentGroupId,
						ClientDocumentId,
						[Order],
						GETUTCDATE(),
						@ModifiedBy
					FROM @ClientDocumentGroupClientDocument
		END
	ELSE
		BEGIN
			UPDATE ClientDocumentGroup SET
				Name = @Name,
				[Description] = @Description,
				ParentClientDocumentGroupId = @ParentClientDocumentGroupId,
				CssClass = @CssClass,
				UtcModifiedDate = GETUTCDate(),
				ModifiedBy = @ModifiedBy
			WHERE ClientDocumentGroupId = @ClientDocumentGroupId

			DECLARE @deleteClientDocumentGroupClientDocument TT_ClientDocumentGroupClientDocument

			DELETE FROM ClientDocumentGroupClientDocument 
				OUTPUT deleted.ClientDocumentGroupId, deleted.ClientDocumentId , deleted.[Order]
				INTO @deleteClientDocumentGroupClientDocument
			WHERE ClientDocumentGroupId = @ClientDocumentGroupId AND
				ClientDocumentId NOT IN 
				(SELECT ClientDocumentId FROM @ClientDocumentGroupClientDocument)

			UPDATE	CUDHistory				 
  				 SET	UserId = @ModifiedBy
				WHERE	TableName = N'ClientDocumentGroupClientDocument'
					AND	[Key] = @ClientDocumentGroupId
					AND [CUDType] = 'D' 
					AND [SecondKey]  in (SELECT ClientDocumentId from @deleteClientDocumentGroupClientDocument);

			INSERT INTO ClientDocumentGroupClientDocument
				(
					ClientDocumentGroupId,
					ClientDocumentId,
					[Order],
					UtcModifiedDate,
					ModifiedBy
				)
			SELECT 
				ClientDocumentGroupId,
				ClientDocumentId,
				[Order],
				GETUTCDATE(),
				@ModifiedBy
			FROM @ClientDocumentGroupClientDocument 
			WHERE ClientDocumentId NOT IN 
				(
					SELECT ClientDocumentId FROM ClientDocumentGroupClientDocument 
					WHERE ClientDocumentGroupId = @ClientDocumentGroupId
				)
		END

END