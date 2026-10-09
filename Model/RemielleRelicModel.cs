using BaseLib.Abstracts;
using BaseLib.Extensions;
using BaseLib.Utils;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Model
{
    [Pool(typeof(RemielleRelicPool))]
    public abstract class RemielleRelicModel : CustomRelicModel
    {
        protected override string IconBaseName =>
            base.Id.Entry.RemovePrefix().ToLowerInvariant();
    }
}
