-- =============================================
-- Author:		Noel Dsouza
-- Create date: 02/10/2014
-- Description:	Return set of Funds for given EdgarID
-- =============================================
ALTER PROCEDURE [dbo].[sp_Admin_Get_FundsUnProcessedWithDocs] 
	@EdgarID int,
	@DocTypeID varchar(3),
	@DocTypeSupplementID varchar(3)
AS
BEGIN
	-- SET NOCOUNT ON added to prevent extra result sets from
	-- interfering with SELECT statements.
	SET NOCOUNT ON;

Declare @LocalEdgarID int

Declare @LocalDocTypeID varchar(3)

Declare @LocalDocTypeSupplementID varchar(3)

SET @LocalEdgarID = @EdgarID

SET @LocalDocTypeID = @DocTypeID

SET @LocalDocTypeSupplementID = @DocTypeSupplementID

SELECT * FROM 
((SELECT Prospectus.ProsID, replace(Prospectus.ProsName,'&','&amp;') as ProsName, EdgarFunds.Processed, EdgarFunds.FundID, 
                                CAST((STR( YEAR( EdgarFunds.DateUpdated ) ) + 
                                '/' + STR( MONTH( EdgarFunds.DateUpdated ) ) + 
                                '/' + STR( DAY( EdgarFunds.DateUpdated ) ) )AS datetime) AS  DateUpdated, 
                                EdgarFunds.isURLAssigned,  MAX(SecurityBenefits.CUSIP) as SB_CUSIP,
								MAX(GenworthSubAccounts.CUSIP) as GENWORTH_CUSIP,MAX(Symetra.CUSIP) as SE2_CUSIP,
								MAX(Forethought.CUSIP) as Forethought_CUSIP,MAX(NYLSubAccounts.CUSIP) as NewYorkLife_CUSIP,
								MAX(OhioNational.CUSIP) as OhioNational_CUSIP,MAX(MOACusips.CUSIP) as MOA_CUSIP,
								MAX(DelawareLife.CUSIP) as DEL_CUSIP,MAX(Allianz.CUSIP) as AZL_CUSIP,
								MAX(BCSAllianceBernsteinWatchListCUSIPsView.CUSIP) as InlineAB_Flag,
								MAX(BCSAllianceBernsteinWatchListCUSIPsForFLPurpose.CUSIP) as InlineABFL_Flag,
								MAX(BCSTransamericaWatchListCUSIPs.CUSIP) as InlineTRA_Flag,
								Prosdocs.ProsDocId, ProsDocTypeID, ProsDocOrder, 
								ProsDocURL, ProsDocUseAltURL, ProsDocAltURL,ProsDocFundSiteURL,
								CustomizedDocClients.ClientID,CustomizedDocType.CustomizedTypeID,
								CustomizedDocClients.ClientName,CustomizedDocType.CustomizedTypeName,isnull(EdgarFundsDocs.EdgarFundsDocId,0) as DocExists   															 
                           FROM Prospectus INNER JOIN ProsTicker ON ProsTicker.ProspectusID = Prospectus.ProsID INNER JOIN 
                                EdgarFunds ON EdgarFunds.TickerID = ProsTicker.TickerID
                                LEFT outer JOIN ProsDocs on Prospectus.ProsID = ProsDocs.ProsId and Prosdocs.ProsDocTypeId in (@LocalDocTypeID,@LocalDocTypeSupplementID) 
								left outer join SecurityBenefits on ProsTicker.CUSIP = SecurityBenefits.CUSIP
								LEFT OUTER JOIN  (SELECT distinct CUSIP FROM GenworthSubAccounts where GenworthSubAccounts.ProductCode != '0' or GenworthSubAccounts.ProductID != 0) as GenworthSubAccounts  ON ProsTicker.CUSIP = GenworthSubAccounts.CUSIP
								left outer join Symetra on ProsTicker.CUSIP = Symetra.CUSIP
								left outer join Forethought on ProsTicker.CUSIP = Forethought.CUSIP
								left outer join NYLSubAccounts on Prosticker.CUSIP = NYLSubAccounts.CUSIP
								left outer join OhioNational on Prosticker.CUSIP = OhioNational.CUSIP
								left outer join MOACusips on Prosticker.CUSIP = MOACusips.CUSIP		
								left outer join DelawareLife on ProsTicker.CUSIP = DelawareLife.CUSIP							
								left outer join Allianz on ProsTicker.CUSIP = Allianz.CUSIP		
								left outer join BCSAllianceBernsteinWatchListCUSIPsView on ProsTicker.CUSIP = BCSAllianceBernsteinWatchListCUSIPsView.CUSIP	
								left outer join BCSAllianceBernsteinWatchListCUSIPsForFLPurpose on ProsTicker.CUSIP = BCSAllianceBernsteinWatchListCUSIPsForFLPurpose.CUSIP	
								left outer join BCSTransamericaWatchListCUSIPs on ProsTicker.CUSIP = BCSTransamericaWatchListCUSIPs.CUSIP													
								left outer join CustomizedDocClients on Prosdocs.ClientID = CustomizedDocClients.ClientID
								left outer join CustomizedDocType on Prosdocs.CustomizedTypeID = CustomizedDocType.CustomizedTypeID
								left outer join EdgarFundsDocs on Prosdocs.ProsId = EdgarFundsDocs.FundID and Prosdocs.ProsDocId = EdgarFundsDocs.ProsDocId
									and EdgarFundsDocs.EdgarID = @LocalEdgarID																							
                           WHERE    (EdgarFunds.EdgarID =@LocalEdgarID ) AND (EdgarFunds.Processed ='0' ) GROUP BY Prospectus.ProsID, 
                                    Prospectus.ProsName,EdgarFunds.Processed, 
                                    CAST( ( STR( YEAR( EdgarFunds.DateUpdated ) ) + '/' + 
                                            STR( MONTH( EdgarFunds.DateUpdated ) ) + '/' + 
                                            STR( DAY( EdgarFunds.DateUpdated ) ) ) AS datetime) , 
                                    EdgarFunds.isURLAssigned, EdgarFunds.FundID,
                                    Prosdocs.ProsDocId,ProsDocTypeID,ProsDocOrder,ProsDocURL,
                                    ProsDocUseAltURL,ProsDocAltURL,ProsDocFundSiteURL,CustomizedDocClients.ClientID,
                                    CustomizedDocType.CustomizedTypeID,CustomizedDocClients.ClientName,
                                    CustomizedDocType.CustomizedTypeName,isnull(EdgarFundsDocs.EdgarFundsDocId,0))
UNION ALL

	(SELECT Prospectus.ProsID, replace(Prospectus.ProsName,'&','&amp;') as ProsName, EdgarNewFunds.Processed, (SELECT 1) as FundID,
                                CAST((STR( YEAR( EdgarNewFunds.DateUpdated ) ) + 
                                '/' + STR( MONTH( EdgarNewFunds.DateUpdated ) ) + 
                                '/' + STR( DAY( EdgarNewFunds.DateUpdated ) ) )AS datetime) AS  DateUpdated, 
                                EdgarNewFunds.isURLAssigned, MAX(SecurityBenefits.CUSIP) as SB_CUSIP,
                                 MAX(GenworthSubAccounts.CUSIP) as GENWORTH_CUSIP,MAX(Symetra.CUSIP) as SE2_CUSIP,
                                 MAX(Forethought.CUSIP) as Forethought_CUSIP,MAX(NYLSubAccounts.CUSIP) as NewYorkLife_CUSIP,
								 MAX(OhioNational.CUSIP) as OhioNational_CUSIP,MAX(MOACusips.CUSIP) as MOA_CUSIP,
								 MAX(DelawareLife.CUSIP) as DEL_CUSIP,MAX(Allianz.CUSIP) as AZL_CUSIP,	
								 MAX(BCSAllianceBernsteinWatchListCUSIPsView.CUSIP) as InlineAB_Flag,
								 MAX(BCSAllianceBernsteinWatchListCUSIPsForFLPurpose.CUSIP) as InlineABFL_Flag,
								 MAX(BCSTransamericaWatchListCUSIPs.CUSIP) as InlineTRA_Flag,							 
								Prosdocs.ProsDocId, ProsDocTypeID, ProsDocOrder, 
								ProsDocURL, ProsDocUseAltURL, ProsDocAltURL,ProsDocFundSiteURL,
								CustomizedDocClients.ClientID,CustomizedDocType.CustomizedTypeID,
								CustomizedDocClients.ClientName,CustomizedDocType.CustomizedTypeName,isnull(EdgarFundsDocs.EdgarFundsDocId,0) as DocExists     
                           FROM Prospectus INNER JOIN ProsTicker ON ProsTicker.ProspectusID = Prospectus.ProsID INNER JOIN 
                                EdgarNewFundTicker ON 
									(EdgarNewFundTicker.CIK = ProsTicker.CIK 
										AND EdgarNewFundTicker.SeriesID = ProsTicker.SeriesID 
										AND EdgarNewFundTicker.ClassContractID = ProsTicker.ClassContractID ) 
								INNER JOIN EdgarNewFunds ON EdgarNewFundTicker.EdgarNewFundID=EdgarNewFunds.EdgarNewFundsID
								LEFT outer JOIN ProsDocs on Prospectus.ProsID = ProsDocs.ProsId and Prosdocs.ProsDocTypeId in (@LocalDocTypeID,@LocalDocTypeSupplementID)		
								LEFT OUTER JOIN SecurityBenefits on ProsTicker.CUSIP = SecurityBenefits.CUSIP
								LEFT OUTER JOIN  (SELECT distinct CUSIP FROM GenworthSubAccounts where GenworthSubAccounts.ProductCode != '0' or GenworthSubAccounts.ProductID != 0) as GenworthSubAccounts  ON ProsTicker.CUSIP = GenworthSubAccounts.CUSIP
								left outer join Symetra on ProsTicker.CUSIP = Symetra.CUSIP
								left outer join Forethought on ProsTicker.CUSIP = Forethought.CUSIP
								left outer join NYLSubAccounts on Prosticker.CUSIP = NYLSubAccounts.CUSIP
								left outer join OhioNational on Prosticker.CUSIP = OhioNational.CUSIP
								left outer join MOACusips on Prosticker.CUSIP = MOACusips.CUSIP				
								left outer join DelawareLife on ProsTicker.CUSIP = DelawareLife.CUSIP			
								left outer join Allianz on ProsTicker.CUSIP = Allianz.CUSIP			
								left outer join BCSAllianceBernsteinWatchListCUSIPsView on ProsTicker.CUSIP = BCSAllianceBernsteinWatchListCUSIPsView.CUSIP	
								left outer join BCSAllianceBernsteinWatchListCUSIPsForFLPurpose on ProsTicker.CUSIP = BCSAllianceBernsteinWatchListCUSIPsForFLPurpose.CUSIP		
								left outer join BCSTransamericaWatchListCUSIPs on ProsTicker.CUSIP = BCSTransamericaWatchListCUSIPs.CUSIP																											
								left outer join CustomizedDocClients on Prosdocs.ClientID = CustomizedDocClients.ClientID
								left outer join CustomizedDocType on Prosdocs.CustomizedTypeID = CustomizedDocType.CustomizedTypeID
								left outer join EdgarFundsDocs on Prosdocs.ProsId = EdgarFundsDocs.FundID and Prosdocs.ProsDocId = EdgarFundsDocs.ProsDocId
									and EdgarFundsDocs.EdgarID = @LocalEdgarID																							
								WHERE    (EdgarNewFunds.EdgarID =@LocalEdgarID ) AND (EdgarNewFunds.Processed ='0' ) GROUP BY Prospectus.ProsID, 
                                    Prospectus.ProsName,EdgarNewFunds.Processed, 
                                    CAST( ( STR( YEAR( EdgarNewFunds.DateUpdated ) ) + '/' + 
                                            STR( MONTH( EdgarNewFunds.DateUpdated ) ) + '/' + 
                                            STR( DAY( EdgarNewFunds.DateUpdated ) ) ) AS datetime) , 
                                    EdgarNewFunds.isURLAssigned,
                                    Prosdocs.ProsDocId,ProsDocTypeID,ProsDocOrder,ProsDocURL,
                                    ProsDocUseAltURL,ProsDocAltURL,ProsDocFundSiteURL,CustomizedDocClients.ClientID,
                                    CustomizedDocType.CustomizedTypeID,CustomizedDocClients.ClientName,
                                    CustomizedDocType.CustomizedTypeName,isnull(EdgarFundsDocs.EdgarFundsDocId,0))) as UnprocessedDocs
                                    where DocExists=0
                                    ORDER BY ProsName,ClientID desc,ProsDocTypeId, ProsDocOrder
END


