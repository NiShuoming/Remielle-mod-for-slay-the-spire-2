using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
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
    /// 飞鸟集 羽攻击+
    /// </summary>
    public class StrayBirdsAPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override decimal ModifyDamageAdditive(Creature target, decimal amount, ValueProp props, Creature dealer, CardModel cardSource, CardPlay cardPlay)
        {
            if(dealer==Owner && cardSource!=null && cardSource.Type == 
                CardType.Attack && props.IsPoweredAttack() && cardSource.Keywords.
                Contains(RemielleKeyWord.Feather))
            {
                return Amount;
            }
            return 0;
        }
    }
}
