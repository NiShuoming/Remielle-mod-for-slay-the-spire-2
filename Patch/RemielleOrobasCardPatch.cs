using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using Remielle.Cards;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Patch
{
    [HarmonyPatch(typeof(ArchaicTooth), "get_TranscendenceUpgrades")]
    public static class RemielleOrobasCardPatch
    {
        static void Postfix(ref Dictionary<ModelId, CardModel> __result)
        {
            if (!__result.ContainsKey(ModelDb.Card<SliverOfLight>().Id))
                __result.Add(ModelDb.Card<SliverOfLight>().Id,
                ModelDb.Card<OdeToDawn>());
        }
    }
}
