using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Powers;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.ValueProps;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Power
{
    /// <summary>
    /// 芳菲之邀 打出3张牌获得3格挡
    /// </summary>
    public class InvitationToBloomPower : PowerModel
    {
        public override PowerType Type => PowerType.Buff;

        public override PowerStackType StackType => PowerStackType.Counter;

        public override PowerInstanceType InstanceType => PowerInstanceType.Instanced;

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new CardsVar(3), new BlockVar(4,ValueProp.Unpowered)];

        public override async Task AfterCardPlayed(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (cardPlay.Card.Owner != base.Owner.Player)
            {
                return;
            }
            base.DynamicVars.Cards.BaseValue--;
            InvokeDisplayAmountChanged();
            if (base.DynamicVars.Cards.IntValue <= 0)
            {
                await Cmd.Wait(0.1f);
                await CreatureCmd.GainBlock(Owner, DynamicVars.Block, cardPlay);
                base.DynamicVars.Cards.BaseValue = 3m;
                InvokeDisplayAmountChanged();
            }
        }

    }
}
