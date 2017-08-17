
USE [db1029]
GO

/****** Object:  StoredProcedure [dbo].[BCS_GetALLClientConfig]    Script Date: 11/14/2013 16:55:41 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_GetALLClientConfig]') AND TYPE in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_GetALLClientConfig]
GO

CREATE PROCEDURE [dbo].[BCS_GetALLClientConfig]
AS
BEGIN

	  SELECT * FROM BCSClientConfig
	  	  
END
GO