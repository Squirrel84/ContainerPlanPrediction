using ContainerPlanPrediction.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace ContainerPlanPrediction
{
    public class RandomContainerGenerater
    {
        Random random = new Random(DateTime.Now.Second);

        public Container GetRandomContainer()
        {
            var size = GetRandomContainerSize();
            var content = GetRandomContentType();
            var type = GetRandomContainerType(content);
            
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


        private ContainerType GetRandomContainerType(ContainerContentType contentType)
        {
            var containerTypes = ((ContainerType[])Enum.GetValues(typeof(ContainerType))).ToList();

            if (contentType == ContainerContentType.Radioactive || contentType == ContainerContentType.Corrosive || contentType == ContainerContentType.Poison)
            {
                containerTypes.Remove(ContainerType.OpenTop);
            }

            if (contentType == ContainerContentType.Flammable)
            {
                containerTypes.Remove(ContainerType.Refridgerated);
            }

            int randomNumber = random.Next(0, containerTypes.Count-1);

            return containerTypes[randomNumber];
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
