using Godot;
using HarmonyLib;
using MegaCrit.Sts2.Core.Modding;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Remielle
{
    [ModInitializer(nameof(Initialize))]
    public static class RemielleModInitialzer
    {
        public const string ModId = "Remielle";
        public static void Initialize()
        {
            GD.Print("Remielle Mod - 加载中!");
            Godot.Bridge.ScriptManagerBridge.LookupScriptsInAssembly(Assembly.GetExecutingAssembly());
            var harmony = new Harmony("Remielle");
            harmony.PatchAll();
            GD.Print("Remielle Mod - 加载成功!");

        }
    }

}
