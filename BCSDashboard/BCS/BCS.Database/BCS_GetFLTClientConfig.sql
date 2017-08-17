
USE [db1029]
GO

/****** Object:  StoredProcedure [dbo].[BCS_GetFLTClientConfig]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_GetFLTClientConfig]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_GetFLTClientConfig]
GO


Create Procedure BCS_GetFLTClientConfig
as
Begin

	  SELECT * FROM BCSClientConfig
	  WHERE IsFLTRequired = 1
End
GO