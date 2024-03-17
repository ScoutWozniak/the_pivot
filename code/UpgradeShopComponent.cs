using Sandbox;

public sealed class UpgradeShopComponent : Component, Component.ITriggerListener
{
	[Property] ShopUicomponent ShopUI { get; set; }
	protected override void OnUpdate()
	{

	}

	void ITriggerListener.OnTriggerEnter(Collider other)
	{
		if (other.Tags.Has("player"))
			ShopUI.Enabled = true;
	}
}
