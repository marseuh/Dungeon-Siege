
using System.Collections.Generic;
using UnityEngine.AI;
public class BlackBoard 
{
    private Dictionary<string,object> _variables = new Dictionary<string,object>();

    public TypeGeneric GetVariable<TypeGeneric>(string key)
    {
        if(_variables.ContainsKey(key))
        {
            return (TypeGeneric)_variables[key];
        }
        else
        {
            return default(TypeGeneric);
        }
    }

    public void SetVariable<TypeGeneric>(string key,TypeGeneric value)
    {
        if(_variables.ContainsKey(key))
        {
            _variables[key] = value;
        }
        else
        {
            _variables.Add(key, value);
        }
    }
    
}
