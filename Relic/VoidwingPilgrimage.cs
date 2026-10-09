using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models;
using Remielle.Model;
using Remielle.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;

namespace Remielle.Relic
{
    /// <summary>
    /// 空翼巡礼 【虚耀】消耗为0时，会获得1能量
    /// </summary>
    public class VoidwingPilgrimage : RemielleRelicModel, IZZZSecondEnergyHook
    {
        public override RelicRarity Rarity => RelicRarity.Shop;

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<Voidflares>(), HoverTipFactory.Static(StaticHoverTip.Energy)];

        public async Task AfterSecondEnergyChanged(Player player, int before, int after, CardModel cardModel)
        {
            if (before > 0 && after == 0) await PlayerCmd.GainEnergy(1, player);
        }

        public int ModifyMaxSecondEnergy(Player player, int amount)
        {
            return amount;
        }

        public int ModifySecondEnergyGain(Player player, int amount, Creature giver, CardModel cardModel)
        {
            return amount;
        }
    }
}
