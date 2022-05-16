using Godot;
using System;

public class HurtBox : Area2D
{

    [Export]
    public bool mShowHit = true;

    [Export]
    public PackedScene mHitEffect;
    public override void _Ready()
    {
        mHitEffect = GD.Load<PackedScene>("res://Scene/HitEffect.tscn");
        if (mShowHit) {
            Connect("area_entered",this,"_on_Hit");
        }
    }

    public void _on_Hit(Area2D area)
    {
        AnimatedSprite hit_effect =  mHitEffect.Instance<AnimatedSprite>();
        AddChild(hit_effect);
        hit_effect.GlobalPosition = GlobalPosition;
    }

}
