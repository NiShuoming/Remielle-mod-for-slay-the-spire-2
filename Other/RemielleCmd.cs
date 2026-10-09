using Godot;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Remielle.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;
using ZZZLib.Cmds;

namespace Remielle.Other
{
    public static class RemielleCmd
    {
        public static async Task<decimal> TriggleRefringe(PowerModel power, Creature giver, Creature target, CardModel cardSource)
        {
            var result = await CreatureCmd.Damage(new ThrowingPlayerChoiceContext(), target,
                ReGlobalData.RefringeDamage(), giver, cardSource, null);
            await ZZZHook.TriggleAnomaly(new ThrowingPlayerChoiceContext(), RemielleKeyWord.Refringe,
                target, giver, result); 
            return 1;
        }
    }
}
