using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;
public class TestUI : BaseUI
{
    private Button btn_Exit;
    private Image table_Exit;
    private Button btn_Close;
    private Button btn_ToMain;
    private Button btn_ToLevel;
    private void start()
    {
        HideExitTable();

    }
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();

        btn_Exit = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_Exit");
        btn_Exit.onClick.AddListener(ShowExitTable);
        table_Exit=GameTool.GetTheChildComponent<Image>(gameObject,"Table_Exit");
        HideExitTable();

        btn_Close = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_Close");
        btn_Close.onClick.AddListener(HideExitTable);

        btn_ToMain = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_ToMain");
        btn_ToMain.onClick.AddListener(EnterMainScene);

        btn_ToLevel = GameTool.GetTheChildComponent<Button>(gameObject, "Btn_ToLevel");
        btn_ToLevel.onClick.AddListener(EnterLevelScene);


        HideExitTable();
    }
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.TestUI;
    }
    private void ShowExitTable()
    {
        table_Exit.gameObject.SetActive (true);
    }
    private void HideExitTable()
    {
        table_Exit.gameObject.SetActive (false);

    }

    private void EnterMainScene()
    {
        SceneController.Instance.LoadSceneAsync("MainScene", delegate
        {
            UIManager.Instance.ShowUI(E_UiId.MainUI, false);
            UIManager.Instance.ShowUI(E_UiId.InforUI, false);
        });
    }

    private void EnterLevelScene()
    {
        SceneController.Instance.LoadSceneAsync("MainScene", delegate
        {
            UIManager.Instance.ShowUI(E_UiId.LevelUI, false);
            UIManager.Instance.ShowUI(E_UiId.InforUI, false);
        });
    }
}
