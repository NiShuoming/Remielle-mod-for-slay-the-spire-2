using BaseLib.Extensions;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
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
    /// 浮晖 0 2(5)格挡， 给予1【虚弱】，选择1张手牌，本回合会获得【保留】
    /// </summary>
    public class FloatingRadiance:RemielleCardModel
    {
        public FloatingRadiance() : base(0, CardType.Skill, CardRarity.Basic, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new PowerVar<WeakPower>(1),new CardsVar(1),new PowerVar<FrailPower>(1),
        new BlockVar(2, MegaCrit.Sts2.Core.ValueProps.ValueProp.Move)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get
            {
                if(IsUpgraded) return [HoverTipFactory.FromKeyword(CardKeyword.Retain),
                    HoverTipFactory.FromPower<WeakPower>()] ;
                else
                    return [HoverTipFactory.FromKeyword(CardKeyword.Retain),
                    HoverTipFactory.FromPower<WeakPower>(),
                    HoverTipFactory.FromPower<FrailPower>()];
            }
        }

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(3);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);

            await PowerCmd.Apply<WeakPower>(choiceContext,
                cardPlay.Target, DynamicVars.Power<WeakPower>().BaseValue,
                Owner.Creature, this);

            if (IsUpgraded)
            {
                await PowerCmd.Apply<FrailPower>(choiceContext,
                cardPlay.Target, DynamicVars.Power<FrailPower>().BaseValue,
                Owner.Creature, this);
            }

                var cards = await CardSelectCmd.FromHand(choiceContext, Owner,
                    new CardSelectorPrefs(SelectionScreenPrompt, DynamicVars.Cards.IntValue),
                    card => !card.Keywords.Contains(CardKeyword.Retain) && !card.ShouldRetainThisTurn,
                    this);

                if (cards != null)
                {
                    foreach (var card in cards)
                    {
                        card.GiveSingleTurnRetain();
                    }
                }
        }
    }
}
