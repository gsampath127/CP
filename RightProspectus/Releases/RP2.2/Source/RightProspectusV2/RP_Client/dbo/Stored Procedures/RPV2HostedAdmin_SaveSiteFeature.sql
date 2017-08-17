

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveSiteFeature]
	@SiteId int,
	@SiteKey  varchar(200),
	@FeatureMode int,
	@ModifiedBy int
AS	
BEGIN 
     IF EXISTS (SELECT 1 FROM SiteFeature WHERE SiteId = @SiteId AND [Key] =@SiteKey)
		 BEGIN
			UPDATE SiteFeature
			SET 
				FeatureMode = @FeatureMode,
				ModifiedBy = @ModifiedBy,
				UtcModifiedDate = GETUTCDATE()
			WHERE SiteId = @SiteId	 AND [Key] = @SiteKey
		 END

	 ELSE
		BEGIN
		    INSERT INTO SiteFeature(
			SiteId,
			[Key],
			FeatureMode,
			UtcModifiedDate,
			ModifiedBy
            )
			VALUES(
			@SiteId,
			@SiteKey,
			@FeatureMode,
			GETUTCDATE(),
			@ModifiedBy
			)
		END

END