using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using Remielle.Relic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Powers;

namespace Remielle.Power
{
    /// <summary>
    /// 虚耀 获得时获得1精通
    /// </summary>
    public class Voidflares : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature applier, CardModel cardSource)
        {
            if (power != this || amount == 0) return;
            int n = 1;
            if (Owner.IsPlayer && 
                Owner.Player.Relics.Any(a => a is FallIntoTheNoise)) n = 2;
            await PowerCmd.Apply<AnomalyProficiency>(choiceContext,
                Owner, amount * n, Owner, null);
        }
    }
}
