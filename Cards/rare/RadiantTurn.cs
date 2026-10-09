using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
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
    /// （2）转辉 【】=》【保留】打出手牌中的【相变时流】
    /// </summary>
    public class RadiantTurn:RemielleCardModel
    {
        public RadiantTurn() : base(2, CardType.Skill, CardRarity.Rare, TargetType.Self) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(RemielleKeyWord.PhaseFlow)];

        protected override void OnUpgrade()
        {
            AddKeyword(CardKeyword.Retain);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            var cards = PileType.Hand.GetPile(Owner).Cards.ToList();

            foreach(var card in cards)
            {
                if (card.Keywords.Contains(RemielleKeyWord.PhaseFlow))
                {
                    await CardCmd.AutoPlay(choiceContext, card,
                        Owner.RunState.Rng.CombatTargets.NextItem(
                            Owner.Creature.CombatState.HittableEnemies));
                }
            }
        }
    }
}
