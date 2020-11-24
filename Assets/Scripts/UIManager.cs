using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public delegate void StartLevel1();
    public static event StartLevel1 OnClickedPlay;
    private void Start()
    {
        
    }
    private void Update()
    {
        
    }
    public void StartGame()
    {
        if (OnClickedPlay != null)
        {
            OnClickedPlay();
        }
        
    }
}
