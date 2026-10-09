using BaseLib.Extensions;
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
using ZZZLib;

namespace Remielle.Cards
{
    /// <summary>
    /// 1 幻色 本回合目标受到的【异常】伤害提高40（60）%
    /// </summary>
    public class Prismatic:RemielleCardModel
    {
        public Prismatic() : base(1, CardType.Skill, CardRarity.Rare, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<PrismaticPower>(40)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [RemielleKeyWord.Feather];

        protected override void OnUpgrade()
        {
            DynamicVars.Power<PrismaticPower>().UpgradeValueBy(20);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await PowerCmd.Apply<PrismaticPower>(choiceContext, cardPlay.Target,
                DynamicVars.Power<PrismaticPower>().BaseValue, Owner.Creature, this);
        }
    }
}
