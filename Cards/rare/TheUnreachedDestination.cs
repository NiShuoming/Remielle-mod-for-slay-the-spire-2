using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Cards
{
    /// <summary>
    /// （1=0）未能抵达的终点 【消耗】选择抽排堆中的1张牌，将这张牌的虚无复制品添加到手牌
    /// </summary>
    public class TheUnreachedDestination:RemielleCardModel
    {
        public TheUnreachedDestination():base(1, CardType.Skill, CardRarity.Rare, TargetType.Self){}

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Exhaust];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(CardKeyword.Ethereal)];

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
                CardModel card = (await CardSelectCmd.FromSimpleGrid(choiceContext, PileType.Draw.GetPile(Owner).Cards,
                    Owner, new CardSelectorPrefs(this.SelectionScreenPrompt, 1))).FirstOrDefault();

                if (card != null)
                {
                    CardModel card2 = card.CreateClone();
                    card2.AddKeyword(CardKeyword.Ethereal);
                    await CardPileCmd.Add(card2, PileType.Hand.GetPile(Owner));
                }
        }
    }
}
