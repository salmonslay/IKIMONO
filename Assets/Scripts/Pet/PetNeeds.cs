namespace IKIMONO.Pet
{
    public class PetNeedHunger : PetNeed
    {
        public override string Name => "Hunger";
        
        public override float DecayRate => 2; // 50h to reach 0
    }
    
    public class PetNeedSocial : PetNeed
    {
        public override string Name => "Social";

        public override float DecayRate => 1f; // 100h to reach 0 
    }

    public class PetNeedEnergy : PetNeed
    {
        public override string Name => "Energy";

        public override float DecayRate => 5; // 20h to reach 0
    }
    
    public class PetNeedFun : PetNeed
    {
        public override string Name => "Fun";

        public override float DecayRate => 1.2f; // 83h to reach 0
    }
    
    public class PetNeedHygiene : PetNeed
    {
        public override string Name => "Hygiene";

        public override float DecayRate => 0.8f; // 125h to reach 0
    }
}