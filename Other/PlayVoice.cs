using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Other
{
    public partial class PlayVoice:Node2D
    {
        public override void _EnterTree()
        {
            ReVoiceManager.PlayInVoice();
        }
    }
}
