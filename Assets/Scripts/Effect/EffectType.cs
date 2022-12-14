
namespace Effect
{
    public enum EffectDirectionType
	{
        Identity,
        ReverseDirection,
        SetParticle3DRotation,
        OriginDirection,
        SetParticleOffsetIs3DRotation,
	}
    public enum EffectType
    {
        None = -1,
        Hit_1 = 0,
        Hit_2,
        Hit_3,
        Hit_4,
        Dirty_01,
        Dirty_02,
        FinalHit,
        StarGetEff,
        DodgeEff,
        Hit_5,
        Shockwave,
        Hit_6,
        Count,
    }
}