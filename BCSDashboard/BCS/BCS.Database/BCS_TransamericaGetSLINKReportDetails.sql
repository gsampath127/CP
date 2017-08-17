USE [db1029]
GO

/****** Object:  StoredProcedure [dbo].[BCS_TransamericaGetSLINKReportDetails]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_TransamericaGetSLINKReportDetails]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_TransamericaGetSLINKReportDetails]
GO  
  
Create Procedure [dbo].[BCS_TransamericaGetSLINKReportDetails]
@DocIDs TT_SLinkDocumentId READONLY,
@SelectedDate DateTime,
@Status nvarchar(200),
@sortDirection NVARCHAR(10),
@sortColumn    NVARCHAR(100),
@startIndex INT,
@endIndex INT
AS  
BEGIN

--e.g @SelectedDate will be passed as '2016-02-23'

IF @SelectedDate IS NOT NULL
	SET @SelectedDate = DATEADD(MINUTE, 90, @SelectedDate)

DECLARE @SLINKINFO TABLE(SLINKFileName nvarchar(200), ZipFileName nvarchar(200), Status nvarchar(200), ReceivedDate DateTime)
DECLARE @DocIDsCount INT = 0
SELECT @DocIDsCount = COUNT(d.DocumentId) FROM @DocIDs d

INSERT INTO @SLINKINFO
SELECT * FROM(
	SELECT DISTINCT 	
		CASE WHEN BCSDocUpdateSupplements.DocumentType IN ('SP', 'RSP') THEN 'SP' + Replace(BCSDocUpdateSupplements.PDFName,'.pdf','') + 'GIM'
				WHEN BCSDocUpdateSupplements.DocumentType IN ('P', 'RP') THEN 'P' + Replace(BCSDocUpdateSupplements.PDFName,'.pdf','') + 'GIM'
				WHEN BCSDocUpdateSupplements.DocumentType IN ('SPS', 'PS') 
										THEN BCSDocUpdateSupplements.DocumentType + Replace(BCSDocUpdateSupplements.PDFName,'.pdf','') + Convert(varchar, BCSDocUpdateSupplements.EdgarID) + 'GIM'
		END AS 'SLINKFileName', 
		BCSDocUpdateSupplementsSlink.ZipFileName,
		CASE WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsAPC, 0) != 0 THEN 'APC'
				WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsOPC, 0) != 0 THEN 'OPC'
				WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsAPF, 0) != 0 THEN 'AP'
				WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsOPF, 0) != 0 THEN 'OP'
				WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsExported, 0) != 0 THEN 'EX'
				ELSE NULL
		END AS 'Status',
		CASE WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsAPC, 0) != 0 THEN APCReceivedDate
				WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsOPC, 0) != 0 THEN OPCReceivedDate
				WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsAPF, 0) != 0 THEN APFReceivedDate
				WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsOPF, 0) != 0 THEN OPFReceivedDate
				WHEN BCSDocUpdateSupplements.IsProcessed = 1 AND ISNULL(BCSDocUpdateSupplementsSlink.IsExported, 0) != 0 THEN ExportedDate
				ELSE NULL
		END AS 'ReceivedDate'
	FROM BCSDocUpdateSupplements
	INNER JOIN BCSTransamericaWatchListCUSIPs ON BCSDocUpdateSupplements.CUSIP = BCSTransamericaWatchListCUSIPs.CUSIP
	INNER JOIN BCSDocUpdateSupplementsSlink On BCSDocUpdateSupplementsSlink.BCSDocUpdateSupplementsSlinkID = BCSDocUpdateSupplements.BCSDocUpdateSupplementsSlinkID
	WHERE BCSDocUpdateSupplementsSlink.IsExported = 1 and (@SelectedDate IS NULL OR BCSDocUpdateSupplementsSlink.ExportedDate BETWEEN DATEADD(minute,-5,DATEADD(day,-1, @SelectedDate)) AND @SelectedDate)
	
	UNION

	SELECT DISTINCT 	
			Replace(Replace(BCSDocUpdateARSAR.DocumentType,'RSAR','SAR'), 'RAR', 'AR') +  Replace(BCSDocUpdateARSAR.PDFName,'.pdf','') + 'GIM' AS 'SLINKFileName', 
		BCSDocUpdateARSARSlink.ZipFileName,
		CASE WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsAPC, 0) != 0 THEN 'APC'
			WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsOPC, 0) != 0 THEN 'OPC'
			WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsAPF, 0) != 0 THEN 'AP'
			WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsOPF, 0) != 0 THEN 'OP'
			WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsExported, 0) != 0 THEN 'EX'
			ELSE NULL
		END AS 'Status',
		CASE WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsAPC, 0) != 0 THEN APCReceivedDate
			WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsOPC, 0) != 0 THEN OPCReceivedDate
			WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsAPF, 0) != 0 THEN APFReceivedDate
			WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsOPF, 0) != 0 THEN OPFReceivedDate
			WHEN BCSDocUpdateARSAR.IsProcessed = 1 AND ISNULL(BCSDocUpdateARSARSlink.IsExported, 0) != 0 THEN ExportedDate
			ELSE NULL
		END AS 'ReceivedDate'
	FROM BCSDocUpdateARSAR
	INNER JOIN BCSTransamericaWatchListCUSIPs ON BCSDocUpdateARSAR.CUSIP = BCSTransamericaWatchListCUSIPs.CUSIP
	INNER JOIN BCSDocUpdateARSARSlink On BCSDocUpdateARSARSlink.BCSDocUpdateARSARSlinkID = BCSDocUpdateARSAR.BCSDocUpdateARSARSlinkID
	WHERE BCSDocUpdateARSARSlink.IsExported = 1 and (@SelectedDate IS NULL OR BCSDocUpdateARSARSlink.ExportedDate BETWEEN DATEADD(minute,-5,DATEADD(day,-1, @SelectedDate)) AND @SelectedDate)
	AND DocumentType NOT IN ('QR', 'RQR')
)t
WHERE (@DocIDsCount = 0 OR t.SLINKFileName IN (SELECT d.DocumentId FROM @DocIDs d)) And (@Status IS NULL OR t.[Status] = @Status)

SELECT * FROM 
(
SELECT *,ROW_NUMBER() OVER(ORDER  BY
							CASE
								WHEN @sortDirection <> 'ASC' THEN ''
								WHEN @sortColumn = 'SLINKFileName' THEN slinkfilename
							END ASC,
							CASE
								WHEN @sortDirection <> 'ASC' THEN ''
								WHEN @sortColumn = 'ZipFileName' THEN zipfilename
							END ASC,
							CASE
								WHEN @sortDirection <> 'ASC' THEN ''
								WHEN @sortColumn = 'Status' THEN [status]
							END ASC,
							CASE
								WHEN @sortDirection <> 'ASC' THEN Cast(NULL AS DATETIME)
								WHEN @sortColumn = 'ReceivedDate' THEN receiveddate
							END ASC,
							CASE
								WHEN @sortDirection <> 'DESC' THEN ''
								WHEN @sortColumn = 'SLINKFileName' THEN slinkfilename
							END DESC,
							CASE
								WHEN @sortDirection <> 'DESC' THEN ''
								WHEN @sortColumn = 'ZipFileName' THEN zipfilename
							END DESC,
							CASE
								WHEN @sortDirection <> 'DESC' THEN ''
								WHEN @sortColumn = 'Status' THEN [status]
							END DESC,
							CASE
								WHEN @sortDirection <> 'DESC' THEN Cast(NULL AS DATETIME)
								WHEN @sortColumn = 'ReceivedDate' THEN receiveddate
							END DESC
		) AS RowNum FROM @SLINKINFO
) AS SLinkInfo 
WHERE SLinkInfo.RowNum BETWEEN @startIndex AND @endIndex

SELECT
(SELECT COUNT(*) FROM @SLINKINFO) as virtualcount,
(SELECT COUNT(*) FROM @SLINKINFO WHERE [Status] = 'EX') as ExCount,
(SELECT COUNT(*) FROM @SLINKINFO WHERE [Status] = 'AP') as APCount,
(SELECT COUNT(*) FROM @SLINKINFO WHERE [Status] = 'OP') as OPCount,
(SELECT COUNT(*) FROM @SLINKINFO WHERE [Status] = 'APC') as APCCount,
(SELECT COUNT(*) FROM @SLINKINFO WHERE [Status] = 'OPC') as OPCCount

END