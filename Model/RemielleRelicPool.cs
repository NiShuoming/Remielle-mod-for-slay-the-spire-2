using BaseLib.Abstracts;
using Godot;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Model
{
    public sealed class RemielleRelicPool : CustomRelicPoolModel
    {
        public override string EnergyColorName => "remielle";

        public override Color LabOutlineColor => Colors.Pink;
    }

}
