using UnityEngine;

public class Movement : MonoBehaviour {
    public GameObject followTarget;
    public float speed;

    public float playerSpeed = 6;
    public float sprintMultiplier = 1.2f;

    public Animator player_Animator;

    // Update is called once per frame
    void FixedUpdate() {
        Vector3 newLocation = 
            Vector3.Lerp(this.transform.position,
                         followTarget.transform.position,
                         speed / (Vector3.Distance(this.transform.position, followTarget.transform.position)) / (Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1)
            );
        this.transform.position = new Vector3(newLocation.x, newLocation.y, this.transform.position.z);
        if(Input.GetAxis("Horizontal") < 0) {
            followTarget.GetComponentInChildren<Transform>().localScale = new Vector3(-1, followTarget.GetComponentInChildren<Transform>().localScale.y, followTarget.GetComponentInChildren<Transform>().localScale.z);
        } else if(Input.GetAxis("Horizontal") > 0) {
            followTarget.GetComponentInChildren<Transform>().localScale = new Vector3(1, followTarget.GetComponentInChildren<Transform>().localScale.y, followTarget.GetComponentInChildren<Transform>().localScale.z);
        }
        if(Input.GetAxis("Horizontal") != 0 || Input.GetAxis("Vertical") != 0) {
            player_Animator.SetBool("isWalking", true);
        } else {
            player_Animator.SetBool("isWalking", false);
        }
        Vector3 moveVec = new Vector3(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"), 0);
        followTarget.transform.position += moveVec * playerSpeed * (Input.GetKey(KeyCode.LeftShift) ? sprintMultiplier : 1);
        player_Animator.SetFloat("WalkSpeed", 1f + (Input.GetKey(KeyCode.LeftShift) ? 0.5f : 0));

    }
}
