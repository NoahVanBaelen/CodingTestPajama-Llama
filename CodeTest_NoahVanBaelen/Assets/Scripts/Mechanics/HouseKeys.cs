using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HouseKeys : MonoBehaviour
{
    // Start is called before the first frame update
    [SerializeField] private Sprite _foundKeySprite;
    [SerializeField] private List<Image> _imageList;

    private List<bool> _gotAlKeys;
    private int _index = 0;
    void Start()
    {
        _gotAlKeys = new List<bool>(new bool[_imageList.Count]);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void FoundKey()
    {
        _imageList[_index].sprite = _foundKeySprite;
        _gotAlKeys[_index] = true;
        _index++;
    }

    public bool HasFoundAllKeys()
    {
        foreach (bool key in _gotAlKeys)
        {
            if (!key)
            {  
                return false; 
            }
        }
        return true;
    }
}
