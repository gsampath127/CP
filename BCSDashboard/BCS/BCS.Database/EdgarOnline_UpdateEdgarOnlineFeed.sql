CREATE PROCEDURE [dbo].[EdgarOnline_UpdateEdgarOnlineFeed]
AS
BEGIN

	--1. Delete all CUSIPs which are not present in latest feed file from Edgar online
	DELETE FROM EdgarOnlineFeed WHERE EClassContractID NOT IN(SELECT 'C' + RIGHT('000000000'+ISNULL(EClassContractID , ''),9) FROM EdgarOnlineFeed_FTP)

	--2. Insert all new CUSIPs which are present in latest feed file from Edgar online but not in EdgarOnlineFeed
	INSERT INTO EdgarOnlineFeed(ECUSIP, EFundName, Eticker, ECompanyName, ECIK, ESeriesID, EClassContractID, Euniverseabbrev)
    SELECT feedFTP.ECUSIP, feedFTP.EFundName, feedFTP.Eticker, feedFTP.ECompanyName, feedFTP.ECIK, 'S' + RIGHT('000000000'+ISNULL(feedFTP.ESeriesID , ''),9), 'C' + RIGHT('000000000'+ISNULL(feedFTP.EClassContractID , ''),9), feedFTP.Euniverseabbrev
	FROM EdgarOnlineFeed_FTP feedFTP
	LEFT JOIN EdgarOnlineFeed ON  EdgarOnlineFeed.EClassContractID = 'C' + RIGHT('000000000'+ISNULL(feedFTP.EClassContractID , ''),9)
	WHERE EdgarOnlineFeed.ECUSIP IS NULL AND IsNull(feedFTP.EClassContractID, '') <> ''

	--3. Update security type from EdgarOnlineFeed as per new feed

	UPDATE EdgarOnlineFeed
	SET Euniverseabbrev = feedFTP.Euniverseabbrev
	FROM EdgarOnlineFeed
	INNER JOIN EdgarOnlineFeed_FTP feedFTP  ON EdgarOnlineFeed.EClassContractID = 'C' + RIGHT('000000000'+ISNULL(feedFTP.EClassContractID , ''),9)
	WHERE EdgarOnlineFeed.Euniverseabbrev <> feedFTP.Euniverseabbrev AND IsNull(feedFTP.EClassContractID, '') <> ''


	--4. Update  SecurityTypeID in proticker table as per EdgarOnlineFeed

	DECLARE @SecurityTypeFeedSourceID INT
	SELECT @SecurityTypeFeedSourceID = SecurityTypeFeedSourceID FROM SecurityTypeFeedSource 
	WHERE SourceName = 'EdgarOnline'

	UPDATE prosticker
	  set SecurityTypeID = SecurityType.SecurityTypeID, SecurityTypeFeedSourceID = @SecurityTypeFeedSourceID
	  From ProsTicker
	  INNER JOIN EdgarOnlineFeed on ProsTicker.ClassContractID = EdgarOnlineFeed.EClassContractID
	  INNER JOIN SecurityType ON EdgarOnlineFeed.Euniverseabbrev = SecurityType.SecurityTypeCode	  
	WHERE ISNULL(ProsTicker.CUSIP, '') <> '' AND (ProsTicker.SecurityTypeID IS NULL OR ProsTicker.SecurityTypeID <> SecurityType.SecurityTypeID)
		AND IsNull(EdgarOnlineFeed.EClassContractID, '') <> ''

	
End
GO