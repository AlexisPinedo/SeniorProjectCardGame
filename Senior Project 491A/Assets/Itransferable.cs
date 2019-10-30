using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface Itransferable
{
    int id { get; set; }
    
    void SetId();

    //Would only contain method signature.
    void DoTransfer();

    void HandleTransfer();

    void OnTransfer();

}
