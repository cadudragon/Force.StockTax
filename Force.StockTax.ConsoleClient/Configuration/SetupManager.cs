using System;
using System.Collections.Generic;
using System.IO;
using System.Text;

namespace Force.StockTax.ConsoleClient.Configuration
{
    class SetupManager
    {
        

        private static void CreateDocumentsDataFolder()
        {
            if (!Directory.Exists(SetupConstants.DocumentsFolderPath))
            {
                Directory.CreateDirectory(SetupConstants.DocumentsFolderPath);
            }

            if (!Directory.Exists(SetupConstants.DocumentsFolderPath + "\\notas-para-processar") )
            {
                Directory.CreateDirectory(SetupConstants.DocumentsFolderPath + "\\notas-para-processar");
            }
        }

        public static void Setup() {
            CreateDocumentsDataFolder();
        }
    }
}
