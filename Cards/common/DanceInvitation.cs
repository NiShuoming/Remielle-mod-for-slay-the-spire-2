using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Main;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Cards
{
    /// <summary>
    /// （1）邀舞 【舞步】获得4（6）格挡，给予1【虚弱】。舞步：获得2（3）格挡
    /// </summary>
    public class DanceInvitation:RemielleCardModel,IDanceStep
    {
        public DanceInvitation() : base(1, CardType.Skill, CardRarity.Common, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new BlockVar(4,ValueProp.Move),new PowerVar<WeakPower>(1),
            new BlockVar("Extra",2,ValueProp.Unpowered)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(RemielleKeyWord.DanceStep),
            HoverTipFactory.FromPower<WeakPower>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(2);
            DynamicVars["Extra"].UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);

            await PowerCmd.Apply<WeakPower>(choiceContext, cardPlay.Target,
                DynamicVars.Power<WeakPower>().BaseValue, Owner.Creature, this);
        }

        public async Task DanceStep()
        {
            await CreatureCmd.GainBlock(Owner.Creature, (BlockVar)(DynamicVars["Extra"]), null);

        }
    }
}
