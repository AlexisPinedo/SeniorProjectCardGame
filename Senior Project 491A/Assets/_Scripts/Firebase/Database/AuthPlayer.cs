using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AuthPlayer
{
    private string email;
    private DateTime createdAt;

    public AuthPlayer(string email, DateTime createdAt)
    {
        this.createdAt = createdAt;
        this.email = email;
    }
}
