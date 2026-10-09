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

namespace Remielle.Cards
{
    /// <summary>
    /// （1）可爱的诞生 前2（4）次【舞步】会触发2次
    /// </summary>
    public class AdorableOrigins:RemielleCardModel
    {
        public AdorableOrigins():base(1,CardType.Power,CardRarity.Uncommon,TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new RepeatVar(2)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(RemielleKeyWord.DanceStep)];

        protected override void OnUpgrade()
        {
            DynamicVars.Repeat.UpgradeValueBy(2);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<AdorableOriginsPower>(choiceContext, Owner.Creature,
                DynamicVars.Repeat.BaseValue, Owner.Creature, this);
        }
    }
}
