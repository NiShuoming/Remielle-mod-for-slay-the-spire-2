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
using ZZZLib;

namespace Remielle.Cards
{
    /// <summary>
    /// （0）吾本如斯 本回合造成【异常】伤害时，获得3（5）格挡。
    /// </summary>
    public class ThisIsHowIAm:RemielleCardModel
    {
        public ThisIsHowIAm() : base(0, CardType.Skill, CardRarity.Uncommon,TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new BlockVar(3,ValueProp.Unpowered)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly)];

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(2);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<ThisIsHowIAmPower>(choiceContext, Owner.Creature,
                DynamicVars.Block.BaseValue, Owner.Creature, this);
        }
    }
}
