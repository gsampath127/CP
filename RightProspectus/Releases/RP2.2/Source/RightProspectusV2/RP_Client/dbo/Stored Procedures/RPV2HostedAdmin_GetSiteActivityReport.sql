﻿CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetSiteActivityReport]  
 @FromDate datetime,  
 @ToDate datetime  
AS  
BEGIN  
   SELECT DISTINCT
      SA.SiteActivityId,       
      SA.SiteId,    
      site.Name as SiteName,     
      SA.RequestBatchId,  
      UR.UriString,       
      SA.TaxonomyAssociationId,  
      TA.NameOverride,  
      SA.RequestUtcDate,  
      SA.ClientIPAddress,  
      SA.TaxonomyAssociationGroupId,  
      SA.DocumentTypeId,  
      DTA.HeaderText,  
      DTA.MarketId,  
      O.Click,  
      SA.InitDoc  
   FROM  
     
   (SELECT  
      SA.RequestBatchId,SA.DocumentTypeId,        
      Count(SA.RequestBatchId) as Click  
      FROM  
      SiteActivity SA  
      group by  
      SA.RequestBatchId,SA.DocumentTypeId  
    )O  
      INNER JOIN (
		SELECT * from(
			SELECT *, ROW_NUMBER() over(partition by RequestBatchId, DocumentTypeId order by RequestUtcDate) 'Rownum' FROM SiteActivity
		)t
		where t.Rownum = 1	  
	  )SA on SA.RequestBatchId= O.RequestBatchId AND SA.DocumentTypeId = o.DocumentTypeId
      INNER JOIN TaxonomyAssociation TA on SA.TaxonomyAssociationId=TA.TaxonomyAssociationId  
      INNER JOIN DocumentTypeAssociation DTA on SA.SiteId=DTA.SiteId AND SA.DocumentTypeId=DTA.DocumentTypeId
      INNER JOIN URI UR on SA.RequestURI=UR.UriId  
      INNER JOIN SITE site on SA.siteId=site.siteId  
      AND SA.SiteActivityId NOT IN(SELECT SiteActivityId FROM BADREQUEST)  
   WHERE   
    cast(RequestUtcDate as datetime) between @FromDate and @ToDate       
	order by SA.RequestBatchId, SA.RequestUtcDate
END