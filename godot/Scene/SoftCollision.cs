using Godot;
using System;

public class SoftCollision : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    [Signal]
    delegate void SoftCollisionEnter(Vector2 val);

    // Called when the node enters the scene tree for the first time.
    public override void _Ready()
    {
        Connect("area_entered",this,"OnSoftCollisionEnter");
    }

    private void OnSoftCollisionEnter(Area2D area)
    {
        if (area is SoftCollision) {
            Vector2 vec = area.GlobalPosition.DirectionTo(this.GlobalPosition);
            EmitSignal("SoftCollisionEnter",vec);
        }
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
