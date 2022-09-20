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

    public static Vector2 DegreeToVector2(float degree, bool rounded = false)
    {
        Vector2 result = RadianToVector2(degree * Mathf.Deg2Rad);
        result = (rounded) ? new Vector2(Mathf.RoundToInt(result.x), Mathf.RoundToInt(result.y)) : result;
        return result;
    }

    public float DegreeToMouse(Vector3 mousePosition)
    {
        Vector3 dir = mousePosition - transform.position;
        float angle = Mathf.Atan2(dir.y, dir.x) * Mathf.Rad2Deg;
        return angle;
    }


    public Vector2 Vector2ToMouse(Vector3 mousePosition, bool rounded = false)
    {
        return DegreeToVector2(DegreeToMouse(mousePosition), rounded);
    }

    private Vector3 GetMousePosition()
    {
        return Camera.main.ScreenToWorldPoint(Input.mousePosition);
    }

    // Funcion Test para hacer Debug, se borrará en el final
    private void TestGetAngles()
    {
        Vector3 mousePosition = GetMousePosition();
        float angle = DegreeToMouse(mousePosition);

        Debug.Log("Direction Angle: " + angle + " Direction Vector: " + DegreeToVector2(angle));
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
            TestGetAngles();

            if(mouseSprite != null)
            {
                SetMouseSpritePosition();
            }
        }
    }
}
