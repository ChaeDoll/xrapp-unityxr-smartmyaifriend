using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    // �̵��ϰ��� �ϴ� ���� �̸��� �ν����Ϳ��� ������ �� �ֵ��� public ������ �����մϴ�.
    public string sceneName;

    // �� �޼���� ��ư�� OnClick �̺�Ʈ�� ����˴ϴ�.
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
