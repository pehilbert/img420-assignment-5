using Godot;

public partial class Player : RigidBody2D
{
	[Export] public float Speed = 200f;

	public override void _PhysicsProcess(double delta)
	{
		Vector2 velocity = Vector2.Zero;

		// Get input and move
		if (Input.IsActionPressed("move_right")) 
		{
			velocity.X += 1;
		}

		if (Input.IsActionPressed("move_left")) 
		{
			velocity.X -= 1;
		}

		if (Input.IsActionPressed("move_down")) 
		{
			velocity.Y += 1;
		}

		if (Input.IsActionPressed("move_up")) 
		{
			velocity.Y -= 1;
		}

		LinearVelocity = velocity * Speed;
	}
}
