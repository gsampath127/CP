
CREATE Procedure [dbo].[RPV2HostedAdmin_SaveUrlRewite]

@UrlRewriteId int,
@MatchPattern nvarchar(2083),
@RewriteFormat nvarchar(2083),
@UtcModifiedDate datetime,
@ModifiedBy int ,
@PatternName nvarchar(100)

as

BEGIN
     IF EXISTS (SELECT 1   FROM   UrlRewrite    WHERE  UrlRewriteId = @UrlRewriteId)
			UPDATE UrlRewrite
			Set MatchPattern = @MatchPattern ,
				RewriteFormat = @RewriteFormat,
				UtcModifiedDate = @UtcModifiedDate ,
				ModifiedBy = @ModifiedBy ,
				PatternName = @PatternName
			WHERE UrlRewriteId = @UrlRewriteId

	 ELSE
	     
		 INSERT INTO UrlRewrite
		    VALUES( 
                   @MatchPattern ,
                    @RewriteFormat ,
                    @UtcModifiedDate ,
                    @ModifiedBy ,
					@PatternName
					)


END