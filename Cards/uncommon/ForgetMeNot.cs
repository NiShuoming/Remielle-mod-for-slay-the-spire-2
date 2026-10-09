using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Cards
{
    /// <summary>
    /// 勿忘我 0 【消耗】获得2（3）能量，给予1张手牌【保留】
    /// </summary>
    public class ForgetMeNot:RemielleCardModel
    {
        public ForgetMeNot() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new EnergyVar(2)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Exhaust];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(CardKeyword.Retain),
            HoverTipFactory.Static(StaticHoverTip.Energy)];

        protected override void OnUpgrade()
        {
            DynamicVars.Energy.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PlayerCmd.GainEnergy(DynamicVars.Energy.BaseValue, Owner);

                var cards = await CardSelectCmd.FromHand(choiceContext, Owner,
                    new CardSelectorPrefs(SelectionScreenPrompt, 0, 1),
                    card => !card.Keywords.Contains(CardKeyword.Retain) && !card.ShouldRetainThisTurn,
                    this);

                if (cards != null)
                {
                    foreach (var card in cards)
                    {
                        card.AddKeyword(CardKeyword.Retain);
                    }
                }
        }
    }
}
