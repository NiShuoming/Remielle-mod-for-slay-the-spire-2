using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using Remielle.Main;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Cards
{
    /// <summary>
    /// （2）月夜密语 【相变时流】下回合获得1（2）能量，额外抽1=2
    /// </summary>
    public class MoonlightWhispers:RemielleCardModel
    {
        public MoonlightWhispers() : base(2, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new EnergyVar(1), new CardsVar(1)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [RemielleKeyWord.PhaseFlow];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.Static(StaticHoverTip.Energy)];

        protected override void OnUpgrade()
        {
            DynamicVars.Energy.UpgradeValueBy(1);
            DynamicVars.Cards.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<EnergyNextTurnPower>(choiceContext, Owner.Creature,
                DynamicVars.Energy.BaseValue, Owner.Creature, this);
            await PowerCmd.Apply<DrawCardsNextTurnPower>(choiceContext, Owner.Creature,
                DynamicVars.Cards.BaseValue, Owner.Creature, this);
        }


    }
}
