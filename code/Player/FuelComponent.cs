using Sandbox;

public sealed class FuelComponent : Component
{
	[Property] float Fuel { get; set; } = 100.0f;

	[Property] float FuelDrainRate { get; set; }

	public Vector3 PlayerWishVel { get; set; }
	protected override void OnFixedUpdate()
	{
		base.OnFixedUpdate();
		Log.Info( PlayerWishVel.Normal.Length );
		Fuel -= FuelDrainRate * PlayerWishVel.Normal.Length * Scene.FixedDelta;
	}
}
