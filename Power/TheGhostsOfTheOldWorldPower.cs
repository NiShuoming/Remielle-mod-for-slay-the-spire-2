using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
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
    /// 旧时代的幽灵p 【伙伴】获得【相变时流】
    /// </summary>
    public class TheGhostsPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Single;

        public override Task BeforeApplied(Creature target, decimal amount, Creature applier, CardModel cardSource)
        {
            if(target.IsPlayer)
            {
                foreach(var card in target.Player.PlayerCombatState.AllCards)
                {
                    TryAddPhaseFlow(card);
                }
            }
            return Task.CompletedTask;
        }

        private static void TryAddPhaseFlow(CardModel card)
        {
            if(card.Keywords.Contains(ZZZKeyWord.Partner))
                card.AddKeyword(RemielleKeyWord.PhaseFlow);
        }

        public override Task AfterRemoved(Creature oldOwner)
        {
            foreach (var card in oldOwner.Player.PlayerCombatState
                    .AllCards.Where(a => a.Keywords.Contains(ZZZKeyWord.Partner)))
            {
                card.RemoveKeyword(RemielleKeyWord.PhaseFlow);
            }
            return Task.CompletedTask;
        }

        public override Task AfterCardEnteredCombat(CardModel card)
        {
            if (card.IsClone || card.Owner.Creature != Owner) return Task.CompletedTask;
               TryAddPhaseFlow(card);
            return Task.CompletedTask;
        }

    }
}
