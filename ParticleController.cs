using Godot;

public partial class ParticleController : GpuParticles2D
{
	private ShaderMaterial _shaderMaterial;

	public override void _Ready()
	{
		// TODO: Load and apply custom shader
		// TODO: Configure particle properties (Amount, Lifetime, Speed, etc.)
		// TODO: Set process material properties

		// Hint: Use a new ShaderMaterial with your custom shader
	}

	public override void _Process(double delta)
	{
		// TODO: Update shader parameters over time
		// Hint: Use shader parameters to create animated effects
	}
}
