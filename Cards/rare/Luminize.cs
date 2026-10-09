using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Main;
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
    /// （2）耀变 给予【流明】、触发【异化】时，造成6（9）【属性异常】伤害，抽1
    /// </summary>
    public class Luminize:RemielleCardModel
    {
        public Luminize() : base(2, CardType.Power, CardRarity.Rare, TargetType.Self) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<Lumiflux>(),
            HoverTipFactory.FromKeyword(RemielleKeyWord.Refringe),
            HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly)];

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(6, ValueProp.Unpowered|ZZZValueProp.Anomaly)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(3);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<LuminizePower>(choiceContext, Owner.Creature,
                DynamicVars.Damage.BaseValue, Owner.Creature, this);
        }
    }
}
