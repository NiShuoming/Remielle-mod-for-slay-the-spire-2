using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Cards
{
    /// <summary>
    /// （1）浮光掠影 获得6（9）格挡，每有1张手牌，额外获得1格挡。
    /// </summary>
    public class FleetingLight:RemielleCardModel
    {
        public FleetingLight() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CalculationBaseVar(6), new CalculationExtraVar(1),
            new CalculatedBlockVar(ValueProp.Move).WithMultiplier
            ((card,_)=>PileType.Hand.GetPile(card.Owner).Cards.Count(a=>a!=card))];

        protected override void OnUpgrade()
        {
            DynamicVars.CalculationBase.UpgradeValueBy(3);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature,
                DynamicVars.CalculatedBlock.Calculate(Owner.Creature), ValueProp.Move,
                cardPlay);
        }
    }
}
