using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Main;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static BaseLib.Utils.BetaMainCompatibility;

namespace Remielle.Cards
{
    /// <summary>
    /// 独舞 1 【舞步】造成7（8）点伤害，丢弃1(2)张牌=》指定。舞步：造成3（4）群伤
    /// </summary>
    public class SoloDance : RemielleCardModel, IDanceStep
    {
        public SoloDance() : base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(7,ValueProp.Move), new CardsVar(1),
            new DamageVar("Damage2", 3, ValueProp.Unpowered)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(RemielleKeyWord.DanceStep)];

        protected override void OnUpgrade()
        {
            DynamicVars.Cards.UpgradeValueBy(1);
            DynamicVars.Damage.UpgradeValueBy(1);
            DynamicVars["Damage2"].UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);

                CardModel card;
                card = (await CardSelectCmd.FromHand(choiceContext, Owner,
                        new CardSelectorPrefs(SelectionScreenPrompt, 0, DynamicVars.Cards.IntValue),
                        null, this)).FirstOrDefault();
                if (card != null)
                {
                    await CardCmd.Discard(choiceContext, card);
                }
        }

        public async Task DanceStep()
        {
            await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(),
                Owner.Creature.CombatState.HittableEnemies,
                DynamicVars["Damage2"].BaseValue, ValueProp.Unpowered,
                Owner.Creature);
        }
    }
}
