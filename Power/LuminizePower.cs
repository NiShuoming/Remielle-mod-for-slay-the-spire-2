using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;

namespace Remielle.Power
{
    /// <summary>
    /// 耀变 给予【流明】、触发【异化】时，造成【属性异常】伤害,抽1
    /// </summary>
    public class LuminizePower : PowerModel, IZZZTriggleAnomalyHook
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature applier, CardModel cardSource)
        {
            if(applier==Owner && power is Lumiflux && amount > 0)
            {
                await CreatureCmd.Damage(choiceContext, power.Owner,
                    Amount, ValueProp.Unpowered | ZZZValueProp.Anomaly, Owner);
                await CardPileCmd.Draw(choiceContext, Owner.Player);
            }
        }

        public async Task TriggleAnomaly(PlayerChoiceContext playerChoice, CardKeyword keyWord, Creature target, Creature applier, IEnumerable<DamageResult> results)
        {
            if(applier==Owner && keyWord == RemielleKeyWord.Refringe)
            {
                await CreatureCmd.Damage(playerChoice, target,
                    Amount, ValueProp.Unpowered | ZZZValueProp.Anomaly, Owner);
                await CardPileCmd.Draw(playerChoice, Owner.Player);

            }
        }
    }
}
