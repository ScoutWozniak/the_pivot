public sealed class PropellerComponent : Component
{
	[Property] Rigidbody Sub { get; set; }
	[Property] ParticleConeEmitter Particles { get; set; }
	[Property] SoundPointComponent SoundPoint { get; set; }
	protected override void OnUpdate()
	{
		Angles addAng = new Angles();
		addAng.yaw = Sub.Velocity.Length / 100.0f;
		LocalRotation *= addAng.ToRotation();
		if ( (Sub.Velocity.Length / 100.0f) <= 2.0f )
			Particles.Enabled = false;
		else
			Particles.Enabled = true;



		var maxSpeedScale = (Sub.Velocity.Length / 100.0f) / 8;
		SoundPoint.Pitch = MathX.Lerp( SoundPoint.Pitch, maxSpeedScale * 1.25f, Time.Delta * 10.0f );
	}
}
