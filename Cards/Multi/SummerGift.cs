using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Powers;
using Remielle.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Remielle.Cards
{
    /// <summary>
    /// 技（0）夏日赠礼 本回合保留1名队友的手牌 给予4(8)格挡
    /// </summary>
    public class SummerGift : RemielleCardModel
    {
        public SummerGift() : base(0, CardType.Skill, CardRarity.Uncommon, TargetType.AnyAlly) { }
        public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new BlockVar(4, MegaCrit.Sts2.Core.ValueProps.ValueProp.Move)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(CardKeyword.Retain)];

        protected override void OnUpgrade()
        {
            DynamicVars.Block.UpgradeValueBy(4);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            if(cardPlay.Target.IsAlive)
                await PowerCmd.Apply<RetainHandPower>(choiceContext, cardPlay.Target,
                    1, Owner.Creature, this);
        }
    }
}
