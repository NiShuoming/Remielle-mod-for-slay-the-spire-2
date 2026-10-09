using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;

namespace Remielle.Cards
{
    /// <summary>
    /// （1）对影 造成9（11）【属性异常】，选择2（3）手牌，本回合保留。
    /// </summary>
    public class MirroredShadow:RemielleCardModel
    {
        public MirroredShadow() : base(1,CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(9, ValueProp.Move|ZZZValueProp.Anomaly),
            new CardsVar(2)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly),
            HoverTipFactory.FromKeyword(CardKeyword.Retain)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2);
            DynamicVars.Cards.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);

                var cards = await CardSelectCmd.FromHand(choiceContext, Owner,
                    new CardSelectorPrefs(SelectionScreenPrompt, 0, DynamicVars.Cards.IntValue),
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
