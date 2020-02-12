namespace ContainerPlanPrediction.Models
{
    public class Container
    {
        public ContainerSize Size { get; set; }
        public ContainerType ContainerType { get; set; }
        public ContainerContentType ContentType { get; set; }

        public override string ToString()
        {
            return $"{ContentType} {Size} {ContainerType} Container";
        }
    }

    public enum ContainerContentType
    { 
        Empty,
        General,
        Flammable,
        Poison,
        Radioactive,
        Corrosive
    }

    public enum ContainerType
    { 
        Dry,
        Refridgerated,
        Tank,
        OpenTop
    }

    public enum ContainerSize 
    {
        TwentyFoot,
        FortyFoot
    }

   
}
