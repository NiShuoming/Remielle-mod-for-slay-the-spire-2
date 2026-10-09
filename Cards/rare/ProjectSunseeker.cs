using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Cards
{
    /// <summary>
    /// （1）逐日计划 丢弃任意张手牌，获得7 9格挡，每丢弃1张，多获得1次
    /// </summary>
    public class ProjectSunseeker:RemielleCardModel
    {
        public ProjectSunseeker() : base(1, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new BlockVar(7,ValueProp.Move), new CardsVar(9)];

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(2);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
                var cards = (await CardSelectCmd.FromHand(choiceContext, Owner,
                    new CardSelectorPrefs(SelectionScreenPrompt, 0, DynamicVars.Cards.IntValue),
                    null, this)).ToList();

                if (cards != null && cards.Count != 0)
                {
                    for (int i = 0; i < cards.Count(); i++)
                    {
                        await CardCmd.Discard(choiceContext, cards[i]);
                        await CreatureCmd.GainBlock(Owner.Creature, DynamicVars.Block, cardPlay);
                    }
                }
        }
    }
}
