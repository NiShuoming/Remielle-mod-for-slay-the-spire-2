using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Relics;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Relic
{
    /// <summary>
    /// 归客痛饮遗忘 回合结束前，对所有敌人造成手中牌数的伤害
    /// </summary>
    public class ToDrinkForgetfulness : RemielleRelicModel
    {
        public override RelicRarity Rarity => RelicRarity.Rare;

        public override async Task BeforeSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if (participants.Contains(Owner.Creature))
            {
                int n = Owner.PlayerCombatState.Hand.Cards.Count;
                await CreatureCmd.Damage(choiceContext,
                    Owner.Creature.CombatState.HittableEnemies,
                    n, ValueProp.Unpowered, Owner.Creature);
            }
        }
    }
}
