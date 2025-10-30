shader_type particles;
// TODO: Add uniform variables for:
// - Time-based animation
// - Color gradient (hint: use multiple color uniforms)
// - Wave intensity
uniform float wave_intensity = 0.1;
uniform vec4 color_start : source_color = vec4(1.0, 0.5, 0.0, 1.0);
uniform vec4 color_end : source_color = vec4(1.0, 0.0, 0.5, 1.0);

void fragment() {
	// TODO: Implement wave distortion using UV and TIME
	// TODO: Create color gradient based on UV.y
	// TODO: Apply final color with alpha

	// Starter hint:
	vec2 uv = UV;
	uv.x += sin(uv.y * 10.0 + TIME) * wave_intensity;
}