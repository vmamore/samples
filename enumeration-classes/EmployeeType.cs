namespace enumeration_classes
{
    public class EmployeeType : Enumeration
    {
        public static readonly EmployeeType Manager =
            new EmployeeType(0, "Manager");
        public static readonly EmployeeType Servant =
            new EmployeeType(1, "Servant");
        public static readonly EmployeeType AssistantToTheRegionalManager =
            new EmployeeType(2, "Assistant to the Regional Manager");

        private EmployeeType() { }
        private EmployeeType(int value, string displayName) : base(value, displayName) { }
    }


    public abstract class EmployeeTypeAbstract : Enumeration {
        public static readonly EmployeeTypeAbstract Manager =
            new ManagerType();

            public static readonly EmployeeTypeAbstract Developer =
            new DeveloperType();

            protected EmployeeTypeAbstract(){}
            protected EmployeeTypeAbstract(int value, string displayName) : base(value, displayName){}

            public abstract decimal BonusSize {get;}


        private class ManagerType : EmployeeTypeAbstract
        {
            public ManagerType() : base(0, "Manager"){}
            public override decimal BonusSize => 1000m;
        }

        private class DeveloperType : EmployeeTypeAbstract
        {
            public DeveloperType() : base(1, "Developer"){}
            public override decimal BonusSize => 1500m;
        }
    }
}