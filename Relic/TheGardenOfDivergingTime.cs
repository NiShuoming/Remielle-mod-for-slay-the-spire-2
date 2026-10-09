using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Rooms;
using MegaCrit.Sts2.Core.Saves.Runs;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Relic
{
    /// <summary>
    /// 时间分岔的花园 每隔一回合，抽1
    /// </summary>
    public class TheGardenOfDivergingTime : RemielleRelicModel
    {
        private bool _isActivating;

        private int _turnsSeen;

        public override RelicRarity Rarity => RelicRarity.Uncommon;

        public override bool ShowCounter => true;

        public override int DisplayAmount
        {
            get
            {
                if (!IsActivating)
                {
                    return TurnsSeen;
                }
                return base.DynamicVars["Turns"].IntValue;
            }
        }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
        [new CardsVar(1),
        new DynamicVar("Turns", 2m)];

        private bool IsActivating
        {
            get
            {
                return _isActivating;
            }
            set
            {
                AssertMutable();
                _isActivating = value;
                InvokeDisplayAmountChanged();
            }
        }

        [SavedProperty]
        public int TurnsSeen
        {
            get
            {
                return _turnsSeen;
            }
            set
            {
                AssertMutable();
                _turnsSeen = value;
                InvokeDisplayAmountChanged();
            }
        }

        public override Task BeforeHandDraw(Player player, PlayerChoiceContext choiceContext, ICombatState combatState)
        {
            if (player != base.Owner)
            {
                return Task.CompletedTask;
            }
            TurnsSeen = (TurnsSeen + 1) % base.DynamicVars["Turns"].IntValue;
            base.Status = ((TurnsSeen == base.DynamicVars["Turns"].IntValue - 1) ? RelicStatus.Active : RelicStatus.Normal);
            if (TurnsSeen == 0)
            {
                TaskHelper.RunSafely(DoActivateVisuals());
            }
            return Task.CompletedTask;
        }

        public override decimal ModifyHandDraw(Player player, decimal count)
        {
            if (player != base.Owner)
            {
                return count;
            }
            if (TurnsSeen != 0)
            {
                return count;
            }
            return count + base.DynamicVars.Cards.BaseValue;
        }

        private async Task DoActivateVisuals()
        {
            IsActivating = true;
            Flash();
            await Cmd.Wait(1f);
            IsActivating = false;
        }

        public override Task AfterCombatEnd(CombatRoom _)
        {
            base.Status = RelicStatus.Normal;
            return Task.CompletedTask;
        }

    }
}
