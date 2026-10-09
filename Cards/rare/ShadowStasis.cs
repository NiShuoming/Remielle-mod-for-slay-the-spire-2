using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
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
    /// （3）相变·滞影 【相变时流】击晕第一个攻击自己的敌人，并造成20（30）伤害
    /// </summary>
    public class ShadowStasis:RemielleCardModel
    {
        public ShadowStasis() : base(3, CardType.Power, CardRarity.Rare, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<ShadowStasisPower>(20)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [RemielleKeyWord.PhaseFlow];

        protected override void OnUpgrade()
        {
            DynamicVars.Power<ShadowStasisPower>().UpgradeValueBy(10);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<ShadowStasisPower>(choiceContext, Owner.Creature,
                DynamicVars.Power<ShadowStasisPower>().BaseValue, Owner.Creature, this);
        }
    }
}
