using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class AllNavBehaviour : MonoBehaviour
{

  public void LoadMyScene(string sceneName)
  {
    SceneManager.LoadScene(sceneName);
  }
  public void LoadMyScene(int sceneNumber)
  {
    SceneManager.LoadScene(sceneNumber);
  }
}
