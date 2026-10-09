using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Cards
{
    /// <summary>
    /// （2=1）与你共赴罪渊  【消耗】替换任意张手牌，本回合保留手牌，
    /// </summary>
    public class IntoTheAbyssWithYou:RemielleCardModel
    {
        public IntoTheAbyssWithYou() : base(2, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(CardKeyword.Retain)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Exhaust,CardKeyword.Retain];

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }
        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
                List<CardModel> list = (await CardSelectCmd.FromHandForDiscard(
                    choiceContext, Owner, new CardSelectorPrefs(
                        base.SelectionScreenPrompt, 0, 999999999), null, this)).ToList();

                await CardCmd.DiscardAndDraw(choiceContext, list, list.Count);
                await PowerCmd.Apply<RetainHandPower>(choiceContext,
                    Owner.Creature, 1, Owner.Creature, this);
        }
    }
}
