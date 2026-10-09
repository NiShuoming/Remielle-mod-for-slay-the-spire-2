using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.CardPools;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Main;
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
    /// （0）倏梦镌影 5【异常伤害】，本回合抽牌大于等于2时，攻击2（3）次
    /// </summary>
    public class FleetingDreamForeverEtched:RemielleCardModel
    {
        public FleetingDreamForeverEtched() : base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(5,ValueProp.Move|ZZZValueProp.Anomaly),
            new RepeatVar(2), new CardsVar(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [RemielleKeyWord.Feather];

        protected override void OnUpgrade()
        {
            DynamicVars.Repeat.UpgradeValueBy(1);
        }
        int draw = 0;
        protected override bool ShouldGlowGoldInternal => draw>= DynamicVars.Cards.IntValue;
        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash").WithHitCount(draw>=DynamicVars.Cards.IntValue?
                DynamicVars.Repeat.IntValue:1).WithValueProp(ValueProp.Move|ZZZValueProp.Anomaly)
                .Execute(choiceContext);

        }

        public override Task AfterCardDrawn(PlayerChoiceContext choiceContext, CardModel card, bool fromHandDraw)
        {
            if(!fromHandDraw)
                draw++;
            return Task.CompletedTask;
        }

        public override Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            draw = 0;
            return Task.CompletedTask;
        }
    }
}
