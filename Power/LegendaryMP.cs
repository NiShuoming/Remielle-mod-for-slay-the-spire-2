using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Power
{
    /// <summary>
    /// （2）传说级的误解体质 【】=》【固有】队友获得负面效果时，将其转移到自己身上,每回合随即减少1层debuff
    /// </summary>
    public class LegendaryMP : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterPowerAmountChanged(PlayerChoiceContext choiceContext, PowerModel power, decimal amount, Creature applier, CardModel cardSource)
        {
            if(power.Owner!=Owner && power.Owner.IsPlayer && power.Owner.GetPowerAmount<LegendaryMP>()<=0)
            {
                await PowerCmd.Apply(choiceContext, power, Owner, power.Amount, power.Applier, cardSource);
                await PowerCmd.Remove(power);
            }
        }
        public override async Task AfterSideTurnStartLate(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(Owner))
            {
                for(int i = 0; i < Amount; i++)
                {
                    var powers = Owner.Powers.Where(a => a.Type == PowerType.Debuff);
                    if (powers != null && powers.Any())
                    {
                        var power = CombatState.RunState.Rng.CombatTargets.NextItem<PowerModel>(powers);
                        await PowerCmd.Decrement(power);
                    }
                }
            }
        }

    }
}
