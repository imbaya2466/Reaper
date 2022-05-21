using Godot;
using System;

public class MainUI : Control
{
    
    private int mHearts = 4;
    private int mMaxHeart = 4; 

    private TextureRect mHealthEmpty;
    private TextureRect mHealthFull;

    public override void _Ready()
    {
        mHealthFull = GetNode<TextureRect>("HeratFull");
        mHealthEmpty = GetNode<TextureRect>("HeratEmpty");
    }

    public void SetHearts(int val)
    {
        mHearts = Mathf.Clamp(val,0,mMaxHeart);
        if (mHealthFull!= null) {
            mHealthFull.SetSize(new Vector2(val*15,mHealthFull.RectSize.y));
        }
    }

    public void SetMaxHearts(int val)
    {
        mMaxHeart = Mathf.Max(1,val);
        if (mHealthEmpty!= null) {
            mHealthEmpty.SetSize(new Vector2(mMaxHeart*15,mHealthEmpty.RectSize.y));
        }
    }

}
