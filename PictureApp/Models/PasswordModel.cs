using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PictureApp.Models
{
    public class PasswordModel
    {
        public string Salt { get; set; } //Пока что будем хранить так
        public string HashedPassword { get; set; }
    }
}
