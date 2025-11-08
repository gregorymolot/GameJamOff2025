using UnityEngine;
using UnityEngine.UI;

public class Test : MonoBehaviour
{
    [SerializeField]
    Image image;

    bool isGoingDown = true;

    float currentSizeX;

    float timer = 0f;

    // Update is called once per frame
    void Update()
    {

        Vector2 currentSize = image.rectTransform.sizeDelta;

        if (isGoingDown)
        {

            currentSize.x = Mathf.Lerp(100, 0, timer*2f);
        }
        else
        {
            currentSize.x = Mathf.Lerp(0, 100, timer*2f);
        }

        currentSizeX = currentSize.x;

        image.rectTransform.sizeDelta = currentSize;

        timer += Time.deltaTime;
        
        if (currentSizeX == 0 && isGoingDown)
        {
            isGoingDown = false;
            timer = 0f;
        }
    }
}
