using Godot;
using System;

public class Player : KinematicBody2D
{
  // Declare member variables here. Examples:
  // private int a = 2;
  // private string b = "text";
  // Called when the node enters the scene tree for the first time.
  public override void _Ready()
  {
    GD.Print("Player Ready");
  }
  public override void _PhysicsProcess(float delta)
  {
    base._PhysicsProcess(delta);
    Vector2 velocity= Vector2.Zero;
    velocity.x = Input.GetActionStrength("ui_right") - Input.GetActionStrength("ui_left");
    velocity.y = Input.GetActionStrength("ui_down") - Input.GetActionStrength("ui_up");
    MoveAndCollide(velocity);
  }

}
