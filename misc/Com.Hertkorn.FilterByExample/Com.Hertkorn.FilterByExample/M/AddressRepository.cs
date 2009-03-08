using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Com.Hertkorn.FilterByExample.M
{
    public class AddressRepository
    {
        private static List<Address> SampleData = new List<Address>       
        {
            new Address { Street="El Colegio", StreetNumber="6667", City="Goleta", Zip=93117, Country=Country.UnitedStates },
            new Address { Street="Hauptstrasse", StreetNumber="1", City="Hamburg", Zip=20459, Country=Country.Germany },
            new Address { Street="Bahnhofstrasse", StreetNumber="1", City="München", Zip=81456, Country=Country.Germany },
            new Address { Street="Bahnhofstrasse", StreetNumber="1", City="Hamburg", Zip=20459, Country=Country.Germany },
            new Address { Street="Bahnhofstrasse", StreetNumber="2", City="Hamburg", Zip=20459, Country=Country.Germany },
            new Address { Street="Bahnhofstrasse", StreetNumber="3", City="Hamburg", Zip=20459, Country=Country.Germany },
            new Address { Street="Bahnhofstrasse", StreetNumber="4", City="Hamburg", Zip=20459, Country=Country.Germany },
            new Address { Street="Rathausstrasse", StreetNumber="1", City="Hamburg", Zip=20459, Country=Country.Germany },
            new Address { Street="Alter Wall", StreetNumber="1", City="Hamburg", Zip=20459, Country=Country.Germany },
            new Address { Street="Herrlichkeit", StreetNumber="2", City="Hamburg", Zip=20459, Country=Country.Germany },
            new Address { Street="Steinstrasse", StreetNumber="2", City="Hamburg", Zip=20459, Country=Country.Germany },
            new Address { Street="Domstrasse", StreetNumber="3", City="Hamburg", Zip=20459, Country=Country.Germany },
            new Address { Street="Burchhardstrasse", StreetNumber="2", City="Hamburg", Zip=20459, Country=Country.Germany },
            new Address { Street="Adenauerallee", StreetNumber="1", City="Hamburg", Zip=20459, Country=Country.Germany },
            new Address { Street="Am Sandtorkai", StreetNumber="1", City="Hamburg", Zip=20459, Country=Country.Germany },
            new Address { Street="Albertstrasse", StreetNumber="1", City="Hamburg", Zip=20459, Country= Country.Germany }
        };

        internal List<Address> GetAll()
        {
            return SampleData.ToList();
        }
    }
}
