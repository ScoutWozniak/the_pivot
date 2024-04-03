using Sandbox;
using Sandbox.Resources;

public sealed class SubCustomisationComponent : Component
{
	[Property] ModelRenderer Model { get; set; }
	[Property] public Color SubColor { get; set; }


	[Property] HatResource Hat { get; set; }

	[Property] GameObject Body { get; set; }

	GameObject ActiveHat { get; set; }

	protected override void OnStart()
	{
		base.OnEnabled();
		var data = CustomisationData.Load();
		
		if ( data == null )
		{
			CustomisationData.Save( new CustomisationData() );
			data = CustomisationData.Load();
		}
		

		SubColor = new Color( data.R / 255.0f, data.G / 255.0f, data.B / 255.0f );
		Hat = ResourceLibrary.Get<HatResource>($"data/hats/{data.HatName}.hat");
		SetupSub();
	}

	void SetupSub()
	{
		if ( Model == null )
			return;

		Model.Tint = SubColor;

		// Handle spawning in hat prefab
		if (Hat != null)
		{
			SetHat( Hat );
		}
	}

	protected override void OnUpdate()
	{
		base.OnUpdate();
		Model.Tint = SubColor;
		if ( ActiveHat != null )
			ActiveHat.Components.Get<ModelRenderer>( FindMode.EverythingInSelfAndChildren ).Tint = SubColor;
	}

	public void SetHat(HatResource hat)
	{
		ActiveHat?.Destroy();
		if ( hat.PrefabFile == null )
			return;
		var go = SceneUtility.GetPrefabScene( hat.PrefabFile ).Clone();
		go.SetParent( Body, false );
		go.Components.Get<ModelRenderer>( FindMode.InDescendants ).Tint = SubColor;
		ActiveHat = go;
	}
}

class CustomisationData
{
	public int R { get; set; } = 255;
	public int G { get; set; } = 100;
	public int B { get; set; } = 0;
	public static void Save(CustomisationData data)
	{
		FileSystem.Data.WriteJson( "custom_data.json", data );
	}

	public string HatName { get; set; } = "none";
	public static CustomisationData Load()
	{
		return FileSystem.Data.ReadJson<CustomisationData>( "custom_data.json" );
	}
}
