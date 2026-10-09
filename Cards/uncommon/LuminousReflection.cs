using BaseLib.Extensions;
using MegaCrit.Sts2.Core.CardSelection;
using MegaCrit.Sts2.Core.Combat;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Enchantments;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.Model;
using Remielle.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cmds;

namespace Remielle.Cards
{
    /// <summary>
    /// （1）映曜 造成8（11）伤害，消耗1【虚耀】，升级并记录1张牌，下次使用时可以召唤到手中。
    /// </summary>
    public class LuminousReflection : RemielleCardModel
    {
        public LuminousReflection() : base(1, CardType.Attack, CardRarity.Uncommon, TargetType.AnyEnemy)
        {
            record = null;
            recordTip = null;
        }

        protected override IEnumerable<DynamicVar> CanonicalVars =>
            [new DamageVar(8, ValueProp.Move), new PowerVar<Voidflares>(1)];

        protected override IEnumerable<IHoverTip> ExtraHoverTips
        {
            get
            {
                if(recordTip == null) return [HoverTipFactory.FromPower<Voidflares>()];
                return [HoverTipFactory.FromPower<Voidflares>(), recordTip];
            }
        }


        protected override void OnUpgrade()
        {
            DynamicVars.Damage.UpgradeValueBy(3);
        }

        CardModel record = null;
        IHoverTip recordTip = null;

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(base.DynamicVars.Damage.BaseValue).FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
            if (record != null) 
            {
                await CardPileCmd.Add(record, PileType.Hand);
            }

            if (ZZZCmd.GetSecondEnergy(Owner).Energy2 >= DynamicVars.Power<Voidflares>().IntValue)
            {
                var card =(await CardSelectCmd.FromHand(choiceContext, Owner,
                    new CardSelectorPrefs(SelectionScreenPrompt, 0, 1),
                    null, this)).FirstOrDefault();
                if(card != null)
                {
                    record = card;
                    recordTip = HoverTipFactory.FromCard(card);
                    await ZZZCmd.AddSecondEnergy(Owner, -DynamicVars.Power<Voidflares>().IntValue,
                        Owner.Creature, this);
                }
            }
        }
    }
}
