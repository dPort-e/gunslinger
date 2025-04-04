using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public enum State {IDLE, RUNNING, JUMPING, FALLING}

	// Public Vars
	[Export]
	public float Speed = 5.0f;
	[Export]
	public float JumpVelocity = 4.5f;

	[Export]
	public float rotation_speed = 1.0f;

	[Export]
	public Camera3D camera3D;

    // Private Vars
	private State m_States;
	private Vector3 m_Velocity;

	private float target_rotation;

	private MeshInstance3D m_MeshInstance;

    // Functions
	public State States 
	{
		get { return m_States; }
		set { m_States = value; }
	}

    public override void _Ready()
    {
        m_MeshInstance = GetNode<MeshInstance3D>("MeshInstance3D");
    }


	public override void _PhysicsProcess(double delta)
	{
		// Add the gravity.
		if (!IsOnFloor())
		{
			m_Velocity += GetGravity() * (float)delta;
			m_States = State.FALLING;
		}

		// Get the input direction and handle the movement/deceleration.
		Vector3 direction = Vector3.Zero;
		direction.X = Input.GetActionStrength("Right") - Input.GetActionStrength("Left");
		direction.Z = Input.GetActionStrength("Backward") - Input.GetActionStrength("Forward");
		direction.Y = Input.GetActionStrength("Jump");

		direction = direction.Rotated(Vector3.Up, camera3D.GlobalRotation.Y);

		set_movement(direction, delta);
	}

	void jump()
	{
		// Handle Jump.
		if (IsOnFloor())
		{
			m_Velocity.Y = JumpVelocity;
			m_States = State.JUMPING;
		}
	}

	void set_movement(Vector3 direction, double delta)
	{
		
		if (direction != Vector3.Zero)
		{
			m_Velocity.X = direction.X * Speed;
			m_Velocity.Z = direction.Z * Speed;
			m_States = State.RUNNING;

			if(direction.Y != 0)
			{
				jump();
			}

			// Update mesh rot target only when moving
			target_rotation = Mathf.Atan2(direction.X, direction.Z) - Rotation.Y;
			Vector3 meshRot = m_MeshInstance.Rotation;
			meshRot.Y = (float)Mathf.LerpAngle(m_MeshInstance.Rotation.Y, target_rotation, rotation_speed * delta);
			m_MeshInstance.Rotation = meshRot;
		}
		else
		{
			m_Velocity.X = Mathf.MoveToward(Velocity.X, 0, Speed);
			m_Velocity.Z = Mathf.MoveToward(Velocity.Z, 0, Speed);
			m_States = State.IDLE;
		}

		Velocity = m_Velocity;

		MoveAndSlide();
	}
}
