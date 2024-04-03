using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Resources
{
	[GameResource("Hat", "hat", "Hat that the sub can wear")]
	public class HatResource : GameResource
	{
		public PrefabFile PrefabFile { get; set; }

		public string Name { get;set; }
		public string Description { get; set; }
	}
}
