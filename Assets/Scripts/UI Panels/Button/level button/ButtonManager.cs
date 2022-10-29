using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class ButtonManager : MonoBehaviour
{

    public bool isLevelLoaded = false;

    public levelButton levelButtonPrefab;

    public int allScene = 1;

    public Transform content;


    public AsyncOperation loadingOperation;

   public string currentLoadedScene;

    private LoadingPanel loadingPanel;
    private void Awake()
    {
        for (int i = 1; i <= allScene; i++)
        {

            levelButton levelButton = Instantiate(levelButtonPrefab, content);

                      levelButton.GetButtonTextMesh().text = "level " + (i); 
            levelButton.AddListener(() =>
            {

            //    loadingOperation = SceneManager.LoadSceneAsync("level " + (i - 1), LoadSceneMode.Single);

                currentLoadedScene = "level " + (i - 1);


                BasePanel basePanel = MasterManager.Instance.uIPanelManager.GetPanel(PanelType.Loading);

                loadingPanel = (LoadingPanel)basePanel;

                MasterManager.Instance.uIPanelManager.AddPanel(basePanel);
                StartCoroutine(loadSceneASync(currentLoadedScene));
               // loadingOperation.allowSceneActivation = false;
            });


        }
    }

    
    private void Update()
    {
        if (!isLevelLoaded)
        {
     

        }




       

    

        isLevelLoaded = true;
    }


    IEnumerator loadSceneASync(string sceneName)
    {
        if (loadingOperation == null)
        {
            loadingOperation = SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
            loadingOperation.completed += LoadingOperation_completed;
        }

        loadingOperation.allowSceneActivation = false;



        while (!loadingOperation.isDone)
        {
            yield return null;
            loadingPanel.statusBar.SetFill(loadingOperation.progress + 0.1f);

            if(loadingPanel.statusBar.fill >= 0.9f)
            {
                loadingPanel.statusBar.fill = 0;
                loadingOperation.allowSceneActivation = true;

            
            }
    

            Debug.Log(loadingOperation.progress);

        }
   
        Debug.Log("scene is ready");
    }

    private void LoadingOperation_completed(AsyncOperation obj)
    {
        Debug.Log("scene is ready");
        Debug.Log(obj.progress);
    }
}
