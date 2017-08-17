/*
	Procedure Name:[dbo].[RPV2HostedAdmin_SaveVerticalDataFromImportXmlData]
	Added By: Noel Dsouza
	Start Date: 21st Oct 2015	
	Final Updates : 21st Oct 2015
	Reason : To Save Vertical Data From ImportXml Data
	
	Details of the SP. Please do not change the sequence of insert,updates and deletes.
	
		-- 1. Temporary Table for TaxonomyAssociationIds that will be deleted and so child can be deleted

	    -- 2. Temporary Table Declaration for updating TaxonomyAssociationIDs in TaxonomyAssociationHierarchy

		-- 3. TaxonomyAssociation UPDATE Records existing records

		-- 4. TaxonomyAssociation INSERT New Records

		-- 5. INSERT New Records in the table TaxonomyLevelExternalId for TaxonomyAssociation marketids

		-- 6. Update DocumentTypeAssociation ------------

		-- 7. Insert New DocumentTypeAssociation ------------

		-- 8. Insert into DocumentTypeExternalID table For DocumentTypeAssociation MarketIds ----------------

		-- 9. Insert New Footnotes  

		-- 10. TaxonomyAssociationHierarchy INSERT  

		-- 11. TaxonomyAssociationHierarchy delete 

		-- 12. TaxonomyAssociation Footnotes Delete 

		-- 13. DocumentTypeAssociation Delete 

		-- 14. DocumentTypeExternalId Delete where documenttypeid does not exists in DocumentTypeAssociaiton Table.
				delete only when Restored from Backup

		-- 15. TaxonomyAssociation DELETE

		-- 16. TaxonomyLevelExternalId Delete Where TaxonomyID and Level have been deleted from TaxonomyAssociation. 
				Delete only when restored from backup XML
*/

CREATE PROCEDURE [dbo].[RPV2HostedAdmin_SaveVerticalDataFromImportXmlData]	
@TT_DocumentTypeAssociation TT_DocumentTypeAssociation readonly,
@TT_TaxonomyAssociation TT_TaxonomyAssociation readonly,
@TT_TaxonomyAssociationFootnotes TT_TaxonomyAssociationFootnotes readonly,
@TT_TaxonomyAssociationHierarchy TT_TaxonomyAssociationHierarchy readonly,
@ImportedBy int,
@IsBackup bit
AS
Begin

  DECLARE @ModifiedDate datetime
  SET @ModifiedDate = GETUTCDATE()

  DECLARE @SiteID int

  SELECT top 1 @SiteID = SiteID FROM 
  @TT_TaxonomyAssociation 
  WHERE SiteID is not null

  
  -- 1. Temporary Table for TaxonomyAssociationIds that will be deleted and so child can be deleted
  
  DECLARE @TempDeleteTaxonomyAssociationIds Table
  (
	TaxonomyAssociationId int,
	MarketId nvarchar(50),
	[Level] int,
	TaxonomyID int
  )

  -- 2. Temporary Table Declaration for updating TaxonomyAssociationIDs in TaxonomyAssociationHierarchy
  DECLARE @TempTaxonomyAssociationHierarchy Table
  (
    ParentTaxonomyAssociationId int,
    ParentTaxonomyLevel int,
    ParentTaxonomyMarketID nvarchar(50),    
    RelationshipType int,
    childimportid nvarchar(60),
    ChildTaxonomyAssociationId int,
    deleteparent bit,
    Deletechild bit     
  )
    INSERT INTO @TempTaxonomyAssociationHierarchy(ParentTaxonomyLevel,ParentTaxonomyMarketID,RelationshipType,childimportid,deleteparent,Deletechild)
		SELECT parentlevel,parenttaxonomymarketId,relationshipType,childimportid,deleteparent,deletechild
		FROM @TT_TaxonomyAssociationHierarchy
		--WHERE deleteparent = 0 AND Deletechild = 0
  
  --3. TaxonomyAssociation UPDATE Records existing records
  UPDATE TaxonomyAssociation
    SET NameOverride = TTTA.nameOverride, DescriptionOverride = TTTA.descriptionOverride, CssClass = TTTA.cssClass,
		TaxonomyId = TTTA.taxonomyId, UtcModifiedDate = @ModifiedDate, ModifiedBy = @ImportedBy
   FROM TaxonomyAssociation TA
    INNER JOIN @TT_TaxonomyAssociation TTTA 
    	ON TA.MarketId = TTTA.marketId 	AND TA.[Level] = TTTA.[Level] AND isnull(TA.SiteId,0) = isnull(TTTA.siteid,0)
   WHERE TTTA.taxonomyId IS NOT NULL AND TTTA.[delete] =0  

  -- 4. TaxonomyAssociation INSERT New Records
  INSERT INTO TaxonomyAssociation([Level],TaxonomyId,SiteId,NameOverride,DescriptionOverride,CssClass,MarketId,UtcModifiedDate,ModifiedBy)				
  SELECT DISTINCT TTTA.[Level],TTTA.taxonomyId,TTTA.siteid,TTTA.nameOverride,TTTA.descriptionOverride,TTTA.cssClass,TTTA.marketId,@ModifiedDate,@ImportedBy
		FROM @TT_TaxonomyAssociation TTTA
  LEFT OUTER JOIN TaxonomyAssociation TA 
		ON TTTA.[Level] = TA.[Level] AND TTTA.marketId = TA.MarketId AND isnull(TA.SiteId,0) = isnull(TTTA.siteid,0)
  WHERE TTTA.taxonomyId IS NOT NULL AND TTTA.[delete] =0 AND TA.TaxonomyAssociationId is null

  
  -- 5. INSERT New Records in the table TaxonomyLevelExternalId for TaxonomyAssociation marketids
  INSERT INTO TaxonomyLevelExternalId(TaxonomyId,[Level],ExternalId,UtcModifiedDate,ModifiedBy)
   SELECT DISTINCT TTTA.TaxonomyId,TTTA.[Level],marketId,GETUTCDATE(),@ImportedBy FROM @TT_TaxonomyAssociation TTTA
	LEFT OUTER JOIN TaxonomyLevelExternalId TLE ON TLE.TaxonomyId = TTTA.taxonomyId AND TLE.[Level] = TTTA.[Level] 
		AND TLE.ExternalId = TTTA.marketId
	WHERE TLE.TaxonomyId IS NULL AND TTTA.taxonomyId IS NOT NULL AND TTTA.[delete]=0
  
  -- 6. Update DocumentTypeAssociation ------------
    UPDATE DocumentTypeAssociation -- Update SiteId Level DTA
    SET HeaderText = TTDTA.headerText , LinkText = TTDTA.linkText, 
			[Order] = TTDTA.[Order], DescriptionOverride = TTDTA.descriptionOverride,
			CssClass  = TTDTA.cssClass,DocumentTypeId = TTDTA.documenttypeid,
			UtcModifiedDate = @ModifiedDate, ModifiedBy = @ImportedBy
   FROM DocumentTypeAssociation DTA
   	INNER JOIN [Site] ON DTA.SiteId = [Site].SiteId
    INNER JOIN @TT_DocumentTypeAssociation TTDTA 
		  ON DTA.MarketId = TTDTA.marketId  AND TTDTA.siteid IS NOT NULL
		  AND DTA.SiteId IS NOT NULL AND TTDTA.siteid = DTA.SiteId
   WHERE TTDTA.documenttypeid IS NOT NULL AND TTDTA.[delete]=0
		  
    UPDATE DocumentTypeAssociation -- Update Taxonomy Level DTA And SiteID at Taxonomy Level is not null
    SET HeaderText = TTDTA.headerText , LinkText = TTDTA.linkText, 
			[Order] = TTDTA.[Order], DescriptionOverride = TTDTA.descriptionOverride,
			CssClass  = TTDTA.cssClass,DocumentTypeId = TTDTA.documenttypeid,
			UtcModifiedDate = @ModifiedDate, ModifiedBy = @ImportedBy
   FROM DocumentTypeAssociation DTA
    INNER JOIN @TT_DocumentTypeAssociation TTDTA 
		  ON DTA.MarketId = TTDTA.marketId  
    INNER JOIN TaxonomyAssociation TA ON TTDTA.taxonomymarketId = TA.MarketId 
				AND TTDTA.[taxonomylevel] = TA.[Level]
				AND DTA.TaxonomyAssociationId = TA.TaxonomyAssociationId
	WHERE TTDTA.siteid IS NULL AND DTA.SiteId IS NULL AND  TA.SiteId = @SiteID
		AND TTDTA.documenttypeid IS NOT NULL AND TTDTA.[delete]=0


    UPDATE DocumentTypeAssociation -- Update Taxonomy Level DTA And SiteID at Taxonomy Level is null
    SET HeaderText = TTDTA.headerText , LinkText = TTDTA.linkText, 
			[Order] = TTDTA.[Order], DescriptionOverride = TTDTA.descriptionOverride,
			CssClass  = TTDTA.cssClass,DocumentTypeId = TTDTA.documenttypeid,
			UtcModifiedDate = @ModifiedDate, ModifiedBy = @ImportedBy
   FROM DocumentTypeAssociation DTA
    INNER JOIN @TT_DocumentTypeAssociation TTDTA 
		  ON DTA.MarketId = TTDTA.marketId  
    INNER JOIN TaxonomyAssociation TA ON TTDTA.taxonomymarketId = TA.MarketId 
				AND TTDTA.[taxonomylevel] = TA.[Level]
				AND DTA.TaxonomyAssociationId = TA.TaxonomyAssociationId
	WHERE TTDTA.siteid IS NULL AND DTA.SiteId IS NULL AND  TA.SiteId IS NULL   
		AND TTDTA.documenttypeid IS NOT NULL AND TTDTA.[delete]=0
   	
    	
  -- 7. Insert New DocumentTypeAssociation ------------
  
					-- INSERT DocumentTypes only at Site level
  INSERT INTO DocumentTypeAssociation(HeaderText, LinkText, [Order], DescriptionOverride, CssClass, SiteId, 
									  DocumentTypeId, MarketId, UtcModifiedDate, ModifiedBy
			)
  SELECT DISTINCT TTDTA.headerText, TTDTA.linkText, TTDTA.[Order], TTDTA.descriptionOverride, TTDTA.cssClass,
				  TTDTA.siteid, TTDTA.documenttypeid, TTDTA.MarketId, @ModifiedDate, @ImportedBy
  FROM @TT_DocumentTypeAssociation TTDTA
    LEFT OUTER JOIN DocumentTypeAssociation DTA ON TTDTA.siteid = DTA.SiteId AND TTDTA.documenttypeid = DTA.DocumentTypeId AND DTA.SiteId = @SiteID
  WHERE TTDTA.siteid IS NOT NULL  AND TTDTA.taxonomylevel IS NULL
	  AND TTDTA.documenttypeid IS NOT NULL AND DTA.DocumentTypeAssociationId IS NULL  AND TTDTA.[delete]=0
	  
					-- INSERT DocumentTypes at Taxonomy level and MarketId	and SiteID under TaxonomyID				
  INSERT INTO DocumentTypeAssociation(HeaderText, LinkText, [Order], DescriptionOverride, CssClass, TaxonomyAssociationId, DocumentTypeId,
									  MarketId, UtcModifiedDate,  ModifiedBy
			)
  SELECT DISTINCT TTDTA.headerText, TTDTA.linkText,TTDTA.[Order],TTDTA.descriptionOverride,TTDTA.cssClass,TA.TaxonomyAssociationId,TTDTA.documenttypeid,
				  TTDTA.MarketId,@ModifiedDate,@ImportedBy		 
  FROM @TT_DocumentTypeAssociation TTDTA
	INNER JOIN TaxonomyAssociation TA ON TTDTA.taxonomymarketId = TA.MarketId 
			AND TTDTA.[taxonomylevel] = TA.[Level] 
    LEFT OUTER JOIN DocumentTypeAssociation DTA ON TA.TaxonomyAssociationId = DTA.TaxonomyAssociationId 
			AND TTDTA.documenttypeid = DTA.DocumentTypeId
  WHERE TTDTA.siteid IS NULL AND TTDTA.taxonomylevel IS NOT NULL
	  AND TTDTA.documenttypeid IS NOT NULL AND DTA.DocumentTypeAssociationId IS NULL AND TTDTA.[delete]=0
	  AND TA.[SiteId] = @SiteID

	     -- INSERT DocumentTypes at Taxonomy level and MarketId	and SiteID is null under TaxonomyID
	    INSERT INTO DocumentTypeAssociation(HeaderText, LinkText, [Order], DescriptionOverride, CssClass, TaxonomyAssociationId, DocumentTypeId,
									  MarketId, UtcModifiedDate,  ModifiedBy
			)
  SELECT DISTINCT TTDTA.headerText, TTDTA.linkText,TTDTA.[Order],TTDTA.descriptionOverride,TTDTA.cssClass,TA.TaxonomyAssociationId,TTDTA.documenttypeid,
				  TTDTA.MarketId,@ModifiedDate,@ImportedBy		 
  FROM @TT_DocumentTypeAssociation TTDTA
	INNER JOIN TaxonomyAssociation TA ON TTDTA.taxonomymarketId = TA.MarketId 
			AND TTDTA.[taxonomylevel] = TA.[Level]
    LEFT OUTER JOIN DocumentTypeAssociation DTA ON TA.TaxonomyAssociationId = DTA.TaxonomyAssociationId 
			AND TTDTA.documenttypeid = DTA.DocumentTypeId
  WHERE TTDTA.siteid IS NULL AND TTDTA.taxonomylevel IS NOT NULL
	  AND TTDTA.documenttypeid IS NOT NULL AND DTA.DocumentTypeAssociationId IS NULL AND TTDTA.[delete]=0
	  AND TA.[SiteId] is null
	  
  
	  
  --8. Insert into DocumentTypeExternalID table For DocumentTypeAssociation MarketIds ----------------
  INSERT INTO DocumentTypeExternalId(DocumentTypeId,ExternalId,ModifiedBy,UtcModifiedDate)
  SELECT DISTINCT TTDTA.DocumentTypeId,TTDTA.MarketId,@ImportedBy,@ModifiedDate 
  FROM @TT_DocumentTypeAssociation TTDTA
  LEFT OUTER JOIN DocumentTypeExternalId DTE ON TTDTA.documenttypeid = DTE.DocumentTypeId
		AND TTDTA.MarketId = DTE.ExternalId		 
  WHERE TTDTA.documenttypeid IS NOT NULL AND DTE.DocumentTypeId IS NULL AND TTDTA.[delete] =0
  
  
  --9. Insert New Footnotes  
  INSERT INTO Footnote(TaxonomyAssociationId, LanguageCulture,	[Text],	UtcModifiedDate, ModifiedBy)
  SELECT DISTINCT TA.TaxonomyAssociationId,  TTFN.languageCulture,  TTFN.[text],  @ModifiedDate, @ImportedBy
	FROM TaxonomyAssociation TA
	INNER JOIN @TT_TaxonomyAssociationFootnotes  TTFN ON TA.MarketId = TTFN.taxonomymarketId
				AND TA.[Level] = TTFN.[level] 
	LEFT OUTER JOIN Footnote FN ON TA.TaxonomyAssociationID = FN.TaxonomyAssociationID
					AND TTFN.[text] = FN.[Text]
	WHERE FN.FootnoteId IS NULL AND TTFN.[delete]=0
	
 --10. TaxonomyAssociationHierarchy INSERT  
  UPDATE @TempTaxonomyAssociationHierarchy
    SET ParentTaxonomyAssociationId = TA.TaxonomyAssociationId
    FROM @TempTaxonomyAssociationHierarchy TTAH
    INNER JOIN TaxonomyAssociation TA ON TTAH.ParentTaxonomyLevel = TA.[Level]
				AND TTAH.ParentTaxonomyMarketID = TA.MarketId	
    WHERE TA.SiteId = @SiteID


	UPDATE @TempTaxonomyAssociationHierarchy
    SET ChildTaxonomyAssociationId = TA.TaxonomyAssociationId
    FROM @TempTaxonomyAssociationHierarchy TTAH
    INNER JOIN @TT_TaxonomyAssociation  TTTA ON TTAH.childimportid = TTTA.importId 
					AND ISNULL(TTAH.childimportid,'') != '' 
					AND ISNULL(TTTA.importId,'') != '' 
    INNER JOIN TaxonomyAssociation TA ON TTTA.[Level] = TA.[Level]
				AND TTTA.marketId = TA.MarketId  
	WHERE TA.SiteId IS NULL AND  TTTA.SiteID IS NULL 
				
  INSERT INTO TaxonomyAssociationHierachy(ParentTaxonomyAssociationId,ChildTaxonomyAssociationId,RelationshipType,
			UtcModifiedDate,ModifiedBy)
  SELECT DISTINCT TTAH.ParentTaxonomyAssociationId,TTAH.ChildTaxonomyAssociationId,TTAH.RelationshipType,
			@ModifiedDate,@ImportedBy
	 FROM @TempTaxonomyAssociationHierarchy TTAH 
	 LEFT OUTER JOIN TaxonomyAssociationHierachy TAH ON TTAH.ParentTaxonomyAssociationId = TAH.ParentTaxonomyAssociationId
				AND TTAH.ChildTaxonomyAssociationId = TAH.ChildTaxonomyAssociationId
				AND TTAH.RelationshipType = TAH.RelationshipType
	WHERE TAH.ParentTaxonomyAssociationId is null
	AND TTAH.ParentTaxonomyAssociationId is not null 
	AND TTAH.ChildTaxonomyAssociationId is not null
	AND TTAH.deleteparent = 0 and TTAH.Deletechild = 0
	
  
  	
  --11. TaxonomyAssociationHierarchy delete 
  
  INSERT INTO @TempDeleteTaxonomyAssociationIds(TaxonomyAssociationId,MarketId,[Level],TaxonomyID) -- Add TA marked to be deleted in XML
  SELECT DISTINCT TA.TaxonomyAssociationId,TTTA.MarketId,TTTA.[Level],TTTA.TaxonomyId 
   FROM TaxonomyAssociation TA   
  INNER JOIN  @TT_TaxonomyAssociation TTTA 
					ON TA.MarketId = TTTA.marketId
					 AND TA.[Level] = TTTA.[level]
					 AND isnull(TA.SiteId,-1) = isnull(TTTA.siteid,-1)
   WHERE TTTA.[delete] = 1	
   
   INSERT INTO @TempDeleteTaxonomyAssociationIds(TaxonomyAssociationId,MarketId,[Level],TaxonomyID) -- If Backup Add items not in XML for delete
   SELECT DISTINCT TA.TaxonomyAssociationId,TTTA.MarketId,TTTA.[Level],TTTA.TaxonomyId 
	 FROM TaxonomyAssociation TA   
	LEFT OUTER JOIN  @TT_TaxonomyAssociation TTTA 
					ON TA.MarketId = TTTA.marketId
					 AND TA.[Level] = TTTA.[level]
					 AND isnull(TA.SiteId,-1) = isnull(TTTA.siteid,-1)
	WHERE TTTA.marketId IS NULL AND @IsBackup=1 AND TA.SiteId = @SiteID

  INSERT INTO @TempDeleteTaxonomyAssociationIds(TaxonomyAssociationId,MarketId,[Level],TaxonomyID) -- If Backup Add items not in XML for delete
   SELECT DISTINCT TA.TaxonomyAssociationId,TTTA.MarketId,TTTA.[Level],TTTA.TaxonomyId 
	 FROM TaxonomyAssociation TA   
	LEFT OUTER JOIN  @TT_TaxonomyAssociation TTTA 
					ON TA.MarketId = TTTA.marketId
					 AND TA.[Level] = TTTA.[level]
					 AND isnull(TA.SiteId,-1) = isnull(TTTA.siteid,-1)
	WHERE TTTA.marketId IS NULL AND @IsBackup=1 AND TA.SiteId IS NULL
	
  DECLARE @TaxonomyAssociationHierachy_Delete TABLE 
  (
    ParentTaxonomyAssociationId int,
    ChildTaxonomyAssociationId int,
    RelationshipType int
  )
  
  DELETE TaxonomyAssociationHierachy -- Delete TaxonomyHierarchy for which TaxonomyHierarchy is marked to be deleted in the XML
   OUTPUT deleted.ParentTaxonomyAssociationId,
					 deleted.ChildTaxonomyAssociationId,
					 deleted.RelationshipType
			   INTO @TaxonomyAssociationHierachy_Delete	
   FROM TaxonomyAssociationHierachy TAH
  INNER JOIN @TempTaxonomyAssociationHierarchy TTAH ON TAH.ParentTaxonomyAssociationId = TTAH.ParentTaxonomyAssociationId
				AND TAH.ChildTaxonomyAssociationId = TTAH.ChildTaxonomyAssociationId
   WHERE TTAH.deleteparent =1 OR TTAH.Deletechild = 1
  
  DELETE TaxonomyAssociationHierachy -- Delete Parent TaxonomyHierarchy for which TaxonomyAssociation is marked to be deleted in the XML
    OUTPUT deleted.ParentTaxonomyAssociationId,
					 deleted.ChildTaxonomyAssociationId,
					 deleted.RelationshipType
			   INTO @TaxonomyAssociationHierachy_Delete	
    FROM TaxonomyAssociationHierachy TAH
  INNER JOIN @TempDeleteTaxonomyAssociationIds TA ON TAH.ParentTaxonomyAssociationId = TA.TaxonomyAssociationId
   
  DELETE TaxonomyAssociationHierachy -- Delete Child TaxonomyHierarchy for which TaxonomyAssociation is marked to be deleted in the XML
   OUTPUT deleted.ParentTaxonomyAssociationId,
					 deleted.ChildTaxonomyAssociationId,
					 deleted.RelationshipType
			   INTO @TaxonomyAssociationHierachy_Delete	  
  FROM TaxonomyAssociationHierachy TAH
  INNER JOIN @TempDeleteTaxonomyAssociationIds TA ON TAH.ChildTaxonomyAssociationId = TA.TaxonomyAssociationId
   
  DELETE TaxonomyAssociationHierachy -- Mirror TaxonomyHierarchy with the XML TaxonomyHierarchy When Backup XML is being restored
     OUTPUT deleted.ParentTaxonomyAssociationId,
					 deleted.ChildTaxonomyAssociationId,
					 deleted.RelationshipType
			   INTO @TaxonomyAssociationHierachy_Delete	
   FROM TaxonomyAssociationHierachy TAH
  LEFT OUTER JOIN @TempTaxonomyAssociationHierarchy TTAH ON TAH.ParentTaxonomyAssociationId = TTAH.ParentTaxonomyAssociationId
				AND TAH.ChildTaxonomyAssociationId = TTAH.ChildTaxonomyAssociationId
   WHERE TTAH.ParentTaxonomyAssociationId IS NULL AND @IsBackup=1
   
   UPDATE CUDHistory -- UPDATE CUDHistory UserID FOR when records are deleted in batches
    SET UserId = @ImportedBy
   FROM CUDHistory 
   INNER JOIN @TaxonomyAssociationHierachy_Delete TAHD ON CUDHistory.[Key] = TAHD.ParentTaxonomyAssociationId
			AND CUDHistory.SecondKey = TAHD.ChildTaxonomyAssociationId
			AND CUDHistory.ThirdKey = TAHD.RelationshipType
			AND CUDHistory.TableName = 'TaxonomyAssociationHierachy'
			AND CUDHistory.CUDType = 'D'
			AND CUDHistory.UserId IS NULL
	
  --12. TaxonomyAssociation Footnotes Delete 
  
  DECLARE @Foootnote_Delete table
  (
	FootnoteId int
  )
  
  DELETE Footnote -- Delete where footnote is marked to be deleted
   OUTPUT deleted.FootnoteId
			   INTO @Foootnote_Delete	
   FROM Footnote FN 
    INNER JOIN TaxonomyAssociation TA ON FN.TaxonomyAssociationId = TA.TaxonomyAssociationId
	INNER JOIN @TT_TaxonomyAssociationFootnotes TTFN 
					ON TA.MarketId = TTFN.taxonomymarketId
					 AND TA.[Level] = TTFN.[level]
					 AND TTFN.[text] = FN.[Text]
   WHERE TTFN.[delete] = 1
   
  DELETE Footnote -- Delete footnote for whihch there is not a matching text in the xml and IsBackup is true
     OUTPUT deleted.FootnoteId
			   INTO @Foootnote_Delete	
   FROM Footnote FN 
    INNER JOIN TaxonomyAssociation TA ON FN.TaxonomyAssociationId = TA.TaxonomyAssociationId
	LEFT OUTER JOIN @TT_TaxonomyAssociationFootnotes TTFN 
					ON TA.MarketId = TTFN.taxonomymarketId
					 AND TA.[Level] = TTFN.[level]
					 AND TTFN.[text] = FN.[Text]
					 WHERE TTFN.taxonomymarketId is null AND @IsBackup=1
   
  DELETE Footnote -- Delete Footnotes where TA is also going to be deleted
   OUTPUT deleted.FootnoteId
			   INTO @Foootnote_Delete	  
   FROM Footnote FN 
    INNER JOIN @TempDeleteTaxonomyAssociationIds TA ON FN.TaxonomyAssociationId = TA.TaxonomyAssociationId  
   
    UPDATE CUDHistory -- UPDATE CUDHistory UserID FOR when records are deleted in batches
      SET UserId = @ImportedBy
   FROM CUDHistory 
	 INNER JOIN @Foootnote_Delete FND ON CUDHistory.[Key] = FND.FootnoteId			
			AND CUDHistory.TableName = 'Footnote'
			AND CUDHistory.CUDType = 'D'
			AND CUDHistory.UserId IS NULL
   
  --13. DocumentTypeAssociation Delete 
  
   	  DECLARE @DocumentTypeAssociation_Delete TABLE 
	  (
		DocumentTypeAssociationId int
	  )

  
	  DELETE DocumentTypeAssociation  -- Delete DocumentTypeAssociation where documenttypes are marked to be deleted in the XML at taxonomy level
		 OUTPUT deleted.DocumentTypeAssociationId
				   INTO @DocumentTypeAssociation_Delete	
	   FROM DocumentTypeAssociation DTA 
		INNER JOIN TaxonomyAssociation TA ON DTA.TaxonomyAssociationId = TA.TaxonomyAssociationId
		INNER JOIN @TT_DocumentTypeAssociation TTDTA 
						ON TA.MarketId = TTDTA.taxonomymarketId
						 AND TA.[Level] = TTDTA.[taxonomylevel]
						 AND TTDTA.documenttypeid = DTA.DocumentTypeId
						 AND DTA.SiteId is null
	   WHERE TTDTA.[delete] = 1 AND TTDTA.siteid IS NULL AND TTDTA.documenttypeid IS NOT NULL AND TA.SiteId IS NOT NULL AND TA.SiteId = @SiteID 


	   	DELETE DocumentTypeAssociation  -- Delete DocumentTypeAssociation where documenttypes are marked to be deleted in the XML at taxonomy level
		 OUTPUT deleted.DocumentTypeAssociationId
				   INTO @DocumentTypeAssociation_Delete	
	   FROM DocumentTypeAssociation DTA 
		INNER JOIN TaxonomyAssociation TA ON DTA.TaxonomyAssociationId = TA.TaxonomyAssociationId
		INNER JOIN @TT_DocumentTypeAssociation TTDTA 
						ON TA.MarketId = TTDTA.taxonomymarketId
						 AND TA.[Level] = TTDTA.[taxonomylevel]
						 AND TTDTA.documenttypeid = DTA.DocumentTypeId
						 AND DTA.SiteId is null
	   WHERE TTDTA.[delete] = 1 AND TTDTA.siteid IS NULL AND TTDTA.documenttypeid IS NOT NULL AND TA.SiteId IS NULL
   
	  DELETE DocumentTypeAssociation  -- Delete DocumentTypeAssociation where documenttypes are marked to be deleted in the XML at site level
		 OUTPUT deleted.DocumentTypeAssociationId
				   INTO @DocumentTypeAssociation_Delete	
	   FROM DocumentTypeAssociation DTA 
		INNER JOIN [Site] ON DTA.SiteId = [Site].SiteId
		INNER JOIN @TT_DocumentTypeAssociation TTDTA 
						ON TTDTA.documenttypeid = DTA.DocumentTypeId 
						AND DTA.SiteId IS NOT NULL
						AND TTDTA.siteid IS NOT NULL 
						AND TTDTA.siteid = DTA.SiteId						
	   WHERE TTDTA.[delete] = 1 AND TTDTA.documenttypeid IS NOT NULL 
	   
	  
	  DELETE DocumentTypeAssociation -- Delete DocumentTypeAssociation where Taxonomy Association is also going to be deleted
		 OUTPUT deleted.DocumentTypeAssociationId
				   INTO @DocumentTypeAssociation_Delete	  
	   FROM DocumentTypeAssociation DTA 
			INNER JOIN @TempDeleteTaxonomyAssociationIds TA ON DTA.TaxonomyAssociationId = TA.TaxonomyAssociationId  
	   AND TA.taxonomyId IS NOT NULL
   
      --Delete for Mirroring when restored from Backup XML   
     
	  DELETE DocumentTypeAssociation  -- Mirroring Delete DocumentTypeAssociation where documenttypes are not in the XML at taxonomy level
	     OUTPUT deleted.DocumentTypeAssociationId
			   INTO @DocumentTypeAssociation_Delete		  
	   FROM DocumentTypeAssociation DTA 
		INNER JOIN TaxonomyAssociation TA ON DTA.TaxonomyAssociationId = TA.TaxonomyAssociationId
		LEFT OUTER JOIN @TT_DocumentTypeAssociation TTDTA 
						ON TA.MarketId = TTDTA.taxonomymarketId
						 AND TA.[Level] = TTDTA.[taxonomylevel]		
						 AND DTA.DocumentTypeId = 	TTDTA.documenttypeid					  					 
	   WHERE DTA.SiteId IS NULL  AND TTDTA.documenttypeid IS NULL 
		AND TA.SiteId is NOT NULL AND TA.SiteId = @SiteID  AND @IsBackup=1


	        
	  DELETE DocumentTypeAssociation  -- Mirroring Delete DocumentTypeAssociation where documenttypes are not in the XML at taxonomy level
	     OUTPUT deleted.DocumentTypeAssociationId
			   INTO @DocumentTypeAssociation_Delete		  
	   FROM DocumentTypeAssociation DTA 
		INNER JOIN TaxonomyAssociation TA ON DTA.TaxonomyAssociationId = TA.TaxonomyAssociationId
		LEFT OUTER JOIN @TT_DocumentTypeAssociation TTDTA 
						ON TA.MarketId = TTDTA.taxonomymarketId
						 AND TA.[Level] = TTDTA.[taxonomylevel]	
						 AND DTA.DocumentTypeId = 	TTDTA.documenttypeid						  					 
	   WHERE DTA.SiteId IS NULL  AND TTDTA.documenttypeid IS NULL 
	   AND TA.SiteId is NULL AND @IsBackup=1
	   
	   

	  DELETE DocumentTypeAssociation  -- Mirroring Delete DocumentTypeAssociation where documenttypes are not in the XML at site level
	     OUTPUT deleted.DocumentTypeAssociationId
			   INTO @DocumentTypeAssociation_Delete		  
	   FROM DocumentTypeAssociation DTA 
	   	INNER JOIN [Site] S on DTA.SiteId = S.SiteId
		LEFT OUTER JOIN @TT_DocumentTypeAssociation TTDTA 
						ON TTDTA.documenttypeid = DTA.DocumentTypeId 						
						AND TTDTA.siteid = DTA.SiteId
	   WHERE TTDTA.documenttypeid IS NULL AND DTA.SiteId IS NOT NULL AND DTA.SiteId = @SiteID AND @IsBackup=1
	   
	   UPDATE CUDHistory -- UPDATE CUDHistory UserID FOR when records are deleted in batches
         SET UserId = @ImportedBy
	   FROM CUDHistory 
	      INNER JOIN @DocumentTypeAssociation_Delete DTAD 
				ON CUDHistory.[Key] = DTAD.DocumentTypeAssociationId			
			AND CUDHistory.TableName = 'DocumentTypeAssociation'
			AND CUDHistory.CUDType = 'D'
			AND CUDHistory.UserId IS NULL
   
  --14. DocumentTypeExternalId Delete where documenttypeid does not exists in DocumentTypeAssociaiton Table .delete only when Restored from Backup
  
	  DECLARE @DocumentTypeExternalId_Delete TABLE
	  (
		DocumentTypeId int,
		ExternalId nvarchar(100)
	  )
	    
	  DELETE DocumentTypeExternalId 
		OUTPUT deleted.DocumentTypeId,
				deleted.ExternalId
				INTO @DocumentTypeExternalId_Delete
		FROM DocumentTypeExternalId DTE
		LEFT OUTER JOIN  DocumentTypeAssociation DTA on DTE.DocumentTypeId = DTA.documenttypeid		
		WHERE DTA.DocumentTypeAssociationId IS NULL	AND @IsBackup=1
		
	   UPDATE CUDHistory -- UPDATE CUDHistory UserID FOR when records are deleted in batches
         SET UserId = @ImportedBy
	   FROM CUDHistory 
	      INNER JOIN @DocumentTypeExternalId_Delete DTED 
				ON CUDHistory.[Key] = DTED.DocumentTypeId
				 AND CUDHistory.SecondKey = DTED.ExternalId			
			AND CUDHistory.TableName = 'DocumentTypeExternalId'
			AND CUDHistory.CUDType = 'D'
			AND CUDHistory.UserId IS NULL

   
   
   --15. TaxonomyAssociation DELETE
	   DELETE TaxonomyAssociation
		  FROM TaxonomyAssociation TA 
		INNER JOIN @TempDeleteTaxonomyAssociationIds TTTA 
			ON TA.TaxonomyAssociationId = TTTA.TaxonomyAssociationId 
			
	   UPDATE CUDHistory -- UPDATE CUDHistory UserID FOR when records are deleted in batches
         SET UserId = @ImportedBy
	   FROM CUDHistory 
	      INNER JOIN @TempDeleteTaxonomyAssociationIds TDTA 
				ON CUDHistory.[Key] = TDTA.TaxonomyAssociationId			
			AND CUDHistory.TableName = 'TaxonomyAssociation'
			AND CUDHistory.CUDType = 'D'
			AND CUDHistory.UserId IS NULL
			
								
  --16. TaxonomyLevelExternalId Delete Where TaxonomyID and Level have been deleted from TaxonomyAssociation. Delete only when restored from backup XML
	DECLARE @TaxonomyLevelExternalId_Delete TABLE
	  (
		[Level] int,
		TaxonomyId int,
		ExternalId nvarchar(100)
	  )
	  
	  DELETE TaxonomyLevelExternalId 
	  		OUTPUT deleted.[Level],
				deleted.TaxonomyId,
				deleted.ExternalId
				INTO @TaxonomyLevelExternalId_Delete
		FROM TaxonomyLevelExternalId TLE
		LEFT OUTER JOIN  TaxonomyAssociation TA on TLE.TaxonomyId = TA.taxonomyId
			AND TLE.[Level] = TA.[Level]			
		WHERE TA.TaxonomyAssociationId is null	AND @IsBackup=1							

	   UPDATE CUDHistory -- UPDATE CUDHistory UserID FOR when records are deleted in batches
         SET UserId = @ImportedBy
	   FROM CUDHistory 
	      INNER JOIN @TaxonomyLevelExternalId_Delete TLED 
				ON CUDHistory.[Key] = TLED.[Level]
				 AND CUDHistory.SecondKey = TLED.TaxonomyId
				 AND CUDHistory.ThirdKey = TLED.ExternalId			
			AND CUDHistory.TableName = 'TaxonomyLevelExternalId'
			AND CUDHistory.CUDType = 'D'
			AND CUDHistory.UserId IS NULL
								
End