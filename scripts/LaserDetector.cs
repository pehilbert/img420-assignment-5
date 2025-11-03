using Godot;
using System;

public partial class LaserDetector : Node2D
{
	[Export] public float LaserLength = 500f;
	[Export] public Color LaserColorNormal = Colors.Green;
	[Export] public Color LaserColorAlert = Colors.Red;
	[Export] public NodePath PlayerPath;
	private RayCast2D _rayCast;
	private Line2D _laserBeam;
	private Node2D _player;
	private bool _isAlarmActive = false;
	private Timer _alarmTimer;

	public override void _Ready()
	{
		_player = GetNodeOrNull<Node2D>(PlayerPath) ?? GetNodeOrNull<Node2D>("Player");

		// Setup alarm timer
		_alarmTimer = new Timer();
		_alarmTimer.WaitTime = 2.0;
		_alarmTimer.OneShot = true;
		_alarmTimer.Timeout += () => ResetAlarm();
		AddChild(_alarmTimer);

		SetupRaycast();
		SetupVisuals();
	}

	private void SetupRaycast()
	{
		_rayCast = new RayCast2D();
		_rayCast.TargetPosition = new Vector2(LaserLength, 0);
		_rayCast.Enabled = true;
		AddChild(_rayCast);
	}

	private void SetupVisuals()
	{
		_laserBeam = new Line2D();
		_laserBeam.Width = 2f;
		_laserBeam.DefaultColor = LaserColorNormal;
		_laserBeam.Points = new Vector2[] { Vector2.Zero, _rayCast.TargetPosition };
		AddChild(_laserBeam);
	}

	public override void _PhysicsProcess(double delta)
	{
		_rayCast.ForceRaycastUpdate();

		UpdateLaserBeam();

		if (_rayCast.IsColliding())
		{
			var collider = _rayCast.GetCollider();
			if (collider is Node2D hitNode && _player != null && hitNode == _player)
			{
				TriggerAlarm();
			}
		}
	}

	private void UpdateLaserBeam()
	{
		Vector2 endPointLocal;
		if (_rayCast.IsColliding())
		{
			Vector2 collisionGlobal = _rayCast.GetCollisionPoint();
			endPointLocal = ToLocal(collisionGlobal);
		}
		else
		{
			endPointLocal = _rayCast.TargetPosition;
		}

		_laserBeam.Points = new Vector2[] { Vector2.Zero, endPointLocal };

		if (!_isAlarmActive)
		{
			_laserBeam.DefaultColor = LaserColorNormal;
		}
	}

	private void TriggerAlarm()
	{
		if (_isAlarmActive)
			return;

		_isAlarmActive = true;

		_laserBeam.DefaultColor = LaserColorAlert;
		_alarmTimer.Start();

		GD.Print("ALARM! Player detected!");
	}

	private void ResetAlarm()
	{
		_isAlarmActive = false;
		if (_laserBeam != null)
			_laserBeam.DefaultColor = LaserColorNormal;
	}
}
