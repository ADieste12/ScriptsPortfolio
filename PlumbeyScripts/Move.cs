using UnityEngine;
using UnityEngine.SceneManagement;

public class NewMonoBehaviourScript : MonoBehaviour
{
    [SerializeField] Transform leftLeg;
    [SerializeField] Transform rightLeg;
    [SerializeField] Transform leftOrbitation;
    [SerializeField] Transform rightOrbitation;
    [SerializeField] float speed;
    [SerializeField] RightCollide rPosition;
    [SerializeField] LeftCollide lPosition;
    [SerializeField] LeftCollideWall lWallPosition;
    [SerializeField] RightCollideWall rWallPosition;
    [SerializeField] LeftCollideWallLeft lWallLeftPosition;
    [SerializeField] RightCollideWallLeft rWallLeftPosition;
    [SerializeField] RightCollideCeiling rCeilingPosition;
    [SerializeField] LeftCollideCeiling lCeilingPosition;

    void Update()
    {
        if ((rPosition.isRightHereR == true && lPosition.isLeftHereL == true) || (lWallPosition.isLeftHere == true && rWallPosition.isRightHere == true) || (lWallLeftPosition.isLeftHere == true && rWallLeftPosition.isRightHere == true) || (lCeilingPosition.isLeftHere == true && rCeilingPosition.isRightHere == true))
        {
            if (Input.GetKey(KeyCode.D))
            {
                leftLeg.RotateAround(leftOrbitation.position, -Vector3.forward, speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rightLeg.RotateAround(rightOrbitation.position, Vector3.forward, speed * Time.deltaTime);
            }
        }
        if (((rPosition.isRightHereR == true || lPosition.isRightHereL == true) && lPosition.isLeftHereL == false) || ((rWallPosition.isRightHere == true || lWallPosition.isRightHere == true) && lWallPosition.isLeftHere == false) || ((rWallLeftPosition.isRightHere == true || lWallLeftPosition.isRightHere == true) && lWallLeftPosition.isLeftHere == false) || ((rCeilingPosition.isRightHere == true || lCeilingPosition.isRightHere == true) && lCeilingPosition.isLeftHere == false))
        {
            if (Input.GetKey(KeyCode.D))
            {
                leftLeg.RotateAround(leftOrbitation.position, -Vector3.forward, speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                leftLeg.RotateAround(leftOrbitation.position, Vector3.forward, speed * Time.deltaTime);
            }
        }
        if (((rPosition.isLeftHereR == true || lPosition.isLeftHereL == true) && lPosition.isRightHereL == false) || ((rWallPosition.isLeftHere == true || lWallPosition.isLeftHere == true) && lWallPosition.isRightHere == false) || ((rWallLeftPosition.isLeftHere == true || lWallLeftPosition.isLeftHere == true) && lWallLeftPosition.isRightHere == false) || ((rCeilingPosition.isLeftHere == true || lCeilingPosition.isLeftHere == true) && lCeilingPosition.isRightHere == false))
        {
            if (Input.GetKey(KeyCode.D))
            {
                rightLeg.RotateAround(rightOrbitation.position, -Vector3.forward, speed * Time.deltaTime);
            }
            if (Input.GetKey(KeyCode.A))
            {
                rightLeg.RotateAround(rightOrbitation.position, Vector3.forward, speed * Time.deltaTime);
            }
        }
        if (Input.GetKeyDown(KeyCode.R))
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
        }
    }
}
