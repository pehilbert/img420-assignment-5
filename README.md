# IMG 420 Assignment 5
## Particle Shader
The shader creates a gradient effect by mixing the start and end color with an alpha 
value based on the Y coordinate. This means that near the top of the particle, it is
the start color, and blends to the end color as you move down the particle in the
positive Y direction.

The shader also creates a wave distortion effects by offsetting the X and Y coordinates
of each vertex based on sin and cos functions over time with adjustable frequency
and amplitude/intensity values.

Finally, the ParticleController script cyclically adjusts the intensity of the distortion
effect over time using the sin function, creating a growing and shrinking effect
on the particles over time.

## Physics Demo
For the chain physics demo, I used PinJoint2Ds with a configurable softness value so
that each chain segment could freely rotate around its joint position, creating
a realistic chain-link effect.

## Raycast Demo
The raycast system casts a ray in the positive X direction with a configurable length
value, and draws a Line2D between the detector's point and either the point it collided
with, or the point at the end of its max length if it did not collide with anything. 
If it detects a collision, it checks if it collided with the player, and if it did,
it triggers the alarm system which will turn the laser beam red for a short period of
time.
