using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
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

namespace Remielle.Cards
{
    /// <summary>
    /// （1）乱羽 【羽】7（9）伤害，手牌中每有1张【羽】，多打出一次。
    /// </summary>
    public class TurbidPlume:RemielleCardModel
    {
        public TurbidPlume() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(7, ValueProp.Move), 
            new CalculationBaseVar(1), new CalculationExtraVar(1),
            new CalculatedVar("CalculatedRepeat").WithMultiplier(
                (card,_)=> PileType.Hand.GetPile(card.Owner).Cards
                .Count(a=>a!=card && a.Keywords.Contains(RemielleKeyWord.Feather)))];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [RemielleKeyWord.Feather];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(2);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);

        }

        public override int ModifyCardPlayCount(CardModel card, Creature target, int playCount)
        {
            if(card == this)
            {
                return ((int)((CalculatedVar)DynamicVars["CalculatedRepeat"])
                    .Calculate(Owner.Creature) + playCount -1);
            }
            return playCount;
        }
    }
}
