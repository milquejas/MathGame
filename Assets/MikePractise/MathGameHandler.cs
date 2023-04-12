using UnityEngine;

public class MathGameHandler : MonoBehaviour
{
    public GameObject[] objects;

    private int selectedObjectIndex = -1;
    private int currentValue = 0;

    void Start()
    {
        objects = new GameObject[11];
        for (int i = 0; i <= 10; i++)
        {
            string objectName = "Object" + i.ToString();
            objects[i] = GameObject.Find(objectName);
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            RaycastHit hit;
            if (Physics.Raycast(mousePos, Vector3.forward, out hit))
            {
                if (hit.collider != null)
                {
                    string objectName = hit.collider.gameObject.name;
                    Debug.Log("Hit object: " + objectName);
                    int number;
                    if (int.TryParse(objectName.Substring(6), out number))
                    {
                        if (selectedObjectIndex == -1)
                        {
                            selectedObjectIndex = number;
                            Debug.Log(selectedObjectIndex);
                        }
                        else
                        {
                            int result = 0;
                            switch (currentValue)
                            {
                                case 1:
                                    result = selectedObjectIndex + number;
                                    break;
                                case 2:
                                    result = selectedObjectIndex - number;
                                    break;
                                case 3:
                                    result = selectedObjectIndex * number;
                                    break;
                                case 4:
                                    result = selectedObjectIndex / number;
                                    break;
                                default:
                                    break;
                            }
                            Debug.Log(result);
                            selectedObjectIndex = -1;
                        }
                    }
                }
                else
                {
                    Debug.Log("No object hit");
                }
            }
            else
            {
                Debug.Log("Raycast missed");
            }
        }
    }

    public void OnOperatorButtonClick(int op)
    {
        currentValue = op;
    }
}

/* T�m� skripti on hiirell� valitsemista varten. Tarkoitus olisi ett�, pelaajan tulisi valita hiirell� eri arvoja sis�lt�vi�
 * objekteja sek�, suorittaa matemaattisia teht�vi�. Olisi hyv� ett� pelaaja voisi siirrell� objekteja hiirell�.
 */
