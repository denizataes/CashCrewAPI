using System;
namespace Entities.RequestFeatures
{
    public class SearchParameters : RequestParameters
    {
        public String? SearchTerm { get; set; }

        public SearchParameters()
        {
            OrderBy = "id"; // ? belki kaldırılabilir.
        }

    }
}

