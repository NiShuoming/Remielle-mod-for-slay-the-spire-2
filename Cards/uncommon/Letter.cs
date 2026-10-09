using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using Remielle.Model;
using Remielle.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Cards
{
    /// <summary>
    /// （1）战术黑板 【】=》【固有】回合结束时,保留最z边的2张牌
    /// </summary>
    public class Letter:RemielleCardModel
    {
        public Letter() : base(1, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CardsVar(2)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(CardKeyword.Retain)];

        protected override void OnUpgrade()
        {
            AddKeyword(CardKeyword.Retain);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<LetterPower>(choiceContext, Owner.Creature,
                DynamicVars.Cards.BaseValue, Owner.Creature, this);
        }
    }
}
