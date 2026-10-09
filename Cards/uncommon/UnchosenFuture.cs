using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Cards
{
    /// <summary>
    /// （2）未选择的未来 造成12（16）伤害，手牌中每有1个关键词，伤害增加4（6）
    /// </summary>
    public class UnchosenFuture:RemielleCardModel
    {
        public UnchosenFuture():base(2, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CalculationBaseVar(12),
            new ExtraDamageVar(4), new CalculatedDamageVar(ValueProp.Move).
            WithMultiplier((card,_)=>
            (PileType.Hand.GetPile(card.Owner).Cards.Sum(a=>a.Keywords.Count)))];

        protected override void OnUpgrade()
        {
            DynamicVars.CalculationBase.UpgradeValueBy(4);
            DynamicVars.ExtraDamage.UpgradeValueBy(2);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await DamageCmd.Attack(base.DynamicVars.CalculatedDamage)
                .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
        }
    }
}
