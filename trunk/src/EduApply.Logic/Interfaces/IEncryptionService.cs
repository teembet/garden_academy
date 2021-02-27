using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;

namespace EduApply.Logic.Interfaces
{
  public  interface IEncryptionService
    {
        string EncryptUserName(string userName);
        string DecryptUserName(string encryptedUserName);
        void SaveEncryptedData(byte[] key, byte[] iv);
        EncryptionKeys GetEncryptionSettings();
    }
}
