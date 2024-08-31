using UnityEngine;
using UnityEngine.UI;

public class SprayBar : MonoBehaviour
{
    RectTransform rect;
    Image image;

    float energy = 1f;
    [SerializeField] float energyGrowRate = 0f;
    [SerializeField] Gradient color = null;

    [HideInInspector] public bool isReady;

    private void Awake()
    {
        isReady = true;
        rect = GetComponent<RectTransform>();
        image = GetComponent<Image>();
    }
    void Update()
    {
        DisplayBar();
    }

    void DisplayBar()
    {
        if(energy < 1f)
        {
            energy += energyGrowRate * Time.deltaTime;
        }
        if (energy > 1f && !isReady)
        {
            isReady = true;
            energy = 1f;
        }

        Vector3 scaler = new Vector3(energy, rect.localScale.y, rect.localScale.z);
        rect.localScale = scaler;

        image.color = color.Evaluate(energy);
    }

    public void UseEnergy()
    {
        energy = 0f;
        isReady = false;
    }
}
