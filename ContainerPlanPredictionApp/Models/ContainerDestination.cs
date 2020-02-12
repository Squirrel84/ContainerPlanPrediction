namespace ContainerPlanPrediction.Models
{
    public class ContainerDestination
    {
        public Container Container { get; set; } 

        public Port Destination { get; set; }

        public override string ToString()
        {
            return $"{Container.ToString()} destination is {Destination.Name}";
        }
    }
}
