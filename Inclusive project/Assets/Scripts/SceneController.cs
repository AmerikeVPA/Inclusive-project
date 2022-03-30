using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


namespace AAAstdio.InclusiveProject
{
    public class SceneController : MonoBehaviour
    {
        public void SceneChanger() { SceneManager.LoadSceneAsync(SceneManager.GetActiveScene().buildIndex + 1); }
        public void ExitGame() { Application.Quit(); }
    }
}
