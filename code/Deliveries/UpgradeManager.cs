using Sandbox;

public sealed class UpgradeManager : Component
{
	protected override void OnUpdate()
	{

	}

	[Property] public bool ExtraTimeUpgrade { get; set; } = false;
	public int GetExtraTime()
	{
		if ( ExtraTimeUpgrade )
			return 10;
		else
			return 0;
	}

	[Property] public bool ExtraSpeedUpgrade { get; set; } = false;
	public float GetSpeedMult()
	{
		if ( ExtraSpeedUpgrade )
			return 15f;
		else
			return 1.0f;
	}

	public float GetFrictionMult()
	{
		if ( ExtraSpeedUpgrade )
			return 0.9f;
		else
			return 1.0f;
	}

	[Property] public bool RocketBoosterUpgrade { get; set; } = false;

	public bool CanRocketBoost()
	{
		return RocketBoosterUpgrade;
	}

	public void Upgrade( int u )
	{
		switch ( u )
		{
			case 0:
				ExtraTimeUpgrade = true; break;
			case 1: ExtraSpeedUpgrade = true; break;
			case 2: RocketBoosterUpgrade = true; break;

		}
	}

	public bool HasUpgrade(int u)
	{
		switch ( u )
		{
			case 0:
				return ExtraTimeUpgrade;
			case 1: return ExtraSpeedUpgrade;
			case 2: return RocketBoosterUpgrade;
			default:
				return false;
		}
	}
}
