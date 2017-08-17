USE [hostedadmin]
GO

/****** Object:  StoredProcedure [dbo].[BCS_GetBCSTRPReportFLTMissingData]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_GetBCSTRPReportFLTMissingData]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_GetBCSTRPReportFLTMissingData]
GO

CREATE PROCEDURE [dbo].[BCS_GetBCSTRPReportFLTMissingData]
@StartIndex INT,
@EndIndex INT
AS
BEGIN

	DECLARE @Details Table(CompanyName nvarchar(100), [FileName] varchar(100), [Path] varchar(500), DateReceivedOnFTP DateTime)	
	
	--Find	FLTMissingDataTotalCount	
	SELECT Count(*) AS 'FLTMissingDataTotalCount'
	FROM tblBCSTRPFLTDocUploadReadyDocuments
	WHERE IsDocUploadReady = 0 AND 	ISNULL(ProsID,'') != 'NA'
	
	--Fetch All FLTMissingData details and insert into @@Details according to page size.
	INSERT INTO @Details(CompanyName, [FileName], [Path], DateReceivedOnFTP)	
	SELECT Company, OriginalFileName, [Path], [Date]
	FROM
	(
		SELECT Company, OriginalFileName, [Path], [Date]
		,ROW_NUMBER() OVER(ORDER By tblBCSTRPFLTDocUploadReadyDocuments.ID) AS 'RowNumber'
		FROM tblBCSTRPFLTDocUploadReadyDocuments
		WHERE IsDocUploadReady = 0 AND 	ISNULL(ProsID,'') != 'NA'
	)AS tblCUSIPDetails
	WHERE RowNumber >  @StartIndex AND RowNumber <= @EndIndex
	
	
	SELECT CompanyName, [FileName], [Path], DateReceivedOnFTP
	FROM @Details
	
End
GO

