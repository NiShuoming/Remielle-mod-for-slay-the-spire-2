using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Hooks;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Model;
using Remielle.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;
using ZZZLib.Cmds;

namespace Remielle.Cards
{
    /// <summary>
    /// （0）惊鸿 对敌人造成6(8)【异常伤害】，消耗X【虚耀】每1层额外造成6（8）,如果3虚耀，额外造成6（8）
    /// </summary>
    public class FleetingGrace:RemielleCardModel
    {
        public FleetingGrace() : base(0, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CalculationBaseVar(6), new ExtraDamageVar(6), 
            new CalculatedDamageVar(ValueProp.Move|ZZZValueProp.Anomaly)
            .WithMultiplier((card,_)=>{
                int n = Hook.ModifyXValue(card.Owner.Creature.CombatState, card, 
                    ZZZCmd.GetSecondEnergy(card.Owner).Energy2);
                return n>=3?n+1:n;
            })];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
           [HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly), 
            HoverTipFactory.FromPower<Voidflares>()];

        protected override void OnUpgrade()
        {
            DynamicVars.CalculationBase.UpgradeValueBy(2);
            DynamicVars.ExtraDamage.UpgradeValueBy(2);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            int n = ZZZCmd.GetSecondEnergy(Owner).Energy2;
            await DamageCmd.Attack(base.DynamicVars.CalculatedDamage)
                    .FromCard(this, cardPlay).WithValueProp(ValueProp.Move | ZZZValueProp.Anomaly)
                    .WithHitFx("vfx/vfx_attack_slash").Targeting(cardPlay.Target)
                    .Execute(choiceContext);
            await ZZZCmd.AddSecondEnergy(Owner, -n, Owner.Creature, this);
        }
    }
}
