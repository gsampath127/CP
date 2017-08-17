using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BCS.ObjectModel
{
    public class BCSClient
    {
        public int ClientId { get; set; }

        public string ClientName { get; set; }

        public string ClientPrefix { get; set; }

        public string ClientDocsFTPPath { get; set; }

        public string ClientDocsFTPUserName { get; set; }

        public string ClientDocsFTPPassword { get; set; }

        public string DocUploadWorkingFolderName { get; set; }

        public bool IsFLTRequired { get; set; }

        public string FLTodaysTableName { get; set; }

        public string FLTTomorrowsTableName { get; set; }

        public string FLTPickupFTPPath { get; set; }

        public string FLTPickupFTPUserName { get; set; }

        public string FLTPickupFTPPassword { get; set; }

        public string PreFlightDropFTPPath { get; set; }

        public string PreFlightDropFTPUserName { get; set; }

        public string PreFlightDropFTPPassword { get; set; }

        public string DocUpdateMetadataDropFTPPath { get; set; }

        public string DocUpdateMetaDataUserName { get; set; }

        public string DocUpdateMetaDataPassword { get; set; }

        public string PreFlightStatusPickupFTP { get; set; }

        public string PreFlightStatusFTPUserName { get; set; }

        public string PreFlightStatusFTPPassword { get; set; }

        public string DASStatusFTPPickupPath { get; set; }

        public string DASStatusFTPUserName { get; set; }

        public string DASStatusFTPPassword { get; set; }

        public string APFFTPPath { get; set; }

        public string OPFFTPPath { get; set; }

        public string DocUpdateStatusFilesSANPath { get; set; }

        public PreFlightJob preflightjob { get; set; }

        public string CurrentZipFileName { get; set; }

        public int? BCSDocUpdateARSARSlinkID { get; set; }

        public string FLTArchiveDropPath { get; set; }

        public string FTPDocArchiveDropPath { get; set; }

        public bool SendRRDPDFURL { get; set; }

        public bool NeedDocUpdate { get; set; }

        public bool SendIPDocUpdate { get; set; }

        public bool IsCUSIPWatchListProvided { get; set; }

        public bool IncludeRemovedWatchListCUSIPInIPDocUpdate { get; set; }

        public string CUSIPWatchlistPickupFTPLocation { get; set; }

        public string CUSIPWatchlistPickupFTPUserName { get; set; }

        public string CUSIPWatchlistPickupFTPPassword { get; set; }

        public string CUSIPWatchListArchiveDropPath { get; set; }

        public bool ShowClientInDashboard { get; set; }

        public string IPFileNamePrefix { get; set; }

        public bool SendSecurityType { get; set; }

        public string IPDeliveryMethod { get; set; }
    }
}
