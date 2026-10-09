using BaseLib.Extensions;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models.Cards;
using MegaCrit.Sts2.Core.Models.Powers;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Model;
using Remielle.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ZZZLib.Cmds;

namespace Remielle.Cards
{
    /// <summary>
    /// （1）坠落 造成10（13）伤害，消耗1【虚耀】抽1(2)，删1
    /// </summary>
    public class FallingDown:RemielleCardModel
    {
        public FallingDown():base(1, CardType.Attack, CardRarity.Common, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new DamageVar(10,ValueProp.Move), new PowerVar<Voidflares>(1),
            new CardsVar(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromPower<Voidflares>(),
            HoverTipFactory.FromKeyword(CardKeyword.Exhaust)];

        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(3);
            DynamicVars.Cards.UpgradeValueBy(1);

        }

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);

            if (ZZZCmd.GetSecondEnergy(Owner).Energy2 >= DynamicVars.Power<Voidflares>().BaseValue)
            {
                await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue, Owner);
                    await Cmd.Wait(0.1f);
                    var card = (await CardSelectCmd.FromHand(choiceContext, Owner,
                        new CardSelectorPrefs(SelectionScreenPrompt, 1),
                        null, this)).FirstOrDefault();
                    if (card != null) await CardCmd.Exhaust(choiceContext, card);
                await ZZZCmd.AddSecondEnergy(Owner, -1, Owner.Creature, this);
            }
        }
    }
}
