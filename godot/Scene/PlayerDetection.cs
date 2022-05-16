using Godot;
using System;

public class PlayerDetection : Area2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    public Node2D mPlayer = null;
    public override void _Ready()
    {
        Connect("body_entered",this,"_on_play_enter");
        Connect("body_exited",this,"_on_play_exit");
    }

    public Node2D GetPlayr()
    {
        return mPlayer;
    }

    private void _on_play_enter(Node node)
    {
        mPlayer = (Node2D)node;
    }

    private void _on_play_exit(Node node)
    {
        mPlayer = null;
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
