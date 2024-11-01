using Sandbox;
using System.Threading.Tasks;
using System;
using Sandbox.Utility;

public sealed class MenuOpenningComponent : Component
{ 	[Property] SceneFile NextScene { get; set; }
	public async Task WaitABit(SceneFile ToScene)
	{
		await Task.DelayRealtimeSeconds( 2.0f );

		Game.ActiveScene.Load( ToScene );
	}
}
