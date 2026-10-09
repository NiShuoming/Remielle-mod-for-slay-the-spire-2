using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Model;
using Remielle.Power;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using ZZZLib;

namespace Remielle.Cards
{
    /// <summary>
    /// （x）缭乱终幕 【终结技】造成X次12(15【异常伤害】，给予能量消耗量的【流明】
    /// </summary>
    public class DazzlingCurtainCall:RemielleCardModel
    {
        public DazzlingCurtainCall() : base(0, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }

        protected override bool HasEnergyCostX => true;

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(12,ValueProp.Move | ZZZValueProp.Anomaly)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [ZZZKeyWord.Ultimate];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly),
            HoverTipFactory.FromPower<Lumiflux>()];
        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(3);
        }
        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            int a = EnergyCost.CapturedXValue;
            int num = ResolveEnergyXValue();
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
                .WithHitCount(num).FromCard(this, cardPlay).WithValueProp(ValueProp.Move|ZZZValueProp.Anomaly)
                .Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
            if (cardPlay.Target.IsAlive)
            {
                await PowerCmd.Apply<Lumiflux>(choiceContext, cardPlay.Target
                    , a, Owner.Creature, this);
            }
        }
    }
}
