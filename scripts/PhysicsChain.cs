using Godot;
using System.Collections.Generic;

public partial class PhysicsChain : Node2D
{
	[Export] public int ChainSegments = 5;
	[Export] public float SegmentDistance = 20f;
	[Export] public float JointSoftness = 20f;
	[Export] public Vector2 InitialForce = new Vector2(1000, 0);
	[Export] public PackedScene SegmentScene;

	private List<RigidBody2D> _segments = new List<RigidBody2D>();
	private List<Joint2D> _joints = new List<Joint2D>();
	private StaticBody2D _base;
	
	public override void _Ready()
	{
		_base = GetNode<StaticBody2D>("Base");

		if (_base != null)
		{
			CreateChain();
			ApplyForceToSegment(ChainSegments - 1, InitialForce);
		}
	}
	
	private void CreateChain()
	{
		RigidBody2D initialSegment = SegmentScene.Instantiate<RigidBody2D>();
		_segments.Add(initialSegment);
		AddChild(initialSegment);

		PinJoint2D initialJoint = createJoint();
		initialJoint.NodeA = _base.GetPath();
		initialJoint.NodeB = initialSegment.GetPath();
		_joints.Add(initialJoint);
		AddChild(initialJoint);

		for (int segment = 0; segment < ChainSegments - 1; segment++)
		{
			RigidBody2D previousSegment = _segments[segment];
			RigidBody2D currentSegment = SegmentScene.Instantiate<RigidBody2D>();
			currentSegment.Position = previousSegment.Position + new Vector2(0, SegmentDistance * 2);
			_segments.Add(currentSegment);
			AddChild(currentSegment);

			PinJoint2D joint = createJoint();
			joint.NodeA = previousSegment.GetPath();
			joint.NodeB = currentSegment.GetPath();
			joint.Position = currentSegment.Position;
			_joints.Add(joint);
			AddChild(joint);
		}
	}
	
	// TODO: Add method to apply force to chain
	public void ApplyForceToSegment(int segmentIndex, Vector2 force)
	{
		if (segmentIndex < _segments.Count)
		{
			RigidBody2D segment = _segments[segmentIndex];
			segment.ApplyImpulse(force);
		}
	}

	private PinJoint2D createJoint()
	{
		PinJoint2D joint = new PinJoint2D();
		joint.Softness = JointSoftness;
		return joint;
	}
}
