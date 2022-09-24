using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Game_Manager : Singleton<Game_Manager>
{
    // Start is called before the first frame update
    public void QuitGame()
    {
        Application.Quit();
    }
}
