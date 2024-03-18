using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Title : MonoBehaviour
{

    public void GameSceneLoad()
    {
        Manager.Scene.LoadScene("GameScene");
    }

    public void Exit()
    {
        StartCoroutine(ExitCount());
    }
    private IEnumerator ExitCount()
    {
        yield return new WaitForSeconds(1.5f);
        Application.Quit();
        Debug.Log("게임종료 확인");
    }
}