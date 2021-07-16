using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;
public class ShopUI : BaseUI {

    private Button btn_Return;
    private Button btn_ToEquip;
    private Button btn_EnterMyScene;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        btn_Return = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Return");
        btn_Return.onClick.AddListener(ReturnToMain);

        btn_ToEquip = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_ToEquip");
        btn_ToEquip.onClick.AddListener(ToEquipUI);

        btn_EnterMyScene = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_EnterMyScene");
        btn_EnterMyScene.onClick.AddListener(EnterMyScene);
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.ShopUI;
    }
	public override void AddMessageListener()
    {
 	    base.AddMessageListener();
    }

    private void ShowTestUI()
    {
        Debug.Log("成功切换到MyScene");
        UIManager.Instance.ShowUI(E_UiId.TestUI);
    }

    public override void RemoveMessageListener()
    {
        base.RemoveMessageListener();
    }

    private  void ToEquipUI()
    {
        UIManager.Instance.ShowUI(E_UiId.EquipUI);
    }
    private void ReturnToMain()
    {
        UIManager.Instance.ReturnBeforeUI(E_UiId.MainUI);
    }
    
    private void EnterMyScene()
    {
        SceneController.Instance.LoadSceneAsync("MyScene", ShowTestUI);
    }
}
