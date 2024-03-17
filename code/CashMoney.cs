using Sandbox;

public sealed class CashMoney : Component
{
	[Property] public float Cash { get; set; }
	public void Pay( float ammount)
	{
		Cash += ammount;
	}
}
