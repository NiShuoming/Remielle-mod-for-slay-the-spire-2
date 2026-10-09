using BaseLib.Abstracts;
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
using ZZZLib.Powers;

namespace Remielle.Relic
{
    /// <summary>
    /// 时序 触发异化时，获得1虚耀
    /// </summary>
    public class TemporalPatterns : RemielleRelicModel,IZZZTriggleAnomalyHook, IZZZSecondEnergyHook
    {
        public override RelicRarity Rarity => RelicRarity.Starter;

        protected override IEnumerable<IHoverTip> ExtraHoverTips =>
            [HoverTipFactory.FromKeyword(RemielleKeyWord.Refringe),
            HoverTipFactory.FromPower<Voidflares>()];


        public async Task TriggleAnomaly(PlayerChoiceContext playerChoice, CardKeyword keyWord, Creature target, Creature applier, IEnumerable<DamageResult> results)
        {
            if(keyWord == RemielleKeyWord.Refringe && applier == Owner.Creature)
            {
                await ZZZCmd.AddSecondEnergy(Owner, 1, Owner.Creature, null);
            }
        }

        public int ModifySecondEnergyGain(Player player, int amount, Creature giver, CardModel cardModel)
        {
            return amount;
        }

        public async Task AfterSecondEnergyChanged(Player player, int before, int after, CardModel cardModel)
        {
            if(player == Owner && before != after)
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
