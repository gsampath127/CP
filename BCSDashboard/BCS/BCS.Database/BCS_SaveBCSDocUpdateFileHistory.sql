CREATE PROCEDURE [dbo].BCS_SaveBCSDocUpdateFileHistory
@ClientId int
AS
BEGIN
    
    INSERT INTO BCSDocUpdateFileHistory(BCSClientConfigID, DateCreated)
		Values(@ClientId, GetDate())
			  
End
GO