CREATE TABLE [dbo].[ProsTicker] (
    [TickerID]        INT           IDENTITY (1, 1) NOT NULL,
    [ProspectusID]    INT           NULL,
    [TickerSymbol]    NVARCHAR (10) NULL,
    [Class]           VARCHAR (100) NULL,
    [FileNumber]      VARCHAR (50)  NULL,
    [CIK]             VARCHAR (50)  NULL,
    [SeriesID]        VARCHAR (50)  NULL,
    [ClassContractID] VARCHAR (50)  NULL,
    [CUSIP]           VARCHAR (10)  NULL,
    [LIPPER]          VARCHAR (50)  NULL,
    CONSTRAINT [PK_ProsTicker] PRIMARY KEY CLUSTERED ([TickerID] ASC)
);


GO
CREATE TRIGGER ProsTickerDeletion
       ON dbo.ProsTicker
       FOR DELETE
    AS
       IF NOT EXISTS
          (
           SELECT TickerID FROM ProsTickerHistory WHERE TickerID in (SELECT TickerID
           FROM Deleted)
          )
          BEGIN
				insert into ProsTickerHistory (TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, DeletionDate)
					select TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, Getdate() 
				from 
					Deleted
			  
          END



--		 IF NOT EXISTS
--          (
--           SELECT TRowePriceHistoryid FROM TRowePriceHistory WHERE CUSIP in (SELECT CUSIP
--           FROM Deleted)
--          )
--          BEGIN
--				delete from TRowePrice where CUSIP in (SELECT CUSIP
--           FROM Deleted)
--			  
--          END
GO
CREATE TRIGGER ProsTickerUpdate  
       ON dbo.ProsTicker  
       FOR UPDATE  
    AS  
 DECLARE @TDate DATETIME  
 SET @TDate = GETDATE()  
  
    IF UPDATE(TickerSymbol)   
 BEGIN  
  DECLARE @TempTickerSymbol NVARCHAR(10)  
  DECLARE @TempTickerSymbolDelet NVARCHAR(10)  
  SELECT @TempTickerSymbol = TickerSymbol FROM ProsTicker WHERE TickerID in (SELECT TickerID  
           FROM Deleted)  
  SELECT @TempTickerSymbolDelet = TickerSymbol  
           FROM Deleted  
  IF(@TempTickerSymbolDelet <> @TempTickerSymbol)  
  BEGIN  
   insert into ProsTickerHistory (TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, UpdateDate, UpdatedField)  
      select TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, @TDate , 'TickerSymbol'   
     from   
      Deleted  
  END  
 END  
 IF UPDATE(Class)   
 BEGIN  
  DECLARE @TempClass varchar(100)  
  DECLARE @TempClassDelet varchar(100)  
  SELECT @TempClass = Class FROM ProsTicker WHERE TickerID in (SELECT TickerID  
           FROM Deleted)  
  SELECT @TempClassDelet = Class  
           FROM Deleted  
  IF(@TempClassDelet <> @TempClass)  
  BEGIN  
   insert into ProsTickerHistory (TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, UpdateDate, UpdatedField)  
      select TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, @TDate , 'Class'   
     from   
      Deleted  
  END  
 END  
 IF  UPDATE(CUSIP)  
 BEGIN  
  DECLARE @TempCUSIP varchar(10)  
  DECLARE @TempCUSIPDelet varchar(10)  
  SELECT @TempCUSIP = CUSIP FROM ProsTicker WHERE TickerID in (SELECT TickerID  
           FROM Deleted)  
  SELECT @TempCUSIPDelet = CUSIP  
           FROM Deleted  
  IF(@TempCUSIPDelet <> @TempCUSIP)  
  BEGIN  
   insert into ProsTickerHistory (TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, UpdateDate, UpdatedField)  
      select TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, @TDate , 'CUSIP'   
     from   
      Deleted    
   IF (@TempCUSIPDelet<>'' and @TempCUSIP<>'')  
   BEGIN  
    update troweprice set cusip_d = @TempCUSIP, cusip = @TempCUSIP where cusip_d =  @TempCUSIPDelet  
    update axavit set cusip = @TempCUSIP where cusip =  @TempCUSIPDelet  
    update securitybenefits set cusip = @TempCUSIP where cusip = @TempCUSIPDelet  
   END  
  
  
  END  
 END  
 IF UPDATE(LIPPER)  
 BEGIN  
  DECLARE @TempLIPPER varchar(50)  
  DECLARE @TempLIPPERDelet varchar(50)  
 
  SELECT @TempLIPPERDelet = LIPPER  
           FROM Deleted  
  IF(RTrim(Ltrim(@TempLIPPERDelet)) <> RTrim(Ltrim(@TempLIPPER)))  
  BEGIN  
   insert into ProsTickerHistory (TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, UpdateDate, UpdatedField)  
      select TickerID, ProspectusID, TickerSymbol, Class, FileNumber, CIK, SeriesID, ClassContractID, CUSIP, LIPPER, @TDate , 'LIPPER'   
     from   
      Deleted  
  END  
 END  
 IF UPDATE(CIK) OR UPDATE(ClassContractID)  
 BEGIN  
  DECLARE @TempCIK varchar(50)  
  DECLARE @TempCIKDelet varchar(50)  
  DECLARE @TempClassContractID varchar(50)  
  DECLARE @TempClassContractIDDelet varchar(50)  
  DECLARE @TempSeriesID varchar(50)  
  DECLARE @TempSeriesIDDelet varchar(50)  
  DECLARE @CUSIP varchar(10)  
    
  SELECT @CUSIP = CUSIP FROM ProsTicker WHERE TickerID in (SELECT TickerID  
           FROM Deleted)  
  SELECT @TempCIK = CIK FROM ProsTicker WHERE TickerID in (SELECT TickerID  
           FROM Deleted)  
  SELECT @TempCIKDelet = CIK  
           FROM Deleted  
  SELECT @TempClassContractID = ClassContractID FROM ProsTicker WHERE TickerID in (SELECT TickerID  
           FROM Deleted)  
  SELECT @TempClassContractIDDelet = ClassContractID  
           FROM Deleted  
  SELECT @TempSeriesID = SeriesID FROM ProsTicker WHERE TickerID in (SELECT TickerID  
           FROM Deleted)  
  SELECT @TempSeriesIDDelet = SeriesID  
           FROM Deleted  

  IF NOT EXISTS(SELECT CIK FROM Deleted WHERE CIK IS NULL) AND NOT EXISTS(SELECT CIK FROM Deleted WHERE CIK='')  
  BEGIN  
   IF EXISTS(SELECT CUSIP_D FROM TROWEPRICE WHERE CUSIP_D IN(SELECT CUSIP FROM PROSTICKER WHERE CIK=(SELECT CIK FROM DELETED)))  
   BEGIN  
    update troweprice set CIK=(select top 1 cik from prosticker where cusip=troweprice.cusip_d),  
    ClassContractID=(select top 1 ClassContractID from prosticker where cusip=troweprice.cusip_d)  
   END  
  
   IF EXISTS(SELECT CUSIP FROM axavit WHERE CUSIP IN(SELECT CUSIP FROM PROSTICKER WHERE CIK=(SELECT CIK FROM DELETED)))  
   BEGIN  
    update axavit set CIK=(select top 1 cik from prosticker where cusip=axavit.cusip),  
    SeriesID=(select top 1 SeriesID from prosticker where cusip=axavit.cusip),  
    ClassContractID=(select top 1 ClassContractID from prosticker where cusip=axavit.cusip)  
    where (axavit.CUSIP IN ('46600H604', '46600H505', '46600H604', '46600H703', '46600H786', '46600H794', '46600H851', '46600H885'))  
   END  
     
  
  update securitybenefits set CIK = @TempCIK, ClassContractID=@TempClassContractID, SeriesID =@TempSeriesID where cusip = @CUSIP   
  END  
    
 END