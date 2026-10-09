using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
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
    /// （1）月夜游戏 【相变时流】造成8（11）【异常伤害】，给予1【脆弱】
    /// </summary>
    public class MoonlitGames:RemielleCardModel
    {
        public MoonlitGames() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [RemielleKeyWord.PhaseFlow];

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(8, ValueProp.Move | ZZZValueProp.Anomaly),
            new PowerVar<FrailPower>(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(ZZZKeyWord.Anomaly),
            HoverTipFactory.FromPower<FrailPower>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(3);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash").WithValueProp(ZZZValueProp.Anomaly|ValueProp.Move)
                .Execute(choiceContext);

            await PowerCmd.Apply<FrailPower>(choiceContext, cardPlay.Target, 
                DynamicVars.Power<FrailPower>().BaseValue, Owner.Creature, this);
        }
    }
}
