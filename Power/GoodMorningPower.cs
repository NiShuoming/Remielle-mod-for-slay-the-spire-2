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

namespace Remielle.Power
{
    /// <summary>
    /// 早安，共犯小姐p  选择1名队友，本回合双方通过行动获得格挡时，都会为对方给予格挡
    /// </summary>
    public class GoodMorningPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterBlockGained(Creature creature, decimal amount, ValueProp props, CardModel cardSource)
        {
            if(creature == Owner && props.IsPoweredCardOrMonsterMoveBlock())
            {
                await CreatureCmd.GainBlock(Applier, amount, ValueProp.Unpowered, null);
            }
        }

        public override async Task AfterSideTurnEndLate(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            await PowerCmd.Decrement(this);
        }
    }
}
