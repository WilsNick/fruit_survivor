using UnityEngine;
using TMPro;

public class FloatingText : MonoBehaviour
{
    public float moveSpeed = 1f; // Speed at which the text moves upwards
    public float fadeSpeed = 2f; // Speed at which the text fades out
    public float destroyTime = 1.5f; // Time duration before the text is destroyed

    public TextMeshPro textMesh; // Reference to the TextMeshPro component
    private Color startColor; // The initial color of the text
    private float timer = 0f; // Timer to track the duration of the floating text

    private void Start()
    {
        // Cache the initial color of the text
        startColor = textMesh.color;
    }

    private void Update()
    {
        // Move the text upwards
        transform.position += Vector3.up * moveSpeed * Time.deltaTime;

        // Calculate the alpha value based on the timer and destroyTime
        float alpha = 1f - timer / destroyTime;

        // Set the new color of the text with updated alpha value
        textMesh.color = new Color(startColor.r, startColor.g, startColor.b, alpha);

        // Increment the timer
        timer += Time.deltaTime;

        // Destroy the floating text after destroyTime
        if (timer >= destroyTime)
        {
            Destroy(gameObject);
        }
    }

    public void SetText(string text, Color textColor)
    {
        // Set the text and color of the TextMeshPro component
        textMesh.text = text;
        textMesh.color = textColor;
    }
}
