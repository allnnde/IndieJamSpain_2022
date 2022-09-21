using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMouseTracking : MonoBehaviour
{
    // Start is called before the first frame update

    public GameObject mouseSprite = null;
    private bool canDetect = true;
    void Start()
    {
        Cursor.visible = false;
    }

    public static Vector2 RadianToVector2(float radian)
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

    // Funcion Test para hacer Debug, se borrará en el final
    private void TestGetAngles()
    {
        Debug.Log("Direction Angle: " + DegreeTowardsMouse() + " Direction Vector: " + Vector2TowardsMouse());
    }

    private void SetMouseSpritePosition()
    {
        Vector3 mousePosition = GetMousePosition();
        Vector3 newSpritePosition = new Vector3(mousePosition.x, mousePosition.y, 0);
        mouseSprite.transform.position = newSpritePosition;
    }

    // Update is called once per frame
    void Update()
    {
        if (canDetect)
        {
            //TestGetAngles();

            if(mouseSprite != null)
            {
                SetMouseSpritePosition();
            }
        }
    }
}
