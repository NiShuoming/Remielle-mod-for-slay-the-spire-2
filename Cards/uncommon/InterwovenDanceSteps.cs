using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
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
    /// （1）交叠舞步 【舞步】造成8（11）伤害，给予所有敌人1【流明】。舞步：随机升级1张手牌
    /// </summary>
    public class InterwovenDanceSteps:RemielleCardModel,IDanceStep
    {
        public InterwovenDanceSteps() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(8, ValueProp.Move),new PowerVar<Lumiflux>(1),
            new CardsVar(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(RemielleKeyWord.DanceStep),
            HoverTipFactory.FromPower<Lumiflux>()];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2);
            DynamicVars.Cards.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
            await PowerCmd.Apply<Lumiflux>(choiceContext, CombatState.HittableEnemies,
                DynamicVars.Power<Lumiflux>().BaseValue, Owner.Creature, this);
        }

        public async Task DanceStep()
        {
            var cards = PileType.Hand.GetPile(Owner).Cards.Where(
                a => a.IsUpgradable);

            for(int i = 0; i < DynamicVars.Cards.IntValue; i++)
            {
                var card = Owner.RunState.Rng.CombatCardSelection.NextItem(cards);

                if (card != null)
                {
                    await Cmd.Wait(0.1f);
                    CardCmd.Upgrade(card);
                }
            }
        }
    }
}
