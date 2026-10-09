using BaseLib.Extensions;
using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Powers;

namespace Remielle.Power
{
    public class TemporalLumifluxPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterSideTurnStartLate(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if(Owner.IsPlayer && participants.Contains(Owner))
            {
                int n= Owner.Player.RunState.Rng.CombatTargets.NextInt(1);
                if(n==0)
                await PowerCmd.Apply<WeakPower>(new ThrowingPlayerChoiceContext(),
                     CombatState.HittableEnemies,
                     Amount, Owner, null);
                else
                await PowerCmd.Apply<VulnerablePower>(new ThrowingPlayerChoiceContext(),
                     CombatState.HittableEnemies,
                     Amount, Owner, null);
            }
        }
    }
}
