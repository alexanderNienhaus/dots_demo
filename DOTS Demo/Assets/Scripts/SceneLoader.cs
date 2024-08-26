using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneLoader : MonoBehaviour
{
    private void Update()
    {
        if (Input.GetKey(KeyCode.Alpha1))
        {
            SceneManager.LoadScene("WithoutDOTS");
        }

        if (Input.GetKey(KeyCode.Alpha2))
        {
            SceneManager.LoadScene("WithDOTS");
        }
    }
}
