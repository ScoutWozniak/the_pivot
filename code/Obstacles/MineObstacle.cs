public sealed class MineObstacle : Component, Component.ITriggerListener
{

	protected override void OnUpdate()
	{

	}

	void ITriggerListener.OnTriggerEnter( Sandbox.Collider other )
	{
		if ( other.Tags.Has( "player" ) )
		{

			_ = other.GameObject.Components.Get<SubmarineComponent>().ExplosionKnockBack( WorldPosition );
			Sound.Play( "explosion", WorldPosition );
			GameObject.Destroy();
		}
	}
}
