using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthPickup : Pickup{
    public override void Action() {
        Player.instance.health += 1;
        GameControl.instance.updateHealth();
    }
}
