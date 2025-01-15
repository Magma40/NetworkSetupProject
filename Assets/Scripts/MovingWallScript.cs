using System.Collections;
using UnityEngine;
using UnityEngine.InputSystem;

public class MovingWallScript : MonoBehaviour
{
    [SerializeField] Vector3 startPos, endPos;
    public float speed;
    private bool movingTowards = true;

    private void Start()
    {
       StartCoroutine(MovingWall());
    }


    public float lerpedValue;
    public float duration = 3;
    private IEnumerator MovingWall()
    {
        float timeElapsed = 0;
        if (movingTowards)
        {
            while (timeElapsed < duration)
            {
                float t = timeElapsed / duration;
                transform.position = Vector3.Lerp(transform.position, endPos, t);
                timeElapsed += Time.deltaTime;
                movingTowards = false;
                yield return null;
            }
        }
        else
        {
            while (timeElapsed < duration)
            {
                float t = timeElapsed / duration;
                transform.position = Vector3.Lerp(transform.position, startPos, t);
                timeElapsed += Time.deltaTime;
                movingTowards = true;
                yield return null;
            }
        }
        StartCoroutine(MovingWall());

        //if (movingTowards)
        //{
        //  transform.position = Vector3.Lerp(transform.position, endPos, speed * Time.deltaTime);
        //  yield return null;
        //  movingTowards = false;
        //}
        //else
        //{
        //  transform.position = Vector3.Lerp(transform.position, startPos, speed * Time.deltaTime);
        //  yield return null;
        //  movingTowards = true;
        //}
        //StartCoroutine(MovingWall());
    }
}
