CREATE PROCEDURE [dbo].RPV2Hosted_SaveErrorLog
@ErrorCode int,
@Priority int,
@Severity nvarchar(32),
@Title nvarchar(256),
@MachineName nvarchar(32),
@AppDomainName nvarchar(512),
@ProcessID nvarchar(256),
@ProcessName nvarchar(512),
@ThreadName nvarchar(512),
@Win32ThreadId nvarchar(128),
@EventId int,
@SiteActivityId int,
@Message nvarchar(1500),
@FormattedMessage nvarchar(max),
@URL nvarchar(500),
@AbsoluteURL nvarchar(500)
AS
BEGIN

  INSERT INTO ErrorLog
     (ErrorCode,
      ErrorUtcDate,
      [Priority],
      Severity,
      Title,
      MachineName,
      AppDomainName,
      ProcessID,
      ProcessName,
      ThreadName,
      Win32ThreadId,
      EventId,
      SiteActivityId,
      [Message],
      FormattedMessage,
	  URL,
	  AbsoluteURL
      )
   VALUES
     (@ErrorCode,
      GETUTCDATE(),
      @Priority,
      @Severity,
      @Title,
      @MachineName,
      @AppDomainName,
      @ProcessID,
      @ProcessName,
      @ThreadName,
      @Win32ThreadId,
      @EventId,
      @SiteActivityId,
      @Message,
      @FormattedMessage,
	  @URL,
	  @AbsoluteURL
      ) 
     
END

