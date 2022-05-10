using Godot;
using System;

public class Grass : Node2D
{
    // Declare member variables here. Examples:
    // private int a = 2;
    // private string b = "text";

    // Called when the node enters the scene tree for the first time.


    private AnimatedSprite mAnimatedSprite;
    private Sprite mSprite;

    public override void _Ready()
    {
        mAnimatedSprite = GetNode<AnimatedSprite>("AnimatedSprite");
        mSprite = GetNode<Sprite>("Sprite");
    }

    public override void _Process(float delta)
    {
        if (Input.IsActionJustPressed("attack")) {
            BeKilled();
        }
    }

    public void BeKilled() {
        mSprite.Hide();
        mAnimatedSprite.Show();
        mAnimatedSprite.Play("default");
    }

    public void _on_AnimatedSprite_animation_finished(){
        QueueFree();
    }
}
