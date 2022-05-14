using Godot;
using System;

public class Bat : KinematicBody2D
{

    private Vector2 mKnockBack = Vector2.Zero;
    private int mKnockBackSpeed = 170;

    public override void _Ready()
    {
        Area2D hurt_box = GetNode<Area2D>("HurtBox");
        hurt_box.Connect("area_entered", this, "_on_Hurt");
    }

    public override void _PhysicsProcess(float delta)
    {
        mKnockBack = mKnockBack.MoveToward(Vector2.Zero , 10);
        mKnockBack = MoveAndSlide(mKnockBack);
    }

    public void _on_Hurt(Area2D area)
    {
        mKnockBack = this.GlobalPosition - area.GlobalPosition;
        mKnockBack = mKnockBack.Normalized() * mKnockBackSpeed;
    }
}
