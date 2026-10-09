using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Models;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.CustomListener
{
    public class DanceStepListener : CustomSingletonModel
    {
        public DanceStepListener() : base(HookType.Combat)
        {
            step = new Dictionary<Player, int>();
        }
        public override bool ShouldReceiveCombatHooks => true;

        public readonly Dictionary<Player, int> step;

        public override Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (side==CombatSide.Player && combatState.RoundNumber == 1)
            {
                step.Clear();
            }
            return Task.CompletedTask;
        }

        public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel clonedBy)
        {
            if(card is IDanceStep dance && oldPileType!=PileType.Hand
                && card.Pile?.Type==PileType.Hand)
            {
                if (!step.ContainsKey(card.Owner))step.Add(card.Owner, 0);
                step[card.Owner]++;
                await dance.DanceStep();
            }
        }
    }
}
