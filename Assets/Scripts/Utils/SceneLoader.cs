using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;

namespace Coilbound.Utils
{
  [AddComponentMenu("Util/Scene Loader")]
  public class SceneLoader : MonoBehaviour
  {
    public void LoadScene(string sceneName)
    {
      LoadScene(sceneName, null);
    }

    public static void LoadScene(string sceneName, object message)
    {
      // 다음 씬에 데이터를 전달하는 코드
      if (message != null)
      {
        void MessageHandler(Scene scene, LoadSceneMode mode)
        {
          if (EventSystem.current is { } eventSystem) eventSystem.SendMessage("OnMessage", message);

          SceneManager.sceneLoaded -= MessageHandler;
        }

        SceneManager.sceneLoaded += MessageHandler;
      }

      SceneManager.LoadSceneAsync(sceneName, LoadSceneMode.Single);
    }
  }
}