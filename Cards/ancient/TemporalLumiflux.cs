using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
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
    /// 虚狩·时隙流明 （2）【】=【保留】回合开始时，给予所有敌人1层【易伤】或【脆弱】
    /// </summary>
    public class TemporalLumiflux:RemielleCardModel
    {
        public TemporalLumiflux() : base(2, CardType.Power, CardRarity.Ancient, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DynamicVar("Apply",1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<VulnerablePower>(),
            HoverTipFactory.FromPower<FrailPower>()];

        protected override void OnUpgrade()
        {
            AddKeyword(CardKeyword.Retain);  
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<TemporalLumifluxPower>(choiceContext,
                Owner.Creature, 1, Owner.Creature, this);
        }
    }
}
