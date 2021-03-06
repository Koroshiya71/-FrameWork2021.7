using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;

public class PlayUI : BaseUI {

    private Button btn_Exit;
   
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        btn_Exit = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Exit");
      
        btn_Exit.onClick.AddListener(ShowExitUI);
      
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.PlayUI;
        this.uiType.showMode = E_ShowUIMode.HideAll;
        this.uiType.uiPlayAudio = E_UIPlayAudio.NoPlay;
    }
    private void ShowExitUI()
    {
        // Debug.Log("显示退出界面");
        UIManager.Instance.ShowUI(E_UiId.ExitUI);
    }
   
}
