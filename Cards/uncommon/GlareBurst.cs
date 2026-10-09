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
    /// （2）相变·耀光 【相变时流】造成12（16）伤害，给予1(2)【流明】，召回到手中。
    /// </summary>
    public class GlareBurst:RemielleCardModel
    {
        public GlareBurst() : base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(12, ValueProp.Move), new PowerVar<Lumiflux>(1)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [RemielleKeyWord.PhaseFlow];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<Lumiflux>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(4);
            DynamicVars.Power<Lumiflux>().UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);

            await PowerCmd.Apply<Lumiflux>(choiceContext, cardPlay.Target,
                DynamicVars.Power<Lumiflux>().BaseValue, Owner.Creature, this);
        }

        protected override CardLocation GetResultLocationForCardPlay()
        {
            CardLocation resultLocationForCardPlay = base.GetResultLocationForCardPlay();
            if (resultLocationForCardPlay.pileType == PileType.Discard)
            {
                resultLocationForCardPlay.pileType = PileType.Hand;
            }
            return resultLocationForCardPlay;
        }

    }
}
