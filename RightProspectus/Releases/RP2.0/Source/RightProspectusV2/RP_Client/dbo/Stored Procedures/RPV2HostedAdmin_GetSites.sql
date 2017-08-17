
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetSites]
	@siteId INT=NULL,
	@name NVARCHAR(100)=NULL,
	@templateId INT=NULL,
	@defaultPageId INT=NULL,
	@parentSiteId INT=NULL,
	@description NVARCHAR(400)=NULL,
	@utcModifiedDate DATETIME,
	@modifiedBy INT,
	@pageIndex INT,
	@pageSize INT,
	@sortDirection NVARCHAR(10),
	@sortColumn NVARCHAR(100),
	@count int out

  -- exec [RPV2HostedAdmin__GetSites] null,null,null,null,null,null,'',0,1,10,'asc','Name',0
  -- DROP PROCEDURE [RPV2HostedAdmin__GetSites]
AS
BEGIN

  
				SELECT DISTINCT
					RowRank,
					 SiteId,
					 Name,
					 TemplateId,
					 DefaultPageId,
					 ParentSiteId,
					 Description,
					 UtcModifiedDate,
					 ModifiedBy					 
				FROM
				  (
				  SELECT
					 Site.SiteId,
					Site.Name,
					Site.TemplateId,
					Site.defaultPageId,
					Site.ParentSiteId,
					Site.[Description],
					Site.UtcModifiedDate,
					Site.ModifiedBy,
					  		 CASE  										   
								   WHEN @sortColumn = 'SiteId' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY Site.SiteId Asc) 									  
								   WHEN @sortColumn = 'SiteId' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY Site.SiteId Desc) 									  
								   WHEN @sortColumn = 'Name' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY  Site.Name Asc) 
								   WHEN @sortColumn = 'Name' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY  Site.Name Desc) 
								   WHEN @sortColumn = 'TemplateId' AND @sortDirection = 'Asc'  THEN ROW_NUMBER() OVER(ORDER BY Site.TemplateId Asc)
								   WHEN @sortColumn = 'TemplateId' AND @sortDirection = 'Desc'  THEN ROW_NUMBER() OVER(ORDER BY Site.TemplateId Desc)
								   WHEN @sortColumn = 'defaultPageId' AND @sortDirection = 'Asc'  THEN ROW_NUMBER() OVER(ORDER BY Site.defaultPageId Asc)
								   WHEN @sortColumn = 'defaultPageId' AND @sortDirection = 'Desc'  THEN ROW_NUMBER() OVER(ORDER BY Site.defaultPageId Desc)
							 End 										          
							   AS RowRank
				   FROM
					 Site     					
				   WHERE 
					 (Site.SiteId = @siteId OR @siteId IS NULL)
				   AND(Site.Name = @name OR @name IS NULL)	 
				   AND(Site.TemplateId = @templateId OR @templateId IS NULL)	 
				   AND(Site.DefaultPageId = @defaultPageId OR @defaultPageId IS NULL)	 
					) AS SiteTable     
					WHERE RowRank BETWEEN @pageIndex*@pageSize-@pageSize+1  AND @pageSize*@pageIndex
					ORDER BY RowRank
					
					SELECT @count=@@ROWCOUNT
					
					

		

END
