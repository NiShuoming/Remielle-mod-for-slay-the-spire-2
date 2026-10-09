using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.HoverTips;
using Remielle.Model;
using Remielle.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Powers;

namespace Remielle.Relic
{
    /// <summary>
    /// 倾落喧嚣 虚耀提供的精通翻倍
    /// </summary>
    public class FallIntoTheNoise : RemielleRelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Uncommon;

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<Voidflares>(),
            HoverTipFactory.FromPower<AnomalyProficiency>()];
    }
}
