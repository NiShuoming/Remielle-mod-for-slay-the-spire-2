using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Powers;

namespace Remielle.Cards
{
    /// <summary>
    /// （1）夕阳灯塔 【消耗】目标每有1种debuff，获得1（2）【异常精通】
    /// </summary>
    public class SunsetLighthouse:RemielleCardModel
    {
        public SunsetLighthouse() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<AnomalyProficiency>(1)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Exhaust];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<AnomalyProficiency>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Power<AnomalyProficiency>().UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            if (cardPlay.Target == null) return;
            int n = cardPlay.Target.Powers.Count(a => a.Type == PowerType.Debuff);
            await PowerCmd.Apply<AnomalyProficiency>(choiceContext, Owner.Creature,
                n * DynamicVars.Power<AnomalyProficiency>().BaseValue, Owner.Creature, this);

        }
    }
}
