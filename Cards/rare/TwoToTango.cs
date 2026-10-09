using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.HoverTips;
using MegaCrit.Sts2.Core.Localization.DynamicVars;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Nodes.Cards;
using MegaCrit.Sts2.Core.ValueProps;
using Remielle.CustomListener;
using Remielle.Main;
using Remielle.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib.Cmds;

namespace Remielle.Cards
{
    /// <summary>
    /// （1）TwoToTango  【舞步】造成6(9)伤害，战斗中每次触发【舞步】，伤害+1。舞步：触发其他手牌的【舞步】=》2次
    /// </summary>
    public class TwoToTango:RemielleCardModel,IDanceStep
    {
        public TwoToTango() : base(1, CardType.Attack, CardRarity.Rare, TargetType.AnyEnemy) { }

        protected override IEnumerable<DynamicVar> CanonicalVars => 
            [new CalculationBaseVar(6), new ExtraDamageVar(1),
            new CalculatedDamageVar(ValueProp.Move).WithMultiplier((card,_)=>
            { var listen = (DanceStepListener)(card.Owner.Creature.
               CombatState.IterateHookListeners().First(a=>a is DanceStepListener));
               if(listen!=null && listen.step.TryGetValue(card.Owner, out int value))
                    return value;
               return 0;
            })];

        protected override void OnUpgrade()
        {
            DynamicVars.CalculationBase.UpgradeValueBy(3);
        }

        protected override IEnumerable<IHoverTip> ExtraHoverTips => 
            [HoverTipFactory.FromKeyword(RemielleKeyWord.DanceStep)];

        protected override async Task OnPlay(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            ArgumentNullException.ThrowIfNull(cardPlay.Target, "cardPlay.Target");
            await DamageCmd.Attack(base.DynamicVars.CalculatedDamage)
                .FromCard(this, cardPlay).Targeting(cardPlay.Target)
                .WithHitFx("vfx/vfx_attack_slash")
                .Execute(choiceContext);
        }

        public async Task DanceStep()
        {
            var cards = (PileType.Hand.GetPile(Owner).Cards.Where(a =>
                a is IDanceStep && a is not TwoToTango)).ToList();
            var lis = (DanceStepListener)(Owner.Creature.
                CombatState.IterateHookListeners().First(a => a is DanceStepListener));

            foreach (IDanceStep card in cards.Cast<IDanceStep>())
            {
                lis.step[Owner]++;
                await card.DanceStep();
                if(IsUpgraded) await card.DanceStep();
            }
        }
    }
}
