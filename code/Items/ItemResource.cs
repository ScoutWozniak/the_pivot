using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Sandbox.Items
{
	[GameResource("Item Def", "item", "Item data")]
	public class ItemResource : GameResource
	{
		public string ItemName { get; set; }
	}
}
