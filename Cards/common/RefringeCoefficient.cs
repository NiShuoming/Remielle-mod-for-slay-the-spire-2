using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
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
    /// 异化系数 0 本回合获得3（5）【异常精通】
    /// </summary>
    public class RefringeCoefficient:RemielleCardModel
    {
        public RefringeCoefficient() : base(0, CardType.Skill, CardRarity.Common, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<AnomalyProficiency>(3)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<AnomalyProficiency>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Power<AnomalyProficiency>().UpgradeValueBy(2);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<AnomalyProficiencyOneTurn>(choiceContext,
                Owner.Creature, DynamicVars.Power<AnomalyProficiency>().BaseValue,
                Owner.Creature, this);
        }
    }
}
