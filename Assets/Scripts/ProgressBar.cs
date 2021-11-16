using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
public class ProgressBar
{
    public int maximum;
    public int minimum;
    public int current;
    public Image mask;

    public ProgressBar(int curr,int min, int max, Image mask)
    {
        this.mask = mask;
        minimum = min;
        current = curr;
        maximum = max;
    }

    public void GetCurrentFill()
    {
        float fillAmount = (float)(current - minimum) / (float)(maximum - minimum);
        mask.fillAmount = fillAmount;    }
}
