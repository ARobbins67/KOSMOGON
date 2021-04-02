using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SocialPlatforms.Impl;
using UnityEngine.Events;

public enum CubeState { Left, Right, Up, None }

public class MainCubeController : MonoBehaviour
{
    [SerializeField] UnityEvent CubeUp;
    [SerializeField] UnityEvent CubeLeft;
    [SerializeField] UnityEvent CubeRight;
    [SerializeField] UnityEvent CubeNone;

    private Transform trans;
    private CubeState state = CubeState.None;

    private void Awake()
    {
        CubeNone.Invoke();
    }

    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {
        Touch touch = Input.GetTouch(0);
        if (touch.phase == TouchPhase.Began)
        {
            Vector2 pos = touch.position;
            Ray ray = Camera.main.ScreenPointToRay(pos);

            if(Physics.Raycast(ray.origin, ray.direction, out RaycastHit hit))
            {
                if(hit.collider.gameObject.name == "MainPolygon")
                {
                    Debug.Log("Hit!");
                }
            }
        }

        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            CubeUp.Invoke();
            state = CubeState.Up;
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            CubeLeft.Invoke();
            state = CubeState.Left;
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            CubeRight.Invoke();
            state = CubeState.Right;
        }
        if(Input.GetKeyUp(KeyCode.RightArrow) || Input.GetKeyUp(KeyCode.UpArrow) || Input.GetKeyUp(KeyCode.LeftArrow))
        {
            CubeNone.Invoke();
            state = CubeState.None;
        }
    }

    public CubeState GetAlignment()
    {
        return state;
    }

    public void Fray(int frayNum)
    {
        if (frayNum == 1)
        {
            // eastAnim.enabled = true;
        }
        /*else if (frayNum == 2)
        {
            eastAnim.SetFloat("EastSegment",2);
        }
        else if (frayNum == 3)
        {
            eastAnim.SetFloat("EastSegment",3);
        }
        else if (frayNum == 4)
        {
            eastAnim.SetFloat("EastSegment", 4);
        }*/
    }
}
