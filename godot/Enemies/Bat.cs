using Godot;
using System;

public class Bat : KinematicBody2D
{
    private Vector2 mKnockBack = Vector2.Zero;
    private int mKnockBackSpeed = 170;

    private AnimatedSprite mBatSprite;
    private AnimatedSprite mDieAnimation;

    [Export]
    private int mHealth = 2;

    public override void _Ready()
    {
        Area2D hurt_box = GetNode<Area2D>("HurtBox");
        hurt_box.Connect("area_entered", this, "_on_Hurt");
        mBatSprite = GetNode<AnimatedSprite>("BatSprite");
        mDieAnimation = GetNode<AnimatedSprite>("DieAnimation");
        mDieAnimation.Connect("animation_finished", this, "_on_Die");
    }

    public override void _PhysicsProcess(float delta)
    {
        mKnockBack = mKnockBack.MoveToward(Vector2.Zero , 10);
        mKnockBack = MoveAndSlide(mKnockBack);
    }

    public void _on_Hurt(Area2D area) 
    {
        mHealth--;
        if (mHealth == 0){
            mBatSprite.Hide();
            mDieAnimation.Show();
            mDieAnimation.Play("default");
        } else {
            mKnockBack = this.GlobalPosition - area.GlobalPosition;
            mKnockBack = mKnockBack.Normalized() * mKnockBackSpeed;
        }
    }

    public void _on_Die() 
    {
        QueueFree();
    }
}
