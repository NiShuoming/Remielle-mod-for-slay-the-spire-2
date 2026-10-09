using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Main;
using Remielle.Power;
using Remielle.Relic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.CustomModel
{
    /// <summary>
    /// 羽 监测
    /// </summary>
    public class FeatherListener : CustomSingletonModel
    {
        public FeatherListener() : base(HookType.Combat) 
        {
            feathers = [];
        }

        public override bool ShouldReceiveCombatHooks => true;

        private readonly Dictionary<Player, int> feathers = [];

        public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay==null || !cardPlay.Card.Keywords.Contains(RemielleKeyWord.Feather)) return;
            if (!feathers.ContainsKey(cardPlay.Player)) feathers[cardPlay.Player] = 0;

            await Cmd.Wait(0.1f);

            ICombatState combatState = cardPlay.Player.Creature.CombatState;
            List<Creature> targets = [.. combatState.HittableEnemies];

            if(targets==null || targets.Count==0 ||
                CombatManager.Instance.IsOverOrEnding || cardPlay.Card.Owner.Creature.IsDead) return;

            if (cardPlay.Player.Creature.GetPowerAmount<PetalsAndSliverPower>()<=0)
                await CreatureCmd.Damage(choiceContext,
                    cardPlay.Player.RunState.Rng.CombatTargets.NextItem(targets),
                    new DamageVar(ReGlobalData.FeatherBaseDamage+ feathers[cardPlay.Player] * ReGlobalData.FeatherExtraDamage, ValueProp.Unpowered), 
                    cardPlay.Player.Creature);

            else
                await CreatureCmd.Damage(choiceContext,targets,
                    new DamageVar(ReGlobalData.FeatherBaseDamage + feathers[cardPlay.Player] * ReGlobalData.FeatherExtraDamage, ValueProp.Unpowered),
                    cardPlay.Player.Creature);

            feathers[cardPlay.Player] ++;
        }

        public override Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            foreach (Creature creature in participants)
            {
                if(creature.IsPlayer)
                {
                    feathers[creature.Player] = creature.Player.Relics.Any
                        (a=>a is LoneFeatherEndlessFlight)?3:0;
                }
            }
            return Task.CompletedTask; 
        }
    }
}
