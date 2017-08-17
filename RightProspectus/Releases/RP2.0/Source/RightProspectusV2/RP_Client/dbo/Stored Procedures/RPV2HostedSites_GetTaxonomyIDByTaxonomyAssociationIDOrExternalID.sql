﻿CREATE PROCEDURE dbo.RPV2HostedSites_GetTaxonomyIDByTaxonomyAssociationIDOrExternalID
@Level int=NULL,
@ExternalId nvarchar(100)=NULL,
@TAID INT=NULL
AS
BEGIN
  	 
  	 IF @ExternalId IS NOT NULL
  		 BEGIN
	  	   SELECT TOP 1 TA.TaxonomyID,TA.NameOverride
	  	     FROM TaxonomyLevelExternalId TLE
	  	     INNER JOIN TaxonomyAssociation TA ON TLE.TaxonomyId = TA.TaxonomyId AND TLE.[Level] = TA.[Level] 
	  	   WHERE TA.[Level] = @Level
	  	     AND ExternalId = @ExternalId
  		 END
	 ELSE
	    IF @TAID IS NOT NULL
	     BEGIN
	       SELECT TOP 1 TaxonomyID,NameOverride 
	  	     FROM TaxonomyAssociation
	  	   WHERE TaxonomyAssociationId = @TAID
	     
	     END  	
  	
END