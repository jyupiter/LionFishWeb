using LionFishWeb.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace LionFishWeb.Repositories
{
    interface IConfirmationCodeRepo
    {
        bool AddConfirmationCode(ConfirmationCode cc);
        bool DeleteConfirmationCode(string email);
        ConfirmationCode GetConfirmationCode(string email);
        bool CheckConfirmationCodeByEmail(string email);
    }
}