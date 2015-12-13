using UnityEngine;
using System.Collections;

public class Particles_DestroyOnComplete : MonoBehaviour
{
    private ParticleSystem _ps = null;

    // Use this for initialization
    void Start()
    {
        this._ps = this.GetComponent<ParticleSystem>();
    }

    // Update is called once per frame
    void FixedUpdate()
    {
        if (this._ps && !this._ps.IsAlive())
        {
            Destroy(this);
        }
    }
}
