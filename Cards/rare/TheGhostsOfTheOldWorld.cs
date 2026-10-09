using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using Remielle.Main;
using Remielle.Model;
using Remielle.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;

namespace Remielle.Cards
{
    /// <summary>
    /// （1）旧时代的幽灵 【伙伴】获得【相变时流】，【舞步】也会在回合结束后触发
    /// </summary>
    public class TheGhostsOfTheOldWorld : RemielleCardModel
    {
        public TheGhostsOfTheOldWorld() : base(1, CardType.Power, CardRarity.Rare, TargetType.Self) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get
            {
                if (IsUpgraded) 
                    return [HoverTipFactory.FromKeyword(ZZZKeyWord.Partner),
                        HoverTipFactory.FromKeyword(RemielleKeyWord.PhaseFlow),
                        HoverTipFactory.FromKeyword(RemielleKeyWord.DanceStep)];
                return [HoverTipFactory.FromKeyword(ZZZKeyWord.Partner),
                        HoverTipFactory.FromKeyword(RemielleKeyWord.PhaseFlow)];
            }
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<TheGhostsPower>(choiceContext, Owner.Creature,
                1, Owner.Creature, this);
            if (IsUpgraded)
            {
                await PowerCmd.Apply<TheOldWorldPower>(choiceContext, Owner.Creature,
                1, Owner.Creature, this);

            }
        }
    }
}
