using Godot;
using Godot.Collections;
using MegaCrit.Sts2.Core.Bindings.MegaSpine;
using ZZZLib.Other;

namespace Remielle.Other
{
    public partial class VoidFlaresCounter:ZZZSecondEnergyCounter
    {
        [Export]
        public Array<ReParticlesContainer>? _p;
       
        public Node2D Wings;

        public override void _Ready()
        {
            base._Ready();
            Wings = GetNode<Node2D>("%SpineSprite");
            MegaSprite megaSprite = new MegaSprite(Wings);
            if (megaSprite.HasAnimation("glow"))
            {
                megaSprite.GetAnimationState().SetAnimation("glow", loop: true, 1);
            }
            OnEnergyChanged(3, 0);

        }

        protected override void Reflash()
        {
            
        }

        protected override void OnEnergyChanged(int oldEnergy, int newEnergy)
        {
            if(oldEnergy!=secondEnergy.MaxEnergy2 && newEnergy >= secondEnergy.MaxEnergy2)
            {
                MegaSprite megaSprite = new MegaSprite(Wings);
                if (megaSprite.HasAnimation("close"))
                {
                    megaSprite.GetAnimationState().SetAnimation("close", loop: false, 0);
                }
            }
            else if (oldEnergy == secondEnergy.MaxEnergy2 && newEnergy != secondEnergy.MaxEnergy2)
            {
                MegaSprite megaSprite = new MegaSprite(Wings);
                if (megaSprite.HasAnimation("open"))
                {
                    megaSprite.GetAnimationState().SetAnimation("open", loop: false, 0);
                }
            }
            for (int i = 0;i < 3;i++)
            {
                if (i < newEnergy && !_p[i].Visible)
                {
                    _p[i].Visible = true;
                    _p[i].Restart();
                }
                else if(i>=newEnergy && _p[i].Visible)
                {
                    _p[i].Visible = false;
                }
            }
        }
    }
}
