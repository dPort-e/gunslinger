using Godot;
using Microsoft.VisualBasic.FileIO;
using System;

public partial class Bullet : Node3D
{
    public enum Type {DEFAULT, FIRE, ICE, ELECTRIC}

    [Export]
	public float BulletSpeed;

    private Type m_CurrType;

    private RayCast3D m_rayCast;
    private Vector3 m_speedVector;
    private Timer m_timer;

    public override void _Ready()
    {
        m_rayCast = GetNode<RayCast3D>("RayCast3D");
        m_speedVector = new Vector3(0,0, BulletSpeed);
        m_timer = GetNode<Timer>("Timer");
        m_timer.Timeout += OnTimerTimeout;
    }


    public override void _PhysicsProcess(double delta)
    {
        Position += Transform.Basis * m_speedVector * (float)delta;
    }

    private void OnTimerTimeout()
    {
        QueueFree();
    }

}
