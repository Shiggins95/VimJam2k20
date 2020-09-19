using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ClampToParent : MonoBehaviour
{
    public Transform Parent;
    public Vector3 StartPoint;
    public bool ShouldDeleteSelf;
    public Vector2 Offset;

    // Start is called before the first frame update
    void Start()
    {
        if (ShouldDeleteSelf)
        {
            StartCoroutine(DeleteSelf());
        }

        if (Parent)
        {
            StartPoint = new Vector2(Parent.position.x - Offset.x, Parent.position.y - Offset.y);
        }
    }

    private IEnumerator DeleteSelf()
    {
        yield return new WaitForSeconds(1);
        Destroy(gameObject);
    }

    // Update is called once per frame
    void Update()
    {
        if (Parent)
        {
            StartPoint = new Vector2(Parent.position.x - Offset.x, Parent.position.y - Offset.y);
        }

        transform.position = Camera.main.WorldToScreenPoint(StartPoint);
    }
}