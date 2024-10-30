public sealed class RbgrabComponent : Component
{
	GameObject HeldObject { get; set; }
	protected override void OnFixedUpdate()
	{

		if ( HeldObject != null )
		{
			if ( !HeldObject.IsValid )
				HeldObject = null;
			else
				MoveObjectToPoint();
		}
	}

	public bool CanGrabObject()
	{
		if ( HeldObject == null )
			return true;
		return false;
	}

	public void GrabObject( GameObject go )
	{
		HeldObject = go;
		var rb = HeldObject.Components.Get<Rigidbody>();
		rb.Gravity = false;
	}

	public void ReleaseObject()
	{
		var rb = HeldObject.Components.Get<Rigidbody>();
		rb.Gravity = true;

		HeldObject = null;
	}

	public bool CanReleaseObject()
	{
		if ( HeldObject != null )
			return true;
		return false;
	}

	void MoveObjectToPoint()
	{
		var rb = HeldObject.Components.Get<Rigidbody>();
		rb.Velocity = (WorldPosition - rb.WorldPosition) * 15;
		if ( (WorldPosition - rb.WorldPosition).Length > 90 )
			ReleaseObject();
	}
}
