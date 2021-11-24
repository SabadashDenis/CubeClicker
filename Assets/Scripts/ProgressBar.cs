using UnityEngine;
using UnityEngine.UI;

[ExecuteInEditMode]
 public class ProgressBar
{
    private int maximum;
    private int minimum;
    private int current;
    private Image mask;

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
