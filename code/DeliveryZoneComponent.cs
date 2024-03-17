using Sandbox;
using Sandbox.Items;
using System.Collections.Generic;

public sealed class DeliveryZoneComponent : Component, Component.ITriggerListener
{
	[Property] GameObject EnabledIndicator { get; set; }
	public void OnTriggerEnter( Collider other )
	{
		if (other.Tags.Has("grab"))
		{
			if (other.GameObject.Components.TryGet<ItemComponent>(out var comp, FindMode.EverythingInSelf) )
			{
				other.GameObject.Destroy();

				CheckOrderComplete();
				
			}
		}
	}

	public void OnTriggerExit( Collider other ) {}

	void CheckOrderComplete()
	{
		Log.Info( "Order Finished!" );
		var manager = Scene.Components.Get<DeliveryManagerComponent>( FindMode.InChildren );
		manager.OnDeliver(this);
		GameObject.Enabled = false;
	}
}
