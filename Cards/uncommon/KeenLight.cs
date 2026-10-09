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
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Cards
{
    /// <summary>
    /// （1）锐芒 获得6格挡，每有1【虚耀】减少15（20）%所受伤害
    /// </summary>
    public class KeenLight:RemielleCardModel
    {
        public KeenLight() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new BlockVar(7,ValueProp.Move), new PowerVar<KeenLightPower>(15)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<Voidflares>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Power<KeenLightPower>().UpgradeValueBy(5);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);

            await PowerCmd.Apply<KeenLightPower>(choiceContext, Owner.Creature,
                DynamicVars.Power<KeenLightPower>().BaseValue, Owner.Creature, this);

        }
    }
}
