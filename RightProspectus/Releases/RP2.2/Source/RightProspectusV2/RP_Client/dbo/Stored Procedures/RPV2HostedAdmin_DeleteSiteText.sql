CREATE PROCEDURE [dbo].RPV2HostedAdmin_DeleteSiteText
@SiteTextID int,
@Version int,
@IsProofing bit,
@DeletedBy int
AS
BEGIN
  IF @IsProofing = 1
  BEGIN  
    DELETE SiteTextVersion
      WHERE SiteTextId = @SiteTextID
      AND [Version] = @Version    
  END
  ELSE
  BEGIN
     DELETE SiteTextVersion
      WHERE SiteTextId = @SiteTextID      
     DELETE SiteText
       WHERE SiteTextId = @SiteTextID
       
    UPDATE	CUDHistory
	SET		UserId = @DeletedBy
	WHERE	TableName = N'SiteText'
		AND	[Key] = @SiteTextID AND CUDType = 'D';
  END
END