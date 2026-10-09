using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using Remielle.CustomListener;
using Remielle.Model;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Remielle.Power
{
    /// <summary>
    /// 可爱的诞生 前n次【舞步】会触发2次
    /// </summary>
    public class AdorableOriginsPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        readonly int maxTimes = 1;
        int times = 0;

        public override async Task AfterCardChangedPiles(CardModel card, PileType oldPileType, AbstractModel clonedBy)
        {
            if(card?.Owner?.Creature == Owner && card.Pile?.Type==PileType.Hand 
                && oldPileType!=PileType.Hand && card is IDanceStep dance && times<Amount)
            {
                var lis = (DanceStepListener)(card.Owner.Creature.
                    CombatState.IterateHookListeners().First(a => a is DanceStepListener));
                for (int i = 0;  i < maxTimes; i++)
                {
                    if (!lis.step.ContainsKey(card.Owner)) lis.step[card.Owner] = 0;
                    lis.step[card.Owner]++;
                    await dance.DanceStep();
                }
                times++;
            }
        }
        public override Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            times = 0;
            return Task.CompletedTask;
        }
    }
}
