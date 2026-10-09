using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
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
    /// （1）余温 给予1【虚弱】1(2)【易伤】1（2）【流明】
    /// </summary>
    public class LingeringWarmth:RemielleCardModel
    {
        public LingeringWarmth() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<WeakPower>(1), new PowerVar<VulnerablePower>(1),
             new PowerVar<Lumiflux>(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<WeakPower>(),HoverTipFactory.FromPower<VulnerablePower>(),
             HoverTipFactory.FromPower<Lumiflux>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Power<VulnerablePower>().UpgradeValueBy(1);
            DynamicVars.Power<Lumiflux>().UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<WeakPower>(choiceContext, cardPlay.Target,
                DynamicVars.Power<WeakPower>().BaseValue, Owner.Creature, this);
            await PowerCmd.Apply<VulnerablePower>(choiceContext, cardPlay.Target,
                DynamicVars.Power<VulnerablePower>().BaseValue, Owner.Creature, this);
            await PowerCmd.Apply<Lumiflux>(choiceContext, cardPlay.Target,
                DynamicVars.Power<Lumiflux>().BaseValue, Owner.Creature, this);

        }
    }
}
