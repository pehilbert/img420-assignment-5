using Godot;

public partial class ParticleController : Node2D
{
	private GpuParticles2D _particles;
	private ShaderMaterial _shaderMaterial;
	private float _time = 0.0f;

	[Export]
	public float InitialIntensity = 0.5f;
	[Export]
	public float IntensityVariance = 3.0f;
	[Export]
	public float IntensityFrequency = 1.0f;

	public override void _Ready()
	{
		_particles = GetNode<GpuParticles2D>("GPUParticles2D");

		if (_particles != null)
		{
			GD.Print("Particle system found.");
			_shaderMaterial = (ShaderMaterial)_particles.Material;
			_shaderMaterial.SetShaderParameter("wave_intensity_x", InitialIntensity);
			_shaderMaterial.SetShaderParameter("wave_intensity_y", InitialIntensity);
		}
	}

	public override void _Process(double delta)
	{
		// Change wave intensity cyclicly over time
		_time += (float)delta;

		if (_shaderMaterial != null)
		{
			float intensityIncrease = Mathf.Sin(_time * IntensityFrequency) * IntensityVariance;
			float currentIntensityX = (float)_shaderMaterial.GetShaderParameter("wave_intensity_x");
			float currentIntensityY = (float)_shaderMaterial.GetShaderParameter("wave_intensity_y");
			_shaderMaterial.SetShaderParameter("wave_intensity_x", InitialIntensity + intensityIncrease);
			_shaderMaterial.SetShaderParameter("wave_intensity_y", InitialIntensity + intensityIncrease);
		}
	}
}
