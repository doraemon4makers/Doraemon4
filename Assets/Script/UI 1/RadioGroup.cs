using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RadioGroup
{
    private static Dictionary<string, List<Radio>> groupNameMembersPairs = new Dictionary<string, List<Radio>>();
    private static Dictionary<string, Radio> groupNameCheckPairs = new Dictionary<string, Radio>();

    public static void Join(string groupName, Radio member)
    {
        if (groupNameMembersPairs.ContainsKey(groupName))
        {
            if (!groupNameMembersPairs[groupName].Contains(member))
            {
                groupNameMembersPairs[groupName].Add(member);
            }
        }
        else
        {
            groupNameMembersPairs.Add(groupName, new List<Radio>(new Radio[] { member }));
        }
    }

    public static List<Radio> GetOtherMembers(string groupName, Radio yourSelf)
    {
        List<Radio> resultList = GetMembers(groupName);
        if (resultList != null && resultList.Count > 0)
        {
            resultList.Remove(yourSelf);
        }
        return resultList;
    }

    public static List<Radio> GetMembers(string groupName)
    {
        List<Radio> resultList = null;
        if (groupNameMembersPairs.ContainsKey(groupName))
        {
            List<Radio> tempList = groupNameMembersPairs[groupName];
            Radio[] tempArray = new Radio[tempList.Count];
            tempList.CopyTo(tempArray);
            resultList = new List<Radio>(tempArray);
        }
        return resultList;
    }

    public static void SetCheck(string groupName, Radio yourSelf)
    {
        if (yourSelf == null) return;

        List<Radio> otherMembers = GetOtherMembers(groupName, yourSelf);
        if(otherMembers != null && otherMembers.Count > 0)
        {
            foreach(Radio tempMember in otherMembers)
            {
                tempMember.isCheck = false;
            }
        }
        if (groupNameCheckPairs.ContainsKey(groupName))
        {
            groupNameCheckPairs[groupName] = yourSelf;
        }
        else
        {
            groupNameCheckPairs.Add(groupName, yourSelf);
        }
    }

    public static Radio GetCheck(string groupName)
    {
        Radio resultRadio = null;
        if (groupNameCheckPairs.ContainsKey(groupName))
        {
            resultRadio = groupNameCheckPairs[groupName];
        }
        return resultRadio;
    }
}
