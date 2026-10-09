using Godot;
using Godot.Collections;
using MegaCrit.Sts2.Core.Nodes.Vfx.Utilities;
using System.Reflection;

namespace Remielle.Other
{
	public partial class ReParticlesContainer : NParticlesContainer
	{
		[Export(PropertyHint.None, "")]
		public Array<GpuParticles2D>? _p;
		public override void _EnterTree()
		{
			var prop = typeof(NParticlesContainer).GetField(
				"_particles", BindingFlags.NonPublic | BindingFlags.Instance);
			if (_p != null && prop != null)
			{
				prop.SetValue(this, _p);
				return;
			}
		}
	}
}
