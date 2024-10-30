public sealed class OrbitComponent : Component
{
	[Property] float Speed { get; set; } = 1.0f;
	protected override void OnUpdate()
	{
		WorldRotation = WorldRotation * new Angles( 0.0f, Speed * Time.Delta, 0.0f ).ToRotation();
	}
}
