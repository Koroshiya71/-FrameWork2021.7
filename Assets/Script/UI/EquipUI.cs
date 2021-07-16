using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;
public class EquipUI : BaseUI {

    private Button btn_Return;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        btn_Return = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Return");
        btn_Return.onClick.AddListener(ReturnToShop);

    }
    public override void AddMessageListener()
    {
        base.AddMessageListener();
    }



    public override void RemoveMessageListener()
    {
        base.RemoveMessageListener();
    }

    private void ReturnToMain()
    {
        UIManager.Instance.ReturnBeforeUI(E_UiId.MainUI);
    }
    private void ReturnToShop()
    {
        UIManager.Instance.ReturnBeforeUI(E_UiId.ShopUI);
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.EquipUI;
    }
}
