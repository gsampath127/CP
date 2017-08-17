
/*
	Procedure Name:[dbo].[RPV2HostedAdmin_GetUsers]
	Added By: Ashok
	Date: 09/05/2015
	Reason : To List all the Users with paging
*/
CREATE PROCEDURE [dbo].[RPV2HostedAdmin_GetUsers]
	@userId INT=NULL,
	@fName NVARCHAR(100)=NULL,
	@lName NVARCHAR(100)=NULL,
	@eMail NVARCHAR (256)=NULL,
	@pageSize int,
	@pageIndex int,
	@sortDirection NVARCHAR(10),
	@sortColumn NVARCHAR(100),
	@count int out

	
	--exec [RPV2SystemHostedAdmin__GetUsers] null,NULL,NULL,NULL,10,1,'ASC','UserId',0	
	--DROP PROCEDURE [RPV2SystemHostedAdmin__GetUsers]
AS
BEGIN
	
			SELECT DISTINCT
					    RowRank,
					 	UserId,
					 	FirstName,
					 	LastName,
					 	Email
					 FROM 
					  (
				  SELECT
					Users.UserId,
					Users.FirstName,
					Users.LastName,
					Users.Email,
					 CASE  
							WHEN @sortColumn = 'UserId' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY Users.UserId Asc) 									  
							WHEN @sortColumn = 'UserId' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY Users.UserId Desc) 									  
							WHEN @sortColumn = 'FirstName' AND @sortDirection = 'Asc' THEN ROW_NUMBER() OVER(ORDER BY  Users.FirstName Asc) 
							WHEN @sortColumn = 'FirstName' AND @sortDirection = 'Desc' THEN ROW_NUMBER() OVER(ORDER BY  Users.FirstName Desc) 
							WHEN @sortColumn = 'LastName' AND @sortDirection = 'Asc'  THEN ROW_NUMBER() OVER(ORDER BY Users.LastName Asc)
							WHEN @sortColumn = 'LastName' AND @sortDirection = 'Desc'  THEN ROW_NUMBER() OVER(ORDER BY Users.LastName Desc)
							WHEN @sortColumn = 'Email' AND @sortDirection = 'Asc'  THEN ROW_NUMBER() OVER(ORDER BY Users.Email Asc)
							WHEN @sortColumn = 'Email' AND @sortDirection = 'Desc'  THEN ROW_NUMBER() OVER(ORDER BY Users.Email Desc)
					 	 End 										          
							   AS RowRank
					FROM
					 Users     					
				   WHERE 
					  (Users.UserId = @userId OR @userId IS NULL)
				   AND(Users.FirstName = @fName OR @fName IS NULL)	 
				   AND(Users.LastName = @lName OR @lName IS NULL)	 
				   AND(Users.Email = @eMail OR @eMail IS NULL)	 
					) AS UsersTable     
					WHERE RowRank BETWEEN @pageIndex*@pageSize-@pageSize+1  AND @pageSize*@pageIndex
					ORDER BY RowRank
					
					SELECT @count=@@ROWCOUNT
END
