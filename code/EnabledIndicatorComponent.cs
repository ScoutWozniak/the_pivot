using Sandbox;
using System;

public sealed class EnabledIndicatorComponent : Component
{
	Vector3 originalPos { get; set; }
	[Property] bool LookAtCam { get; set; } = true;

	// Sorry for this last minute hack
	[Property] GameObject Propeller { get; set; }
	protected override void OnEnabled()
	{
		base.OnEnabled();
		originalPos = Transform.Position;
	}
	protected override void OnUpdate()
	{
		if (LookAtCam)
			Transform.Rotation = Rotation.LookAt( -(Scene.Camera.Transform.Position - Transform.Position) );
		Transform.Position = originalPos + Vector3.Up * MathF.Sin(Time.Now * 5) * 20.0f;

		if ( Propeller != null )
			Propeller.Transform.LocalRotation *= new Angles( 0, 10.0f, 0.0f );
	}
}
