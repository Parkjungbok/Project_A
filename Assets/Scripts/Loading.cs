using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;
using UnityEngine.UI;

public class Loading : MonoBehaviour
{
    [SerializeField] Image loading;


    private void Start()
    {
        Rotation();
    }

    private void Rotation()
    {
        loading.transform.DORotate(new Vector3(0, 0, 360), 3);
    }
}