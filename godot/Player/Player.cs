using Godot;
using System;

public class Player : KinematicBody2D
{
  // Declare member variables here. Examples:
  // private int a = 2;
  // private string b = "text";
  // Called when the node enters the scene tree for the first time.

  public int MaxSpeed = 160;
  public int Acceleration = 20;
  public int Friction = 1;
 
  public Vector2 Velocity = Vector2.Zero;

  public override void _PhysicsProcess(float delta)
  {
    base._PhysicsProcess(delta);
    Vector2 input_velocity= Vector2.Zero;
    input_velocity.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
    input_velocity.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
    input_velocity = input_velocity.Normalized();

    if (input_velocity == Vector2.Zero) {
      Velocity = Velocity.MoveToward(Vector2.Zero,Friction);
    } else {
      Velocity += input_velocity*Acceleration*delta;
      Velocity = Velocity.Clamped(MaxSpeed *delta);
    }

    GD.Print(Velocity);
    MoveAndCollide(Velocity);
  }
}