using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Potions
{
    /// <summary>
    /// 相变血清 给予一张手牌保留
    /// </summary>
    public class PhaseFlowSerum : RemiellePotionModel
    {
        public override PotionRarity Rarity => PotionRarity.Common;

        public override PotionUsage Usage => PotionUsage.CombatOnly;

        public override TargetType TargetType => TargetType.AnyPlayer;

        public override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(CardKeyword.Retain)];

        protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature target)
        {
            var cards = (await CardSelectCmd.FromHand(choiceContext, target.Player,
                new CardSelectorPrefs(SelectionScreenPrompt, 1),
                card => !card.Keywords.Contains(CardKeyword.Retain),
                this)).FirstOrDefault();

            cards?.AddKeyword(CardKeyword.Retain);

        }
    }
}
