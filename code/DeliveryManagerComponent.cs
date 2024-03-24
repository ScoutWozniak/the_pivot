using Sandbox;
using System;

[Icon( "local_shipping" )]

public sealed class DeliveryManagerComponent : Component
{
	[Property] float DeliveryTime { get; set; } = 30.0f;
	[Property] float BreakTime { get; set; } = 60.0f;

	[Property] GameObject RestockPoint { get; set; }
	[Property] GameObject BoxPrefab { get; set; }	

	int DeliveriesUntilBreak { get; set; }
	public ShiftStates ShiftState { get; set; }

	public TimeSince RoundTime { get; set; }

	DeliveryZoneComponent CurrentZone { get; set; }

	public List<DeliveryZoneComponent> CurrentZones { get; set; } = new List<DeliveryZoneComponent>();

	public Action OnAllDeliver;

	[Property] Curve DifficultyCurve { get; set; }
	[Property] int DeliveriesUntillMaxDifficulty { get; set; }

	public int CurrentRound { get; set; }
	protected override void OnStart()
	{
		base.OnStart();
		foreach(var zone in Scene.Components.GetAll<DeliveryZoneComponent>(FindMode.EverythingInDescendants))
		{
			zone.GameObject.Enabled = false;
		}
		ShiftState = ShiftStates.DELIVERY;
		DeliveriesUntilBreak = 3;
		NextRound();
		Log.Info( (int)DifficultyCurve.Evaluate( 0.5f ) );
	}

	// Use this method to go to next round, will automatically choose if break or delivery
	void NextRound()
	{
		OnAllDeliver.Invoke();
		RoundTime = 0.0f;
		if ( DeliveriesUntilBreak == 0 )
			NextBreak();
		else
			NextDelivery();
	}
	void NextBreak()
	{
		if ( CurrentZone != null )
			CurrentZone.GameObject.Enabled = false;
		DeliveriesUntilBreak = 3;
		Log.Info( "On break!" );
		ShiftState = ShiftStates.BREAK;
	}

	void NextDelivery()
	{
		CurrentRound++;
		GetRandomZones();
		RestockRandomPoints();
		foreach ( var zone in CurrentZones )
			zone.GameObject.Enabled = true;
		DeliveriesUntilBreak--;
		ShiftState = ShiftStates.DELIVERY;
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
		if ( RoundTime > GetRoundMaxTime() )
		{
			if ( IsOnBreak() )
				NextRound();
			else
				OnFailure();
		}
	}

	public void OnDeliver(DeliveryZoneComponent Zone)
	{
		CurrentZones.Remove( Zone );
		Scene.Components.Get<CashMoney>( FindMode.EverythingInDescendants ).Pay((int)GetMoney());
		if ( ShouldDoNextRound() )
		{
			Sound.Play( "narrator.win" );
			NextRound();
		}
	}

	void Restock()
	{
		var go = BoxPrefab.Clone( RestockPoint.Transform.Position );
	}

	public float GetRoundMaxTime()
	{
		if (ShiftState == ShiftStates.DELIVERY)
			return DeliveryTime + Scene.Components.Get<UpgradeManager>(FindMode.InDescendants).GetExtraTime(); 
		else
			return BreakTime;
	}

	void OnFailure()
	{
		Log.Info( "You failed" );
		Enabled = false;
		Sound.Play( "narrator.lose" );
		FailUicomponent.GameOver();
	}

	void GetRandomZones()
	{
		var zones = Scene.Components.GetAll<DeliveryZoneComponent>( FindMode.EverythingInDescendants );
		List<DeliveryZoneComponent> prev = new();

		for (int i = 0; i < GetNumDeliveries(); i++)
		{
			var zone = zones.ElementAt( Game.Random.Int( zones.Count() - 1 ) );
			while ( !prev.Contains( zone ) )
			{
				zone = zones.ElementAt( Game.Random.Int( zones.Count() - 1 ) );
				CurrentZones.Add( zone );
				prev.Add( zone );
			}
		}
	}

	void RestockRandomPoints()
	{
		var points = Scene.Components.GetAll<RestockComponent>( FindMode.EverythingInDescendants );

		for (int i = 0; i < GetNumDeliveries(); i++)
		{
			points.ElementAt( Game.Random.Int( points.Count() - 1 ) ).Restock();
		}
	}

	public bool IsOnBreak()
	{
		return ShiftState == ShiftStates.BREAK;
	}

	bool ShouldDoNextRound()
	{
		if ( CurrentZones.Count <= 0 )
			return true;
		else
			return false;
	}

	float GetMoney()
	{
		return ( (GetRoundMaxTime() - RoundTime) / GetRoundMaxTime()) * 600.0f;
	}

	public int GetNumDeliveries()
	{
		return (int)DifficultyCurve.Evaluate( CurrentRound / DeliveriesUntillMaxDifficulty );
	}
}

public enum ShiftStates
{
	DELIVERY,
	BREAK
} 
