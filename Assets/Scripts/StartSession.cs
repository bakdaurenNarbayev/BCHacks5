using UnityEngine;
using UnityEngine.SceneManagement;

public class StartSession : MonoBehaviour
{
    public DatabaseManager dbManager;
    public void LoadGame()
    {
/*        DatabaseManager dbManager = GameObject.Find("DatabaseManager");
        if (dbManager.isSessionStarted)
        {
            

        }*/
        SceneManager.LoadScene(1);
    }
}
