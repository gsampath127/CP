	IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = 'na\RPIngestorStageServ')		
	 BEGIN
		CREATE LOGIN [na\RPIngestorStageServ] FROM WINDOWS WITH DEFAULT_DATABASE=[RPV2SystemDB], DEFAULT_LANGUAGE=[us_english]
	 END
	GO
	IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = 'na\FSBTFSDevelopers')	
	BEGIN	
		CREATE LOGIN [na\FSBTFSDevelopers] FROM WINDOWS WITH DEFAULT_DATABASE=[RPV2SystemDB], DEFAULT_LANGUAGE=[us_english]
	END
	GO
	IF NOT EXISTS (SELECT * FROM master.dbo.syslogins WHERE loginname = 'ECOMAD\stage-pool-rpconnect')	
	BEGIN	
		CREATE LOGIN [ECOMAD\stage-pool-rpconnect] FROM WINDOWS WITH DEFAULT_DATABASE=[RPV2SystemDB], DEFAULT_LANGUAGE=[us_english]
	END
	GO