namespace IKIMONO.Pet
{
    public class PetNeedHunger : PetNeed
    {
        public override string Name => "Hunger";
    }
    
    public class PetNeedSocial : PetNeed
    {
        public override string Name => "Social";

        public override int DecayRate => 2;
    }

    public class PetNeedEnergy : PetNeed
    {
        public override string Name => "Energy";

        public override int DecayRate => 5;
    }
    
    public class PetNeedFun : PetNeed
    {
        public override string Name => "Fun";

        public override int DecayRate => 3;
    }
    
    public class PetNeedHygiene : PetNeed
    {
        public override string Name => "Hygiene";

        public override int DecayRate => 2;
    }
}