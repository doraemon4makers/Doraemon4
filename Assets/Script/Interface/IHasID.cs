using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IHasID 
{
    string englishName { get; }
    string chineseName { get; }
    void SetEnglishName(string name);
    void SetChineseName(string name);
}
