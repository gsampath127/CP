
CREATE PROCEDURE [dbo].[RPV2HostedSites_GetXBRLDetailsForTaxonomyID]
@TaxonomyID int
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;
	
	DECLARE @ProsTicker Table
	(
	  ProspectusID int,
	  CIK varchar(50),
	  SeriesID varchar(50),
	  ClassContractID varchar(50)	
	)
	
	INSERT INTO @ProsTicker(ProspectusID,CIK,SeriesID,ClassContractID)
	SELECT @TaxonomyID,CIK,SeriesID,ClassContractID 
	 FROM ProsTicker
	WHERE ProspectusID = @TaxonomyID
	

    -- Insert statements for procedure here

	DECLARE @ProFilingDate datetime
	DECLARE @497FilingDate datetime
	DECLARE @MinFilingDate datetime
	
	DECLARE @IsXBRLCompany bit
	
	DECLARE @CompanyID int
	
	SET @IsXBRLCompany=0
	

	SELECT TOP 1 @ProFilingDate=EdgarXBRL.FilingDate,@CompanyID=Prospectus.CompanyID FROM EdgarXBRL 
							LEFT OUTER JOIN Edgar ON EdgarXBRL.EdgarID = Edgar.EdgarID AND CHARINDEX('Supplement', Edgar.DocumentType) <= 0
							INNER JOIN EdgarXBRLFunds ON EdgarXBRL.EdgarXBRLID = EdgarXBRLFunds.EdgarXBRLID
							INNER JOIN @ProsTicker ProsTicker ON ProsTicker.ClassContractID = EdgarXBRLFunds.ClassContractID
									AND ProsTicker.SeriesID = EdgarXBRLFunds.SeriesID
									AND ProsTicker.CIK = EdgarXBRLFunds.CIK
							INNER JOIN Prospectus ON ProsTicker.ProspectusID = Prospectus.ProsID							
							AND EdgarXBRL.FormType = '485BPOS'
							ORDER BY EdgarXBRL.FilingDate DESC
							
							

	SET @497FilingDate = (SELECT TOP 1 EdgarXBRL.FilingDate FROM EdgarXBRL 
							INNER JOIN Edgar ON EdgarXBRL.EdgarID = Edgar.EdgarID AND CHARINDEX('Supplement', Edgar.DocumentType) <= 0
							INNER JOIN EdgarXBRLFunds ON EdgarXBRL.EdgarXBRLID = EdgarXBRLFunds.EdgarXBRLID
							INNER JOIN @ProsTicker ProsTicker  ON ProsTicker.ClassContractID = EdgarXBRLFunds.ClassContractID
									AND ProsTicker.SeriesID = EdgarXBRLFunds.SeriesID
									AND ProsTicker.CIK = EdgarXBRLFunds.CIK
							INNER JOIN Prospectus ON ProsTicker.ProspectusID = Prospectus.ProsID	
							AND EdgarXBRL.FormType = '497'
							ORDER BY EdgarXBRL.FilingDate DESC)		
							
	IF EXISTS(SELECT COMPANYID FROM XBRLViewerCompanyList WHERE CompanyID = @CompanyID)
	BEGIN
	  SET @IsXBRLCompany=1
	END													
	
	
	IF @497FilingDate is not null
		BEGIN
			IF @497FilingDate > @ProFilingDate
			  SET @MinFilingDate = @497FilingDate 
			ELSE
			  SET @MinFilingDate = @ProFilingDate
		END 
	ELSE
	   BEGIN
			SET @MinFilingDate = @ProFilingDate
	   END
	   
	   
	SELECT DISTINCT EdgarXBRL.Acc#, EdgarXBRL.ZipFileName,
		EdgarXBRL.[Path],
		Company.CompanyName, 
		Prospectus.ProsName AS TaxonomyName, 
		Edgar.DocumentDate AS DocDate, 
		EdgarXBRL.FilingDate AS FilingDate, 
		EdgarXBRL.FilingDate AS OrderDate, 
		EdgarXBRL.FormType,
		EdgarXBRL.CreatedDate,
	CASE
		WHEN CHARINDEX(' - New', Edgar.DocumentType) > 0 THEN REPLACE(Edgar.DocumentType, ' - New', '')
		WHEN CHARINDEX('Prospectus - Revised', Edgar.DocumentType) > 0 THEN REPLACE(Edgar.DocumentType, 'Prospectus - Revised', 'Revised Prospectus')
		WHEN CHARINDEX('Prospectus - Supplement', Edgar.DocumentType) > 0 THEN REPLACE(Edgar.DocumentType, 'Prospectus - Supplement', 'Prospectus Supplement')
		ELSE Edgar.DocumentType
	END AS DocumentType,	
	@IsXBRLCompany AS XBRLViewerCompany,
	EdgarXBRL.IsXBRLViewerReady
	FROM EdgarXBRL 
	LEFT OUTER JOIN Edgar ON EdgarXBRL.EdgarID = Edgar.EdgarID
	INNER JOIN EdgarXBRLFunds ON EdgarXBRL.EdgarXBRLID = EdgarXBRLFunds.EdgarXBRLID
	INNER JOIN @ProsTicker ProsTicker  ON ProsTicker.ClassContractID = EdgarXBRLFunds.ClassContractID
							AND ProsTicker.SeriesID = EdgarXBRLFunds.SeriesID
							AND ProsTicker.CIK = EdgarXBRLFunds.CIK
	INNER JOIN Prospectus ON ProsTicker.ProspectusID = Prospectus.ProsID
	INNER JOIN Company ON EdgarXBRL.CompanyID = Company.CompanyID	
	AND EdgarXBRL.FilingDate BETWEEN @MinFilingDate AND getDate() + 1
	

END
