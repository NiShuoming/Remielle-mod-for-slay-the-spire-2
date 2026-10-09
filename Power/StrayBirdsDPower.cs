using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Badges;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Main;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Power
{
    /// <summary>
    /// 飞鸟集 羽格挡+
    /// </summary>
    public class StrayBirdsDPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override decimal ModifyBlockAdditive(Creature target, decimal block, ValueProp props, CardModel cardSource, CardPlay cardPlay)
        {
            if (cardSource != null && cardSource.Owner.Creature==Owner && cardSource.Type ==
                CardType.Skill && props.IsPoweredCardOrMonsterMoveBlock() && cardSource.Keywords.
                Contains(RemielleKeyWord.Feather))
            {
                return Amount;
            }
            return 0;
        }
    }
}
