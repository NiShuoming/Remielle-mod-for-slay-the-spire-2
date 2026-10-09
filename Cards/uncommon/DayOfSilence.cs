using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Model;
using Remielle.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Cards
{
    /// <summary>
    /// （3）给予2（4）【流明】，造成目标debuff总量的伤害2次。
    /// </summary>
    public class DayOfSilence : RemielleCardModel
    {
        public DayOfSilence() : base(3, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => [
            new PowerVar<Lumiflux>(2), new RepeatVar(2),
            new CalculationBaseVar(0), new ExtraDamageVar(1),
            new CalculatedDamageVar(ValueProp.Move).WithMultiplier(
                (card,target)=>{if(target==null)return 0;
                return target.Powers.Sum(a =>
            {
                if (a.Type == PowerType.Debuff) return a.Amount;
                return 0;
            });
                })];
        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<Lumiflux>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Power<Lumiflux>().UpgradeValueBy(2);
            EnergyCost.UpgradeBy(-1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await PowerCmd.Apply<Lumiflux>(choiceContext, cardPlay.Target,
                DynamicVars.Power<Lumiflux>().BaseValue, Owner.Creature, this);

            await DamageCmd.Attack(DynamicVars.CalculatedDamage)
                .WithHitCount(DynamicVars.Repeat.IntValue)
                .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
        }
    }
}
