using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;
using System;

public class ChangePage : MonoBehaviour,IBeginDragHandler,IEndDragHandler
{
    //获取滚动列表
    private ScrollRect levelRect;
    //翻页速度
    private float speed = 5;
    //所有的复选框
    private Toggle[] arrToggle;
    //所有的复选框的父物体
    private Transform allToggle;
    //是否拖拽结束(true正在拖拽,false拖拽结束)
    private bool isDrap = true;
    //当前页数
    private int currentPage = 0;
    //滚动的分页值
    private float[] pagePosArr = new float[] {0,0.5f,1f };
    //拖拽前滚动列表的滚动值
    private float scrollerValue = 0;

    void Awake()
    {
        levelRect = this.GetComponent<ScrollRect>();
        allToggle = GameTool.FindTheChild(this.transform.parent.gameObject, "AllToggle");
        arrToggle = new Toggle[allToggle.childCount];
        for (int i = 0; i < allToggle.childCount; i++)
        {
            arrToggle[i] = allToggle.GetChild(i).GetComponent<Toggle>();
            arrToggle[i].onValueChanged.AddListener(OnChangePage);
        }
    }
    void Update()
    {
        if (isDrap==false)//拖拽结束
        {
            levelRect.horizontalNormalizedPosition = Mathf.Lerp(levelRect.horizontalNormalizedPosition,pagePosArr[currentPage],speed*Time.deltaTime);
            arrToggle[currentPage].isOn = true;
        
        }
    }
    private void OnChangePage(bool isOn)
    {
       
        for (int i = 0; i < arrToggle.Length; i++)
        {
            if (arrToggle[i].isOn)
            {
                currentPage = i;
                break;
            }
        }
        isDrap = false;
    }
    //开始拖拽
    public void OnBeginDrag(PointerEventData eventData)
    {
        //GameDebuger.Log("开始拖拽");
        isDrap = true;
        //记录拖拽前的滚动值
        scrollerValue = levelRect.horizontalNormalizedPosition;
    }
    //结束拖拽
    public void OnEndDrag(PointerEventData eventData)
    {
        //GameDebuger.Log("结束拖拽");
        isDrap = false;
        //计算拖拽前与拖拽后的偏移
        float offset = levelRect.horizontalNormalizedPosition - scrollerValue;
        if (offset > 0)//关卡往左移动
        {
            if (currentPage < pagePosArr.Length - 1)
            {
                currentPage++;
            }
        }
        else if (offset<0)//关卡往右移动
        {
            if (currentPage>0)
            {
                currentPage--;
            }
          
        }
    }
    void OnDisable()
    {
        currentPage = 0;
        levelRect.horizontalNormalizedPosition = 0;
        for (int i = 0; i < arrToggle.Length; i++)
        {
            if (i == 0)
            {
                arrToggle[i].isOn = true;
            }
            else
            {
                arrToggle[i].isOn = false;
            }
        }
       
    }
}
