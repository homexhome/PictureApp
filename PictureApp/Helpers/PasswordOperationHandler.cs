using Newtonsoft.Json;
using PictureApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureApp.Helpers
{
    public static class PasswordOperationHandler {
        public static void SavePaswordAndSalt(string password, string salt) {
            if (password == null || salt == null) {
                throw new ArgumentNullException("Password or salt is null");
            }
            PasswordModel passwordModel = new() {
                Salt = salt,
                HashedPassword = PasswordHasher.HashPassword(password, salt)
            };
            string json = JsonConvert.SerializeObject(passwordModel);
            FileHelper.SaveData(json);
        }

        public static bool LoadAndVerifyPasswordAndSalt(string password, string salt) {
            if (password == null || salt == null) {
                throw new ArgumentNullException("Password or salt is null");
            }
            string loadingHashedPassword = PasswordHasher.HashPassword(password, salt);

            PasswordModel inputPasswordModel = new() {
                Salt = salt,
                HashedPassword = loadingHashedPassword
            };

            string savedPassword = FileHelper.LoadData();
            PasswordModel loadedPasswordModel = JsonConvert.DeserializeObject<PasswordModel>(savedPassword);

            if (loadedPasswordModel.HashedPassword == inputPasswordModel.HashedPassword) {
                return true;
            } else {
                return false;
            }
        }
    }
}
