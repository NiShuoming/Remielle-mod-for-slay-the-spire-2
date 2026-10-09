using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Models.Cards;
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
    /// 2=1 结使化 状态、诅咒进入弃牌堆时，会消耗。
    /// </summary>
    public class EnchainerTransformation:RemielleCardModel
    {
        public EnchainerTransformation() : base(2, CardType.Power, CardRarity.Uncommon, TargetType.Self) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(CardKeyword.Exhaust),
            HoverTipFactory.FromCard<Decay>()];

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<EnchainerTransformationPower>(choiceContext,
                Owner.Creature, 1, Owner.Creature, this);
            await CardPileCmd.AddGeneratedCardToCombat(CombatState.CreateCard<Decay>(Owner),
                PileType.Draw, Owner);
        }
    }
}
