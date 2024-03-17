using Sandbox;

public sealed class ImpactComponent : Component, Component.ICollisionListener
{
	protected override void OnUpdate()
	{

	}

	void ICollisionListener.OnCollisionStart(Sandbox.Collision other)
	{
		if ( other.Contact.Speed.Length > 700.0f )
			Sound.Play( "heavyimpact.metal", other.Contact.Point );
	}
}
