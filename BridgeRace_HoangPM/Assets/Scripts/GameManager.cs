using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{

    public List<Character> charactersOnField = new List<Character>();

    // Start is called before the first frame update
    void Start()
    {
        OnInit();
    }

    public virtual void OnInit()
    {

    }

    //public int GetCharacterIdOnField()
    //{
    //    // Trả về ID của nhân vật đầu tiên trên sân, hoặc -1 nếu không có nhân vật nào
    //    if (charactersOnField.Count > 0)
    //    {
    //        return charactersOnField[0].id;
    //    }
    //    else
    //    {
    //        return -1;
    //    }
    //}
}
