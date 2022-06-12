using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MiscScript : MonoBehaviour
{
    public bool car, door, isOpen;
    public Transform start, end, affectedObj;
    private void Move() { StartCoroutine(MoveCR()); }
    public void RotateObj() 
    {
        if (isOpen) { affectedObj.localRotation = Quaternion.Euler(0f, 0f, 0f); }
        if(!isOpen) { affectedObj.rotation = Quaternion.Euler(0f, 90f, 0f); }
    }
    IEnumerator MoveCR()
    {
        yield return new WaitForEndOfFrame();
        if (transform.position != end.position)
        {
            transform.position = Vector3.MoveTowards(transform.position, end.position, 4.37f * Time.deltaTime);
            StartCoroutine(MoveCR());
        } else {
            StartCoroutine(ResetPos());
        }
    }
    IEnumerator ResetPos()
    {
        yield return new WaitForSeconds(3.25f);
        transform.position = start.position;
        StartCoroutine(MoveCR());
    }
    private void Awake()
    {
        if(car) { StartCoroutine(MoveCR()); }
    }
}
