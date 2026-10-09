using BaseLib.Extensions;
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

namespace Remielle.Cards
{
    /// <summary>
    /// （3）相变·聚色 【相变时流】群伤10，如果通过【相变时流】打出，下次打出的伤害+8（12）
    /// </summary>
    public class HueConvergence:RemielleCardModel
    {
        public HueConvergence() : base(3, CardType.Attack, CardRarity.Uncommon, TargetType.AllEnemies) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(10m, ValueProp.Move), new DynamicVar("Increase", 8m) ];

        private decimal _extraDamageFromPlays;

        private decimal ExtraDamageFromPlays
        {
            get
            {
                return _extraDamageFromPlays;
            }
            set
            {
                AssertMutable();
                _extraDamageFromPlays = value;
            }
        }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [RemielleKeyWord.PhaseFlow];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<Lumiflux>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2);
            DynamicVars["Increase"].UpgradeValueBy(4);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay)
                .WithHitFx("vfx/vfx_attack_slash").TargetingAllOpponents(Owner.Creature.CombatState)
                .Execute(choiceContext);
        }

        public override Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if(cardPlay.Card==this && cardPlay.IsAutoPlay)
            {
                base.DynamicVars.Damage.BaseValue += base.DynamicVars["Increase"].BaseValue;
                ExtraDamageFromPlays += base.DynamicVars["Increase"].BaseValue;
            }
            return Task.CompletedTask;
        }

        protected override void AfterDowngraded()
        {
            base.AfterDowngraded();
            base.DynamicVars.Damage.BaseValue += ExtraDamageFromPlays;
        }

    }
}
