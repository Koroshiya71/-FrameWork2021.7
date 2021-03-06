using UnityEngine;
using System.Collections;
using GameCore;
using UnityEngine.UI;

public class PackUI : BaseUI
{
    private Button btn_Return;
    private GameObject goodsPrefab;
    private Transform content;
    //五个物品分类按钮
    private GameObject btn_All;
    private GameObject btn_Equipment;
    private GameObject btn_Potions;
    private GameObject btn_Rune;
    private GameObject btn_Material;
    //背包物品类型的指示图标
    private Transform pointer;
    //被选中的物品类型
    private E_GoodsType beChooseType = E_GoodsType.Default;
    //被选中物品信息面板的相关元素
    private Image img_BeChooseGoods;
    private Text txt_BeChooseName;
    private Text txt_HasCount;
    //被选中物品的ID
    private int beChooseId = 0;
    private Text txt_Introduce;
    private Text txt_Price;
    //当前显示的物品类型
    //private E_GoodsType currentType = E_GoodsType.Default;

    //出售面板的相关元素
    //出售按钮
    private Button btn_Sell;
    //出售面板
    private GameObject sellPanel;
    //取消出售按钮
    private Button btn_Cancel;
    //确定出售按钮
    private Button btn_Sure;
    //增加出售数量的按钮
    private Button btn_Add;
    //减少出售数量的按钮
    private Button btn_Minus;
    //当前被选中物品的数量
    private int beChooseCount = 0;
    //将要出售的数量
    private int sellCount = 0;
    //用于显示将要出售物品数量的Text
    private Text txt_SellCount;

    private Scrollbar scrollbar;
    //未选中物品前的图片
    private Sprite defaultSprite;
    protected override void InitUiOnAwake()
    {
        base.InitUiOnAwake();
        defaultSprite = Resources.Load<Sprite>("PackSprite/GoodsDefault");
       
        goodsPrefab = Resources.Load<GameObject>("GoodsPrefab/Goods");
        btn_Return = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Return");
        content = GameTool.FindTheChild(this.gameObject,"Content");
        btn_Return.onClick.AddListener(ReturnUI);

        btn_All = GameTool.FindTheChild(this.gameObject, "Btn_All").gameObject;
        btn_Equipment = GameTool.FindTheChild(this.gameObject, "Btn_Equipment").gameObject;
        btn_Potions = GameTool.FindTheChild(this.gameObject, "Btn_Potions").gameObject;
        btn_Rune = GameTool.FindTheChild(this.gameObject, "Btn_Rune").gameObject;
        btn_Material = GameTool.FindTheChild(this.gameObject, "Btn_Material").gameObject;
        pointer = GameTool.FindTheChild(btn_All.gameObject, "Pointer");

        img_BeChooseGoods = GameTool.GetTheChildComponent<Image>(this.gameObject, "Img_BeChooseGoods");
        txt_BeChooseName = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_BeChooseName");
        txt_HasCount = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_HasCount");
        txt_Introduce = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_Introduce");
        txt_Price = GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_Price");

        btn_Sell = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Sell");
        btn_Sure = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Sure");
        sellPanel = GameTool.FindTheChild(this.gameObject,"SellPanel").gameObject;
        btn_Cancel= GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Cancel");
        btn_Add= GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Add");
        btn_Minus = GameTool.GetTheChildComponent<Button>(this.gameObject, "Btn_Minus");
        txt_SellCount= GameTool.GetTheChildComponent<Text>(this.gameObject, "Txt_SellCount");

        btn_Sell.onClick.AddListener(ShowSellPanel);
        btn_Sure.onClick.AddListener(Sell);
        btn_Cancel.onClick.AddListener(HideSellPanel);
        btn_Add.onClick.AddListener(AddSellGoods);
        btn_Minus.onClick.AddListener(MinusSellGoods);

        UguiEventListener.Get(btn_All).onClick = ChangeType;
        UguiEventListener.Get(btn_Equipment).onClick = ChangeType;
        UguiEventListener.Get(btn_Potions).onClick = ChangeType;
        UguiEventListener.Get(btn_Rune).onClick = ChangeType;
        UguiEventListener.Get(btn_Material).onClick = ChangeType;

        scrollbar = GameTool.GetTheChildComponent<Scrollbar>(this.gameObject, "Scrollbar Vertical");
    }
    
    protected override void InitDataOnAwake()
    {
        base.InitDataOnAwake();
        this.uiId = E_UiId.PackUI;
        CoroutineController.Instance.StartCoroutine(InitPack());     
    }
    private void ChangeType(GameObject go)
    {
        switch (go.name)
        {
            case "Btn_Equipment":
                beChooseType = E_GoodsType.Equipment;
                break;
            case "Btn_Potions":
                beChooseType = E_GoodsType.Potions;
                break;
            case "Btn_Rune":
                beChooseType = E_GoodsType.Rune;
                break;
            case "Btn_Material":
                beChooseType = E_GoodsType.Material;
                break;
            default:
                beChooseType = E_GoodsType.Default;
                break;
        }
        ShowGoodsByType();
        GameTool.AddChildToParent(go.transform, pointer);
        pointer.localPosition = new Vector3(-147,0,0);
       // currentType = beChooseType;
    }
    private void Sell()
    {
        sellPanel.SetActive(false);
        //计算销售后所得到的金币数
        int getCoinCount= sellCount * (int.Parse(txt_Price.text));
        int allCoinCount = InforData.Instance.CoinCount + getCoinCount;
        //InforData.Instance.CoinCount = allCoinCount;
        InforData.Instance.EditorCoin(allCoinCount);
        EventDispatcher.TriggerEvent<int>(E_MessageType.SellGoods, allCoinCount);
        //计算销售后的剩下的件数
        txt_HasCount.text = (int.Parse(txt_HasCount.text) - sellCount).ToString();
        beChooseCount = int.Parse(txt_HasCount.text);
        PackData.Instance.EditorGoodsCount(beChooseId.ToString(), beChooseCount);
        UpdateGoodsCount();
    }
    //更新背包中物品的数量显示
    private void UpdateGoodsCount()
    {
        for (int i = 0; i < content.childCount; i++)
        {
            GoodsEntity entity = content.GetChild(i).GetComponent<GoodsEntity>();
            if (entity.goodsId ==beChooseId)
            {
                GameTool.GetTheChildComponent<Text>(content.GetChild(i).gameObject, "Txt_Count").text = txt_HasCount.text;
            }
        }
    }
    //减少出售物品的数量
    private void MinusSellGoods()
    {
        if (sellCount > 1)
        {
            sellCount--;
            txt_SellCount.text = sellCount.ToString();
        }
    }
    //增加出售物品的数量
    private void AddSellGoods()
    {
        if (beChooseCount>sellCount)
        {
            sellCount++;
            txt_SellCount.text = sellCount.ToString();
        }
    }
    //显示出售面板
    private void ShowSellPanel()
    {
        if (beChooseCount < 1)
        {
            return;
        }
        if (!sellPanel.activeInHierarchy)
        {
            sellPanel.SetActive(true);
            sellCount = 1;
            txt_SellCount.text = "1";
        }
       
    }
    //隐藏出售面板
    private void HideSellPanel()
    {
        if (sellPanel.activeInHierarchy)
        {
            sellPanel.SetActive(false);
        }
    }
  
    //重置背包
    private void ResetPack()
    {   
        //重置背包分类
        beChooseType = E_GoodsType.Default;
        for (int i = 0; i < content.childCount; i++)
        {
            if (!content.GetChild(i).gameObject.activeInHierarchy)
            {
                content.GetChild(i).gameObject.SetActive(true);
            }                
        }
        //重置销售面板
        //if (sellPanel.activeInHierarchy)
        //{
            sellPanel.SetActive(false);
        //}   
        txt_BeChooseName.text = "未选中";
        txt_Introduce.text = "无";
        txt_Price.text = "0";
        txt_HasCount.text = "0";
        img_BeChooseGoods.sprite = defaultSprite;
        GameTool.AddChildToParent(btn_All.transform, pointer);
        pointer.localPosition = new Vector3(-147, 0, 0);
        scrollbar.value = 1;
        beChooseCount = 0;
        // StartCoroutine(ResetScrollerbar());
        //CoroutineController.Instance.StartCoroutine(ResetScrollerbar());
    }
    //private IEnumerator ResetScrollerbar()
    //{
    //    yield return new WaitForEndOfFrame();
    //    scrollbar.value = 1;
    //}
    //根据类别来显示物品
    private void ShowGoodsByType()
    {
        //显示全部
        if (beChooseType == E_GoodsType.Default)
        {
            for (int i = 0; i < content.childCount; i++)
            {
                if (content.GetChild(i).gameObject.activeInHierarchy==false)
                {
                    content.GetChild(i).gameObject.SetActive(true);
                }
            }
        }
        else//显示其他类型
        {
            for (int i = 0; i < content.childCount; i++)
            {
                if (content.GetChild(i).GetComponent<GoodsEntity>().type == beChooseType)
                {
                    content.GetChild(i).gameObject.SetActive(true);
                }
                else
                {
                    content.GetChild(i).gameObject.SetActive(false);
                }
            }
        }
    }
   IEnumerator InitPack()
    {
       //等待摄像机捕获到的所有元素都渲染完毕显示出来之后
        yield return  new WaitForEndOfFrame();
        GameObject goods;
        string iconName;
        for (int i = 0; i < 22; i++)
        {  
          //生成一个物品
          goods =  Instantiate(goodsPrefab);
           //放在content下面
          GameTool.AddChildToParent(content, goods.transform);
           //通过读取配置表,获取图片的名称
          iconName = DataController.Instance.ReadCfg("IconName",i+1,DataController.Instance.dicPack);
          GameTool.GetTheChildComponent<Image>(goods, "Img_Goods").sprite = Resources.Load<Sprite>("PackSprite/"+ iconName);
          GoodsEntity entity= goods.AddComponent<GoodsEntity>();
          entity.goodsId = i + 1;
          //读取配置表,给物品的类型赋值
          int typeIndex =int.Parse( DataController.Instance.ReadCfg("Type",i+1,DataController.Instance.dicPack));
          entity.type = (E_GoodsType)typeIndex;
           //在内存里面读取物品数量
          int count = PackData.Instance.ReadCountById((i+1).ToString());
          GameTool.GetTheChildComponent<Text>(goods, "Txt_Count").text= count.ToString();

        }
    }
    private void ReturnUI()
    {
      
        UIManager.Instance.ReturnBeforeUI(this.BeforeUiId);
        ResetPack();
    }
    public override void AddMessageListener()
    {
        EventDispatcher.AddListener<int,Image,Text>(E_MessageType.GoodsBeClick, UpdateInfor);
    }
    private void UpdateInfor(int beClickId,Image goodsImg,Text countTxt)
    {
        beChooseId = beClickId;
        // GameDebuger.Log("物品被单击,Id为"+beClickId);
        //被选中的物品名称、介绍、售价
        string name = DataController.Instance.ReadCfg("Name",beClickId,DataController.Instance.dicPack);
        string introduce= DataController.Instance.ReadCfg("Introduce", beClickId, DataController.Instance.dicPack);
        string price = DataController.Instance.ReadCfg("Price", beClickId, DataController.Instance.dicPack);


        img_BeChooseGoods.sprite = goodsImg.sprite;
        txt_BeChooseName.text = name;
        txt_HasCount.text = countTxt.text;
        txt_Introduce.text = introduce;
        txt_Price.text = price;
        beChooseCount = int.Parse(countTxt.text);
        if (sellPanel.activeInHierarchy)
        {
            sellPanel.gameObject.SetActive(false);
        }

    }
    public override void RemoveMessageListener()
    {
        EventDispatcher.RemoveListener<int, Image, Text>(E_MessageType.GoodsBeClick, UpdateInfor);
    }
}
