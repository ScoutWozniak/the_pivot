using Sandbox;
using Sandbox.Items;

public sealed class ItemComponent : Component
{
	[Property] public ItemResource ItemType { get; set; }
	protected override void OnUpdate()
	{

	}
}
