using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using System.Collections.Generic;

public class LevelUI : BaseUI {

    private Button btn_Return;
    private Button btn_ToPackUI;
    //关卡预制体
    private GameObject levelUp;
    private GameObject levelDown;
    private Transform content;

    //所有的关卡
    //private List<GameObject> AllLevelList=new List<GameObject>();

    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        btn_Return = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Return");
        btn_ToPackUI= GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_ToPackUI");
        content = GameTool.FindTheChild(this.gameObject, "Content");
        btn_Return.onClick.AddListener(ReturnUI);
        btn_ToPackUI.onClick.AddListener(ToPackUI);
       
    }
    //private void LoadLevelScene(GameObject go)
    //{
    //    //Debug.Log("********");
    //    int levelId =int.Parse( GameTool.FindTheChild(go, "Txt_Level").GetComponent<Text>().text);
    //    string sceneFileName = DataController.Instance.ReadCfg("SceneFileName",levelId,DataController.Instance.dicLevel);
    //    SceneManager.LoadScene(sceneFileName);
    //    UIManager.Instance.ShowUI(E_UiId.PlayUI);
    //}
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.LevelUI;
        this.uiType.showMode = E_ShowUIMode.HideOther;
       
    }
    protected override void Start()
    {
        InitLevels();
        //for (int i = 0; i < content.childCount; i++)
        //{
        //    AllLevelList.Add(content.GetChild(i).FindChild("Btn_Level").gameObject);
        //    UguiEventListener.Get(AllLevelList[i]).onClick = LoadLevelScene;
        //}
    }
    private void InitLevels()
    {
        levelUp = Resources.Load<GameObject>("Level/LevelUp");
        levelDown = Resources.Load<GameObject>("Level/LevelDown");
        AutoSetContentWidth(15);
        GameObject level;
        for (int i = 0; i < 15; i++)
        {
            if (i % 2 == 0)//LevelDown
            {
                level= Instantiate(levelDown);
            }
            else//LevelUp
            {
                level = Instantiate(levelUp);
            }
            LevelEntity levelEntity= GameTool.AddTheChildComponent<LevelEntity>(level, "Btn_Level");
            levelEntity.levelId = i + 1;
            GameTool.GetTheChildComponent<Text>(level, "Txt_Level").text = (i + 1).ToString();
            string sceneName= DataController.Instance.ReadCfg("SceneName", i + 1, DataController.Instance.dicLevel);
            GameTool.GetTheChildComponent<Text>(level, "Txt_LevelName").text = sceneName;
            GameTool.AddChildToParent(content, level.transform);
        }
    }
    private void AutoSetContentWidth(int levelCount)
    {
        //获取关卡预制体的宽度
        float width = content.GetComponent<GridLayoutGroup>().cellSize.x;
        //每一个关卡的间距
        float offset = content.GetComponent<GridLayoutGroup>().spacing.x;
        float allWidth = width * levelCount+ offset*(levelCount-1);
        content.GetComponent<RectTransform>().sizeDelta = new Vector2(allWidth,0);
    }
    private void ReturnUI()
    {
        //UIManager.Instance.ShowUI(this.BeforeUiId);
        UIManager.Instance.ReturnBeforeUI(this.BeforeUiId);
    }
    private void ToPackUI()
    {
        UIManager.Instance.ShowUI(E_UiId.PackUI);
    }
}
