using Sandbox;

public sealed class SubCamFollowerComponent : Component
{
	public Angles EyeAngles { get; set; }
	[Property] GameObject Following { get; set; }

	[Property] float CamDist { get; set; }	

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
		var startPos = (Following.Transform.Position + lookDir.Backward * CamDist + Vector3.Up * 64.0f);

		CamDist -= Input.MouseWheel.y * 30.0f;
		CamDist = MathX.Clamp( CamDist, 200, 1000 );
		
		var tr = Scene.Trace.Ray( Following.Transform.Position, startPos ).WithoutTags("grab").Run();
		if ( tr.Hit )
			cam.Transform.Position = tr.HitPosition;
		else
			cam.Transform.Position = startPos;
	}
}
