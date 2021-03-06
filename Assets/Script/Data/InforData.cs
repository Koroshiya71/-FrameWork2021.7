using UnityEngine;
using System.Collections;
using GameCore;
public class InforData : Singleton<InforData>
{
    //所有的金币数量
    private int coinCount = 0;
    //红药水的数量
    private int redCount = 0;

    public int CoinCount
    {
        get
        {
            return coinCount;
        }

    }

    public int RedCount
    {
        get
        {
            return redCount;
        }

    }

    public void InitInforData()
    {
        if (!GameTool.HasKey("CoinCount"))
        {
            GameTool.SetInt("CoinCount", 0);
        }
        coinCount = GameTool.GetInt("CoinCount");
        if (!GameTool.HasKey("RedCount"))
        {
            GameTool.SetInt("RedCount", 0);
        }
        redCount = GameTool.GetInt("RedCount");
    }
    public void EditorCoin(int newCoinCount)
    {
        GameTool.SetInt("CoinCount", newCoinCount);
        coinCount = newCoinCount;
    }
    public void EditorRed(int newRedCount)
    {
        GameTool.SetInt("RedCount", newRedCount);
        redCount = newRedCount;
    }
}
