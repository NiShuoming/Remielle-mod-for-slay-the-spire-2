using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Nodes.Combat;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Potions
{
    /// <summary>
    /// 羽翼药水 选择1张手牌，将这张牌的2张虚无复制品加入手牌
    /// </summary>
    public class FeatherPotion : RemiellePotionModel
    {
        public override PotionRarity Rarity => PotionRarity.Uncommon;

        public override PotionUsage Usage => PotionUsage.CombatOnly;

        public override TargetType TargetType => TargetType.AnyPlayer;

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CardsVar(2)];

        public override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(CardKeyword.Ethereal)];

        protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature target)
        {
            var cards = (await CardSelectCmd.FromHand(choiceContext, target.Player,
                new CardSelectorPrefs(SelectionScreenPrompt, 1),
                card => !card.Keywords.Contains(CardKeyword.Retain) && !card.ShouldRetainThisTurn,
                this)).FirstOrDefault();

            for(int i = 0; i < DynamicVars.Cards.IntValue; i++)
            {
                CardModel card = cards.CreateClone();
                CardCmd.ApplyKeyword(card, CardKeyword.Ethereal);
                await CardPileCmd.AddGeneratedCardToCombat(card, PileType.Hand, base.Owner);
            }
            
        }
    }
}
