using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
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
    /// 回合结束时，抽【伙伴】，获得a格挡
    /// </summary>
    public class Accomplice1Power : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel clonedBy)
        {
            if (card.Owner.Creature == Owner && card.Keywords.Contains(ZZZKeyWord.Partner)
                && card.Pile.Type == PileType.Hand && oldPileType != PileType.Hand)
            {
                await CreatureCmd.GainBlock(Owner, Amount, ValueProp.Unpowered, null);

                await PowerCmd.Apply<Lumiflux>(new ThrowingPlayerChoiceContext(), 
                    card.Owner.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies),
                    1, Owner, null);
            }

        }
    }
}
