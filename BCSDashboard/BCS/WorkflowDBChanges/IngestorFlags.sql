

Alter FUNCTION [dbo].[IngestorFlags] (@isSecurityBenefit bit,@isSymetra bit,@isGenworth bit,@isTRowePrice bit,
										@isAXA bit,@isForethought bit,@isHostedFund bit,@isNewYorkLife bit,
										@isOhioNational bit,@isMutualOfAmerica bit,@isDelawareLife bit,
										@isAllianz bit,@isTransamerica bit,@isAllianceBernstein bit)
    RETURNS varchar(max)
AS
BEGIN
    Declare @IngestorFlag varchar(max)
    
    set @IngestorFlag = ''
    
     
	 IF @isTRowePrice = 1     
        set @IngestorFlag = 'TRowe Price'        

     IF @isSecurityBenefit = 1
     Begin
        IF @IngestorFlag = ''
         set @IngestorFlag = 'Security Benefits'
        else
         set @IngestorFlag = @IngestorFlag + ', Security Benefits'
	 End
	
     IF @isGenworth = 1
     Begin
        IF @IngestorFlag = ''
         set @IngestorFlag = 'Genworth'
        else
         set @IngestorFlag = @IngestorFlag + ', Genworth'
	 End
        

     IF @isAXA = 1
     Begin
        IF @IngestorFlag = ''
         set @IngestorFlag = 'AXA'
        else
         set @IngestorFlag = @IngestorFlag + ', AXA'
	 End
     
        
     IF @isSymetra = 1
     Begin
        IF @IngestorFlag = ''
         set @IngestorFlag = 'Symetra'
        else
         set @IngestorFlag = @IngestorFlag + ', Symetra'
	 End    
	 
	 IF @isForethought = 1
     Begin
        IF @IngestorFlag = ''
         set @IngestorFlag = 'Forethought'
        else
         set @IngestorFlag = @IngestorFlag + ', Forethought'
	 End    
       
	 IF @isNewYorkLife = 1
     Begin
        IF @IngestorFlag = ''
         set @IngestorFlag = 'New York Life'
        else
         set @IngestorFlag = @IngestorFlag + ', New York Life'
	 End    
       
	 IF @isOhioNational = 1
     Begin
        IF @IngestorFlag = ''
         set @IngestorFlag = 'Ohio National'
        else
         set @IngestorFlag = @IngestorFlag + ', Ohio National'
	 End    
	 
	 IF @isMutualOfAmerica = 1
     Begin
        IF @IngestorFlag = ''
         set @IngestorFlag = 'Mutual Of America'
        else
         set @IngestorFlag = @IngestorFlag + ', Mutual Of America'
	 End    
	 
	 IF @isDelawareLife = 1
     Begin
        IF @IngestorFlag = ''
         set @IngestorFlag = 'Delaware'
        else
         set @IngestorFlag = @IngestorFlag + ', Delaware'
	 End
	 
	 IF @isAllianz = 1
     Begin
        IF @IngestorFlag = ''
         set @IngestorFlag = 'Allianz'
        else
         set @IngestorFlag = @IngestorFlag + ', Allianz'
	 End
	 
	 IF @isTransamerica = 1
     Begin
        IF @IngestorFlag = ''
         set @IngestorFlag = 'Transamerica'
        else
         set @IngestorFlag = @IngestorFlag + ', Transamerica'
	 End

	 IF @isAllianceBernstein = 1
     Begin
        IF @IngestorFlag = ''
         set @IngestorFlag = 'Alliance Bernstein'
        else
         set @IngestorFlag = @IngestorFlag + ', Alliance Bernstein'
	 End
	 
	 IF @isHostedFund = 1
     Begin
        IF @IngestorFlag = ''
         set @IngestorFlag = 'Hosted Family'
        else
         set @IngestorFlag = @IngestorFlag + ', Hosted Family'
	 End    
    
    return @IngestorFlag
END




GO


