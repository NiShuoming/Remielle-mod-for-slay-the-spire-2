using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Power
{
    /// <summary>
    /// 共舞 下一张攻击、技能牌会打出a次
    /// </summary>
    public class DanceTogetherPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override int ModifyCardPlayCount(CardModel card, Creature target, int playCount)
        {
            if(card.Owner.Creature == Owner &&( 
                card.Type == CardType.Attack || card.Type == CardType.Skill))
            {
                return Amount+playCount;
            }
            return playCount;
        }

        public override async Task AfterModifyingCardPlayCount(CardModel card)
        {
            await PowerCmd.Remove(this);
        }

    }
}
