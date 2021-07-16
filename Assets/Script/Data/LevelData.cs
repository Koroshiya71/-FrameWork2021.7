using UnityEngine;
using System.Collections;
using GameCore;
public class LevelData : Singleton<LevelData> {

    //玩家等级
    private int playerLevel = 1;

    public int PlayerLevel
    {
        get
        {
            return playerLevel ;
        }

    }
    public void InitLevelData()
    {
        if (!GameTool.HasKey("PlayerLevel"))
        {
            GameTool.SetInt("PlayerLevel", 1);
        }
        playerLevel = GameTool.GetInt("PlayerLevel");
        
    }

   
    public void EditLevel(int level)
    {

        GameTool.SetInt("PlayerLevel", playerLevel + level);
        playerLevel = GameTool.GetInt("PlayerLevel");

    }
}
