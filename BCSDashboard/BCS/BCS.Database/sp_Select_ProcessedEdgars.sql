


ALTER Procedure [dbo].[sp_Select_ProcessedEdgars]
@Company_ID int
as
Begin
	DECLARE     @NumberOfRows INT

	SELECT @NumberOfRows = count(Edgar.EdgarID)
	FROM Edgar, Company where Edgar.CompanyId=Company.CompanyID  and Edgar.CompanyID=@Company_ID;

	Select [Edgar].[EdgarID]
								  ,[Edgar].[Acc#]
								  ,[Edgar].[CIK#]
								  ,[Edgar].[FileName]
								  ,[Edgar].[FormType]
								  ,[Edgar].[DateFiled]
								  ,[Edgar].[DocumentType]
								  ,[Edgar].[EffectiveDate]
								  ,convert(varchar(5000),[Edgar].[Notes]) as Notes
								  ,[Edgar].[DateUpdated]
								  ,[Edgar].[Company]
								  ,[Edgar].[CompanyId]
								  ,[Edgar].[DocumentDate]
								  ,[Edgar].[UInfo]
								  ,[Edgar].[IsPDF]
								  ,[Edgar].[InitialPage]
								  ,Company.CompanyName
								  ,@NumberOfRows  as NumberOfRows 
								  ,MAX(convert(int, [EdgarFunds].[isUrlassigned])) as isUrlassigned,
								  TICKERcusip =MAX(LEN(ProsTicker.CUSIP)),
								   SB_Flag = MAX(LEN(SecurityBenefits.CUSIP)), 
								   AXA_Flag = MAX(LEN(AXAVIT.CUSIP)), 
								   GEN_Flag = MAX(LEN(GenworthSubAccounts.CUSIP)), 
								   TR_Flag = MAX(LEN(TRowePrice.CUSIP)),
								   SE2_Flag = MAX(LEN(Symetra.CUSIP)),
								   FFG_Flag = MAX(LEN(Forethought.CUSIP)),
								   NYL_Flag = MAX(LEN(NYLSubAccounts.CUSIP)),
						           OHN_Flag = MAX(LEN(OhioNational.CUSIP)),
						           MOA_Flag = MAX(LEN(MOACusips.CUSIP)),
						           DEL_Flag = MAX(LEN(DelawareLife.CUSIP)),  
						           AZL_Flag = MAX(LEN(Allianz.CUSIP)),  
						           InlineTRA_Flag = MAX(LEN(BCSTransamericaWatchListCUSIPs.CUSIP)),
						           InlineAB_Flag = MAX(LEN(BCSAllianceBernsteinWatchListCUSIPs.CUSIP))
				FROM Edgar INNER JOIN Company on Edgar.CompanyId=Company.CompanyID 
							INNER JOIN EdgarFunds on Edgar.EdgarID = EdgarFunds.EdgarID
							Left JOIN ProsTicker ON EdgarFunds.TickerID = ProsTicker.TickerID 
								LEFT OUTER JOIN  SecurityBenefits ON ProsTicker.CUSIP = SecurityBenefits.CUSIP
								LEFT OUTER JOIN  AXAVIT ON ProsTicker.CUSIP = AXAVIT.CUSIP
								LEFT OUTER JOIN  (SELECT distinct CUSIP FROM GenworthSubAccounts where GenworthSubAccounts.ProductCode != '0' or GenworthSubAccounts.ProductID != 0) as GenworthSubAccounts  ON ProsTicker.CUSIP = GenworthSubAccounts.CUSIP 
								LEFT OUTER JOIN  TRowePrice ON ProsTicker.CUSIP = TRowePrice.CUSIP
								LEFT OUTER JOIN  Symetra ON ProsTicker.CUSIP = Symetra.CUSIP 
								LEFT OUTER JOIN  Forethought ON ProsTicker.CUSIP = Forethought.CUSIP
								LEFT OUTER JOIN  NYLSubAccounts ON ProsTicker.CUSIP = NYLSubAccounts.CUSIP
								LEFT OUTER JOIN  OhioNational ON ProsTicker.CUSIP = OhioNational.CUSIP	
								LEFT OUTER JOIN  MOACusips on Prosticker.CUSIP = MOACusips.CUSIP 
								LEFT OUTER JOIN DelawareLife on ProsTicker.CUSIP = DelawareLife.CUSIP
								LEFT OUTER JOIN Allianz on ProsTicker.CUSIP = Allianz.CUSIP
								LEFT OUTER JOIN BCSTransamericaWatchListCUSIPs on ProsTicker.CUSIP = BCSTransamericaWatchListCUSIPs.CUSIP
								LEFT OUTER JOIN BCSAllianceBernsteinWatchListCUSIPs on ProsTicker.CUSIP = BCSAllianceBernsteinWatchListCUSIPs.CUSIP
				WHERE Edgar.CompanyID=@Company_ID		
					AND  datefiled >= (GETDATE()-548) --convert(datetime , '2010-05-05 00:00:00.000')--
				GROUP BY [Edgar].[EdgarID]
								  ,[Edgar].[Acc#]
								  ,[Edgar].[CIK#]
								  ,[Edgar].[FileName]
								  ,[Edgar].[FormType]
								  ,[Edgar].[DateFiled]
								  ,[Edgar].[DocumentType]
								  ,[Edgar].[EffectiveDate]
								  ,convert(varchar(5000),[Edgar].[Notes])
								  ,[Edgar].[DateUpdated]
								  ,[Edgar].[Company]
								  ,[Edgar].[CompanyId]
								  ,[Edgar].[DocumentDate]
								  ,[Edgar].[UInfo]
								  ,[Edgar].[IsPDF]
								  ,[Edgar].[InitialPage]
								  ,Company.CompanyName
				ORDER BY Edgar.edgarid desc
End