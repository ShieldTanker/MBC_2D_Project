using UnityEngine;

public enum CharDirection
{
    Right,
    Left,
}

public class Rotation2D : MonoBehaviour
{
    public CharDirection dir;
    private bool _canFlip = true;
    public bool CanFlip
    {
        get { return _canFlip; }
        set { _canFlip = value; }
    }

    private void Update()
    {
        FlipRotation();
    }

    public void FlipRotation()
    {
        if (!_canFlip) return;
        Vector3 curr = transform.localScale;
        switch (dir)
        {
            case CharDirection.Right:
                curr.x = 1;
                break;
            case CharDirection.Left:
                curr.x = -1;
                break;
        }
        transform.localScale = curr;
    }
}
