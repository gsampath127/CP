

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SavePageFeature]
	@SiteId int,
	@PageId int,
	@PageKey  varchar(200),
	@FeatureMode int,
	@ModifiedBy int
AS	
BEGIN 
     IF EXISTS (SELECT 1 FROM PageFeature WHERE PageId = @PageId AND [Key] = @PageKey AND SiteId = @SiteId)
		 BEGIN
			UPDATE PageFeature
			SET FeatureMode = @FeatureMode,
				ModifiedBy = @ModifiedBy,
				UtcModifiedDate = GETUTCDATE()
			WHERE PageId = @PageId AND [Key] = @PageKey AND SiteId = @SiteId

		 END

	 ELSE
		BEGIN
		    INSERT INTO PageFeature(
			SiteId,
			PageId,
			[Key],
			FeatureMode,
			UtcModifiedDate,
			ModifiedBy
            )
			VALUES(
			@SiteId,
			@PageId,
			@PageKey,
			@FeatureMode,
			GETUTCDATE(),
			@ModifiedBy
			)
		END

END