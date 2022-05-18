using Godot;
using System;

public class HurtBox : Area2D
{

    [Export]
    public bool mShowHit = true;

    private bool mInvincible = false;
    private Timer mTimer;

    [Export]
    public PackedScene mHitEffect;
    public override void _Ready()
    {
        mHitEffect = GD.Load<PackedScene>("res://Scene/HitEffect.tscn");
        if (mShowHit) {
            Connect("area_entered",this,"_on_Hit");
        }
        mTimer = GetNode<Timer>("Timer");
        mTimer.Connect("timeout",this,"OnTimer");
    }

    [Signal]
    delegate void OnHit();

    public void StartInvincible(float time)
    {
        mTimer.Start(time);
        mInvincible = true;
    }

    private void OnTimer()
    {
        mInvincible = false;
        Godot.Collections.Array areas = GetOverlappingAreas();
        foreach(Area2D i in areas){
            if (i.Name == "HitBox") {
                _on_Hit(i);
                break;
            }
        }
    }

    public void _on_Hit(Area2D area)
    {   
        if (mInvincible) {
            return;
        }
        if (area.Name == "HitBox") {
            EmitSignal(nameof(OnHit));
            AnimatedSprite hit_effect =  mHitEffect.Instance<AnimatedSprite>();
            AddChild(hit_effect);
            hit_effect.GlobalPosition = GlobalPosition;
        }
    }

}
