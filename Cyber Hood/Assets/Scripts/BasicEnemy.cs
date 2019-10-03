using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BasicEnemy : MonoBehaviour {

    public Transform[] path;
    public float speed;

    private Rigidbody body;
    private int currentIndex;
    private int nextIndex;

    // Start is called before the first frame update
    void Start()
    {
        body = GetComponent<Rigidbody>();

        nextIndex = GetIndexToClosestPoint();
        currentIndex = (nextIndex - 1) % path.Length;
    }

    int GetIndexToClosestPoint()
    {
        float minDistance = Mathf.Infinity;
        int index = -1;
        for (int i = 0; i < path.Length; i++)
        {
            Vector3 position = path[i].position;

            if (minDistance > Vector3.Distance(transform.position, position))
            {
                minDistance = Vector3.Distance(transform.position, position);
                index = i;
            }
        }

        return index;
    }

    // Update is called once per frame
    void Update()
    {
        if (Vector3.Distance(transform.position, path[nextIndex].position) >= 0.001f)
        {
            transform.position = Vector3.MoveTowards(transform.position, path[nextIndex].position, speed * Time.fixedDeltaTime);
        }
        else
        {
            currentIndex = nextIndex;
            nextIndex = (currentIndex + 1) % path.Length;
        }
    }
}
