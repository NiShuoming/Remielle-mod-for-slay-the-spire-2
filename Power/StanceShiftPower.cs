using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
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
    /// （3）转换姿态 【虚无】=》【】回合结束后，如果还在手牌中，下次打出的费用-1
    /// </summary>
    public class StanceShiftPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Single;

        public override Task AfterSideTurnEndLate(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (participants.Contains(Owner) && Owner.IsPlayer)
            {
                var cards = PileType.Hand.GetPile(Owner.Player).Cards;
                foreach(var card in cards)
                {
                    card.EnergyCost.AddUntilPlayed(-1);
                }
            } 
            return Task.CompletedTask;
        }
    }
}
