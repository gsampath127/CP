USE [db1029]
GO
/****** Object:  StoredProcedure [dbo].[BCS_GetBCSTRPFLTFileInfo]    Script Date: 3/18/2016 4:36:25 PM ******/
SET ANSI_NULLS ON
GO
SET QUOTED_IDENTIFIER ON
GO


ALTER PROCEDURE [dbo].[BCS_GetBCSTRPFLTFileInfo]
@FLTDate DateTime
AS
BEGIN
SELECT * FROM [dbo].[BCSTRPFLTFileInfo]
where DateReceived = @FLTDate
END







--