using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using Remielle.Main;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Cards
{
    /// <summary>
    /// （1）展翼高飞 【羽】抽2（3），选择1张牌，本回合保留
    /// </summary>
    public class SpreadWings:RemielleCardModel
    {
        public SpreadWings() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CardsVar(2)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [RemielleKeyWord.Feather];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(CardKeyword.Retain)];

        protected override void OnUpgrade()
        {
            DynamicVars.Cards.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue ,Owner);
            
                var cards = await CardSelectCmd.FromHand(choiceContext, Owner,
                new CardSelectorPrefs(SelectionScreenPrompt, 0, 1),
                card => !card.Keywords.Contains(CardKeyword.Retain) && !card.ShouldRetainThisTurn,
                this);
                if (cards != null)
                {
                    foreach (var card in cards)
                    {
                        card.GiveSingleTurnRetain();
                    }
                }
        }
    }
}
