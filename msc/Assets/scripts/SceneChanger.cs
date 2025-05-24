using UnityEngine;
using System.Collections;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeScene(string sceneName)
    {
        if (SaveData.current != null)
        {
            SaveData.current.save();
        }
        SceneManager.LoadScene(sceneName);
    }
    public void LeaveGame()
    {
        if (SaveData.current != null)
        {
            SaveData.current.save();
        }
        Application.Quit();
        Debug.Log("quit");
    }
}
