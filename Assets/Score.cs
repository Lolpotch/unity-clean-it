using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour
{
    CounterManager counter;
    [SerializeField] Text[] scores;
    int[] amount;

    private void Awake()
    {
        counter = FindObjectOfType<CounterManager>();

        amount = new int[3];
        amount[0] = counter.coinAmount;
        amount[1] = counter.givenMaskAmount;
        amount[2] = counter.disinfectedObjectsAmount;
    }

    void Start()
    {
        for(int i = 0; i < scores.Length; i++)
        {
            scores[i].text = amount[i].ToString() + scores[i].text;
        }
    }

    void Update()
    {
        
    }
}
