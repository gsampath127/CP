CREATE TABLE [dbo].[Company] (
    [CompanyID]    INT            IDENTITY (1, 1) NOT FOR REPLICATION NOT NULL,
    [CompanyName]  NVARCHAR (255) NOT NULL,
    [CompanyURL]   NVARCHAR (255) NULL,
    [Image]        NVARCHAR (50)  NULL,
    [AlertMessage] TEXT           NULL,
    [Online]       CHAR (1)       NULL,
    [CompLevel]    INT            NULL,
    CONSTRAINT [PK_Company] PRIMARY KEY CLUSTERED ([CompanyID] ASC)
);


GO
CREATE TRIGGER CompanyDeletion
       ON dbo.Company
       FOR DELETE
    AS
       IF NOT EXISTS
          (
           SELECT CompanyID FROM CompanyHistory WHERE CompanyID in (SELECT CompanyID
           FROM Deleted)
          )
          BEGIN
			   insert into CompanyHistory (  CompanyID, CompanyName, CompanyURL, [Image], [Online], CompLevel,DeletionDate)
				select   CompanyID, CompanyName, CompanyURL, [Image], [Online], CompLevel, Getdate() 
					from Deleted 
          END