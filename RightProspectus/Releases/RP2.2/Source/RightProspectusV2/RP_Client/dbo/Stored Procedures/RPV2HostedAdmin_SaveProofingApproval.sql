CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveProofingApproval]
@modifiedBy int
AS
BEGIN

	--Approve Site Text changes

	DECLARE @SiteTextProofingVersion TABLE
	( 
		 SiteTextId int,
		 ProofingVersion int
	)
	INSERT INTO @SiteTextProofingVersion
	SELECT SiteTextId, MAX([Version]) AS ProofingVersion
	FROM SiteTextVersion 
	GROUP BY SiteTextId

	UPDATE SiteText 
	SET CurrentVersion = ProofingVersion, ModifiedBy = @modifiedBy, UtcModifiedDate = GETUTCDATE()
	FROM SiteText ST
	INNER JOIN	@SiteTextProofingVersion versions ON ST.SiteTextId = versions.SiteTextId

	--Approve Page Text changes

	DECLARE @PageTextProofingVersion TABLE
	( 
		 PageTextId int,
		 ProofingVersion int
	)
	INSERT INTO @PageTextProofingVersion
	SELECT PageTextId, MAX([Version]) AS ProofingVersion
	FROM PageTextVersion  
	GROUP BY PageTextId

	UPDATE PageText 
	SET CurrentVersion = ProofingVersion, ModifiedBy = @modifiedBy, UtcModifiedDate = GETUTCDATE()
	FROM PageText PT
	INNER JOIN	@PageTextProofingVersion versions ON PT.PageTextId = versions.PageTextId


	--Approve Site Navigation changes

	DECLARE @SiteNavigationProofingVersion TABLE
	( 
		 SiteNavigationId int,
		 ProofingVersion int
	)
	INSERT INTO @SiteNavigationProofingVersion
	SELECT SiteNavigationId, MAX([Version]) AS ProofingVersion
	FROM SiteNavigationVersion  
	GROUP BY SiteNavigationId

	UPDATE SiteNavigation 
	SET CurrentVersion = ProofingVersion, ModifiedBy = @modifiedBy, UtcModifiedDate = GETUTCDATE()
	FROM SiteNavigation SN
	INNER JOIN	@SiteNavigationProofingVersion versions ON SN.SiteNavigationId = versions.SiteNavigationId


	--Approve Page Navigation changes

	DECLARE @PageNavigationProofingVersion TABLE
	( 
		 PageNavigationId int,
		 ProofingVersion int
	)
	INSERT INTO @PageNavigationProofingVersion
	SELECT PageNavigationId, MAX([Version]) AS ProofingVersion
	FROM PageNavigationVersion  
	GROUP BY PageNavigationId

	UPDATE PageNavigation 
	SET CurrentVersion = ProofingVersion, ModifiedBy = @modifiedBy, UtcModifiedDate = GETUTCDATE()
	FROM PageNavigation PN
	INNER JOIN	@PageNavigationProofingVersion versions ON PN.PageNavigationId = versions.PageNavigationId


END
GO