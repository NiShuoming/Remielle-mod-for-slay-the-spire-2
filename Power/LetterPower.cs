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
    /// （1）战术黑板 回合结束时,保留最左边的a张牌
    /// </summary>
    public class LetterPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override Task BeforeSideTurnEndEarly(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (participants.Contains(Owner) && Owner.IsPlayer)
            {
                var cards = PileType.Hand.GetPile(Owner.Player).Cards;
                for(int i = 0;  i < Amount && i<cards.Count; i++)
                {
                    cards[i]?.GiveSingleTurnRetain();
                }
            }
            return Task.CompletedTask;
        }
    }
}
