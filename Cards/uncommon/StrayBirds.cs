using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
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
    /// 1）飞鸟集 【羽】的攻击牌伤害+3（4），技能牌格挡+2（3）
    /// </summary>
    public class StrayBirds:RemielleCardModel
    {
        public StrayBirds() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DynamicVar("Attack", 3), new DynamicVar("Defend", 2)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [RemielleKeyWord.Feather];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.Static(StaticHoverTip.Block)];

        protected override void OnUpgrade()
        {
            DynamicVars["Attack"].UpgradeValueBy(1);
            DynamicVars["Defend"].UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<StrayBirdsAPower>(choiceContext, Owner.Creature,
                DynamicVars["Attack"].BaseValue, Owner.Creature, this);
            await PowerCmd.Apply<StrayBirdsDPower>(choiceContext, Owner.Creature,
                DynamicVars["Defend"].BaseValue, Owner.Creature, this);

        }
    }
}
