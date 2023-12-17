using UnityEngine;
using UnityEngine.UI;

public class ScrollText : MonoBehaviour
{
    public float scrollSpeed = 30f; // Adjust the speed as needed
    private RectTransform textRect;

    void Start()
    {
        textRect = GetComponent<RectTransform>();
    }

    void Update()
    {
        ScrollTextHorizontally();
    }

    void ScrollTextHorizontally()
    {
        // Move the text to the left
        textRect.anchoredPosition += new Vector2(-scrollSpeed * Time.deltaTime, 0f);

        // Check if the text has moved out of the mask
        if (textRect.anchoredPosition.x < -textRect.sizeDelta.x)
        {
            // Reset the position to the right of the mask
            textRect.anchoredPosition = new Vector2(0f, 0f);
        }
    }
}
