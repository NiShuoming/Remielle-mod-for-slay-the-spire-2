using BaseLib.Abstracts;
using Godot;
using MegaCrit.Sts2.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Remielle.Model
{
    public sealed class RemiellePotionPool : CustomPotionPoolModel
    {
        public override string EnergyColorName => "remielle";

        public override Color LabOutlineColor => Colors.Pink;

    }
}
