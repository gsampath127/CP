using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Data.SqlClient;
using System.Data;
using BCS.ObjectModel;
using System.IO;
using BCS.ObjectModel.Factories;

namespace BCSInitialLoadBatch
{
    class Program
    {
        static void Main(string[] args)
        {


            //string md5 = UtilityFactory.GetMD5HashFromFile(@"C:\Users\nodsouza\Desktop\f7575adc06c27b392b7d7df6961f2b1b.pdf");   

            //List<string> filenames = new List<string>();

            //filenames.Add(@"C:\Users\nodsouza\Desktop\SP_fa8626285b2245068ddeba3682b7d057.pdf");
            //filenames.Add(@"C:\Users\nodsouza\Desktop\SP_fa8626285b2245068ddeba3682b7d057.txt");

            //using (ZipOutputStream s = new ZipOutputStream(File.Create(@"C:\Users\nodsouza\Desktop\GIM_SLINK_201411211847_73542.zip")))
            //{

            //    s.SetLevel(9); // 0-9, 9 being the highest compression

            //    byte[] buffer = new byte[4096];

            //    foreach (string file in filenames)
            //    {

            //        ZipEntry entry = new ZipEntry(Path.GetFileName(file));

            //        entry.DateTime = DateTime.Now;
            //        entry.Size = new FileInfo(file).Length;
            //        s.PutNextEntry(entry);

            //        using (FileStream fs = File.OpenRead(file))
            //        {

            //            int sourceBytes;

            //            do
            //            {

            //                sourceBytes = fs.Read(buffer, 0, buffer.Length);

            //                s.Write(buffer, 0, sourceBytes);

            //            }

            //            while (sourceBytes > 0);

            //        }

            //    }

            //    s.Finish();

            //    s.Close();

            //}

            //return;


            ServiceFactory servicefactory = new ServiceFactory();

            servicefactory.GetMasterDBAndUpdateInWorkflowDB();

            //servicefactory.ProcessDocUpdatesForFilingsPendingToBeProcessed();

            //servicefactory.ProcessNewlyAddedorModifiedCUSIPDetails();

           // servicefactory.DequeueAndProcessURLToDownloadJob(1);

            return;

            //servicefactory.SendBCSDocUpdateValidationReport();

            Console.WriteLine("Started at " + DateTime.Now.ToString());

            BCSClient bcsclient = servicefactory.GetGIMClientConfigs();

                if (bcsclient.ClientPrefix == "GIM")
                {

                    //servicefactory.ProcessInitialLoad(bcsclient);
                    //servicefactory.GenerateDocUpdateFile(bcsclient);
                    

                    //try
                    //{

                    //    servicefactory.ProcessInitialLoad(bcsclient);

                    //    foreach (string eml in ConfigValues.ConfirmationEmailListTo.Split(';'))
                    //    {
                    //        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null,
                    //                "Initial Load Completed in Production",
                    //                "Initial Load has been completed ",
                    //                 "support", null);
                    //    }

                    //}
                    //catch (Exception exception)
                    //{
                    //    foreach (string eml in ConfigValues.ConfirmationEmailListTo.Split(';'))
                    //    {
                    //        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null,
                    //                "Initial Load Failed",
                    //                "Initial Load Failed - " + exception.Message,
                    //                 "support", null);
                    //    }
                    //}

                    //try
                    //{

                    //    servicefactory.ProcessNewlyAddedorModifiedCUSIPDetails();

                    //    foreach (string eml in ConfigValues.ConfirmationEmailListTo.Split(';'))
                    //    {
                    //        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null,
                    //                "ProcessNewlyAddedorModifiedCUSIPDetails Completed in Production",
                    //                "ProcessNewlyAddedorModifiedCUSIPDetails has been completed ",
                    //                 "support", null);
                    //    }
                    //}
                    //catch (Exception exception)
                    //{
                    //    foreach (string eml in ConfigValues.ConfirmationEmailListTo.Split(';'))
                    //    {
                    //        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null,
                    //                "ProcessNewlyAddedorModifiedCUSIPDetails Failed",
                    //                "ProcessNewlyAddedorModifiedCUSIPDetails Failed - " + exception.Message,
                    //                 "support", null);
                    //    }
                    //}

                    //try
                    //{
                    //    servicefactory.ProcessDocUpdatesForFilingsPendingToBeProcessed();

                    //    foreach (string eml in ConfigValues.ConfirmationEmailListTo.Split(';'))
                    //    {
                    //        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null,
                    //                "ProcessDocUpdatesForFilingsPendingToBeProcessed Completed in Production",
                    //                "ProcessDocUpdatesForFilingsPendingToBeProcessed has been completed ",
                    //                 "support", null);
                    //    }
                    //}
                    //catch (Exception exception)
                    //{
                    //    foreach (string eml in ConfigValues.ConfirmationEmailListTo.Split(';'))
                    //    {
                    //        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null,
                    //                "ProcessDocUpdatesForFilingsPendingToBeProcessed Failed",
                    //                "ProcessDocUpdatesForFilingsPendingToBeProcessed Failed - " + exception.Message,
                    //                 "support", null);
                    //    }
                    //}
                    //Console.ReadLine();
                    //try
                    //{

                    //    servicefactory.UploadSlinkForPreflightAndUpdateExportedStatus(bcsclient);

                    //    foreach (string eml in ConfigValues.ConfirmationEmailListTo.Split(';'))
                    //    {
                    //        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null,
                    //                "UploadSlinkForPreflightAndUpdateExportedStatus Completed in Production",
                    //                "UploadSlinkForPreflightAndUpdateExportedStatus has been completed ",
                    //                 "support", null);
                    //    }
                    //}
                    //catch (Exception exception)
                    //{
                    //    foreach (string eml in ConfigValues.ConfirmationEmailListTo.Split(';'))
                    //    {
                    //        UtilityFactory.SendEmail(ConfigValues.EmailFrom, eml, null, null,
                    //                "UploadSlinkForPreflightAndUpdateExportedStatus Failed",
                    //                "UploadSlinkForPreflightAndUpdateExportedStatus Failed - " + exception.Message,
                    //                 "support", null);
                    //    }
                    //}


                     
               
                }

            

            Console.WriteLine("Completed at " + DateTime.Now.ToString());

            Console.ReadLine();
        }
    }
}
