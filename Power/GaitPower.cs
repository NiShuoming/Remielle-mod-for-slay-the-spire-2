using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using Remielle.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Power
{
    public class GaitPower : TemporaryDexterityPower
    {
        public override AbstractModel OriginModel => ModelDb.Card<Gait>();
    }
}
