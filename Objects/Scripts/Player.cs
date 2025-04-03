using Godot;
using System;

public partial class Player : CharacterBody3D
{
	public enum State {IDLE, RUNNING, JUMPING, FALLING}

	// Public Vars
	public const float Speed = 5.0f;
	public const float JumpVelocity = 4.5f;

    // Private Vars
	private State m_States;
	private Vector3 m_Velocity;

    // Functions
	public State States 
	{
		get { return m_States; }
		set { m_States = value; }
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

		set_movement(direction);
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

	void set_movement(Vector3 direction)
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
