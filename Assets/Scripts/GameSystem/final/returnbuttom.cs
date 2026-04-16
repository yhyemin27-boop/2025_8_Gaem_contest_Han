using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class returnbuttom : MonoBehaviour
{
    // Start is called before the first frame update
    public string nextSceneName = "firstrull";
    public void reset()
    {
        SceneManager.LoadScene(nextSceneName);
    }
}
