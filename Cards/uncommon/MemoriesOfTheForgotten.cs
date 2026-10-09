using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Cards
{
    /// <summary>
    /// （1）被遗忘者的追忆 【消耗】丢弃2张牌，从弃牌堆中选择本回合弃牌数=》+1张牌加入手牌。
    /// </summary>
    public class MemoriesOfTheForgotten : RemielleCardModel
    {
        public MemoriesOfTheForgotten() : base(1, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CardsVar(2), new CardsVar("Back",0)];

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Exhaust];

        int discard=0;

        protected override void OnUpgrade()
        {
            DynamicVars["Back"].UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
                for (int i = 0; i < DynamicVars.Cards.IntValue; i++)
                {
                    if (PileType.Hand.GetPile(Owner).Cards.Count > 0)
                    {
                        var card = Owner.RunState.Rng.CombatCardSelection.NextItem(
                            PileType.Hand.GetPile(base.Owner).Cards);
                        if (card != null)
                        {
                            await CardCmd.Discard(choiceContext, card);
                        }
                    }
                }
                await Cmd.Wait(.1f);
                var cards = (await CardSelectCmd.FromCombatPile(
                    prefs: new CardSelectorPrefs(base.SelectionScreenPrompt,
                    0, discard + (IsUpgraded ? 1 : 0)),
                    context: choiceContext, pile: PileType.Discard.GetPile(base.Owner),
                    player: base.Owner));

                if (cards != null && cards.Any())
                {
                    await CardPileCmd.Add(cards, PileType.Hand);
                }
        }

        public override Task AfterCardDiscarded(PlayerChoiceContext choiceContext, CardModel card)
        {
            discard++;
            DynamicVars["Back"].BaseValue++;
            return Task.CompletedTask;
        }

        public override Task AfterSideTurnEnd(PlayerChoiceContext choiceContext, CombatSide side, IEnumerable<Creature> participants)
        {
            DynamicVars["Back"].BaseValue-=discard;
            discard = 0;

            return Task.CompletedTask;
        }
    }
}
