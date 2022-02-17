using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 
using UnityEngine.Video;

public class ReturnToMenu : MonoBehaviour
{
    public VideoPlayer player;

    public void stopAndReturn() {
        player.Stop();
        UnityEngine.SceneManagement.SceneManager.LoadScene("WaterMarkVRTemp");
        UnityEngine.SceneManagement.SceneManager.SetActiveScene(UnityEngine.SceneManagement.SceneManager.GetSceneByName("WaterMarkVRTemp"));
    }
}
