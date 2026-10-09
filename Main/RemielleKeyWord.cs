using BaseLib.Patches.Content;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;
using ZZZLib.Cards.Equips.DriveDiscs;

namespace Remielle.Main
{
    public class RemielleKeyWord
    {
        /// <summary>
        /// 异化 造成6【属性异常】伤害，使该debuff增加1层
        /// </summary>
        [CustomEnum("Refringe")]
        public static CardKeyword Refringe;

        /// <summary>
        /// 相变时流 回合结束后，如果还在手牌中，会自动打出。
        /// </summary>
        [CustomEnum("PhaseFlow")]
        [KeywordProperties(AutoKeywordPosition.Before)]
        public static CardKeyword PhaseFlow;

        /// <summary>
        /// 舞步 抽到手牌时，触发额外效果
        /// </summary>
        [CustomEnum("DanceStep")]
        public static CardKeyword DanceStep;

        /// <summary>
        /// 羽 打出时，对随机敌人造成3伤害
        /// </summary>
        [CustomEnum("Feather")]
        [KeywordProperties(AutoKeywordPosition.Before)]
        public static CardKeyword Feather;
    }

    public static class ReGlobalData
    {
        public readonly static decimal FeatherBaseDamage = 2;
        public readonly static decimal FeatherExtraDamage = 1;

        public static DamageVar RefringeDamage()=> new DamageVar(
            4, ValueProp.Unpowered | ZZZValueProp.Anomaly);
    }
}
