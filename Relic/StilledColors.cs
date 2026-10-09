using Godot;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using Remielle.Main;
using Remielle.Model;
using Remielle.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;
using ZZZLib.Cmds;

namespace Remielle.Relic
{
    /// <summary>
    /// 万色滞行 触发【异化】时，获得1层【虚耀】，战斗开始3虚耀
    /// </summary>
    public class StilledColors : RemielleRelicModel, IZZZTriggleAnomalyHook, IZZZSecondEnergyHook
    {
        public override RelicRarity Rarity => RelicRarity.Starter;

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(RemielleKeyWord.Refringe),
            HoverTipFactory.FromPower<Voidflares>()];

        public async Task TriggleAnomaly(PlayerChoiceContext playerChoice, CardKeyword keyWord, Creature target, Creature applier, IEnumerable<DamageResult> results)
        {
            if (keyWord == RemielleKeyWord.Refringe && applier == Owner.Creature)
            {
                await ZZZCmd.AddSecondEnergy(Owner, 1, Owner.Creature, null);
            }
        }

        public override async Task AfterSideTurnStart(CombatSide side, IReadOnlyList<Creature> participants, ICombatState combatState)
        {
            if(side.HasFlag(CombatSide.Player) && participants.Contains(Owner.Creature) && combatState.RoundNumber == 1)
            {
                await ZZZCmd.AddSecondEnergy(Owner, 3, Owner.Creature, null);
            }
        }

        public int ModifySecondEnergyGain(Player player, int amount, Creature giver, CardModel cardModel)
        {
            return amount;
        }

        public async Task AfterSecondEnergyChanged(Player player, int before, int after, CardModel cardModel)
        {
            if (player == Owner && before != after)
            {
                await PowerCmd.Apply<Voidflares>(new ThrowingPlayerChoiceContext(),
                    Owner.Creature, after - before, Owner.Creature, null);
            }
        }

        public int ModifyMaxSecondEnergy(Player player, int amount)
        {
            return amount;
        }
    }
}
