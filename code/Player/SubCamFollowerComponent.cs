public sealed class SubCamFollowerComponent : Component
{
	public Angles EyeAngles { get; set; }
	[Property] GameObject Following { get; set; }

	[Property] float CamDist { get; set; }

	protected override void OnEnabled()
	{
		base.OnEnabled();
		EyeAngles = Following.WorldRotation.Angles();
	}

	protected override void OnUpdate()
	{
		var ee = EyeAngles;
		ee += Input.AnalogLook * 0.5f;
		ee.roll = 0;
		EyeAngles = ee;

		var cam = Scene.Camera;
		var lookDir = EyeAngles.ToRotation();
		cam.WorldRotation = lookDir;
		var startPos = (Following.WorldPosition + lookDir.Backward * CamDist + Vector3.Up * 64.0f);

		CamDist -= Input.MouseWheel.y * 30.0f;
		CamDist = MathX.Clamp( CamDist, 200, 1000 );

		var tr = Scene.Trace.Ray( Following.WorldPosition, startPos ).WithoutTags( "grab" ).Run();
		if ( tr.Hit )
			cam.WorldPosition = tr.HitPosition + tr.Normal * 2.0f;
		else
			cam.WorldPosition = startPos;
	}
}
