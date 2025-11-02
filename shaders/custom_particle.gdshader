shader_type canvas_item;

uniform float wave_intensity_x = 0.1;
uniform float wave_frequency_x = 5;
uniform float wave_intensity_y = 0.1;
uniform float wave_frequency_y = 5;
uniform vec4 color_start : source_color = vec4(1.0, 0.5, 0.0, 1.0);
uniform vec4 color_end : source_color = vec4(1.0, 0.0, 0.5, 1.0);

void vertex() {
    float wave_x = sin(UV.y + TIME * wave_frequency_x) * wave_intensity_x;
	float wave_y = cos(UV.x + TIME * wave_frequency_y) * wave_intensity_y;
    VERTEX.x += wave_x;
	VERTEX.y += wave_y;
}

void fragment() {
    vec2 uv = UV;

    // vertical gradient
    vec4 grad_color = mix(color_start, color_end, uv.y);

    COLOR = grad_color;
}