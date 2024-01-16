using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class HealthText : MonoBehaviour
{
    public Vector3 moveSpeed = new Vector3 (0, 75, 0);

    RectTransform textTransform;
    TextMeshProUGUI textMeshPro;

    public float timeToFade = 1f;
    private float timeElapsed = 0f;

    private Color startColor;

    private void Awake()
    {
        textTransform = GetComponent<RectTransform>();
        textMeshPro = GetComponent<TextMeshProUGUI>();
        startColor = textMeshPro.color;
    }

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        textTransform.position += moveSpeed * Time.deltaTime;

        timeElapsed += Time.deltaTime;

        if (timeElapsed < timeToFade)
        {
            float alpha = startColor.a * (1- timeElapsed/timeToFade);   
            textMeshPro.color = new Color(startColor.r, startColor.g, startColor.b, alpha);
        }
        else
        {
            Destroy(gameObject);
        }
    }
}
