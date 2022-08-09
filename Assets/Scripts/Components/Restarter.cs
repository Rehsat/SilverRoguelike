using UnityEngine.SceneManagement;
using UnityEngine;

public class Restarter : MonoBehaviour
{
    public void Restart()
    {
        SceneManager.LoadScene(0);
    }
}
