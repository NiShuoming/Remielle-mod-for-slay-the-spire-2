using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Commands.Builders;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Combat;
using MegaCrit.Sts2.Core.Nodes.Rooms;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Power
{
    /// <summary>
    /// 相变·滞影 击晕第一个攻击自己的敌人，并造成伤害
    /// </summary>
    public class ShadowStasisPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override decimal ModifyHpLostAfterOstyLate(Creature target, decimal amount, ValueProp props, Creature? dealer, CardModel? cardSource)
        {
            if (target != base.Owner||amount<=0)
            {
                return amount;
            }
            return 1m;
        }
        bool gg=false;
        public override async Task AfterDamageReceived(PlayerChoiceContext choiceContext, Creature target, DamageResult result, ValueProp props, Creature dealer, CardModel cardSource)
        {
            if (target != Owner || gg || result.UnblockedDamage<=0 || dealer==Owner) return;
            else
            {
                NCreature nCreature = NCombatRoom.Instance?.GetCreatureNode(dealer);
                if (nCreature == null) return;
                using MegaTrackEntry megaTrackEntry = nCreature.SpineAnimation.GetCurrentTrack();
                if (megaTrackEntry != null)
                {
                    megaTrackEntry.SetTimeScale(0f);
                    await Cmd.CustomScaledWait(1, 1.5f);
                    megaTrackEntry.SetTimeScale(1f);
                }
                await CreatureCmd.Damage(choiceContext, dealer, Amount, ValueProp.Unpowered, Owner);
                gg = true;
            }
        }

        public override async Task AfterAttack(PlayerChoiceContext choiceContext, AttackCommand command)
        {
            if(gg) await PowerCmd.Remove(this);
        }
    }
}
