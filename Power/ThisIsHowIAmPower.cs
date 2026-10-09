using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;

namespace Remielle.Power
{
    /// <summary>
    /// 吾本如斯p 本回合造成【异常】伤害时，获得格挡。
    /// </summary>
    public class ThisIsHowIAmPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature dealer, CardModel cardSource)
        {
            if (dealer == Owner && props.HasFlag(ZZZValueProp.Anomaly))
            {
                await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Unpowered, null);
            }
        }

        public override async Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if(participants.Contains(Owner))
            {
                await PowerCmd.Remove(this);
            }
        }
    }
}
