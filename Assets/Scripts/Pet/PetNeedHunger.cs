namespace IKIMONO.Pet
{
    public class PetNeedHunger : PetNeed
    {
        public override string Name => "Hunger";
        public override float MaxValue { get; } = 100;
        public override float MinValue { get; } = 0;
        public override int DecayRate { get; } = 5;
    }
}