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

namespace Remielle.Cards
{
    /// <summary>
    /// （1）步伐 【舞步】获得3格挡2（3）次，抽1。舞步：本回合获得1敏捷
    /// </summary>
    public class Gait:RemielleCardModel, IDanceStep
    {
        public Gait() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new BlockVar(3,ValueProp.Move), new RepeatVar(2)
            ,new PowerVar<DexterityPower>(1), new CardsVar(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(RemielleKeyWord.DanceStep),
            HoverTipFactory.FromPower<DexterityPower>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Repeat.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            for(int i = 0;i<DynamicVars.Repeat.IntValue;i++)
            {
                await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block,
                    cardPlay);
                await Cmd.CustomScaledWait(.1f, .2f);
            }
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
        }

        public async Task DanceStep()
        {
            await PowerCmd.Apply<GaitPower>(new ThrowingPlayerChoiceContext(),
                Owner.Creature, DynamicVars.Power<DexterityPower>().BaseValue,
                Owner.Creature, this);
        }
    }
}
