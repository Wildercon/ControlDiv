using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ControlDiv.App.Data
{
    internal class Apis
    {
        public const string Base = "https://localhost:7090/api/";
        public const string Voucher = $"{Base}Voucher";
        public const string Account = $"{Base}Account";
        public const string Operation = $"{Base}Operation";
        public const string User = $"{Base}User";
        public const string Seller = $"{Base}Seller";
    }
}
