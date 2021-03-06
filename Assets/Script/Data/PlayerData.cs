using UnityEngine;
using System.Collections;
using GameCore;
public class PlayerData : Singleton<PlayerData>
{   
    //角色等级
    private int grade=1;

    public int Grade
    {
        get
        {
            return grade;
        }
    }
    public void EditorGrade(int newGrade)
    {
        grade = newGrade;
        GameTool.SetInt("PlayerData",newGrade);
    }
    public void InitPlayerData()
    {
        if (!GameTool.HasKey("PlayerData"))
        {
            GameTool.SetInt("PlayerData",1); 
        }
        grade = GameTool.GetInt("PlayerData");
    }
}
