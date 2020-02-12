namespace ContainerPlanPrediction.Models
{
    public class Port
    {
        public Port(int regionNumber, string name)
        {
            RegionNumber = regionNumber;
            Name = name;
        }

        public int RegionNumber { get; private set; }
        public string Name { get; private set; }

    }
}
