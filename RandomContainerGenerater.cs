using ContainerPlanPrediction.Models;
using System;

namespace ContainerPlanPrediction
{
    public class RandomContainerGenerater
    {
        Random random = new Random(DateTime.Now.Second);

        public Container GetRandomContainer()
        {
            var size = GetRandomContainerSize();
            var type = GetRandomContainerType();
            var content = GetRandomContentType();

            return new Container() {
                ContentType = content,
                Size = size,
                ContainerType = type,
            };
        }

        private ContainerSize GetRandomContainerSize()
        {
            if (random.Next(100) > 50)
            {
                return ContainerSize.FortyFoot;
            }
            else
            {
                return ContainerSize.TwentyFoot;
            }
        }


        private ContainerType GetRandomContainerType()
        {
            int randomNumber = random.Next(100);

            if (randomNumber > 75)
            {
                return ContainerType.Dry;
            }
            else if (randomNumber > 50)
            {
                return ContainerType.OpenTop;
            }
            else if (randomNumber > 25)
            {
                return ContainerType.Refridgerated;
            }
            else
            {
                return ContainerType.Tank;
            }
        }

        private ContainerContentType GetRandomContentType()
        {
            int randomNumber = random.Next(100);

            if (randomNumber > 83)
            {
                return ContainerContentType.Corrosive;
            }
            else if (randomNumber > 66)
            {
                return ContainerContentType.Empty;
            }
            else if (randomNumber > 49)
            {
                return ContainerContentType.Flammable;
            }
            else if (randomNumber > 32)
            {
                return ContainerContentType.General;
            }
            else if (randomNumber > 17)
            {
                return ContainerContentType.Poison;
            }
            else
            {
                return ContainerContentType.Radioactive;
            }
        }
    }
}
