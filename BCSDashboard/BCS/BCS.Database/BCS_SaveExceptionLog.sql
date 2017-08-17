USE [db1029]
GO

/****** Object:  StoredProcedure [dbo].[BCS_SaveExceptionLog]    Script Date: 1/1/2014 13:44:08 ******/
IF  EXISTS (SELECT * FROM sys.objects WHERE object_id = OBJECT_ID(N'[dbo].[BCS_SaveExceptionLog]') AND type in (N'P', N'PC'))
DROP PROCEDURE [dbo].[BCS_SaveExceptionLog]
GO

CREATE PROCEDURE [dbo].[BCS_SaveExceptionLog]
@ExceptionMessage VARCHAR(MAX),
@ApplicationName VARCHAR(250),
@ExceptionID INT OUTPUT
AS
BEGIN
  
	INSERT INTO BCSExceptionLog(
							ExceptionMessage,
							ApplicationName,
							Audit_Ts
							)
				VALUES(						
						@ExceptionMessage,
						@ApplicationName,
						GETDATE()
						)
						
	SELECT @ExceptionID = @@IDENTITY
      
END
GO


