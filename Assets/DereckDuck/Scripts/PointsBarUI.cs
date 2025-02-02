using UnityEngine.UI;
using UnityEngine;

public class PointsBarUI : MonoBehaviour
{
    public Sprite points0;
    public Sprite points1;
    public Sprite points2;
    public Sprite points3;
    public Sprite points4;
    public Sprite points5;

    private Image image;

    private void Start()
    {
        image = GetComponent<Image>();
    }

    public void UpdatePoints(int points)
    {
        switch (points)
        {
            case 0:
                image.sprite = points0;
                break;
            case 1:
                image.sprite = points1;
                break;
            case 2:
                image.sprite = points2;
                break;
            case 3:
                image.sprite = points3;
                break;
            case 4:
                image.sprite = points4;
                break;
            default:
                image.sprite = points5;
            break;
        }
    }
}
