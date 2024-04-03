using Sandbox;

public sealed class CitizenCustomisationComponent : Component
{
	[Property] SkinnedModelRenderer body { get; set; }
	protected override void OnUpdate()
	{
		var clothes = ClothingContainer.CreateFromLocalUser();
		clothes.Apply( body );
	}
}
