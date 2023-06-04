using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeSceneToSimulation()
    {
        SceneManager.LoadScene(1);
    }
}