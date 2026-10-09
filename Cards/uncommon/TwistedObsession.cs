using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Cards
{
    /// <summary>
    /// （0）倒错的执念 丢弃最多3（5）张牌，抽1
    /// </summary>
    public class TwistedObsession:RemielleCardModel
    {
        public TwistedObsession() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CardsVar(3)];

        protected override void OnUpgrade()
        {
            DynamicVars.Cards.UpgradeValueBy(1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
                var cards = await CardSelectCmd.FromHandForDiscard(choiceContext, Owner,
                    new CardSelectorPrefs(SelectionScreenPrompt, 0, DynamicVars.Cards.IntValue),
                    null, this);

                await CardCmd.Discard(choiceContext, cards);
            await CardPileCmd.Draw(choiceContext,Owner);
        }
    }
}
