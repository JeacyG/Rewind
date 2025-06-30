using System.Collections.Generic;

public interface IDamageZone
{
    public void Damage(int damage, out List<HitData> hitData);
}
