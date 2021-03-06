

ALTER Procedure [dbo].[UpdateFirstDollarCusipMaster]
@CusipMasterTable FirstDollarCusipMasterTable READONLY,
@Prefix varchar(10)
as
Begin
  IF @Prefix = 'AZL'
  Begin
     DELETE tblAllianz WHERE ClassContractID not in
                            (
								SELECT ClassContractID
                                FROM  @CusipMasterTable  CusipMasterTable                            
                             )
     INSERT INTO tblAllianz
					(
					CUSIP,
					CIK,
					SeriesID,
					ClassContractID,
					TickerSymbol
					)
			 SELECT [CUSIP]
				  ,[CIK]
				  ,[SeriesID]
				  ,[ClassContractID]
				  ,[TickerSymbol]
			  FROM @CusipMasterTable CusipMasterTable
				  WHERE ClassContractID NOT IN
					(
						SELECT ClassContractID 
						FROM tblAllianz 
						WHERE ClassContractID is not null)
						AND (ClassContractID != '' 
						AND ClassContractID is not null
					 )
  End
   Else
  IF @Prefix = 'DEL'
  Begin
     DELETE tblDelawareLife WHERE ClassContractID not in
                            (
								SELECT ClassContractID
                                FROM  @CusipMasterTable  CusipMasterTable                            
                             )
     INSERT INTO tblDelawareLife
					(
					CUSIP,
					CIK,
					SeriesID,
					ClassContractID,
					TickerSymbol
					)
			 SELECT [CUSIP]
				  ,[CIK]
				  ,[SeriesID]
				  ,[ClassContractID]
				  ,[TickerSymbol]
			  FROM @CusipMasterTable CusipMasterTable
				  WHERE ClassContractID NOT IN
					(
						SELECT ClassContractID 
						FROM tblDelawareLife 
						WHERE ClassContractID is not null)
						AND (ClassContractID != '' 
						AND ClassContractID is not null
					 )
  End
   Else
	IF @Prefix = 'SBR'
		  Begin
			 DELETE tblSecurityBenefits WHERE ClassContractID not in
									(
										SELECT ClassContractID
										FROM  @CusipMasterTable CusipMasterTable                            
									 )
			 INSERT INTO tblSecurityBenefits
							(
							CUSIP,
							CIK,
							SeriesID,
							ClassContractID,
							TickerSymbol
							)
					 SELECT [CUSIP]
						  ,[CIK]
						  ,[SeriesID]
						  ,[ClassContractID]
						  ,[TickerSymbol]
					  FROM @CusipMasterTable CusipMasterTable
						  WHERE ClassContractID NOT IN
							(
								SELECT ClassContractID 
								FROM tblSecurityBenefits 
								WHERE ClassContractID is not null)
								AND (ClassContractID != '' 
								AND ClassContractID is not null
							 )
		  End
		   Else
			IF @Prefix = 'SYA'
				  Begin
					 DELETE tblSymetra WHERE ClassContractID not in
											(
												SELECT ClassContractID
												FROM  @CusipMasterTable CusipMasterTable                            
											 )
					 INSERT INTO tblSymetra
									(
									CUSIP,
									CIK,
									SeriesID,
									ClassContractID,
									TickerSymbol
									)
							 SELECT [CUSIP]
								  ,[CIK]
								  ,[SeriesID]
								  ,[ClassContractID]
								  ,[TickerSymbol]
							  FROM @CusipMasterTable CusipMasterTable
								  WHERE ClassContractID NOT IN
									(
										SELECT ClassContractID 
										FROM tblSymetra 
										WHERE ClassContractID is not null)
										AND (ClassContractID != '' 
										AND ClassContractID is not null
									 )
				  End
				   Else
					IF @Prefix = 'OHN'
						  Begin
							 DELETE tblOhioNational WHERE ClassContractID not in
													(
														SELECT ClassContractID
														FROM  @CusipMasterTable CusipMasterTable                            
													 )
							 INSERT INTO tblOhioNational
											(
											CUSIP,
											CIK,
											SeriesID,
											ClassContractID,
											TickerSymbol
											)
									 SELECT [CUSIP]
										  ,[CIK]
										  ,[SeriesID]
										  ,[ClassContractID]
										  ,[TickerSymbol]
									  FROM @CusipMasterTable CusipMasterTable
										  WHERE ClassContractID NOT IN
											(
												SELECT ClassContractID 
												FROM tblOhioNational 
												WHERE ClassContractID is not null)
												AND (ClassContractID != '' 
												AND ClassContractID is not null
											 )
						  End
						   Else
							IF @Prefix = 'FFG'
								  Begin
									 DELETE tblForethought WHERE ClassContractID not in
															(
																SELECT ClassContractID
																FROM  @CusipMasterTable CusipMasterTable                            
															 )
									 INSERT INTO tblForethought
													(
													CUSIP,
													CIK,
													SeriesID,
													ClassContractID,
													TickerSymbol
													)
											 SELECT [CUSIP]
												  ,[CIK]
												  ,[SeriesID]
												  ,[ClassContractID]
												  ,[TickerSymbol]
											  FROM @CusipMasterTable CusipMasterTable
												  WHERE ClassContractID NOT IN
													(
														SELECT ClassContractID 
														FROM tblForethought 
														WHERE ClassContractID is not null)
														AND (ClassContractID != '' 
														AND ClassContractID is not null
													 )
								  End
								   Else
									IF @Prefix = 'GEN'
										  Begin
											 DELETE tblGenworth WHERE ClassContractID not in
																	(
																		SELECT ClassContractID
																		FROM  @CusipMasterTable CusipMasterTable                            
																	 )
											 INSERT INTO tblGenworth
															(
															CUSIP,
															CIK,
															SeriesID,
															ClassContractID															
															)
													 SELECT [CUSIP]
														  ,[CIK]
														  ,[SeriesID]
														  ,[ClassContractID]														  
													  FROM @CusipMasterTable CusipMasterTable
														  WHERE ClassContractID NOT IN
															(
																SELECT ClassContractID 
																FROM tblGenworth 
																WHERE ClassContractID is not null)
																AND (ClassContractID != '' 
																AND ClassContractID is not null
															 )
										  End
										   Else
											IF @Prefix = 'NYL'
												  Begin
													 DELETE tblNewYorkLife WHERE ClassContractID not in
																			(
																				SELECT ClassContractID
																				FROM  @CusipMasterTable CusipMasterTable                            
																			 )
													 INSERT INTO tblNewYorkLife
																	(
																	CUSIP,
																	CIK,
																	SeriesID,
																	ClassContractID															
																	)
															 SELECT [CUSIP]
																  ,[CIK]
																  ,[SeriesID]
																  ,[ClassContractID]														  
															  FROM @CusipMasterTable CusipMasterTable
																  WHERE ClassContractID NOT IN
																	(
																		SELECT ClassContractID 
																		FROM tblNewYorkLife 
																		WHERE ClassContractID is not null)
																		AND (ClassContractID != '' 
																		AND ClassContractID is not null
																	 )
												  End
												Else
													IF @Prefix = 'AEG'
															  Begin
																 DELETE tblTransamerica WHERE ClassContractID not in
																						(
																							SELECT ClassContractID
																							FROM  @CusipMasterTable CusipMasterTable                            
																						 )
																 INSERT INTO tblTransamerica
																				(
																				CUSIP,
																				CIK,
																				SeriesID,
																				ClassContractID															
																				)
																		 SELECT [CUSIP]
																			  ,[CIK]
																			  ,[SeriesID]
																			  ,[ClassContractID]														  
																		  FROM @CusipMasterTable CusipMasterTable
																			  WHERE ClassContractID NOT IN
																				(
																					SELECT ClassContractID 
																					FROM tblTransamerica 
																					WHERE ClassContractID is not null)
																					AND (ClassContractID != '' 
																					AND ClassContractID is not null
																				 )
															  End
												Else
													IF @Prefix = 'AB'
															  Begin
																 DELETE tblAllianceBernstein WHERE ClassContractID not in
																						(
																							SELECT ClassContractID
																							FROM  @CusipMasterTable CusipMasterTable                            
																						 )
																 INSERT INTO tblAllianceBernstein
																				(
																				CUSIP,
																				CIK,
																				SeriesID,
																				ClassContractID															
																				)
																		 SELECT [CUSIP]
																			  ,[CIK]
																			  ,[SeriesID]
																			  ,[ClassContractID]														  
																		  FROM @CusipMasterTable CusipMasterTable
																			  WHERE ClassContractID NOT IN
																				(
																					SELECT ClassContractID 
																					FROM tblAllianceBernstein 
																					WHERE ClassContractID is not null)
																					AND (ClassContractID != '' 
																					AND ClassContractID is not null
																				 )
															  End

End

