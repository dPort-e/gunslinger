using Godot;
using System;

public partial class Gun : MeshInstance3D
{
	// Public Vars
	[Export]
	public float RateOfFire;

    [Export]
	public float BulletSpeed;

    [Export]
    public PackedScene bullet_scene;

    public override void _PhysicsProcess(double delta)
    {
        float shootInput = Input.GetActionStrength("Shoot");

        if(shootInput > 0)
        {
            fireBullet();
        }

    }

    private void fireBullet()
    {
        Bullet bullet = bullet_scene.Instantiate<Bullet>();

        AddChild(bullet);

        return;
    }

}
