using BaseLib.Abstracts;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.Entities.Potions;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Powers;
using Remielle.Model;
using Remielle.Power;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Potions
{
    /// <summary>
    /// 早安奶茶 抽2张牌，下2个回合开始时，额外抽2
    /// </summary>
    public class MorningMilkTea : RemiellePotionModel
    {
        public override PotionRarity Rarity => PotionRarity.Rare;

        public override PotionUsage Usage => PotionUsage.CombatOnly;

        public override TargetType TargetType => TargetType.AnyPlayer;

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CardsVar(2),new DynamicVar("Turn",2)];

        protected override async Task OnUse(PlayerChoiceContext choiceContext, Creature target)
        {
            await CardPileCmd.Draw(choiceContext, DynamicVars.Cards.BaseValue,target.Player);
            await PowerCmd.Apply<MorningMilkTeaPower>(choiceContext,
                target, DynamicVars["Turn"].BaseValue, Owner.Creature, null);
        }
    }
}
