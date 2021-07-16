using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;

public class InforUI : BaseUI
{
    private Text txt_coinCount;
    private Text text_Level;
    private Button btn_Upgrade;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        txt_coinCount = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_CoinCount");
        text_Level = GameTool.GetTheChildComponent<Text>(this.gameObject, "Text_Level");
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.InforUI;
        this.uiType.showMode = E_ShowUIMode.DoNothing;
        this.uiType.uiRootType = E_UIRootType.KeepAbove;
        this.uiType.uiPlayAudio = E_UIPlayAudio.NoPlay;
        EventDispatcher.AddListener<int>(E_MessageType.UpgradeMsg, delegate(int level)
        {
            PlayerData.Instance.EditorGrade(level);
            text_Level.text = PlayerData.Instance.Grade.ToString();

        });
    }
    protected override void Start()
    {
        base.Start();
        txt_coinCount.text = InforData.Instance.CoinCount.ToString();
    }
   
    public override void AddMessageListener()
    {
        EventDispatcher.AddListener<int>(E_MessageType.SellGoods, UpdateCoin);
    }
    public override void RemoveMessageListener()
    {
        EventDispatcher.RemoveListener<int>(E_MessageType.SellGoods, UpdateCoin);
    }
    private void UpdateCoin(int coinCount)
    {
        txt_coinCount.text = coinCount.ToString();
    }
    private void UpdateLevel()
    {
        text_Level.text = LevelData.Instance.PlayerLevel.ToString();
    }
}
