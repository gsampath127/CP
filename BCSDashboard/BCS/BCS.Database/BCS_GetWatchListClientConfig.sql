USE [db1029]
GO

/****** Object:  StoredProcedure [dbo].[BCS_GetWatchListClientConfig]    Script Date: 02/15/2016 17:46:28 ******/
SET ANSI_NULLS ON
GO

SET QUOTED_IDENTIFIER ON
GO




CREATE Procedure [dbo].[BCS_GetWatchListClientConfig]
as
Begin

	  SELECT * FROM BCSClientConfig
	  WHERE BCSClientConfig.IsCUSIPWatchListProvided = 1
	  AND ClientPrefix = 'AEG'
End


GO


