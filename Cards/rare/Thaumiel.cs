using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
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
    /// （2）塔乌弥尔 【相变时流】=》【保留】造成8（11）伤害。第一次打出【相变时流】时，自动打出。
    /// </summary>
    public class Thaumiel:RemielleCardModel
    {
        public Thaumiel() : base(2, CardType.Attack, CardRarity.Rare, TargetType.RandomEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(8,ValueProp.Move)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [RemielleKeyWord.PhaseFlow];

        protected override void OnUpgrade()
        {
            AddKeyword(CardKeyword.Retain);
            DynamicVars.Damage.UpgradeValueBy(3);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue)
                .FromCard(this, cardPlay).TargetingRandomOpponents(Owner.Creature.CombatState)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
        }

        bool did = false;

        public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if(cardPlay!=null && cardPlay.Card.Keywords.Contains(RemielleKeyWord.PhaseFlow) &&
                cardPlay.Player==Owner && !did)
            {
                did = true;
                await CardCmd.AutoPlay(choiceContext, this, null);
            }
        }

        public override Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            did = false;
            return Task.CompletedTask;
        }
    }
}
