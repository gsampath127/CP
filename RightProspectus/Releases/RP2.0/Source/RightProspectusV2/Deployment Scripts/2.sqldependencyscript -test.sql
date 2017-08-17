/* Create notification table */ 
IF NOT EXISTS (SELECT name FROM sysobjects WITH (NOLOCK) WHERE name = 'AspNet_SqlCacheTablesForChangeNotification' AND type = 'U') 
   IF NOT EXISTS (SELECT name FROM sysobjects WITH (TABLOCKX) WHERE name = 'AspNet_SqlCacheTablesForChangeNotification' AND type = 'U') 
      CREATE TABLE dbo.AspNet_SqlCacheTablesForChangeNotification (
      tableName             NVARCHAR(450) NOT NULL PRIMARY KEY,
      notificationCreated   DATETIME NOT NULL DEFAULT(GETDATE()),
      changeId              INT NOT NULL DEFAULT(0)
      )

/* Create polling SP */
IF NOT EXISTS (SELECT name FROM sysobjects WITH (NOLOCK) WHERE name = 'AspNet_SqlCachePollingStoredProcedure' AND type = 'P') 
   IF NOT EXISTS (SELECT name FROM sysobjects WITH (TABLOCKX) WHERE name = 'AspNet_SqlCachePollingStoredProcedure' AND type = 'P') 
   EXEC('CREATE PROCEDURE dbo.AspNet_SqlCachePollingStoredProcedure AS
         SELECT tableName, changeId FROM dbo.AspNet_SqlCacheTablesForChangeNotification
         RETURN 0')

/* Create SP for registering a table. */ 
IF NOT EXISTS (SELECT name FROM sysobjects WITH (NOLOCK) WHERE name = 'AspNet_SqlCacheRegisterTableStoredProcedure' AND type = 'P') 
   IF NOT EXISTS (SELECT name FROM sysobjects WITH (TABLOCKX) WHERE name = 'AspNet_SqlCacheRegisterTableStoredProcedure' AND type = 'P') 
   EXEC('CREATE PROCEDURE dbo.AspNet_SqlCacheRegisterTableStoredProcedure 
             @tableName NVARCHAR(450) 
         AS
         BEGIN

         DECLARE @triggerName AS NVARCHAR(3000) 
         DECLARE @fullTriggerName AS NVARCHAR(3000)
         DECLARE @canonTableName NVARCHAR(3000) 
         DECLARE @quotedTableName NVARCHAR(3000) 

         /* Create the trigger name */ 
         SET @triggerName = REPLACE(@tableName, ''['', ''__o__'') 
         SET @triggerName = REPLACE(@triggerName, '']'', ''__c__'') 
         SET @triggerName = @triggerName + ''_AspNet_SqlCacheNotification_Trigger'' 
         SET @fullTriggerName = ''dbo.['' + @triggerName + '']'' 

         /* Create the cannonicalized table name for trigger creation */ 
         /* Do not touch it if the name contains other delimiters */ 
         IF (CHARINDEX(''.'', @tableName) <> 0 OR 
             CHARINDEX(''['', @tableName) <> 0 OR 
             CHARINDEX('']'', @tableName) <> 0) 
             SET @canonTableName = @tableName 
         ELSE 
             SET @canonTableName = ''['' + @tableName + '']'' 

         /* First make sure the table exists */ 
         IF (SELECT OBJECT_ID(@tableName, ''U'')) IS NULL 
         BEGIN 
             RAISERROR (''00000001'', 16, 1) 
             RETURN 
         END 

         BEGIN TRAN
         /* Insert the value into the notification table */ 
         IF NOT EXISTS (SELECT tableName FROM dbo.AspNet_SqlCacheTablesForChangeNotification WITH (NOLOCK) WHERE tableName = @tableName) 
             IF NOT EXISTS (SELECT tableName FROM dbo.AspNet_SqlCacheTablesForChangeNotification WITH (TABLOCKX) WHERE tableName = @tableName) 
                 INSERT  dbo.AspNet_SqlCacheTablesForChangeNotification 
                 VALUES (@tableName, GETDATE(), 0)

         /* Create the trigger */ 
         SET @quotedTableName = QUOTENAME(@tableName, '''''''') 
         IF NOT EXISTS (SELECT name FROM sysobjects WITH (NOLOCK) WHERE name = @triggerName AND type = ''TR'') 
             IF NOT EXISTS (SELECT name FROM sysobjects WITH (TABLOCKX) WHERE name = @triggerName AND type = ''TR'') 
                 EXEC(''CREATE TRIGGER '' + @fullTriggerName + '' ON '' + @canonTableName +''
                       FOR INSERT, UPDATE, DELETE AS BEGIN
                       SET NOCOUNT ON
                       EXEC dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure N'' + @quotedTableName + ''
                       END
                       '')
         COMMIT TRAN
         END
   ')

/* Create SP for updating the change Id of a table. */ 
IF NOT EXISTS (SELECT name FROM sysobjects WITH (NOLOCK) WHERE name = 'AspNet_SqlCacheUpdateChangeIdStoredProcedure' AND type = 'P') 
   IF NOT EXISTS (SELECT name FROM sysobjects WITH (TABLOCKX) WHERE name = 'AspNet_SqlCacheUpdateChangeIdStoredProcedure' AND type = 'P') 
   EXEC('CREATE PROCEDURE dbo.AspNet_SqlCacheUpdateChangeIdStoredProcedure 
             @tableName NVARCHAR(450) 
         AS

         BEGIN 
             UPDATE dbo.AspNet_SqlCacheTablesForChangeNotification WITH (ROWLOCK) SET changeId = changeId + 1 
             WHERE tableName = @tableName
         END
   ')

/* Create SP for unregistering a table. */ 
IF NOT EXISTS (SELECT name FROM sysobjects WITH (NOLOCK) WHERE name = 'AspNet_SqlCacheUnRegisterTableStoredProcedure' AND type = 'P') 
   IF NOT EXISTS (SELECT name FROM sysobjects WITH (TABLOCKX) WHERE name = 'AspNet_SqlCacheUnRegisterTableStoredProcedure' AND type = 'P') 
   EXEC('CREATE PROCEDURE dbo.AspNet_SqlCacheUnRegisterTableStoredProcedure 
             @tableName NVARCHAR(450) 
         AS
         BEGIN

         BEGIN TRAN
         DECLARE @triggerName AS NVARCHAR(3000) 
         DECLARE @fullTriggerName AS NVARCHAR(3000)
         SET @triggerName = REPLACE(@tableName, ''['', ''__o__'') 
         SET @triggerName = REPLACE(@triggerName, '']'', ''__c__'') 
         SET @triggerName = @triggerName + ''_AspNet_SqlCacheNotification_Trigger'' 
         SET @fullTriggerName = ''dbo.['' + @triggerName + '']'' 

         /* Remove the table-row from the notification table */ 
         IF EXISTS (SELECT name FROM sysobjects WITH (NOLOCK) WHERE name = ''AspNet_SqlCacheTablesForChangeNotification'' AND type = ''U'') 
             IF EXISTS (SELECT name FROM sysobjects WITH (TABLOCKX) WHERE name = ''AspNet_SqlCacheTablesForChangeNotification'' AND type = ''U'') 
             DELETE FROM dbo.AspNet_SqlCacheTablesForChangeNotification WHERE tableName = @tableName 

         /* Remove the trigger */ 
         IF EXISTS (SELECT name FROM sysobjects WITH (NOLOCK) WHERE name = @triggerName AND type = ''TR'') 
             IF EXISTS (SELECT name FROM sysobjects WITH (TABLOCKX) WHERE name = @triggerName AND type = ''TR'') 
             EXEC(''DROP TRIGGER '' + @fullTriggerName) 

         COMMIT TRAN
         END
   ')

/* Create SP for querying all registered table */ 
IF NOT EXISTS (SELECT name FROM sysobjects WITH (NOLOCK) WHERE name = 'AspNet_SqlCacheQueryRegisteredTablesStoredProcedure' AND type = 'P') 
   IF NOT EXISTS (SELECT name FROM sysobjects WITH (TABLOCKX) WHERE name = 'AspNet_SqlCacheQueryRegisteredTablesStoredProcedure' AND type = 'P') 
   EXEC('CREATE PROCEDURE dbo.AspNet_SqlCacheQueryRegisteredTablesStoredProcedure 
         AS
         SELECT tableName FROM dbo.AspNet_SqlCacheTablesForChangeNotification   ')

IF NOT EXISTS(SELECT principal_id FROM sys.database_principals WHERE name = 'na\RPIngestorStageServ') 
BEGIN
	CREATE USER [na\RPIngestorStageServ] FOR LOGIN [na\RPIngestorStageServ] 
	WITH DEFAULT_SCHEMA = [RPIngestorStageServ]
END

IF NOT EXISTS (SELECT  schema_name FROM   information_schema.schemata WHERE   schema_name = 'RPIngestorStageServ' ) 
BEGIN
  EXEC sp_executesql N'CREATE SCHEMA [RPIngestorStageServ] AUTHORIZATION [na\RPIngestorStageServ]' 
END



IF NOT EXISTS(SELECT principal_id FROM sys.database_principals WHERE name = 'ECOMAD\Test-pool-RPConnect') 
BEGIN
 CREATE USER [ECOMAD\Test-pool-RPConnect] FOR LOGIN [ECOMAD\Test-pool-RPConnect] WITH DEFAULT_SCHEMA = [Test-pool-RPConnect]
END

IF NOT EXISTS (SELECT  schema_name FROM   information_schema.schemata WHERE   schema_name = 'Test-pool-RPConnect' ) 
BEGIN
  EXEC sp_executesql N'CREATE SCHEMA [Test-pool-RPConnect] AUTHORIZATION [ECOMAD\Test-pool-RPConnect]' 
END


IF NOT EXISTS (select 1 from sys.database_principals where name='rpv2sql_dependency' and Type = 'R')
BEGIN
   EXEC sp_addrole 'rpv2sql_dependency' 
END

GRANT SUBSCRIBE QUERY NOTIFICATIONS TO rpv2sql_dependency
GRANT RECEIVE ON QueryNotificationErrorsQueue TO rpv2sql_dependency
GRANT CREATE QUEUE to rpv2sql_dependency
GRANT CREATE SERVICE to rpv2sql_dependency
GRANT REFERENCES on 
CONTRACT::[http://schemas.microsoft.com/SQL/Notifications/PostQueryNotification]
  to rpv2sql_dependency 
GRANT VIEW DEFINITION TO rpv2sql_dependency 


GRANT SELECT to rpv2sql_dependency 



    EXEC sp_addrolemember 'rpv2sql_dependency', [na\RPIngestorStageServ]


	EXEC sp_addrolemember 'db_datareader', [na\RPIngestorStageServ];
	
	EXEC sp_addrolemember 'db_datawriter', [na\RPIngestorStageServ]; 	
	
	GRANT EXECUTE ON SCHEMA::dbo TO [na\RPIngestorStageServ];
	
	
	
    EXEC sp_addrolemember 'rpv2sql_dependency', [ECOMAD\Test-pool-RPConnect]


	EXEC sp_addrolemember 'db_datareader', [ECOMAD\Test-pool-RPConnect];
	
	EXEC sp_addrolemember 'db_datawriter', [ECOMAD\Test-pool-RPConnect]; 
	
	GRANT EXECUTE ON SCHEMA::dbo TO [ECOMAD\Test-pool-RPConnect];
	
	
	
	grant alter on schema::dbo to rpv2sql_dependency

    grant control on schema::dbo to rpv2sql_dependency
    
    GRANT CREATE TABLE  to rpv2sql_dependency
   GRANT CREATE PROCEDURE  to rpv2sql_dependency

IF NOT EXISTS (select 1 from sys.database_principals where name='aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess' and Type = 'R')
BEGIN

CREATE ROLE [aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess] AUTHORIZATION [dbo]
END

IF NOT EXISTS (SELECT  schema_name FROM   information_schema.schemata WHERE   schema_name = 'aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess' ) 
BEGIN
  EXEC sp_executesql N'CREATE SCHEMA [aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess] AUTHORIZATION [aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess]' 
END


GRANT EXECUTE ON dbo.AspNet_SqlCachePollingStoredProcedure to aspnet_ChangeNotification_ReceiveNotificationsOnlyAccess
