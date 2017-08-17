CREATE PROCEDURE [dbo].[BCS_GetCompanyDetails]
AS
BEGIN
  
   SELECT DISTINCT CompanyId , CompanyName FROM Company

   SELECT DISTINCT SecurityTypeID ,SecurityTypeCode FROM SecurityType 

END
GO