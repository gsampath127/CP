


ALTER Procedure [dbo].[UpdateEdgarFlagsWithSeriesData]
@AccNum varchar(250),
@classcontractids varchar(max)
as
Begin
  
	DECLARE @isSecurityBenefit bit
	DECLARE @isSymetra bit
	DECLARE @isForethought bit
	DECLARE @isGenworth bit
	DECLARE @isTRowePrice bit
	DECLARE @isAXA bit
	DECLARE @isHostedFund bit
	DECLARE @isNewYorkLife bit
	DECLARE @isOhioNational bit
	DECLARE @isMutualOfAmerica bit
	Declare @isDelawareLife bit
	DECLARE @isAllianz bit
	DECLARE @isTransamerica bit
	DECLARE @isAllianceBernstein bit
	
	SET @isSecurityBenefit=0
	
	SET @isSymetra=0
	
	SET @isGenworth=0
	
	SET @isTRowePrice=0
	
	SET @isAXA=0
	
	SET @isHostedFund=0
	
	SET @isNewYorkLife=0
	
	SET @isOhioNational=0
	
	SET @isMutualOfAmerica=0
	
	SET @isDelawareLife=0
	
	SET @isAllianz=0
	
	SET @isTransamerica=0
	
	SET @isAllianceBernstein=0
	

	
	 Declare @ClassTable Table
	 (
	   classcontractid varchar(50) not null
	 )
 
 
	 insert into @ClassTable(classcontractid)
	 select ID from [dbo].[SplitIDs](@classcontractids)
   


	IF exists(
				SELECT * FROM tblSecurityBenefits WHERE ClassContractID in
				(
				  select classcontractid from @ClassTable
				)
			  )
	BEGIN
			SET @isSecurityBenefit=1  
	END


	IF exists(
				SELECT * from tblSymetra where ClassContractID in
				(
				  select classcontractid from @ClassTable
				)
			  )
	Begin
			set @isSymetra=1  
	End
	
	IF exists(
				SELECT * from tblForethought where ClassContractID in
				(
				  select classcontractid from @ClassTable
				)
			  )
	Begin
			set @isForethought=1  
	End
	
	IF exists(
				SELECT * from tblNewYorkLife where ClassContractID in
				(
				  select classcontractid from @ClassTable
				)
			  )
	Begin
			set @isNewYorkLife =1  
	End
	
	IF exists(
				SELECT * from tblOhioNational where ClassContractID in
				(
				  select classcontractid from @ClassTable
				)
			  )
	Begin
			set @isOhioNational =1  
	End
	

	IF exists(
				SELECT * from tblGenworth where ClassContractID in
				(
				  select classcontractid from @ClassTable
				)
			  )
	Begin
			SET @isGenworth=1  
	End

	IF exists(
				SELECT * from tblTRowePrice where ClassContractID in
				(
				  select classcontractid from @ClassTable
				)
			  )
	Begin
			SET @isTRowePrice=1  
	End



	IF exists(
				SELECT * from tblAXAVIT where ClassContractID in
				(
				  select classcontractid from @ClassTable
				)
			  )
	Begin
			SET @isAXA=1  
	End
	
	IF exists(
				SELECT top 1 ClassContractID from tblDelawareLife where ClassContractID in
				(
				  select classcontractid from @ClassTable
				)
			  )
	Begin
			SET @isDelawareLife=1  
	End
	
	IF exists(
			SELECT top 1 classtable.ClassContractID from tblAllianz 
			INNER JOIN @ClassTable classtable on tblAllianz.ClassContractID = classtable.classcontractid				 
		  )		
	Begin
			SET @isAllianz=1  
	End
	
	IF exists(
				SELECT top 1 ClassContractID from tblMutualOfAmerica where ClassContractID in
				(
				  select classcontractid from @ClassTable
				)
			  )
	Begin
			SET @isMutualOfAmerica=1  
	End
	
	IF exists(
			SELECT top 1 classtable.ClassContractID from tblTransamerica 
			INNER JOIN @ClassTable classtable on tbltransamerica.ClassContractID = classtable.classcontractid				 
		  )		
	Begin
			SET @isTransamerica=1  
	End
	
	IF exists(
			SELECT top 1 classtable.ClassContractID from tblAllianceBernstein 
			INNER JOIN @ClassTable classtable on tblAllianceBernstein.ClassContractID = classtable.classcontractid				 
		  )		
	Begin
			SET @isAllianceBernstein=1  
	End
	
	UPDATE tblEdgar 
	  SET isSecurityBenefit=@isSecurityBenefit,
		  isSymetra=@isSymetra,
		  isForethought=@isForethought,
		  isGenworth=@isGenworth,
		  isAXA=@isAXA,
		  isTRowePrice=@isTRowePrice,
		  isHostedFund=@isHostedFund,
		  isNewYorkLife=@isNewYorkLife,
		  isOhioNational=@isOhioNational,
		  isMutualOfAmerica=@isMutualOfAmerica,
		  isDelawareLife=@isDelawareLife,
		  IsAllianz=@isAllianz,
		  IsTransamerica=@isTransamerica,
		  isAllianceBernstein=@isAllianceBernstein
	  WHERE AccNum=@AccNum

End

