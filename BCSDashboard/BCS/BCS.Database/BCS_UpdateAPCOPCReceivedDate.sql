USE [db1029]
GO

/****** Object:  StoredProcedure [dbo].[BCS_UpdateAPCOPCReceivedDate]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_UpdateAPCOPCReceivedDate]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_UpdateAPCOPCReceivedDate]
GO

CREATE PROCEDURE [dbo].[BCS_UpdateAPCOPCReceivedDate]
@EdgarID INT,
@PDFName nvarchar(110),
@IsAPF bit,
@TableName NVARCHAR(100)
AS
BEGIN
    
    IF @IsAPF = 1
    BEGIN
			IF @TableName = 'BCSDocUpdate'
			BEGIN

				UPDATE BCSDocUpdateGIMSlink
				SET IsAPC = 1, APCReceivedDate = GETDATE()
				FROM BCSDocUpdateGIMSlink
				INNER JOIN BCSDocUpdate ON BCSDocUpdate.BCSDocUpdateID =  BCSDocUpdateGIMSlink.DocUpdateID
				WHERE EdgarId = @EdgarID and PDFName = @PDFName

			END
			ELSE IF @TableName = 'BCSDocUpdateSupplements'
			BEGIN	
					  
				UPDATE BCSDocUpdateSupplementsSlink
				SET IsAPC = 1, APCReceivedDate = GETDATE()
				FROM BCSDocUpdateSupplementsSlink
				INNER JOIN BCSDocUpdateSupplements ON BCSDocUpdateSupplements.BCSDocUpdateSupplementsSlinkID =  BCSDocUpdateSupplementsSlink.BCSDocUpdateSupplementsSlinkID
				WHERE EdgarId = @EdgarID and PDFName = @PDFName

			END
			ELSE IF @TableName = 'BCSDocUpdateARSAR'
			BEGIN

				UPDATE BCSDocUpdateARSARSlink
				SET IsAPC = 1, APCReceivedDate = GETDATE()
				FROM BCSDocUpdateARSARSlink
				INNER JOIN BCSDocUpdateARSAR ON BCSDocUpdateARSAR.BCSDocUpdateARSARSlinkID =  BCSDocUpdateARSARSlink.BCSDocUpdateARSARSlinkID
				WHERE EdgarId = @EdgarID and PDFName = @PDFName

			END
    END
    ELSE
    BEGIN
			
			IF @TableName = 'BCSDocUpdate'
			BEGIN

				UPDATE BCSDocUpdateGIMSlink
				SET IsOPC = 1, OPCReceivedDate = GETDATE()
				FROM BCSDocUpdateGIMSlink
				INNER JOIN BCSDocUpdate ON BCSDocUpdate.BCSDocUpdateID =  BCSDocUpdateGIMSlink.DocUpdateID
				WHERE EdgarId = @EdgarID and PDFName = @PDFName

			END
			ELSE IF @TableName = 'BCSDocUpdateSupplements'
			BEGIN			  

				UPDATE BCSDocUpdateSupplementsSlink
				SET IsOPC = 1, OPCReceivedDate = GETDATE()
				FROM BCSDocUpdateSupplementsSlink
				INNER JOIN BCSDocUpdateSupplements ON BCSDocUpdateSupplements.BCSDocUpdateSupplementsSlinkID =  BCSDocUpdateSupplementsSlink.BCSDocUpdateSupplementsSlinkID
				WHERE EdgarId = @EdgarID and PDFName = @PDFName
			
			END
			ELSE IF @TableName = 'BCSDocUpdateARSAR'
			BEGIN

				UPDATE BCSDocUpdateARSARSlink
				SET IsOPC = 1, OPCReceivedDate = GETDATE()
				FROM BCSDocUpdateARSARSlink
				INNER JOIN BCSDocUpdateARSAR ON BCSDocUpdateARSAR.BCSDocUpdateARSARSlinkID =  BCSDocUpdateARSARSlink.BCSDocUpdateARSARSlinkID
				WHERE EdgarId = @EdgarID and PDFName = @PDFName	
						   
			END
    END
			  
End
GO