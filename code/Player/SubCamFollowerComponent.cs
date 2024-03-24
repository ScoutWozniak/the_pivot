using Sandbox;

public sealed class SubCamFollowerComponent : Component
{
	public Angles EyeAngles { get; set; }
	[Property] GameObject Following { get; set; }

	protected override void OnEnabled()
	{
		base.OnEnabled();
		EyeAngles = Following.Transform.Rotation.Angles();
	}

	protected override void OnUpdate()
	{
		var ee = EyeAngles;
		ee += Input.AnalogLook * 0.5f;
		ee.roll = 0;
		EyeAngles = ee;

		var cam = Scene.Camera;
		var lookDir = EyeAngles.ToRotation();
		cam.Transform.Rotation = lookDir;
		var startPos = (Following.Transform.Position + lookDir.Backward * 600 + Vector3.Up * 64.0f);
		
		var tr = Scene.Trace.Ray( Following.Transform.Position, startPos ).WithoutTags("grab").Run();
		if ( tr.Hit )
			cam.Transform.Position = tr.HitPosition;
		else
			cam.Transform.Position = startPos;


	}
}
