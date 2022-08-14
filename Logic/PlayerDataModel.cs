public class PlayerDataModel
{
    public Abilities Abilities { get; set; }
    public ChampionStats ChampionStats { get; set; }
}
public class Abilities
{
    public Ability E { get; set; }
    public Ability Q { get; set; }
    public Ability R { get; set; }
    public Ability W { get; set; }
    public Ability Passive { get; set; }

}
public class Ability
{
    public int AbilityLevel { get; set; }
}
public class ChampionStats
{
    public double AbilityHaste { get; set; }
}