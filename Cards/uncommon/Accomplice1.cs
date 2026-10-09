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
using ZZZLib;

namespace Remielle.Cards
{
    /// <summary>
    /// （1）头号共犯 每抽到1张【伙伴】，获得2（4）格挡，随机1【流明】
    /// </summary>
    public class Accomplice1: RemielleCardModel
    {
        public Accomplice1():base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self){}

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DynamicVar("Acc", 2), new PowerVar<Lumiflux>(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Partner),
            HoverTipFactory.Static(StaticHoverTip.Block),
            HoverTipFactory.FromPower<Lumiflux>()];

        protected override void OnUpgrade()
        {
            DynamicVars["Acc"].UpgradeValueBy(2);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<Accomplice1Power>(choiceContext, Owner.Creature,
                DynamicVars["Acc"].BaseValue, Owner.Creature, this);
        }
    }
}
