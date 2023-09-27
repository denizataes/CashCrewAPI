using System;
namespace Entities.RequestFeatures
{
    public class UserParameters : RequestParameters
    {
        public String? SearchTerm { get; set; }

        public UserParameters()
        {
            OrderBy = "id";
        }

    }
}

