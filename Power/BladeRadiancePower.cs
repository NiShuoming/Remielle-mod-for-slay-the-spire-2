using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cmds;

namespace Remielle.Power
{
    /// <summary>
    /// 淬锋映曜p 回合开始时，消耗1虚耀，获得1能量
    /// </summary>
    public class BladeRadiancePower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(Owner) && Owner.IsPlayer)
            {
                var energy = ZZZCmd.GetSecondEnergy(Owner.Player).Energy2;
                int n = Amount>energy?energy:Amount;
                if(n > 0)
                {
                    await ZZZCmd.AddSecondEnergy(Owner.Player,-n,Owner,null);
                    await PlayerCmd.GainEnergy(n, Owner.Player);
                }
            }
        }
    }
}
