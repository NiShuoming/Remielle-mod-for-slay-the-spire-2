using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
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
using ZZZLib.Cmds;

namespace Remielle.Cards
{
    /// <summary>
    /// 1 垂虹 造成10（13）伤害，消耗1【虚耀】给予1【虚弱】1【易伤】
    /// </summary>
    public class RainbowEnd:RemielleCardModel
    {
        public RainbowEnd() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(10,ValueProp.Move),new PowerVar<Voidflares>(1),
            new PowerVar<WeakPower>(1), new PowerVar<VulnerablePower>(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<Voidflares>(),
            HoverTipFactory.FromPower<WeakPower>(),
            HoverTipFactory.FromPower<VulnerablePower>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(3);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);

            if (ZZZCmd.GetSecondEnergy(Owner).Energy2 >= DynamicVars.Power<Voidflares>().IntValue)
            {
                await PowerCmd.Apply<WeakPower>(choiceContext, cardPlay.Target,
                    DynamicVars.Power<WeakPower>().BaseValue, Owner.Creature, this);
                await PowerCmd.Apply<VulnerablePower>(choiceContext, cardPlay.Target,
                    DynamicVars.Power<VulnerablePower>().BaseValue, Owner.Creature, this);
                await ZZZCmd.AddSecondEnergy(Owner, -1, Owner.Creature, this);

            }
        }
    }
}
