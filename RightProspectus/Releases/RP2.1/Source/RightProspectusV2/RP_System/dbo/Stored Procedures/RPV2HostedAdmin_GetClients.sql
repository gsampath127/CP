
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_GetClients]
	Added By: Ashok
	Date: 09/05/2015
	Reason : To List all the Clients for Paging
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetClients]
	@clientId INT=NULL,
	@clientName NVARCHAR(400)=NULL,
	@connectionStringName VARCHAR(200)=NULL,
	@databaseName VARCHAR (200)=NULL,
	@verticalMarketId int=NULL,
	@clientDescription NVARCHAR(800)=NULL,
	@utcModifiedDate DATETIME,
	@isActive BIT,
	@modifiedBy INT,
	@pageIndex int,
	@pageSize int,
	@sortDirection NVARCHAR(10),
	@sortColumn NVARCHAR(100),
	@count int out

	
	--exec [RPV2SystemHostedAdmin_GetClients] null,null,null,null,null,null,'',null,0,1,10,'desc','databaseName',0	
	--DROP PROCEDURE [RPV2System_GetClients]
AS
BEGIN
	
			SELECT DISTINCT
						RowRank,
						ClientId,
						ClientName,
						ConnectionStringName,
						DatabaseName,
						VerticalMarketId,
						ClientDescription,
						UtcModifiedDate,
					    ModifiedBy
								
			FROM
						(
					 SELECT 
						Clients.ClientId,
					 	Clients.ClientName,
						Clients.ConnectionStringName,
						Clients.DatabaseName,
						Clients.VerticalMarketId,
						Clients.ClientDescription,	
						Clients.UtcModifiedDate,
					    Clients.ModifiedBy,
							CASE  										   
								   WHEN @sortColumn = 'ClientId' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY Clients.ClientId Asc) 									  
								   WHEN @sortColumn = 'ClientId' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY Clients.ClientId Desc) 									  
								   WHEN @sortColumn = 'ClientName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY  Clients.ClientName Asc) 
								   WHEN @sortColumn = 'ClientName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY  Clients.ClientName Desc) 
								   WHEN @sortColumn = 'DatabaseName' AND @sortDirection = 'Asc'  THEN ROW_NUMBER() OVER(ORDER BY Clients.DatabaseName Asc)
								   WHEN @sortColumn = 'DatabaseName' AND @sortDirection = 'Desc'  THEN ROW_NUMBER() OVER(ORDER BY Clients.DatabaseName Desc)
								   WHEN @sortColumn = 'VerticalMarketId' AND @sortDirection = 'Asc'  THEN ROW_NUMBER() OVER(ORDER BY Clients.VerticalMarketId Asc)
								   WHEN @sortColumn = 'VerticalMarketId' AND @sortDirection = 'Desc'  THEN ROW_NUMBER() OVER(ORDER BY Clients.VerticalMarketId Desc)
							 End 										          
							   AS RowRank
					 FROM
					  Clients				 
					 WHERE
					 	 (Clients.ClientId = @clientId OR @clientId IS NULL)
				   AND(Clients.ClientName = @clientName OR @clientName IS NULL)	 
				   AND(Clients.DatabaseName = @databaseName OR @databaseName IS NULL)	 
				   AND(Clients.VerticalMarketId = @verticalMarketId OR @verticalMarketId IS NULL)	 
					) AS ClientsTable     
					WHERE RowRank BETWEEN @pageIndex*@pageSize-@pageSize+1  AND @pageSize*@pageIndex
					ORDER BY RowRank
					
					SELECT @count=@@ROWCOUNT
	END
					   
		