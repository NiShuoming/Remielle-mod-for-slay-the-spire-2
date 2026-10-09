using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
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
    /// 2 空羽呢喃 【相变时流】【羽】获得7（10）格挡
    /// </summary>
    public class FeatheredMurmurs : RemielleCardModel
    {
        public FeatheredMurmurs() : base(2, CardType.Skill, CardRarity.Common, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new BlockVar(7, ValueProp.Move)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [RemielleKeyWord.PhaseFlow, RemielleKeyWord.Feather];

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(3);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature,
                DynamicVars.Block, cardPlay);
        }
    }
}
