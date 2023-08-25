using UnityEngine;
public class Hammer : MonoBehaviour
{
    // Start is called before the first frame update

    public float z = 180f;

    private float speed = 2f;
    private float speed_factor = .01f;
    private bool is_move = false;
    private int dir = -1;
    private const int MAX = 5;
    private int time = 0;
    private bool collision = false;
    private int stop_time = 0;

    void Start()
    {
        is_move = true;
    }

    // Update is called once per frame
    void Update()
    {
        if (stop_time-- > 0) return;

        if (is_move == false) return;

        if (time >= MAX)
        {
            if (Mathf.Abs(this.z) <= 2)
            {
                is_move = false;
                this.z = 0;
                transform.localEulerAngles = new Vector3(0, 0, this.z);
                return;
            }
        }

        float z = this.z + speed * dir;

        if (z <= 0 && this.z >= 0)
        {
            speed_factor = -.05f - time * .01f;
        }

        if (z >= 0 && this.z <= 0)
        {
            speed_factor = -.05f - time * .01f;
        }

        speed = speed * (1 + speed_factor);

        this.z = z;

        transform.localEulerAngles = new Vector3(0, 0, this.z);

        if (speed <= 0.1)
        {
            dir = -dir;
            speed_factor = .05f;
            speed = .2f;
            time++;
        }

        if (collision)
        {
            if (this.z <= 20)
            {
                stop_time = 30;
                speed = speed*.5f;
                collision = false;
            }
        }
    }
}
