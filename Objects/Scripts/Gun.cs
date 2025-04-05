using Godot;
using System;

public partial class Gun : MeshInstance3D
{
	// Public Vars
	[Export]
	public float RateOfFire;

    [Export]
    public PackedScene bullet_scene;

    private Timer m_timer;
    private RayCast3D m_rayCast;

    private bool canShoot = true;

    public override void _Ready()
    {
        m_timer = GetNode<Timer>("Timer");
        m_timer.Timeout += OnTimerTimeout;
        m_timer.WaitTime = RateOfFire;

        m_rayCast = GetNode<RayCast3D>("RayCast3D");
    }


    public override void _PhysicsProcess(double delta)
    {
        float shootInput = Input.GetActionStrength("Shoot");

        if(canShoot && shootInput > 0)
        {
            fireBullet();
        }
    }

    private void OnTimerTimeout()
    {
        canShoot = true;
        m_timer.WaitTime = RateOfFire;
    }

    private void fireBullet()
    {
        Bullet bulletInstance = bullet_scene.Instantiate<Bullet>();
        bulletInstance.Position = m_rayCast.GlobalPosition;
        bulletInstance.Basis = m_rayCast.GlobalTransform.Basis;

        AddChild(bulletInstance);

        canShoot = false;
        m_timer.Start();
    }

}
