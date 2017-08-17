/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveUrlRewrite]
	Added By: Noel Dsouza
	Date: 09/19/2015
	Reason : To add and update the URLRewrite
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveUrlRewrite]	
	@UrlRewriteId int,
	@MatchPattern NVARCHAR(2083),
	@RewriteFormat NVARCHAR(2083),
	@PatternName NVARCHAR(100),
	@modifiedBy int
AS
BEGIN
	
	IF(@UrlRewriteId=0) 
		BEGIN
			INSERT INTO UrlRewrite(
				MatchPattern,
				RewriteFormat,
				PatternName,
				utcModifiedDate,
				ModifiedBy) 
			VALUES (
				@MatchPattern,
				@RewriteFormat,
				@PatternName,
				GETUTCDATE(),
				@modifiedBy)
		END
	ELSE
		BEGIN
			UPDATE UrlRewrite 
				SET MatchPattern=@MatchPattern,
				RewriteFormat=@RewriteFormat,
				PatternName=@PatternName,
				ModifiedBy=@modifiedBy,
				utcModifiedDate=GETUTCDATE()
			WHERE UrlRewriteId = @UrlRewriteId
		END

END


