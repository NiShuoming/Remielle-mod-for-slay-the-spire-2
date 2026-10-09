using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Models;
using Remielle.Main;
using Remielle.Model;
using Remielle.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.CustomModel
{
    /// <summary>
    /// 相变时流 监测
    /// </summary>
    public class PhaseFlowListener:CustomSingletonModel
    {
        public PhaseFlowListener() : base(HookType.Combat) { }

        public override bool ShouldReceiveCombatHooks => true;

        public override async Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            if(side == CombatSide.Player)
            {
                foreach(var player in participants)
                {
                    List<CardModel> cards = player.Player.PlayerCombatState.Hand.Cards.ToList();
                    bool dance = player.Player.Creature.GetPowerAmount<TheOldWorldPower>() > 0;
                    foreach (CardModel card in cards)
                    {
                        if (card.Keywords.Contains(RemielleKeyWord.PhaseFlow))
                            await CardCmd.AutoPlay(choiceContext, card, null);
                        else if (dance && card is IDanceStep step)
                        {
                            await step.DanceStep();
                        }
                    }

                }

            }
        }
    }
}
