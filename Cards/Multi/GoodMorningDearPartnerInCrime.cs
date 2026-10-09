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
    /// 技（2=1）早安，共犯小姐 选择1名队友，双方获得格挡时，都会为对方给予格挡
    /// </summary>
    public class GoodMorningDearPartnerInCrime:RemielleCardModel
    {
        public override CardMultiplayerConstraint MultiplayerConstraint => CardMultiplayerConstraint.MultiplayerOnly;

        public GoodMorningDearPartnerInCrime() : base(2, CardType.Skill, CardRarity.Rare, TargetType.AnyAlly) { }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.Static(StaticHoverTip.Block)];

        protected override void OnUpgrade()
        {
            EnergyCost.UpgradeBy(-1);
        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");

            await PowerCmd.Apply<GoodMorningPower>(choiceContext, Owner.Creature,
                1, cardPlay.Target, this);
            await PowerCmd.Apply<GoodMorningPower>(choiceContext, cardPlay.Target,
                1, Owner.Creature, this);
        }
    }
}
