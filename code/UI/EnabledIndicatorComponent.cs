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
		originalPos = WorldPosition;
	}
	protected override void OnUpdate()
	{
		if ( LookAtCam )
			WorldRotation = Rotation.LookAt( -(Scene.Camera.WorldPosition - WorldPosition) );
		WorldPosition = originalPos + Vector3.Up * MathF.Sin( Time.Now * 5 ) * 20.0f;

		if ( Propeller != null )
			Propeller.LocalRotation *= new Angles( 0, 100.0f * Time.Delta, 0.0f );
	}
}
