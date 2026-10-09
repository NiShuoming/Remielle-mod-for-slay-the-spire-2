using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;
using ZZZLib.Cmds;
using ZZZLib.Other;

namespace Remielle.Power
{
    /// <summary>
    /// 百年之梦 虚耀改变时抽1
    /// </summary>
    public class ADreamOfAHundredYearsPower : PowerModel, IZZZSecondEnergyHook
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;
        
        public async Task AfterSecondEnergyChanged(Player player, int before, int after, CardModel cardModel)
        {
            if(player.Creature == Owner)
            {
                await CardPileCmd.Draw(new ThrowingPlayerChoiceContext(),
                    Amount, player);
            }
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
