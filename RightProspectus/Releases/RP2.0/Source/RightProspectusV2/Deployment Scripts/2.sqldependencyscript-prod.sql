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

IF NOT EXISTS(SELECT principal_id FROM sys.database_principals WHERE name = 'na\RPIngestorProdServ') 
BEGIN
	CREATE USER [na\RPIngestorProdServ] FOR LOGIN [na\RPIngestorProdServ] 
	WITH DEFAULT_SCHEMA = [RPIngestorProdServ]
END

IF NOT EXISTS (SELECT  schema_name FROM   information_schema.schemata WHERE   schema_name = 'RPIngestorProdServ' ) 
BEGIN
  EXEC sp_executesql N'CREATE SCHEMA [RPIngestorProdServ] AUTHORIZATION [na\RPIngestorProdServ]' 
END

IF NOT EXISTS(SELECT principal_id FROM sys.database_principals WHERE name = 'na\rr173192') 
BEGIN
	CREATE USER [na\rr173192] FOR LOGIN [na\rr173192] 
	WITH DEFAULT_SCHEMA = [rr173192]
END

IF NOT EXISTS (SELECT  schema_name FROM   information_schema.schemata WHERE   schema_name = 'rr173192' ) 
BEGIN
  EXEC sp_executesql N'CREATE SCHEMA [rr173192] AUTHORIZATION [na\rr173192]' 
END

IF NOT EXISTS(SELECT principal_id FROM sys.database_principals WHERE name = 'na\alex.xavier') 
BEGIN
	CREATE USER [na\alex.xavier] FOR LOGIN [na\alex.xavier] 
	WITH DEFAULT_SCHEMA = [alex.xavier]
END

IF NOT EXISTS (SELECT  schema_name FROM   information_schema.schemata WHERE   schema_name = 'alex.xavier' ) 
BEGIN
  EXEC sp_executesql N'CREATE SCHEMA [alex.xavier] AUTHORIZATION [na\alex.xavier]' 
END

IF NOT EXISTS(SELECT principal_id FROM sys.database_principals WHERE name = 'na\rr093914') 
BEGIN
	CREATE USER [na\rr093914] FOR LOGIN [na\rr093914] 
	WITH DEFAULT_SCHEMA = [rr093914]
END

IF NOT EXISTS (SELECT  schema_name FROM   information_schema.schemata WHERE   schema_name = 'rr093914' ) 
BEGIN
  EXEC sp_executesql N'CREATE SCHEMA [rr093914] AUTHORIZATION [na\rr093914]' 
END

IF NOT EXISTS(SELECT principal_id FROM sys.database_principals WHERE name = 'na\rr215388') 
BEGIN
	CREATE USER [na\rr215388] FOR LOGIN [na\rr215388] 
	WITH DEFAULT_SCHEMA = [rr215388]
END

IF NOT EXISTS (SELECT  schema_name FROM   information_schema.schemata WHERE   schema_name = 'rr215388' ) 
BEGIN
  EXEC sp_executesql N'CREATE SCHEMA [rr215388] AUTHORIZATION [na\rr215388]' 
END



IF NOT EXISTS(SELECT principal_id FROM sys.database_principals WHERE name = 'na\nodsouza') 
BEGIN
	CREATE USER [na\nodsouza] FOR LOGIN [na\nodsouza] 
	WITH DEFAULT_SCHEMA = [nodsouza]
END

IF NOT EXISTS (SELECT  schema_name FROM   information_schema.schemata WHERE   schema_name = 'nodsouza' ) 
BEGIN
  EXEC sp_executesql N'CREATE SCHEMA [nodsouza] AUTHORIZATION [na\nodsouza]' 
END

IF NOT EXISTS(SELECT principal_id FROM sys.database_principals WHERE name = 'na\dgentry') 
BEGIN
	CREATE USER [na\dgentry] FOR LOGIN [na\dgentry] 
	WITH DEFAULT_SCHEMA = [dgentry]
END

IF NOT EXISTS (SELECT  schema_name FROM   information_schema.schemata WHERE   schema_name = 'dgentry' ) 
BEGIN
  EXEC sp_executesql N'CREATE SCHEMA [dgentry] AUTHORIZATION [na\dgentry]' 
END


IF NOT EXISTS(SELECT principal_id FROM sys.database_principals WHERE name = 'ECOMAD\prod-pool-rpconnect') 
BEGIN
 CREATE USER [ECOMAD\prod-pool-rpconnect] FOR LOGIN [ECOMAD\prod-pool-rpconnect] WITH DEFAULT_SCHEMA = [prod-pool-rpconnect]
END

IF NOT EXISTS (SELECT  schema_name FROM   information_schema.schemata WHERE   schema_name = 'prod-pool-rpconnect' ) 
BEGIN
  EXEC sp_executesql N'CREATE SCHEMA [prod-pool-rpconnect] AUTHORIZATION [ECOMAD\prod-pool-rpconnect]' 
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



    EXEC sp_addrolemember 'rpv2sql_dependency', [na\RPIngestorProdServ]


	EXEC sp_addrolemember 'db_datareader', [na\RPIngestorProdServ];
	
	EXEC sp_addrolemember 'db_datawriter', [na\RPIngestorProdServ]; 	
	
	GRANT EXECUTE ON SCHEMA::dbo TO [na\RPIngestorProdServ];
	
	EXEC sp_addrolemember 'rpv2sql_dependency', [na\nodsouza]


	EXEC sp_addrolemember 'db_datareader', [na\nodsouza];
	
	EXEC sp_addrolemember 'db_datawriter', [na\nodsouza]; 
	
	GRANT EXECUTE ON SCHEMA::dbo TO [na\nodsouza];
	
	EXEC sp_addrolemember 'rpv2sql_dependency', [na\rr173192]

	EXEC sp_addrolemember 'db_datareader', [na\rr173192];
	
	EXEC sp_addrolemember 'db_datawriter', [na\rr173192]; 
	
	GRANT EXECUTE ON SCHEMA::dbo TO [na\rr173192];

	
	EXEC sp_addrolemember 'rpv2sql_dependency', [na\alex.xavier]

	EXEC sp_addrolemember 'db_datareader', [na\alex.xavier];
	
	EXEC sp_addrolemember 'db_datawriter', [na\alex.xavier]; 
	
	GRANT EXECUTE ON SCHEMA::dbo TO [na\alex.xavier];
	
	EXEC sp_addrolemember 'rpv2sql_dependency', [NA\rr093914]

	EXEC sp_addrolemember 'db_datareader', [NA\rr093914];
	
	EXEC sp_addrolemember 'db_datawriter', [NA\rr093914]; 
	
	GRANT EXECUTE ON SCHEMA::dbo TO [NA\rr093914];
	
	EXEC sp_addrolemember 'rpv2sql_dependency', [NA\rr215388]

	EXEC sp_addrolemember 'db_datareader', [NA\rr215388];
	
	EXEC sp_addrolemember 'db_datawriter', [NA\rr215388]; 
	
	GRANT EXECUTE ON SCHEMA::dbo TO [NA\rr215388];
	
    EXEC sp_addrolemember 'rpv2sql_dependency', [NA\dgentry]

	EXEC sp_addrolemember 'db_datareader', [NA\dgentry];
	
	EXEC sp_addrolemember 'db_datawriter', [NA\dgentry]; 
	
	GRANT EXECUTE ON SCHEMA::dbo TO [NA\dgentry];

    EXEC sp_addrolemember 'rpv2sql_dependency', [ECOMAD\prod-pool-rpconnect]


	EXEC sp_addrolemember 'db_datareader', [ECOMAD\prod-pool-rpconnect];
	
	EXEC sp_addrolemember 'db_datawriter', [ECOMAD\prod-pool-rpconnect]; 
	
	GRANT EXECUTE ON SCHEMA::dbo TO [ECOMAD\prod-pool-rpconnect];
	
	
	
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
