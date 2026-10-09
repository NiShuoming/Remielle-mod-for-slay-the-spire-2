using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
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
    /// （1）静默的落羽 【羽】【消耗】给予1（2）张手牌【羽】关键词。
    /// </summary>
    public class SilentFallenFeathers:RemielleCardModel
    {
        public SilentFallenFeathers() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CardsVar(1)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [RemielleKeyWord.Feather, CardKeyword.Exhaust];

        protected override void OnUpgrade()
        {
            DynamicVars.Cards.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
                var cards = await CardSelectCmd.FromHand(choiceContext, Owner,
                    new CardSelectorPrefs(SelectionScreenPrompt, 0, DynamicVars.Cards.IntValue),
                    card => !card.Keywords.Contains(RemielleKeyWord.Feather), this);

                if (cards != null)
                {
                    foreach (var card in cards)
                    {
                        card.AddKeyword(RemielleKeyWord.Feather);
                    }
                }
        }
    }
}
