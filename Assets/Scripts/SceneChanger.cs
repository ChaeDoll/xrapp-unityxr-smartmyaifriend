using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // 이동하고자 하는 씬의 이름을 인스펙터에서 지정할 수 있도록 public 변수로 선언합니다.
    public string sceneName;

    // 이 메서드는 버튼의 OnClick 이벤트에 연결됩니다.
    public void ChangeScene()
    {
        //SceneManager.LoadScene(sceneName);
        StartCoroutine(LoadSceneCoroutine());
    }
    private IEnumerator LoadSceneCoroutine()
    {
        yield return null;
        AsyncOperation asyncLoad = SceneManager.LoadSceneAsync(sceneName);
        while (!asyncLoad.isDone)
        {
            yield return null;
        }
    }
}
