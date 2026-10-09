using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Remielle.Other;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cmds;

namespace Remielle.Power
{
    /// <summary>
    /// 流明 debuff+1.触发异化
    /// </summary>
    public class Lumiflux : PowerModel
    {
        public override PowerType Type => PowerType.Debuff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override decimal ModifyPowerAmountGivenAdditive(PowerModel power, Creature giver, decimal amount, Creature target, CardModel cardSource)
        {
            if(power.Type == PowerType.Debuff && target == Owner && power is not Lumiflux && amount>0)
            {
                return 1;
            }
            return 0;
        }

        public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature applier, CardModel cardSource)
        {
            if (power.Type == PowerType.Debuff && power.Owner == Owner && power is not Lumiflux && amount > 0)
            {
                await RemielleCmd.TriggleRefringe(power, applier, Owner, cardSource);
                await PowerCmd.Decrement(this);
            }

        }
    }
}
