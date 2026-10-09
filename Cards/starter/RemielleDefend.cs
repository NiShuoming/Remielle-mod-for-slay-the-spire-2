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
    internal class RemielleDefend:RemielleCardModel
    {
        public override bool GainsBlock => true;

        protected override HashSet<CardTag> CanonicalTags => [CardTag.Defend];

        protected override List<DynamicVar> CanonicalVars => [new BlockVar(5m, ValueProp.Move)];

        public RemielleDefend()
            : base(1, CardType.Skill, CardRarity.Basic, TargetType.Self)
        {

        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(base.Owner.Creature, base.DynamicVars.Block, cardPlay);
        }

        protected override void OnUpgrade()
        {
            base.DynamicVars.Block.UpgradeValueBy(3m);
        }

    }
}
