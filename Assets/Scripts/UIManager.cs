using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public Text coinText;

  

    public void UpdateCoins(int coin)
    {
        coinText.text = coin.ToString();
    }
    void Start()
    {

    }
}
