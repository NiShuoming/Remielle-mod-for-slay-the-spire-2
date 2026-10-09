using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Main;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Cards
{
    /// <summary>
    /// （2）过去的过去 【相变时流】随机打出1（2）张弃牌堆中牌
    /// </summary>
    public class PastOfThePast:RemielleCardModel
    {
        public PastOfThePast() : base(2, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [RemielleKeyWord.PhaseFlow];

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CardsVar(1)];

        protected override void OnUpgrade()
        {
            DynamicVars.Cards.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            var cards = PileType.Discard.GetPile(Owner).Cards.ToList();
            for(int i = 0; i < cards.Count && i < DynamicVars.Cards.IntValue; i++)
            {
                var card = Owner.RunState.Rng.CombatCardSelection.NextItem(cards);
                await CardCmd.AutoPlay(choiceContext, card, null);
            }
        }
    }
}
