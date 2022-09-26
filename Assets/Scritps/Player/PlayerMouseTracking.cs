using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseTracking : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject mouseSprite = null;
    public bool canDetect = true;
    public GameObject weaponPivot;
    void Start()
    {
        Cursor.visible = false;
    }

    private static Vector2 RadianToVector2(float radian)
    {
        return new Vector2(Mathf.Cos(radian), Mathf.Sin(radian));
    }


    public float DegreeTowardsMouse()
    {
        Vector3 dir = GetMousePosition() - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return angle;
    }

    public Quaternion QuaternionTowardsMouse()
    {
        Vector3 vector = new Vector3(0, 0, DegreeTowardsMouse() - 90);
        Quaternion result = Quaternion.identity;
        result.eulerAngles = vector;
        return result;
    }

    public Quaternion QuaternionTowardsMouseCooler()
    {
        Vector3 vector = new Vector3(0, 0, DegreeTowardsMouse());
        Quaternion result = Quaternion.identity;
        result.eulerAngles = vector;
        return result;
    }


    public Vector2 Vector2TowardsMouse(bool rounded = false)
    {
        Vector2 result = RadianToVector2(DegreeTowardsMouse() * Mathf.Deg2Rad);
        result = (rounded) ? new Vector2(Mathf.RoundToInt(result.x), Mathf.RoundToInt(result.y)) : result;
        return result;
    }

    public Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }
    
    private void SetMouseSpritePosition()
    {
        if (mouseSprite == null)
            return;

        Vector3 mousePosition = GetMousePosition();
        Vector3 newSpritePosition = new Vector3(mousePosition.x, mousePosition.y, 0);
        mouseSprite.transform.position = newSpritePosition;
        weaponPivot.transform.rotation = QuaternionTowardsMouseCooler();
    }

    // Update is called once per frame
    void Update()
    {
        if (canDetect)
        {
            SetMouseSpritePosition();
        }
    }
}
