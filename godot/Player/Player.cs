using Godot;
using System;

public class Player : KinematicBody2D
{
  public int mMaxSpeed = 100;
  public float mAcceleration = 10;
  public float mFriction = 10;
 
  public Vector2 mVelocity = Vector2.Zero;

  public PlayerSate mPlayerSate= PlayerSate.MOVE;

  public AnimationTree mAnimation = null;
  public AnimationNodeStateMachinePlayback mAnimationState = null;

  public enum PlayerSate{
    MOVE,
    ROLL,
    ATTACK
  } 

  public override void _Ready()
  {
    mAnimation = GetNode<AnimationTree>("AnimationTree");
    mAnimationState = (AnimationNodeStateMachinePlayback)mAnimation.Get("parameters/playback");
  }
 
  public override void _Process(float delta)
  {
    base._PhysicsProcess(delta);
    switch (mPlayerSate) {
      case PlayerSate.MOVE:
        MoveProcess(delta);
        break;
      case PlayerSate.ROLL:
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
      mAnimation.Set("parameters/Idle/blend_position", input_velocity);
      mAnimation.Set("parameters/Run/blend_position", input_velocity);
      mAnimation.Set("parameters/Attack/blend_position", input_velocity);
      mAnimationState.Travel("Run");
      mVelocity = mVelocity.MoveToward(input_velocity * mMaxSpeed, mAcceleration);
    }
    
    mVelocity = MoveAndSlide(mVelocity);
    if (Input.IsActionJustPressed("attack")) {
      mPlayerSate = PlayerSate.ATTACK;
    }
  }

  private void AttacksProcess(float delta) {
    mVelocity = Vector2.Zero;
    mAnimationState.Travel("Attack");
  }

  public void AttackAnimationFinish() {
    mPlayerSate = PlayerSate.MOVE;
  }
}