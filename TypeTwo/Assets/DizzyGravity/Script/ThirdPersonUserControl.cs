using UnityEngine;

/// <summary>
/// The input that is forwarded to GravityCharacterController.
/// Only slightly modified from the standard assets' ThirdPersonUserControl:
/// Not using CrossPlatformInput & not creating a movement vector relative to camera view.
/// </summary>
[RequireComponent(typeof (GravityCharacterController))]
public class ThirdPersonUserControl : MonoBehaviour
{
    private GravityCharacterController m_Character; // A reference to the ThirdPersonCharacter on the object
    private Vector3 m_CamForward;             // The current forward direction of the camera
    private Vector3 m_Move;
    private bool m_Jump;                      // the world-relative desired move direction, calculated from the camForward and user input.
    public Animator animator;
        
    private void Start()
    {
        // get the third person character ( this should never be null due to require component )
        m_Character = GetComponent<GravityCharacterController>();
    }


    private void Update()
    {
        if (!m_Jump)
        {
            //m_Jump = Input.GetButtonDown("Jump");
        }

        if (Input.GetKeyDown("z"))
        {
            int length = Random.Range(2, 9);
            string name = "";
            string[] letterBank = { "a", "b", "c", "d", "e", "f", "g", "h", "i", "j", "k", "l", "m", "n", "o", "p", "qu", "r", "s", "t", "u", "v", "w", "x", "y", "z", "sh", "ch", "ll", "th", "gh", "ck" };
            string[] consonants = { "b", "c", "d", "f", "g", "h", "j", "k", "l", "m", "n", "p", "qu", "r", "s", "t", "v", "w", "x", "y", "z", "sh", "ch", "ll", "th", "gh", "ck", "ng" };
            string[] vowels = { "a", "e", "i", "o", "u", "aa", "ai", "au", "ao", "ee", "ie", "ei", "oo", "ou" };
            for (int i = 0; i < length; i++)
            {
                if (i == 0)
                {
                    name += letterBank[Random.Range(0, letterBank.Length)];
                }
                else if (isConsonant(name.Substring(i - 1, 1)))
                {
                    name += vowels[Random.Range(0, vowels.Length)];
                }
                else if (!isConsonant(name.Substring(i - 1, 1)))
                {
                    name += consonants[Random.Range(0, consonants.Length)];
                }
            }

            Debug.Log(name.Substring(0, 1).ToUpper() + name.Substring(1, length - 1));
        }
    }

    public bool isConsonant(string letter)
    {
        if (letter == "a" || letter == "e" || letter == "i" || letter == "o" || letter == "u")
        {
            return false;
        }

        return true;
    }


    // Fixed update is called in sync with physics
    private void FixedUpdate()
    {
        // read inputs
        float h = Input.GetAxis("Horizontal");
        float v = Input.GetAxis("Vertical");
        bool crouch = Input.GetKey(KeyCode.C);

        // movement vector is not relative to camera position
        m_Move = v * Vector3.forward + h * Vector3.right;

        // pass all parameters to the character control script
        m_Character.Move(m_Move, crouch, m_Jump);
        m_Jump = false;

        if (m_Move != Vector3.zero)
        {
            animator.SetBool("isRunning", true);
        }
        else
        {
            animator.SetBool("isRunning", false);
        }
    }
}