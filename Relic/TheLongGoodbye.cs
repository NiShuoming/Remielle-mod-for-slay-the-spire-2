using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using Remielle.Main;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Relic
{
    /// <summary>
    /// 漫长的告别 每回合第一个【相变时流】会多打出1次
    /// </summary>
    public class TheLongGoodbye : RemielleRelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Rare;

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(RemielleKeyWord.PhaseFlow)];

        int n = 0;

        public override int ModifyCardPlayCount(CardModel card, Creature target, int playCount)
        {
            if(card.Owner == Owner && card.Keywords.Any(a => 
            a == RemielleKeyWord.PhaseFlow) && n<1)
            {
                n++;
                return playCount + 1;
            }
            return playCount;
        }

        public override Task BeforeSideTurnStart(PlayerChoiceContext choiceContext, CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(Owner.Creature))
            {
                n = 0;
            }
            return Task.CompletedTask;
        }
    }
}
