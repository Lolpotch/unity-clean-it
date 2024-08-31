using UnityEngine;
using UnityEngine.UI;

public class CounterManager : MonoBehaviour
{
    [SerializeField] Text coinCounter = null;
    [SerializeField] Text maskCounter = null;

    [HideInInspector] public int coinAmount = 0;
    [HideInInspector] public int maskAmount = 0;
    [HideInInspector] public int givenMaskAmount = 0;
    [HideInInspector] public int disinfectedObjectsAmount = 0;

    void Start()
    {
        DisplayCoin();
        DisplayMask();
    }

    void DisplayCoin()
    {
        coinCounter.text = coinAmount.ToString();
    }

    void DisplayMask()
    {
        maskCounter.text = maskAmount.ToString();
    }

    public void AddCoin(int amount)
    {
        coinAmount += amount;
        DisplayCoin();
    }

    public void AddMask(int amount)
    {
        maskAmount += amount;
        DisplayMask();
    }

    public void AddGivenMask(int amount)
    {
        givenMaskAmount += amount;
    }

    public void AddDisinfected(int amount)
    {
        disinfectedObjectsAmount += amount;
    }
}
