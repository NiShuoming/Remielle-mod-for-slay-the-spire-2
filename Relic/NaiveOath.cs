using BaseLib.Extensions;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using Remielle.Model;
using Remielle.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cmds;

namespace Remielle.Relic
{
    /// <summary>
    /// 青涩誓言 开局2流明
    /// </summary>
    public class NaiveOath : RemielleRelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Common;

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<Lumiflux>(2)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<Lumiflux>()];

        public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if (participants.Contains(Owner.Creature) && combatState.RoundNumber == 1)
            {
                await PowerCmd.Apply<Lumiflux>(new ThrowingPlayerChoiceContext(),
                    combatState.HittableEnemies, DynamicVars.Power<Lumiflux>().BaseValue,
                    Owner.Creature, null);
            }
        }

    }
}
