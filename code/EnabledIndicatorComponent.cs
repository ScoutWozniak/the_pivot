using Sandbox;
using System;

public sealed class EnabledIndicatorComponent : Component
{
	Vector3 originalPos { get; set; }
	protected override void OnEnabled()
	{
		base.OnEnabled();
		originalPos = Transform.Position;
	}
	protected override void OnUpdate()
	{
		Transform.Rotation = Rotation.LookAt( -(Scene.Camera.Transform.Position - Transform.Position) );
		Transform.Position = originalPos + Vector3.Up * MathF.Sin(Time.Now * 5) * 20.0f;
	}
}
