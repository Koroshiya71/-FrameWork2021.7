using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using GameCore;

public class LevelEntity : MonoBehaviour
{   
    //关卡ID
    public int levelId = 0;
    void Start()
    {
        this.GetComponent<Button>().onClick.AddListener(LoadLevelScene);
    }
    private void LoadLevelScene()
    {
        string sceneFileName = DataController.Instance.ReadCfg("SceneFileName", levelId, DataController.Instance.dicLevel);
        //SceneManager.LoadScene(sceneFileName);
        //UIManager.Instance.ShowUI(E_UiId.PlayUI);

        //SceneManager.LoadScene("LoadingScene");
        //UIManager.Instance.ShowUI(E_UiId.LoadingUI);

        //普通调用
        //SceneController.Instance.LoadSceneAsync(sceneFileName, ShowPlayUI);
        //简写
        SceneController.Instance.LoadSceneAsync(sceneFileName, delegate
        {
            UIManager.Instance.ShowUI(E_UiId.PlayUI);

        });

    }
    private void ShowPlayUI()
    {
        UIManager.Instance.ShowUI(E_UiId.PlayUI);
    }
}
