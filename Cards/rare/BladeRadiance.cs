using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
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
    /// （1）淬锋映曜 回合开始时，消耗1虚耀，获得1能量
    /// </summary>
    public class BladeRadiance:RemielleCardModel
    {
        public BladeRadiance() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<Voidflares>(1), new EnergyVar(1)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Ethereal];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.Static(StaticHoverTip.Energy)];

        protected override void OnUpgrade()
        {
            RemoveKeyword(CardKeyword.Ethereal);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<BladeRadiancePower>(choiceContext,
                Owner.Creature, DynamicVars.Power<Voidflares>().BaseValue,
                Owner.Creature,this);
        }
    }
}
