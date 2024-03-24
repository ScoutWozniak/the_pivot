using Sandbox;
using System.Threading.Tasks;

public sealed class FadeComponent : Component
{
	[Property] float Time { get; set; }
	protected override void OnStart()
	{
		base.OnStart();
		FadeIn();
	}
	async Task FadeIn()
	{
		TimeSince timeSince = 0.0f;
		float from = 1.0f;
		var panel = Components.Get<ScreenPanel>();
		while ( timeSince < Time )
		{
			float size = MathX.Lerp( from, 0.0f, timeSince / Time );
			panel.Opacity = size;
			await Task.Frame(); // wait one frame
		}
	}

	async Task FadeOut()
	{
		TimeSince timeSince = 0.0f;
		float from = 0.0f;
		var panel = Components.Get<ScreenPanel>();
		while ( timeSince < Time )
		{
			float size = MathX.Lerp( from, 1.0f, timeSince / Time );
			panel.Opacity = size;
			await Task.Frame(); // wait one frame
		}
	}

	public static void GlobalFadeOut()
	{
		_ = Game.ActiveScene.Components.Get<FadeComponent>( FindMode.EverythingInDescendants ).FadeOut();
	}
}
