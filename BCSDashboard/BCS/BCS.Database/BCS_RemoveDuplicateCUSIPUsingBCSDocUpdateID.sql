USE [db1029]
GO

/****** Object:  StoredProcedure [dbo].[BCS_RemoveDuplicateCUSIPUsingBCSDocUpdateID]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_RemoveDuplicateCUSIPUsingBCSDocUpdateID]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_RemoveDuplicateCUSIPUsingBCSDocUpdateID]
GO

CREATE PROCEDURE [dbo].[BCS_RemoveDuplicateCUSIPUsingBCSDocUpdateID]
@BCSDocUpdateId INT
AS
BEGIN

	UPDATE BCSDocUpdate
	SET IsRemoved = 1
	WHERE BCSDocUpdateId = @BCSDocUpdateId

End
GO

