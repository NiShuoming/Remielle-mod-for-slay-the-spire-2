using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Animation;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using MegaCrit.Sts2.Core.Commands;
using MegaCrit.Sts2.Core.Entities.Cards;
using MegaCrit.Sts2.Core.Entities.Characters;
using MegaCrit.Sts2.Core.Entities.Creatures;
using MegaCrit.Sts2.Core.GameActions.Multiplayer;
using MegaCrit.Sts2.Core.Helpers;
using MegaCrit.Sts2.Core.Localization;
using MegaCrit.Sts2.Core.Models;
using MegaCrit.Sts2.Core.Models.Characters;
using Remielle.Cards;
using Remielle.Model;
using Remielle.Power;
using Remielle.Relic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using ZZZLib;

namespace Remielle
{
    public class Remielle : PlaceholderCharacterModel, IZZZCharacter
    {
        public const string CharacterId = "Remielle";

        public override string PlaceholderID => "remielle";

        public const string energyColorName = "remielle"; // 能量显示的颜色名称

        public override CharacterGender Gender => CharacterGender.Feminine; // 性别

        protected override CharacterModel? UnlocksAfterRunAs => null;

        public override Color NameColor => Colors.Pink; // 名称显示的颜色

        public override int StartingHp => 72; // 角色初始生命值

        public override int StartingGold => 99; // 角色初始拥有的金钱

        public int MaxSecondEnergy() => 3;

        public LocString energyTitle() => new LocString("powers", "VOIDFLARES.title");

        public LocString energyDescription() => new LocString("powers", "VOIDFLARES.description");

        public string energy2Icon() => "voidflares";

        public override string CustomTrailPath => SceneHelper.GetScenePath("vfx/card_trail_" + "ironclad");

        public override CardPoolModel CardPool => ModelDb.CardPool<RemielleCardPool>();

        public override PotionPoolModel PotionPool => ModelDb.PotionPool<RemiellePotionPool>();

        public override RelicPoolModel RelicPool => ModelDb.RelicPool<RemielleRelicPool>();

        public override List<CardModel> StartingDeck => [

            ModelDb.Card<RemielleDefend>(),
            ModelDb.Card<RemielleDefend>(),
            ModelDb.Card<RemielleDefend>(),
            ModelDb.Card<RemielleDefend>(),

            ModelDb.Card<RemielleStrike>(),
            ModelDb.Card<RemielleStrike>(),
            ModelDb.Card<RemielleStrike>(),
            ModelDb.Card<RemielleStrike>(),

            ModelDb.Card<FloatingRadiance>(),

            ModelDb.Card<SliverOfLight>()

        ]; // 初始卡组

        public override List<RelicModel> StartingRelics => [
            ModelDb.Relic<TemporalPatterns>()
        ];
        // 角色播放攻击动作后，到真正出手/命中前的延迟时间
        public override float AttackAnimDelay => 0.15f;

        // 角色播放施法动作后，到效果实际触发前的延迟时间
        public override float CastAnimDelay => 0.25f;

        /*
        // 卡牌左上角费用数字的描边颜色
        public override Color EnergyLabelOutlineColor => Colors.Pink;
        */

        // 角色相关对白、气泡、事件发言等文本的颜色
        public override Color DialogueColor => Colors.Pink;

        // 地图上该角色绘制连线时使用的颜色
        public override Color MapDrawingColor => Colors.Pink;

        // 联机状态下，这个角色的指向线主体颜色
        public override Color RemoteTargetingLineColor => Colors.Pink;

        // 联机指向线的外描边颜色
        public override Color RemoteTargetingLineOutline => Colors.Pink;

        public override string CharacterSelectSfx =>
    ModelDb.Character<Ironclad>().CharacterSelectSfx;

        public override string CharacterTransitionSfx =>
            "event:/sfx/ui/wipe_ironclad";

        public override string CustomCharacterSelectTransitionPath => "res://materials/transitions/" + "ironclad" + "_transition_mat.tres";

        public override CreatureAnimator GenerateAnimator(MegaSprite controller, Creature creature)
        {
            return base.GenerateAnimator(controller, creature);
            /*
            AnimState animState = new AnimState("idle_loop", isLooping: true);
             AnimState animState2 = new AnimState("cast");
             AnimState animState3 = new AnimState("attack");
            AnimState animState5 = new AnimState("attack_heavy");
            AnimState animState4 = new AnimState("hurt");
            AnimState state = new AnimState("die");
            AnimState animState6 = new AnimState("relaxed_loop", isLooping: true);
            
            animState2.NextState = animState;
            animState3.NextState = animState;
            animState5.NextState = animState;
            animState4.NextState = animState;
            animState6.AddBranch("Idle", animState);
            CreatureAnimator creatureAnimator = new CreatureAnimator(animState, controller);
            creatureAnimator.AddAnyState("Idle", animState);
            creatureAnimator.AddAnyState("Dead", state);
          
            creatureAnimator.AddAnyState("Attack", animState3);
            creatureAnimator.AddAnyState("Cast", animState2);
            creatureAnimator.AddAnyState("heavyAttack", animState5);
            creatureAnimator.AddAnyState("PowerUp", animState2);
            creatureAnimator.AddAnyState("Hit", animState4);
            creatureAnimator.AddAnyState("Relaxed", animState6);
            MegaAnimationState animationState = controller.GetAnimationState();
            animationState.SetAnimation("FlyingSword", loop: true, 1);
            animationState.SetAnimation("NormalMode", loop: false, 2);
            return creatureAnimator;*/
        }

        public async Task PlayUltimate(PlayerChoiceContext choiceContext, CardPlay cardPlay)
        {
            await PowerCmd.Apply<Lumiflux>(choiceContext, 
                cardPlay.Card.Owner.Creature.CombatState.HittableEnemies,
                2, cardPlay.Card.Owner.Creature, cardPlay.Card);
        }


    }

}
