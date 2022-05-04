using Godot;
using System;

public class Player : KinematicBody2D
{
  // Declare member variables here. Examples:
  // private int a = 2;
  // private string b = "text";
  // Called when the node enters the scene tree for the first time.

  public int MaxSpeed = 100;
  public float Acceleration = 10;
  public float Friction = 10;
 
  public Vector2 Velocity = Vector2.Zero;
 
  public override void _PhysicsProcess(float delta)
  {
    base._PhysicsProcess(delta);
    Vector2 input_velocity= Vector2.Zero;
    input_velocity.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
    input_velocity.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
    input_velocity = input_velocity.Normalized();

    Velocity = Velocity.MoveToward(input_velocity * MaxSpeed, input_velocity == Vector2.Zero?Friction:Acceleration);
    GD.Print(Velocity);
    Velocity = MoveAndSlide(Velocity);
  }
}