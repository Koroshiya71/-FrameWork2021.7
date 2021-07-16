using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;
public class TestNoticeUI : BaseUI {

    private Button btn_Close;

    //用于显示公告名称的Text
    private Text text_Name;

    //用于显示公告内容的Text
    private Text text_Details;

    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        btn_Close = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Close");
        text_Details = GameTool.GetTheChildComponent<Text>(this.gameObject, "Text_Details");
        btn_Close.onClick.AddListener(Close);
        Debug.Log(DataController.Instance.dicTestNotice);
        //从配置表读取公告内容
        text_Details.text = DataController.Instance.ReadCfg("Name", 1, DataController.Instance.dicTestNotice);

        text_Details.text +="  "+ DataController.Instance.ReadCfg("Time", 1, DataController.Instance.dicTestNotice);

        text_Details.text +="   "+ DataController.Instance.ReadCfg("Details", 1, DataController.Instance.dicTestNotice);


        text_Details.text +="\n\n"+ DataController.Instance.ReadCfg("Name", 2, DataController.Instance.dicTestNotice);

        text_Details.text +="  "+ DataController.Instance.ReadCfg("Time", 2, DataController.Instance.dicTestNotice);

        text_Details.text += "   " + DataController.Instance.ReadCfg("Details", 2, DataController.Instance.dicTestNotice);

        text_Details.text += "\n\n" + DataController.Instance.ReadCfg("Name", 3, DataController.Instance.dicTestNotice);

        text_Details.text += "  " + DataController.Instance.ReadCfg("Time", 3, DataController.Instance.dicTestNotice);

        text_Details.text += "   " + DataController.Instance.ReadCfg("Details", 3, DataController.Instance.dicTestNotice);
        
    }

    private void Close()
    {
        UIManager.Instance.HideSingleUI(this.uiId);

    }

    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiType.showMode = E_ShowUIMode.DoNothing;
    }
}
