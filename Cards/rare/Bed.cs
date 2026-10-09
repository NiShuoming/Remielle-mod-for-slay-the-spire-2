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
using ZZZLib.Cmds;
using ZZZLib.Powers;

namespace Remielle.Cards
{
    /// <summary>
    /// 0 蕾米埃尔的床 【消耗】获得1能量、1【虚耀】1(2【异常精通】、抽1（2）张牌
    /// </summary>
    public class Bed:RemielleCardModel
    {
        public Bed() : base(0, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new EnergyVar(1), new PowerVar<Voidflares>(1),
            new PowerVar<AnomalyProficiency>(1), new CardsVar(1)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Exhaust];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<Voidflares>(), HoverTipFactory.Static(StaticHoverTip.Energy),
        HoverTipFactory.FromPower<AnomalyProficiency>(1)];

        protected override void OnUpgrade()
        {
            DynamicVars.Power<AnomalyProficiency>().UpgradeValueBy(1);
            DynamicVars.Cards.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PlayerCmd.GainEnergy(DynamicVars.Energy.IntValue, Owner);
            await ZZZCmd.AddSecondEnergy(Owner, DynamicVars.Power<Voidflares>().IntValue,
                Owner.Creature, this);
            await PowerCmd.Apply<AnomalyProficiency>(choiceContext, Owner.Creature,
                DynamicVars.Power<AnomalyProficiency>().BaseValue, Owner.Creature, this);
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
        }
    }
}
