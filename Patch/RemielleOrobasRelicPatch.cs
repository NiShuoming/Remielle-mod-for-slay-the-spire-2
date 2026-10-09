using HarmonyLib;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Relics;
using Remielle.Relic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Patch
{

    [HarmonyPatch(typeof(TouchOfOrobas), "get_RefinementUpgrades")]
    public static class RemielleOrobasRelicPatch
    {
        static void Postfix(ref Dictionary<ModelId, RelicModel> __result)
        {
            if (!__result.ContainsKey(ModelDb.Relic<TemporalPatterns>().Id))
                __result.Add(ModelDb.Relic<TemporalPatterns>().Id,
                    ModelDb.Relic<StilledColors>());
        }
    }

}
