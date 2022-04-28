namespace IKIMONO.Pet
{
    public class PetNeedHunger : PetNeed
    {
        public override string Name => "Hunger";
        public override string NotificationTitle => "Your pet is hungry!";
        public override string NotificationDescription => "Your pet is hungry and needs to eat, come and feed it!";
        public override string NotificationIcon => "icon_hunger";
        public override float DecayRate => 2; // 50h to reach 0
    }
    
    public class PetNeedSocial : PetNeed
    {
        public override string Name => "Social";
        public override string NotificationTitle => "Your pet is lonely!";
        public override string NotificationDescription => "Your pet is lonely and needs to be socialized, come and play with it!";
        public override string NotificationIcon => "icon_social";
        public override float DecayRate => 0f; // Will not decay, not used for now
        public override bool HasNotifications => false;
    }

    public class PetNeedEnergy : PetNeed
    {
        public override string Name => "Energy";
        public override string NotificationTitle => "Your pet is tired!";
        public override string NotificationDescription => "Your pet is tired and needs to sleep, come and give it a nap!";
        public override string NotificationIcon => "icon_energy";
        public override float DecayRate => 5; // 20h to reach 0
    }
    
    public class PetNeedFun : PetNeed
    {
        public override string Name => "Fun";
        public override string NotificationTitle => "Your pet is bored!";
        public override string NotificationDescription => "Your pet is bored and needs to be played with, come and play with it!";
        public override string NotificationIcon => "icon_fun";
        public override float DecayRate => 1.2f; // 83h to reach 0
    }
    
    public class PetNeedHygiene : PetNeed
    {
        public override string Name => "Hygiene";
        public override string NotificationTitle => "Your pet is dirty!";
        public override string NotificationDescription => "Your pet is dirty and needs to be cleaned, come and clean it!";
        public override string NotificationIcon => "icon_hygiene";
        public override float DecayRate => 0.8f; // 125h to reach 0
    }

    public class PetNeedOverall : PetNeed
    {
        public override string Name { get; } = "Overall";
        public override string NotificationTitle { get; } 
        public override string NotificationDescription { get; }
        public override string NotificationIcon { get; }
        public override bool HasNotifications => false;
        public override float Percentage => Player.Instance.Pet.GetGeneralMood();
    }
}