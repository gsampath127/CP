USE db1029
GO

/****** Object:  StoredProcedure [dbo].[BCS_CheckBCSTRPFLTFTPDataDiscrepancy]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_CheckBCSTRPFLTFTPDataDiscrepancy]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_CheckBCSTRPFLTFTPDataDiscrepancy]
GO

CREATE PROCEDURE [dbo].[BCS_CheckBCSTRPFLTFTPDataDiscrepancy]
AS
BEGIN

	DECLARE @IsBCSTRPFLTFTPDataDiscrepancy BIT
	SET @IsBCSTRPFLTFTPDataDiscrepancy = 0
	
	
	IF EXISTS(
	
		SELECT TOP 1 BCSTRPFLT.BCSTRPFLTID AS 'ID'
		FROM BCSTRPFLT
		INNER JOIN ProsTicker ON ProsTicker.CUSIP like BCSTRPFLT.FUNDCUSIPNUMBER + '%'      
		INNER JOIN Prospectus ON ProsTicker.ProspectusID = Prospectus.ProsID
		INNER JOIN Company ON Company.CompanyID = Prospectus.CompanyID
		WHERE BCSTRPFLT.DatePDFReceivedonFTP IS NULL
		
		UNION
		
		SELECT TOP 1 hostedadmin.dbo.tblBCSTRPFLTDocUploadReadyDocuments.ID AS 'ID'
		FROM hostedadmin.dbo.tblBCSTRPFLTDocUploadReadyDocuments
		WHERE IsDocUploadReady = 0 AND 	ISNULL(ProsID,'') != 'NA'
	)
	BEGIN
	
		SET @IsBCSTRPFLTFTPDataDiscrepancy = 1
	
	END
	
	SELECT @IsBCSTRPFLTFTPDataDiscrepancy
	
End
GO

