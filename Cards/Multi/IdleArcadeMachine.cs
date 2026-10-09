using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Players;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Cards
{
    /// <summary>
    /// （1）闲置街机 （自己和）1名（所有）队友的下一回合获得2能量，抽1张牌
    /// </summary>
    public class IdleArcadeMachine: RemielleCardModel
    {
        public IdleArcadeMachine():base(1, CardType.Skill, CardRarity.Uncommon, TargetType.AnyAlly) { }
        public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new EnergyVar(2), new CardsVar(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.Static(StaticHoverTip.Energy)];

        public override TargetType TargetType
        {
            get
            {
                if (!IsUpgraded)
                {
                    return TargetType.AnyPlayer;
                }

                return TargetType.Self;
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if (!IsUpgraded)
            {
                if (cardPlay.Target.IsAlive)
                {
                    await PowerCmd.Apply<EnergyNextTurnPower>(choiceContext, cardPlay.Target,
                        DynamicVars.Energy.BaseValue, Owner.Creature, this);
                    await PowerCmd.Apply<DrawCardsNextTurnPower>(choiceContext, cardPlay.Target,
                        DynamicVars.Cards.BaseValue, Owner.Creature, this);
                }
            }
            else
            {
                await PowerCmd.Apply<EnergyNextTurnPower>(choiceContext, Owner.Creature,
                     DynamicVars.Energy.BaseValue, Owner.Creature, this);
                await PowerCmd.Apply<DrawCardsNextTurnPower>(choiceContext, Owner.Creature,
                    DynamicVars.Cards.BaseValue, Owner.Creature, this);

                foreach (Player pl in CombatState.Players)
                {
                    if (pl.Creature.IsAlive)
                    {
                        await PowerCmd.Apply<EnergyNextTurnPower>(choiceContext, pl.Creature,
                             DynamicVars.Energy.BaseValue, Owner.Creature, this);
                        await PowerCmd.Apply<DrawCardsNextTurnPower>(choiceContext, pl.Creature,
                            DynamicVars.Cards.BaseValue, Owner.Creature, this);
                    }
                }
            }
        }

    }
}
