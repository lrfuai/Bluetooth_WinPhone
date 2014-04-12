using System;
using System.Collections.Generic;
using System.IO;
using System.IO.IsolatedStorage;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using GamePad_1.BE;

namespace GamePad_1.BE
{
    public class DataSaver<Movements>
    {
        private const string TargetFolderName = "MyFolderName";
        private DataContractSerializer _mySerializer;
        private IsolatedStorageFile _isoFile;
        

        IsolatedStorageFile IsoFile
        {
            get
            {
                if (_isoFile == null)
                    _isoFile = System.IO.IsolatedStorage.
                                IsolatedStorageFile.GetUserStoreForApplication();
                return _isoFile;
            }
        }

        public DataSaver()
        {
            _mySerializer = new DataContractSerializer(typeof(Movements));
        }

        public void SaveMyData(Movements sourceData, String targetFileName)
        {
            string TargetFileName = String.Format("{0}/{1}.dat",
                                           TargetFolderName, targetFileName);
            if (!IsoFile.DirectoryExists(TargetFolderName))
                IsoFile.CreateDirectory(TargetFolderName);
            try
            {
                using (var targetFile = IsoFile.CreateFile(TargetFileName))
                {
                    _mySerializer.WriteObject(targetFile, sourceData);
                }
            }
            catch (Exception e)
            {
                IsoFile.DeleteFile(TargetFileName);
            }
        }

        public Movements LoadMyData(string sourceName)
        {            
            Movements retVal = default(Movements);      
            
            string TargetFileName = String.Format("{0}/{1}.dat", TargetFolderName, sourceName);
            if (IsoFile.FileExists(TargetFileName))
                using (var sourceStream =
                        IsoFile.OpenFile(TargetFileName, FileMode.Open))
                {
                    retVal = (Movements)_mySerializer.ReadObject(sourceStream);
                }
            
            return retVal;
        }
    }
}
