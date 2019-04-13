/*
    Created by: David Taitingfong
    Date:       2019-04-11
 */


/*
    This interface describes attributes for all enemies, e.g.,
    Minions and Bosses
 */
public interface IEnemy
{
    int health { get; set; }
    int rewardValue { get; set; }
}
