using Godot;
using System;

public class Main : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.
    Player mPlayer;
    MainUI mMainUI;
    public override void _Ready()
    {
        mMainUI = GetNode<MainUI>("CanvasLayer/HealthUI");
        mPlayer = GetNode<Player>("YSort/Player");
        mMainUI.SetMaxHearts(mPlayer.mHealth);
        mMainUI.SetHearts(mPlayer.mHealth);
        mPlayer.Connect("HealthChange",mMainUI , "SetHearts");
    }

//  // Called every frame. 'delta' is the elapsed time since the previous frame.
//  public override void _Process(float delta)
//  {
//      
//  }
}
