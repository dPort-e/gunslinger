using Godot;
using System;

public partial class Bullet : CharacterBody3D
{
    public enum Type {DEFAULT, FIRE, ICE, ELECTRIC}

    private Type m_CurrType;

    public void Initialize(Vector3 lookDir, float bulletSpeed)
    {
        //set trajectory to where camera is pointed
        Velocity = lookDir * bulletSpeed;
    }


    public override void _PhysicsProcess(double delta)
    {
        MoveAndSlide();
    }
}
