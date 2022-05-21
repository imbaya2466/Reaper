using Godot;
using System;

public class Player : KinematicBody2D
{
  public int mMaxSpeed = 100;
  public float mAcceleration = 10;
  public float mFriction = 10;
  public float mRollMaxSpeed = 150;
 
  [Export]
  public int mHealth = 4;

  public Vector2 mVelocity = Vector2.Zero;
  public Vector2 mRollVelocity = Vector2.Right;

  public PlayerSate mPlayerSate= PlayerSate.MOVE;

  public AnimationTree mAnimation = null;
  public AnimationNodeStateMachinePlayback mAnimationState = null;
  private HurtBox mHurtBox;

  [Signal]
  delegate void HealthChange(int val);

  public enum PlayerSate{
    MOVE,
    ROLL,
    ATTACK
  } 

  public override void _Ready()
  {
    mAnimation = GetNode<AnimationTree>("AnimationTree");
    mAnimationState = (AnimationNodeStateMachinePlayback)mAnimation.Get("parameters/playback");
    mHurtBox = GetNode<HurtBox>("HurtBox");
    mHurtBox.Connect("OnHit", this, "_on_Hurt");
  }
 
  public override void _PhysicsProcess(float delta)
  {
    base._PhysicsProcess(delta);
    switch (mPlayerSate) {
      case PlayerSate.MOVE:
        MoveProcess(delta);
        break;
      case PlayerSate.ROLL:
        RollProcess(delta);
        break;
      case PlayerSate.ATTACK:
        AttacksProcess(delta);
        break;
    }
  }

  private void MoveProcess(float delta)
  {
    base._PhysicsProcess(delta);
    Vector2 input_velocity= Vector2.Zero;
    input_velocity.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
    input_velocity.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
    input_velocity = input_velocity.Normalized();

    if (input_velocity == Vector2.Zero) {
      mAnimationState.Travel("Idle");
      mVelocity = mVelocity.MoveToward(input_velocity * mMaxSpeed, mFriction);
    } else {
      mRollVelocity = input_velocity;
      mAnimation.Set("parameters/Idle/blend_position", input_velocity);
      mAnimation.Set("parameters/Run/blend_position", input_velocity);
      mAnimation.Set("parameters/Attack/blend_position", input_velocity);
      mAnimation.Set("parameters/Roll/blend_position", input_velocity);
      mAnimationState.Travel("Run");
      mVelocity = mVelocity.MoveToward(input_velocity * mMaxSpeed, mAcceleration);
    }
    
    mVelocity = MoveAndSlide(mVelocity);
    if (Input.IsActionJustPressed("attack")) {
      mPlayerSate = PlayerSate.ATTACK;
    } else if (Input.IsActionJustPressed("roll")) {
      mPlayerSate = PlayerSate.ROLL;
    }
  }

  private void AttacksProcess(float delta) {
    mVelocity = Vector2.Zero;
    mAnimationState.Travel("Attack");
  }

  private void RollProcess(float delta) {
    Vector2 velocity = mRollMaxSpeed * mRollVelocity;
    mAnimationState.Travel("Roll");
    MoveAndSlide(velocity);
  }

  public void RollAnimationFinish()
  {
    mPlayerSate = PlayerSate.MOVE;
  }
  public void AttackAnimationFinish()
  {
    mPlayerSate = PlayerSate.MOVE;
  }

  public void _on_Hurt()
  {
    mHealth--;
    mHurtBox.StartInvincible(1);
    EmitSignal("HealthChange",mHealth);
    if (mHealth==0) {
      QueueFree();
    }
  }
}