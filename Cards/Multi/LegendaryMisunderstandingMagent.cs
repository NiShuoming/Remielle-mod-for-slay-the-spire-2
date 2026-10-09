using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
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
    /// （1）传说级的误解体质 【】=》【固有】队友获得负面效果时，将其转移到自己身上
    /// </summary>
    public class LegendaryMisunderstandingMagent:RemielleCardModel
    {
        public LegendaryMisunderstandingMagent() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }
        public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

        protected override void OnUpgrade()
        {
            AddKeyword(CardKeyword.Innate);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<LegendaryMP>(choiceContext, Owner.Creature, 1, Owner.Creature, this);
        }
    }
}
