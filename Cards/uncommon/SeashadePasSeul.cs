using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Main;
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
    /// （2）影池独舞 【舞步】获得12（16）格挡，获得1（2）【虚耀】。舞步：随机1虚弱
    /// </summary>
    public class SeashadePasSeul:RemielleCardModel,IDanceStep
    {
        public SeashadePasSeul() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new BlockVar(12, ValueProp.Move), new PowerVar<Voidflares>(1),
            new PowerVar<WeakPower>(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(RemielleKeyWord.DanceStep),
            HoverTipFactory.FromPower<Voidflares>(),
            HoverTipFactory.FromPower<WeakPower>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(4);
            DynamicVars.Power<Voidflares>().UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
            await ZZZCmd.AddSecondEnergy(Owner, DynamicVars.Power<Voidflares>().IntValue, 
                Owner.Creature, this);
        }

        public async Task DanceStep()
        {
            await PowerCmd.Apply<WeakPower>(new ThrowingPlayerChoiceContext(),
                 Owner.RunState.Rng.CombatTargets.NextItem(CombatState.HittableEnemies),
                 DynamicVars.Power<WeakPower>().BaseValue, Owner.Creature, this);
        }

    }
}
