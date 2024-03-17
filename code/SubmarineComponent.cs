using Sandbox;
using System.Threading.Tasks;

public sealed class SubmarineComponent : Component
{

	Vector3 WishVelocity { get; set; }
	[Property] Rigidbody RB { get; set; }
	[Property] GameObject Body { get; set; }

	[Property] RbgrabComponent RbGrab { get; set; }

	public bool IsBoosting { get; set; }

	public bool CanGrab = false;

	public bool BoostCoolDownDone = true;

	bool KnockedDown = false;
	protected override void OnFixedUpdate()
	{
		if (RB.Velocity != Vector3.Zero)
			Body.Transform.Rotation = Rotation.Lerp( Body.Transform.Rotation, Rotation.LookAt( RB.Velocity.Normal ), Time.Delta * 20.0f );


		if ( KnockedDown )
			return;

		BuildWishVelocity();

		if ( !IsBoosting )
		{
			RB.Velocity += WishVelocity * Scene.Components.Get<UpgradeManager>( FindMode.InDescendants ).GetSpeedMult();

			RB.Velocity = RB.Velocity * (0.99f * Scene.Components.Get<UpgradeManager>( FindMode.InDescendants ).GetFrictionMult());
		}

		if ( IsBoosting )
			RB.Velocity = Scene.Camera.Transform.Rotation.Forward * 2000.0f;

		//RB.Velocity += Vector3.Down;
		if ( RB.Velocity.IsNearlyZero() )
			RB.Velocity = Vector3.Zero;

		// Rotate Body
		

		// Pickup Code
		var startPos = Transform.Position;
		var endPos = Transform.Position + Transform.Rotation.Down * 128.0f;
		var tr = Scene.Trace.Ray(startPos, endPos).Radius(32.0f).IgnoreGameObjectHierarchy(GameObject).WithTag("grab").Run();

		CanGrab = tr.Hit;
		if ( RbGrab.CanGrabObject() && Input.Pressed( "jump" ) && tr.Hit )
			RbGrab.GrabObject( tr.GameObject );
		else if ( RbGrab.CanReleaseObject() && Input.Pressed( "jump" ) )
			RbGrab.ReleaseObject();

		if ( Input.Pressed( "run" ) && Scene.Components.Get<UpgradeManager>( FindMode.InDescendants ).CanRocketBoost() && !IsBoosting && BoostCoolDownDone )
			_ = RocketBoost();
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
	}

	void BuildWishVelocity()
	{
		var dir = Vector3.Zero;
		dir += Input.AnalogMove * Scene.Camera.Transform.Rotation;

		if ( Input.Down( "FlyUp" ) )
			dir += Scene.Camera.Transform.Rotation.Up;
		if ( Input.Down( "FlyDown" ) )
			dir -= Scene.Camera.Transform.Rotation.Up;

		WishVelocity = dir;
		//WishVelocity = WishVelocity.WithZ( 0 );

		if ( !WishVelocity.IsNearZeroLength ) WishVelocity = WishVelocity.Normal;
		 
		WishVelocity *= 10.0f;
	}

	async Task RocketBoost()
	{
		IsBoosting = true;
		await Task.DelaySeconds( 2.0f );
		IsBoosting = false;
		BoostCoolDownDone = false;
		BoostCoolDown();
	}

	async Task BoostCoolDown()
	{
		await Task.DelayRealtimeSeconds( 10 );
		BoostCoolDownDone = true;
	}

	public async Task ExplosionKnockBack(Vector3 Pos) 
	{
		Log.Info( (Transform.Position - Pos).Normal );
		KnockedDown = true;
		RB.Gravity = true;
		RB.Velocity += ( (Transform.Position - Pos).Normal * 3000.0f );
		await Task.DelaySeconds( 3.0f );
		KnockedDown = false;
		RB.Gravity = false;
	}
}
