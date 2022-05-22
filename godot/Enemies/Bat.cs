using Godot;
using System;

public class Bat : KinematicBody2D
{
    private Vector2 mVelocity = Vector2.Zero;
    private int mKnockBackSpeed = 170;
    public int MAX_SPEED = 50;
    public float ACCELERATION = 10;

    private AnimatedSprite mBatSprite;
    private AnimatedSprite mDieAnimation;
    private PlayerDetection mPlayerDetection;
    private SoftCollision mSoftCollision;

    [Export]
    private int mHealth = 2;

    public enum Sate{
        IDLE,
        WANDER,
        ATTACK
    } 

    private Sate sate = Sate.IDLE;

    public override void _Ready()
    {
        Area2D hurt_box = GetNode<Area2D>("HurtBox");
        hurt_box.Connect("area_entered", this, "_on_Hurt");
        mBatSprite = GetNode<AnimatedSprite>("BatSprite");
        mDieAnimation = GetNode<AnimatedSprite>("DieAnimation");
        mDieAnimation.Connect("animation_finished", this, "_on_Die");
        mPlayerDetection = GetNode<PlayerDetection>("PlayerDetection");
        mSoftCollision = GetNode<SoftCollision>("SoftCollision");
        mSoftCollision.Connect("SoftCollisionEnter",this,"OnSoftCollisionEnter");
    }

    public override void _PhysicsProcess(float delta)
    {
        Node2D player = mPlayerDetection.GetPlayr();
        if (player == null) {
            sate = Sate.IDLE;
        } else {
            sate = Sate.ATTACK;
        }

        switch(sate) {
            case Sate.IDLE:
                mVelocity = mVelocity.MoveToward(Vector2.Zero,10);
                break;
            case Sate.WANDER:
                break;
            case Sate.ATTACK:
                if (player!=null) {
                    Vector2 vect=(player.GlobalPosition - GlobalPosition).Normalized();
                    mVelocity = mVelocity.MoveToward(vect * MAX_SPEED, ACCELERATION);
                    mBatSprite.FlipH = mVelocity.x < 0;
                }
                break;
        }

        mVelocity = MoveAndSlide(mVelocity);
    }

    public void _on_Hurt(Area2D area) 
    {
        mHealth--;
        if (mHealth == 0){
            mBatSprite.Hide();
            mDieAnimation.Show();
            mDieAnimation.Play("default");
        } else {
            mVelocity = this.GlobalPosition - area.GlobalPosition;
            mVelocity = mVelocity.Normalized() * mKnockBackSpeed;
        }
    }

    public void _on_Die() 
    {
        QueueFree();
    }

    public void OnSoftCollisionEnter(Vector2 vec)
    {
        mVelocity = vec * 50;
    }
}
