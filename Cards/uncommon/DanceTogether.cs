using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using Remielle.Main;
using Remielle.Model;
using Remielle.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Powers;

namespace Remielle.Cards
{
    /// <summary>
    /// 1 共舞 【消耗】【舞步】下一张攻击、技能牌会多打出1（2）次。舞步：获得2【异常精通】
    /// </summary>
    public class DanceTogether:RemielleCardModel,IDanceStep
    {
        public DanceTogether() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new RepeatVar(1), new PowerVar<AnomalyProficiency>(2)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Exhaust];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(RemielleKeyWord.DanceStep),
            HoverTipFactory.FromPower<AnomalyProficiency>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Repeat.UpgradeValueBy(1);
            DynamicVars.Power<AnomalyProficiency>().UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<DanceTogetherPower>(choiceContext, Owner.Creature,
                DynamicVars.Repeat.BaseValue, Owner.Creature, this);
        }

        public async Task DanceStep()
        {
            await PowerCmd.Apply<AnomalyProficiencyOneTurn>(new ThrowingPlayerChoiceContext(),
                Owner.Creature, DynamicVars.Power<AnomalyProficiency>().BaseValue,
                Owner.Creature, this);
        }
    }
}
