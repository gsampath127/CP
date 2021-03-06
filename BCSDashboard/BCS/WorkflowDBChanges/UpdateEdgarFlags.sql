


ALTER Procedure [dbo].[UpdateEdgarFlags]
@AccNum varchar(250)
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
	DECLARE @isDelawareLife bit
	DECLARE @isAllianz bit	
	DECLARE @isTransamerica bit
	DECLARE @isAllianceBernstein bit
	
	SET @isSecurityBenefit=0
	
	SET @isSymetra=0
	
	SET @isForethought=0
	
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
	   CIK varchar(50) not null,
	   classcontractid varchar(50) not null	   
	 )
 
 
	 insert into @ClassTable(CIK,classcontractid)
	 select CIK,ClassContractID from tblShareClass 
	   where AccNum = @AccNum
   
 



	IF exists(
				SELECT top 1 classtable.ClassContractID  FROM tblSecurityBenefits
				INNER JOIN @ClassTable classtable on tblSecurityBenefits.ClassContractID = classtable.classcontractid				 
			  )
	BEGIN
			SET @isSecurityBenefit=1  
	END


	IF exists(
				SELECT top 1 classtable.ClassContractID from tblSymetra 
				INNER JOIN @ClassTable classtable on tblSymetra.ClassContractID = classtable.classcontractid				 
			  )
	Begin
			set @isSymetra=1  
	End
	
	IF exists(
			SELECT top 1 classtable.ClassContractID from tblForethought 
			INNER JOIN @ClassTable classtable on tblForethought.ClassContractID = classtable.classcontractid				 
		  )
	Begin
			set @isForethought=1  
	End
	
	IF exists(
			SELECT top 1 classtable.ClassContractID from tblNewYorkLife 
			INNER JOIN @ClassTable classtable on tblNewYorkLife.ClassContractID = classtable.classcontractid				 
		  )	
	Begin
			set @isNewYorkLife =1  
	End
	
	IF exists(
			SELECT top 1 classtable.ClassContractID from tblOhioNational 
			INNER JOIN @ClassTable classtable on tblOhioNational.ClassContractID = classtable.classcontractid				 
		  )		
	Begin
			set @isOhioNational =1  
	End

	IF exists(
			SELECT top 1 classtable.ClassContractID from tblGenworth 
			INNER JOIN @ClassTable classtable on tblGenworth.ClassContractID = classtable.classcontractid				 
		  )
	Begin
			SET @isGenworth=1  
	End

	IF exists(
			SELECT top 1 classtable.ClassContractID from tblTRowePrice 
			INNER JOIN @ClassTable classtable on tblTRowePrice.ClassContractID = classtable.classcontractid				 
		  )
	Begin
			SET @isTRowePrice=1  
	End

	IF exists(
			SELECT top 1 classtable.ClassContractID from tblAXAVIT 
			INNER JOIN @ClassTable classtable on tblAXAVIT.ClassContractID = classtable.classcontractid				 
		  )
	Begin
			SET @isAXA=1  
	End
	
	IF exists(
			SELECT top 1 classtable.ClassContractID from tblMutualOfAmerica 
			INNER JOIN @ClassTable classtable on tblMutualOfAmerica.ClassContractID = classtable.classcontractid				 
		  )	
	Begin
			SET @isMutualOfAmerica=1  
	End
	
	IF exists(
			SELECT top 1 classtable.ClassContractID from tblDelawareLife 
			INNER JOIN @ClassTable classtable on tblDelawareLife.ClassContractID = classtable.classcontractid				 
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
	
	IF exists(
				SELECT top 1 classtable.CIK from tblHostedCompaniesWithSummaryProspectus 
				INNER JOIN @ClassTable classtable on tblHostedCompaniesWithSummaryProspectus.CIK = classtable.CIK
			  )
	Begin
			SET @isHostedFund=1  
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

