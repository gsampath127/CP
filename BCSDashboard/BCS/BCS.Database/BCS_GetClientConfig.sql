USE [db1029]
GO
/****** Object:  StoredProcedure [dbo].[BCS_GetClientConfig]    Script Date: 05/01/2015 13:04:53 ******/


ALTER Procedure [dbo].[BCS_GetClientConfig]
as
Begin
	  SELECT ClientName,
	ClientPrefix,
	ClientDocsFTPPath,
	ClientDocsFTPUserName,
	ClientDocsFTPPassword,
	DocUploadWorkingFolderName,
	DocUpdateStatusFilesSANPath,
	IsFLTRequired,
	FLTodaysTableName,
	FLTTomorrowsTableName,
	FLTPickupFTPPath,
	FLTPickupFTPUserName,
	FLTPickupFTPPassword,
	FLTArchiveDropPath,
	FTPDocArchiveDropPath,
	PreFlightDropFTPPath,
	PreFlightDropFTPUserName,
	PreFlightDropFTPPassword,
	DocUpdateMetadataDropFTPPath,
	DocUpdateMetaDataUserName,
	DocUpdateMetaDataPassword,
	PreFlightStatusPickupFTP,
	PreFlightStatusFTPUserName,
	PreFlightStatusFTPPassword,
	DASStatusFTPPickupPath,
	DASStatusFTPUserName,
	DASStatusFTPPassword,
	APFFTPPath,
	OPFFTPPath	
 FROM BCSClientConfig
 WHERE ClientName = 'GIM'
 
	
End
