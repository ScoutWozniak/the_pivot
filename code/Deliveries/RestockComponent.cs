public sealed class RestockComponent : Component
{
	[Property] GameObject ToSpawn { get; set; }
	[Property] GameObject SpawnPoint { get; set; }

	[Property] GameObject LineRenderer { get; set; }

	protected override void OnStart()
	{
		base.OnStart();
		LineRenderer.Enabled = false;
		Scene.Components.Get<DeliveryManagerComponent>( FindMode.InDescendants ).OnAllDeliver += OnDeliver;
	}

	void OnDeliver()
	{
		LineRenderer.Enabled = false;
	}

	public void Restock()
	{
		LineRenderer.Enabled = true;
		var go = ToSpawn.Clone( SpawnPoint.WorldPosition, WorldRotation );
	}

	protected override void DrawGizmos()
	{
		base.DrawGizmos();
		if ( SpawnPoint == null ) return;
		Gizmo.Draw.LineBBox( BBox.FromPositionAndSize( SpawnPoint.WorldPosition, 32.0f ) );
	}
}
