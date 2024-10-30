public sealed class MineFieldComponent : Component
{
	[Property] float BoxSize { get; set; } = 256.0f;
	[Property] GameObject MinePrefab { get; set; }

	[Property] int Mines { get; set; }
	[Property] float Radius { get; set; }


	List<Vector3> Points { get; set; }

	protected override void OnStart()
	{
		base.OnStart();
		Points = GetRandomPointsWithRadius( Mines, Radius );

		foreach ( var point in Points )
		{
			MinePrefab.Clone( WorldPosition + point );
		}
	}

	protected override void OnValidate()
	{
		base.OnValidate();
		Points = GetRandomPointsWithRadius( Mines, Radius );
	}

	protected override void DrawGizmos()
	{
		base.DrawGizmos();
		Gizmo.Draw.LineBBox( BBox.FromPositionAndSize( Vector3.Zero, BoxSize ) );

		if ( Points == null )
			return;

		foreach ( var point in Points )
		{
			Gizmo.Draw.LineSphere( point, 32.0f );
		}
	}

	List<Vector3> GetRandomPointsWithRadius( float amount, float radius )
	{
		List<Vector3> points = new List<Vector3>();

		for ( int i = 0; i < amount; i++ )
		{
			Vector3 newPoint = new();
			bool CanBreak = true;
			int test = 0;
			while ( CanBreak )
			{
				// HACK because i cant be fucked fixing this
				test++;
				if ( test > 10 )
					break;


				CanBreak = false;
				newPoint = GetRandomPoint();
				foreach ( var point in points )
				{
					if ( (point - newPoint).Length < radius )
						CanBreak = true;
				}
			}

			points.Add( newPoint );
		}

		return points;
	}

	Vector3 GetRandomPoint()
	{

		return Game.Random.VectorInCube( BoxSize / 2 );
	}
}
