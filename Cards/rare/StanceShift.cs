using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
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
    /// （3）转换姿态 虚无=》【】 回合结束后，如果还在手牌中，下次打出的费用-1
    /// </summary>
    public class StanceShift:RemielleCardModel
    {
        public StanceShift() : base(3,CardType.Power,CardRarity.Rare,TargetType.Self) { }

        public override IEnumerable<CardKeyword> CanonicalKeywords => 
            [CardKeyword.Ethereal];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(CardKeyword.Retain)];

        protected override void OnUpgrade()
        {
            RemoveKeyword(CardKeyword.Ethereal);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<StanceShiftPower>(choiceContext, Owner.Creature,
                1, Owner.Creature, this);
        }
    }
}
