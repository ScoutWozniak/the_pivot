using Sandbox;

public sealed class MineObstacle : Component, Component.ITriggerListener
{

	protected override void OnUpdate()
	{

	}

	void ITriggerListener.OnTriggerEnter(Sandbox.Collider other)
	{
		if (other.Tags.Has("player"))
		{
			
			_ = other.GameObject.Components.Get<SubmarineComponent>().ExplosionKnockBack( Transform.Position );
			Sound.Play( "explosion", Transform.Position );
			GameObject.Destroy();
		}
	}
}
