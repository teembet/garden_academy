using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using EduApply.Data.Entities;
using EduApply.Logic.Interfaces;
using EduApply.Logic.Repository;
using Effortless.Net.Encryption;

namespace EduApply.Logic.Service
{
  public  class EncryptionService :SqlRepository, IEncryptionService
    {
      public EncryptionService(IDbContext context) : base(context)
      {
          
      }
        public string EncryptUserName(string userName)
        {
            string encryptedUserName = "";
            byte[] key;
            byte[] IV;
            EncryptionKeys encryptKeys = GetEncryptionSettings() ?? new EncryptionKeys();
            if (encryptKeys.EncryptionKey != null && encryptKeys.EncryptionIv != null)
            {
                key = encryptKeys.EncryptionKey;
                IV = encryptKeys.EncryptionIv;

                encryptedUserName = Strings.Encrypt(userName, key, IV);
            }
            else
            {
                key = Bytes.GenerateKey();
                IV = Bytes.GenerateIV();
                encryptedUserName = Strings.Encrypt(userName, key, IV);
                SaveEncryptedData(key, IV);
            }
            return encryptedUserName;
        }

        public string DecryptUserName(string encryptedUserName)
        {
            string decryptedUserName = "";
            var encryptKeys = GetEncryptionSettings();
            if (encryptKeys.EncryptionKey != null && encryptKeys.EncryptionIv != null)
            {
                decryptedUserName = Strings.Decrypt(encryptedUserName, encryptKeys.EncryptionKey, encryptKeys.EncryptionIv);
            }
            return decryptedUserName;
        }

        public void SaveEncryptedData(byte[] key, byte[] iv)
        {
            var encryptionKeys = new EncryptionKeys()
            {
                EncryptionKey = key,
                EncryptionIv = iv
            };
            this.Insert<EncryptionKeys>(encryptionKeys);
            this.SaveChanges();
        }

        public Data.Entities.EncryptionKeys GetEncryptionSettings()
        {
            var encryptionKeys = this.GetAll<EncryptionKeys>();
            return encryptionKeys.FirstOrDefault();
        }
    }
}
